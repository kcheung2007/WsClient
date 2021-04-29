using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Dime;

namespace WsClient
{
    public partial class UcZAPIs : UserControl
    {
        private FilesListObj m_filesListObj = null; // remember to initialize it..
        private Thread[] m_thdList;
        private static int m_thdCount;
        private string m_zKeyFile = "";
        private string m_mdeFolder = "";
        private string m_domainName = "";
        private string m_zApiPerfLog = "ZApiPerf.log";
        private string m_zapiLog = "ZApiLog.txt";
        private int m_initialThread = 1;
        private int m_delay;
        private int m_callCount; // number of successful mde call count - return from api
        private int m_failCount; // number of fail call - attachment count <= 0
        private long m_totalFileSize;
        private DateTime m_procStartTime;
        private DateTime m_procEndTime;
        private CommObj commObj = new CommObj();

        private delegate void DelegateUpdate_txtFailCall(int numFailCall);
        private DelegateUpdate_txtFailCall m_dg_txtFailCallCtrl;

        private delegate void DelegateUpdate_txtNumCall(int numCall);
        private DelegateUpdate_txtNumCall m_dg_txtNumCallCtrl;

        private delegate void DelegateJobDoneNotification(int thdID); // all thread done
        private DelegateJobDoneNotification m_delegateJobDoneNotification;


        public UcZAPIs()
        {
            InitializeComponent();

            m_delegateJobDoneNotification = new DelegateJobDoneNotification(JobDoneHandler);
            m_dg_txtNumCallCtrl = new DelegateUpdate_txtNumCall(Update_txtNumCall);
            m_dg_txtFailCallCtrl = new DelegateUpdate_txtFailCall(Update_txtFailCall);
        }

        private void Update_txtNumCall(int numCall)
        {
            txtNumCall.Text = numCall.ToString();
        }//end of Update_txtNumCall

        private void Update_txtFailCall(int numFail)
        {
            txtFailCall.Text = numFail.ToString();
        }//end of Update_txtNumCall

        private void lnkZdkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                if (ofDlg.FileName != "")
                {
                    cboZdkFile.Text = ofDlg.FileName;
                }//end of if - open file name
            }// end of if - open file dialog
        }//end of lnkZdkFile_LinkClicked

        #region Initial combo box control Code
        //////////////////////////////////
        /// T E M P    S O L U T I O N ///
        /// //////////////////////////////
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcZAPIs.cs - SaveComboBoxItem");
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\UcZApisInitFile.xml";
                if (!File.Exists(cboPath))
                {
                    using (StreamWriter sw = File.CreateText(cboPath))
                    {
                    }//end of using - for auto close etc... yes... empty
                }

                // Save the combox
                tw = new XmlTextWriter(Application.StartupPath + "\\UcZApisInitFile.xml", System.Text.Encoding.UTF8);

                Debug.WriteLine("\t ComboBox Item file" + Application.StartupPath + "\\UcZApisInitFile.xml");

                tw.WriteStartDocument();
                tw.WriteStartElement("InitData");

                // TO DO : Only update the selected item based on the control
                // The order is important                
                WriteComboBoxEntries(cboDomainName, "cboDomainName", cboDomainName.Text, tw);
                WriteComboBoxEntries(cboZdkFile, "cboZdkFile", cboZdkFile.Text, tw);
                WriteComboBoxEntries(cboMdeFolder, "cboMdeFolder", cboMdeFolder.Text, tw);
                tw.WriteEndElement();
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "SaveComboBoxItem()");
            }//end of catch
            finally
            {
                tw.Flush();
                tw.Close();
            }

            LoadComboBoxes();
        }//end of SaveComboBoxItem

        /// <summary>
        /// Write a list of combox box entries into an xml file
        /// </summary>
        /// <param name="cboBox">ComboBox control</param>
        /// <param name="cboBoxName">Name of the control in XML</param>
        /// <param name="cboBoxText">The input text in combo box</param>
        /// <param name="tw">XmlTextWriter</param>
        private void WriteComboBoxEntries(ComboBox cboBox, string cboBoxName, string txtBoxText, XmlTextWriter tw)
        {
            Debug.WriteLine("UcZAPIs.cs - WriteComboBoxEntries");
            int maxEntriesToStore = 10;

            tw.WriteStartElement("combobox");
            tw.WriteStartAttribute("name", string.Empty);
            tw.WriteString(cboBoxName);
            tw.WriteEndAttribute();

            // Write the item from the text box first.
            if (txtBoxText.Length != 0)
            {
                tw.WriteStartElement("entry");
                tw.WriteString(txtBoxText);
                tw.WriteEndElement();
                maxEntriesToStore -= 1;
            }//end of if

            // Write the rest of the entries (up to 10).
            for (int i = 0; i < cboBox.Items.Count && i < maxEntriesToStore; ++i)
            {
                if (txtBoxText != cboBox.Items[i].ToString())
                {
                    tw.WriteStartElement("entry");
                    tw.WriteString(cboBox.Items[i].ToString());
                    tw.WriteEndElement();
                }
            }//end of for
            tw.WriteEndElement();
        }//end of WriteComboBoxEntries

        /// <summary>
        /// Load the text value into combo boxes. (OK... hardcode)
        /// </summary>
        private void LoadComboBoxes()
        {
            Debug.WriteLine("UcZAPIs.cs - LoadComboBoxes");
            try
            {
                cboDomainName.Items.Clear();
                cboZdkFile.Items.Clear();
                cboMdeFolder.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\UcZApisInitFile.xml";
                if (!File.Exists(cboPath))
                {
                    //File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load(cboPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                int numComboBox = nodeList.Count;
                int x;
                // Order is important
                for (int i = 0; i < numComboBox; i++)
                {
                    switch (nodeList.Item(i).Attributes.GetNamedItem("name").InnerText)
                    {
                        case "cboDomainName":
                            for (x = 0; x < nodeList.Item(0).ChildNodes.Count; ++x)
                            {
                                cboDomainName.Items.Add(nodeList.Item(0).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboZdkFile":
                            for (x = 0; x < nodeList.Item(1).ChildNodes.Count; ++x)
                            {
                                cboZdkFile.Items.Add(nodeList.Item(1).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboMdeFolder":
                            for (x = 0; x < nodeList.Item(2).ChildNodes.Count; ++x)
                            {
                                cboMdeFolder.Items.Add(nodeList.Item(2).ChildNodes.Item(x).InnerText);
                            }
                            break;
                    }//end of switch
                }//end of for

                if (0 < cboDomainName.Items.Count) 
                    cboDomainName.Text = cboDomainName.Items[0].ToString();
                if (0 < cboZdkFile.Items.Count) 
                    cboZdkFile.Text = cboZdkFile.Items[0].ToString();
                if (0 < cboMdeFolder.Items.Count) 
                    cboMdeFolder.Text = cboMdeFolder.Items[0].ToString();

            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "Exception");
                MessageBox.Show(msg, "LoadComboBoxes()", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }//end of catch
        }//end of LoadComboBoxes
        #endregion //Initial combo box control Code // temp solution.. need more work

        #region Handle Key down and press
        private void cboMdeFolder_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcZAPIs.cs - cboMdeFolder_KeyDown: Save current value");
            switch (e.KeyValue)
            {
                case 13: // enter key down - save the current value
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }//end of cboMdeFolder_KeyDown

        private void cboZdkFile_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcZAPIs.cs - cboZdkFile_KeyDown: Save current value");
            switch (e.KeyValue)
            {
                case 13: // enter key down - save the current value
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboZdkFile_KeyDown

        private void cboDomainName_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcZAPIs.cs - cboZdkFile_KeyPress: Save current value");
            switch (e.KeyValue)
            {
                case 13: // enter key down - save the current value
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboDomainName_KeyDown

        private void cboZdkFile_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcZAPIs.cs - cboZdkFile_KeyPress");
            AutoCompleteCombo(sender, e);
        }//end of cboZdkFile_KeyPress

        private void cboDomainName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcZAPIs.cs - cboDomainName_KeyPress");
            AutoCompleteCombo(sender, e);

        }//end of cboDomainName_KeyPress

        private void cboMdeFolder_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcZAPIs.cs - cboMdeFolder_KeyPress");
            AutoCompleteCombo(sender, e);
        }//end of cboMdeFolder_KeyPress
        
        /// <summary>
        /// Handle the auto completion for user input in combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCompleteCombo(object sender, KeyPressEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;//this.ComboBox1
            if (Char.IsControl(e.KeyChar))
                return;

            String ToFind = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            int index = cb.FindStringExact(ToFind);
            if (index == -1)
                index = cb.FindString(ToFind);

            if (index == -1)
                return;

            cb.SelectedIndex = index;
            cb.SelectionStart = ToFind.Length;
            cb.SelectionLength = cb.Text.Length - cb.SelectionStart;
            e.Handled = true;

        }//end of AutoCompleteCombo
        #endregion

        private void UcZAPIs_Load(object sender, EventArgs e)
        {
            SplashScreen.SetStatus("Loading ZAPI Panel");
            LoadComboBoxes(); // cannot do in constructor
        }//end of UcZAPIs_Load

        /// <summary>
        /// Save user preference for this tabbed page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavePref_Click(object sender, EventArgs e)
        {
            SaveComboBoxItem();
        }//end of btnSaveZdk_Click

        /// <summary>
        /// Set the GUI data to object member variables for avoiding cross-thread operation issue
        /// </summary>
        private void SetDeleteData()
        {
            m_zKeyFile = cboZdkFile.Text;
            m_domainName = cboDomainName.Text;
            m_initialThread = 1; // Due to delete function, must be 1
            m_delay = (int)nudDelay.Value;

            //// reset value
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            txtDuration.Text = "";
            nudThread.Value = m_initialThread; // must be 1.

            m_thdCount = 0;
            m_callCount = 0;
            m_failCount = 0;
            m_totalFileSize = 0;
            m_thdList = null;
            m_filesListObj = null;
        }//end of SetDeleteData

        /// TO DO:
        /// <summary>
        /// Set the GUI data to object member variables for avoiding cross-thread operation issue
        /// </summary>
        private void SetMdeUpdateData()
        {
            m_zKeyFile = cboZdkFile.Text;
            m_domainName = cboDomainName.Text;
            m_mdeFolder = cboMdeFolder.Text;
            //m_storeTime = dtpArchive.ToString();
            //m_mailFrom = cboMailFrom.Text;
            //m_rcptTo = cboRcptTo.Text;

            m_initialThread = (int)nudThread.Value;
            m_delay = (int)nudDelay.Value;

            //// reset value
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            //txtNumMail.Text = "";
            //txtMailPerSec.Text = "";
            //txtMailsSize.Text = "";
            //txtAveSize.Text = "";
            txtDuration.Text = "";
            m_thdCount = 0;
            m_callCount = 0;
            m_failCount = 0;
            m_totalFileSize = 0;
            m_thdList = null;
            m_filesListObj = null;
        }//end of SetMdeUpdateData

        /// <summary>
        /// Enable/Disable controls 
        /// </summary>
        /// <param name="value">true: enable; false: disable</param>
        public void EnableControls(bool value)
        {                        
            cboDomainName.Enabled = value;

            cboMdeFolder.Enabled = value;
            zApiTabCtrl.Enabled = value;
            lnkMdeFolder.Enabled = value;
            //cboMailFrom.Enabled = value;
            //cboRcptTo.Enabled = value;

            //nudCycle.Enabled = value;
            //if (!chkZAPIPerf.Checked)
            //    nudThread.Enabled = value;

            //chkZAPIPerf.Enabled = value;
            //btnArchive.Enabled = value;
        }//end of EnableControls

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //MessageBox.Show( "Under construction" );

            Debug.WriteLine( "UcZAPIs.cs - btnDelete_Click" );
            if(!ValidateDeleteUserInput())
                return;

            SetDeleteData(); // avoiding cross-thread operation - timer start inside this function

            EnableControls( false ); // user input good... 

            m_thdList = new Thread[m_initialThread]; // only 1 thread
            for(int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread( new ThreadStart( this.Thd_DeleteFromDS ) );
                m_thdList[i].Name = "Thd_Delete_" + i.ToString(CultureInfo.InvariantCulture);
                m_thdList[i].Start();
                Debug.WriteLine( "\t Start Thread: " + m_thdList[i].Name );
            }//end of for
        }//end of btnDelete_Click

        private void btnMDEUpdate_Click(object sender, EventArgs e)
        {
            Debug.WriteLine( "UcZAPIs.cs - btnDelete_Click" );

            if (!ValidateMdeUserInput())
                return;

            SetMdeUpdateData(); // avoiding cross-thread operation - timer start inside this function

            m_filesListObj = new FilesListObj(cboMdeFolder.Text); // get a list of file name
            if (m_filesListObj.numFile <= 0)
            {
                MessageBox.Show("File doesn't exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                cboMdeFolder.Focus();
                cboMdeFolder.BackColor = Color.YellowGreen;
                return; // exit
            }// end of if

            EnableControls(false); // user input good... 

            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_UpdateToDS));
                m_thdList[i].Name = "Thd_Update_" + i.ToString(CultureInfo.InvariantCulture);
                m_thdList[i].Start();
                Debug.WriteLine("\t Start Thread: " + m_thdList[i].Name);
            }//end of for
        }//end of btnMDEUpdate_Click

        private void Thd_DeleteFromDS()
        {
            Debug.WriteLine( "UcZAPIs.cs - Thd_DeleteFromDS" );
            string strApiRc = ""; // Zero length string indicates success (according Bojan email)
            string strTmp = "";
            string line = "";
            string zkey = "";
            DateTime startTime;
            TimeSpan timeSpan;
            //Random autoRand = new Random();

            ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
            //zApi.Timeout = 600000; // ensure large file transfer in time

            try
            {
                lock(this)
                {
                    m_thdCount++; // increment thread count - only 1 in this case
                    Debug.WriteLine( "\t Start the Delete Document thread Count: " + m_thdCount );
                }

                StreamReader srZdk = new StreamReader( m_zKeyFile );
                while((line = srZdk.ReadLine()) != null)
                {
                    string[] tmp = line.Split( new Char[] { ';' } );
                    zkey = tmp[0];

                    Debug.WriteLine( "\t Start calling zAPI" );
                    if(chkZAPIPerf.Checked)
                    {
                        startTime = DateTime.Now;
                        strApiRc = zApi.destroyDocument( zkey, m_domainName );
                        timeSpan = DateTime.Now - startTime;
                        strTmp = "Delete API: " + timeSpan.TotalSeconds.ToString();
                    }
                    else
                        strApiRc = zApi.destroyDocument( zkey, m_domainName ); // iApiRc == 0 if OK

                    if(strApiRc.Length == 0) // Zero length string indicates success (according to Bojan mail)
                    {
                        lock(this)
                        {
                            m_callCount++;
                        }
                    }
                    else
                    {
                        lock(this)
                        {
                            m_failCount++;
                        }
                    }

                    if(chkZAPIPerf.Checked)
                        commObj.WriteLineByLine( m_zApiPerfLog, strTmp );
                   
                    IAsyncResult rFailCall = BeginInvoke( m_dg_txtFailCallCtrl, new object[] { m_failCount } );
                    IAsyncResult rNumCall = BeginInvoke( m_dg_txtNumCallCtrl, new object[] { m_callCount } );
                    Thread.Sleep( m_delay * 1000 ); // real time delay per mail
                    
                }//end of while - get the zdk
            }//end of try
            catch(ThreadAbortException thdEx)
            {
                Trace.WriteLine( thdEx.Message );
            }//end of catch - ThreadAbortException thdEx
            catch(Exception ex)
            {
                commObj.LogToFile( "\t Exception" + ex.Message );
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace.ToString();
                MessageBox.Show( msg, "Generic Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch - generic exception
            finally
            {
                lock(this)
                {
                    // Debug.WriteLine("\t Invoking the JobDoneNotification");
                    // IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });

                    if(--m_thdCount == 0)
                        BeginInvoke( m_delegateJobDoneNotification, new object[] { m_thdCount } );
                }
            }//end of finally - enable control
        }//end of Thd_DeleteFromDS

        private void Thd_UpdateToDS()
        {
            Debug.WriteLine("UcZAPIs.cs - Thd_UpdateToDS");
            bool apiRC = true; // assume OK
            string strTmp = "";
            string line = "";
            string zkey = "";
            DateTime startTime;
            TimeSpan timeSpan;
            Random autoRand = new Random();

            ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
            zApi.Timeout = 600000; // ensure large file transfer in time

            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start the MDE update thread Count: " + m_thdCount);
                }

                StreamReader srZdk = new StreamReader(m_zKeyFile);
                while ((line = srZdk.ReadLine()) != null)
                {
                    string[] tmp = line.Split(new Char[] { ';' });
                    zkey = tmp[0];

                    Debug.WriteLine("\t files index = " + m_filesListObj.idxFile.ToString());
                    Debug.WriteLine("\t files name  = " + m_filesListObj.fullFileName);

                    SoapContext outSOAPContext = zApi.RequestSoapContext;                    

                    DimeAttachment outAttachment = new DimeAttachment("text/plain", TypeFormat.None, File.OpenRead(m_filesListObj.fullFileName)); // file name
                    outAttachment.Id = m_filesListObj.fullFileName + "_" + autoRand.Next().ToString(); // use file name for Attachment ID
                    outSOAPContext.Attachments.Add(outAttachment);
                    Debug.WriteLine("\t Done: Adding Attachment");

                    m_totalFileSize += m_filesListObj.currentFileSize;

                    Debug.WriteLine("\t Start calling zAPI");
                    if (chkZAPIPerf.Checked)
                    {
                        startTime = DateTime.Now;
                        apiRC = zApi.updateDocument(zkey, m_domainName);
                        timeSpan = DateTime.Now - startTime;
                        strTmp = "MDE Update API: " + m_filesListObj.fullFileName + ';'
                               + m_filesListObj.currentFileSize + ';'
                               + timeSpan.TotalSeconds.ToString();
                    }
                    else
                        apiRC = zApi.updateDocument(zkey, m_domainName);

                    if (apiRC)
                    {
                        lock (this)
                        {
                            m_callCount++;
                        }
                    }
                    else
                    {
                        lock (this)
                        {
                            m_failCount++;
                        }
                    }

                    if (chkZAPIPerf.Checked)
                        commObj.WriteLineByLine(m_zApiPerfLog, strTmp);

                    m_filesListObj.idxFile++; // point to next file
                    if (m_filesListObj.numFile <= m_filesListObj.idxFile)
                        m_filesListObj.idxFile = 0; //reset file index

                    IAsyncResult rFailCall = BeginInvoke(m_dg_txtFailCallCtrl, new object[] { m_failCount });
                    IAsyncResult rNumCall = BeginInvoke(m_dg_txtNumCallCtrl, new object[] { m_callCount });
                    Thread.Sleep(m_delay * 1000); // real time delay per mail

                    outAttachment.Stream.Close();
                    outAttachment = null;
                }//end of while - get the zdk

                for (int j = 0; j < m_filesListObj.numFile; j++)
                {
                    SoapContext outSOAPContext = zApi.RequestSoapContext;

                    DimeAttachment outAttachment = new DimeAttachment("text/plain", TypeFormat.None, File.OpenRead(m_filesListObj.fullFileName)); // file name
                    outAttachment.Id = m_filesListObj.fullFileName + "_" + autoRand.Next().ToString(); // use file name for Attachment ID
                    outSOAPContext.Attachments.Add(outAttachment);
                    Debug.WriteLine("\t Done: Adding Attachment");

                    m_totalFileSize += m_filesListObj.currentFileSize;
                }//end of for - send everything in the folder               
            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Trace.WriteLine(thdEx.Message);
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                commObj.LogToFile("\t Exception" + ex.Message);
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace.ToString();
                MessageBox.Show( msg, "Generic Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch - generic exception
            finally
            {
                lock (this)
                {
                    // Debug.WriteLine("\t Invoking the JobDoneNotification");
                    // IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });

                    if (--m_thdCount == 0)
                        BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                }
            }//end of finally - enable control

        }//end of Thd_UpdateToDS

        /// <summary>
        /// Validate Delete User Input
        /// </summary>
        /// <returns></returns>
        private bool ValidateDeleteUserInput()
        {
            bool rv = true; // assume everything is OK
            if(cboZdkFile.Text == "")
            {
                cboZdkFile.Focus();
                cboZdkFile.BackColor = Color.YellowGreen;
                rv = false;
            }//end of if - check txtFolder
            else
                if(cboDomainName.Text == "")
                {
                    cboDomainName.Focus();
                    cboDomainName.BackColor = Color.YellowGreen;
                    rv = false;
                }//end of if
                else
                    if(nudThread.Value != 1) // only 1 thread
                    {
                        nudThread.Focus();
                        nudThread.BackColor = Color.YellowGreen;
                        rv = false;
                    }//end of if
            return (rv);
        }//end of ValidateDeleteUserInput

        /// <summary>
        /// Validate MDE user input
        /// </summary>
        /// <returns>bool: OK - true; Fail - false</returns>
        private bool ValidateMdeUserInput()
        {
            bool rv = true; // assume everything is OK
            if (cboZdkFile.Text == "")
            {
                cboZdkFile.Focus();
                cboZdkFile.BackColor = Color.YellowGreen;
                rv = false;
            }//end of if - check txtFolder
            else
                if (cboDomainName.Text == "")
                {
                    cboDomainName.Focus();
                    cboDomainName.BackColor = Color.YellowGreen;
                    rv = false;
                }//end of if
                else
                    if (cboMdeFolder.Text == "")
                    {
                        cboMdeFolder.Focus();
                        cboMdeFolder.BackColor = Color.YellowGreen;
                        rv = false;
                    }//end of if            
                    else
                        if (nudThread.Value < 1)
                        {
                            nudThread.Focus();
                            nudThread.BackColor = Color.YellowGreen;
                            rv = false;
                        }//end of if
            return (rv);
        }//end of ValidateMdeUserInput

        /// <summary>
        /// Handle everything after all archive threads done....
        /// Enable controls, set end time etc.
        /// </summary>
        /// <param name="value"></param>
        public void JobDoneHandler(int thdID)
        {
            Debug.WriteLine("UcZAPIs.cs - +++++++ JobDoneHandler ++++++++");
            m_procEndTime = DateTime.Now;
            txtEndTime.Text = m_procEndTime.ToString(CultureInfo.CurrentCulture);
            txtNumCall.Text = m_callCount.ToString(CultureInfo.CurrentCulture);

            TimeSpan timeSpan = m_procEndTime - m_procStartTime;
            txtDuration.Text = timeSpan.TotalSeconds.ToString(CultureInfo.CurrentCulture);

            double callPerSec = m_callCount / timeSpan.TotalSeconds;
            txtCallPerSec.Text = callPerSec.ToString(CultureInfo.CurrentCulture);
            EnableControls(true);

            string msg = "Thread: " + thdID.ToString() + " - ZAPI Call Duration: " + txtDuration.Text + "\r\n"
                + "Total ZPAI Called: " + txtNumCall.Text + "\r\n"
                + "Total Fail Called: " + txtFailCall.Text + "\r\n"
                + "Called per second: " + txtCallPerSec.Text;

            commObj.LogToFile(m_zapiLog, msg);
        }//end of JobDoneHandler

        private void lnkMdeFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            if (cboMdeFolder.Text != "")
            {
                if (Directory.Exists(cboMdeFolder.Text))
                    fbDlg.SelectedPath = cboMdeFolder.Text;  // set the default folder
            }

            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                cboMdeFolder.Text = fbDlg.SelectedPath;
            }
        }//end of lnkMdeFolder_LinkClicked

        private void btnAbortAll_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcZAPIs.cs - btnAbort_Click");
            try
            {
                OnUpdateStatusBar(new StatusEventArgs("ZAPI Panel: Abort"));

                for (int i = 0; i < m_initialThread; i++)
                {
                    if (m_thdList[i] != null && m_thdList[i].IsAlive)
                        KillZApiThread(ref m_thdList[i]);
                }//end of for

                // reset mouse cursor and enable control
                //IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                BeginInvoke( m_delegateJobDoneNotification, new object[] { m_thdCount } );
            }//end of try
            catch (Exception ex)
            {
                commObj.LogToFile("UcArchive.cs - btnAbort_Click " + ex.Message);
            }//end of catch
        }//end of btnAbortAll_Click

        private void KillZApiThread(ref Thread oneThd)
        {
            try
            {
                oneThd.Abort(); // abort
                oneThd.Join(); // require for ensure the thread kill

            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Trace.WriteLine(thdEx.Message);
                commObj.LogToFile("Thread.log", "\t Exception ocurr in KillZApiThread:" + oneThd.Name);
                MessageBox.Show("Abort all thread done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }//end of catch
        }//end of KillArchiveThread

        // "UpdateStatusEventHandler" same as delegate above
        public event EventHandler<StatusEventArgs> statusChanged;
        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }

    }
}
