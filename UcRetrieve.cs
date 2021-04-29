using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Dime;

namespace WsClient
{
    public partial class UcRetrieve : UserControl
    {
//        private ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
        private CommObj commObj = new CommObj();
        private Thread[] m_thdList;
        private string myRetriLogTitle = "XaZ ReTrIeVal Log impossible title"; // this is for killing windows
        private static int m_thdCount = 0;
        private string m_zKeyFile = "";
        private string m_dataFolder = "";
        private string m_domainName = "";
        private string m_apiRetriLog = "ApiRetriLog.txt";
        private string m_retrieveLog = "RetrieveLog.txt";
        private string m_integrityLog = "IntegrityLog.txt";
        private string m_xmlFile = "Retrieve.xml";
        private long m_totalFileSize = 0;
        private int m_initialThread = 1;
        private int m_delay = 0;
        private int m_mailCount = 0; // number of retrieved mail
        private int m_failCount = 0; // number of fail call - attachment count <= 0
        private DateTime m_procStartTime;
        private DateTime m_procEndTime;
        private bool m_bMIME = false; // default using DIME retrieval.

        private delegate void DelegateThdDoneNotification(int thdID); // all thread done
        private DelegateThdDoneNotification m_delegateThdDoneNotification;

        private delegate void DelegateUpdate_txtNumMail(int numMail);
        private DelegateUpdate_txtNumMail m_delegateNumMailCtrl;

        // "UpdateStatusEventHandler" same as delegate above
        public event EventHandler<StatusEventArgs> statusChanged;
        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar( StatusEventArgs eArgs )        
        {
            statusChanged( this, eArgs );
        }//end of virtual

        public UcRetrieve()
        {
            InitializeComponent();            
            m_delegateThdDoneNotification = new DelegateThdDoneNotification(ThdDoneHandler);
            m_delegateNumMailCtrl = new DelegateUpdate_txtNumMail(Update_txtNumMail);
        }

        private void Update_txtNumMail(int numMail)
        {
            Debug.WriteLine("UcRetrieve.cs - numMail = " + numMail);
            txtNumMail.Text = numMail.ToString();
        }//end of Update_txtNumMail

        /// <summary>
        /// Point to the file that contains zkey for multiple reading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkBrowseKeyFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show the Open File Dialog.
            // If the user clicked OK in the dialog and a text file was selected, open it.
            // Display an OpenFileDialog and show the read-only files...
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.Filter = "zdk files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                txtKeyFile.Text = ofDlg.FileName;
            }// end of if - open file dialog
            else 
            {
                txtKeyFile.BackColor = Color.YellowGreen;
                txtKeyFile.Focus();
            }
        }//end of lnkBrowseKeyFile_LinkClicked

        /// <summary>
        /// Handle everything after all retrieval threads done....
        /// Enable controls, set end time etc.
        /// </summary>
        /// <param name="value"></param>
        public void ThdDoneHandler(int thdID)
        {
            Debug.WriteLine("UcRetrieve.cs - ThdDoneHandler");
            string fullPathRetrieveLog = commObj.logFullPath + "\\" + m_retrieveLog;

            m_procEndTime = DateTime.Now;
            txtEndTime.Text = m_procEndTime.ToString();
            txtNumMail.Text = m_mailCount.ToString();

            TimeSpan timeSpan = m_procEndTime - m_procStartTime;
            txtDuration.Text = timeSpan.TotalSeconds.ToString();

            txtMailsSize.Text = m_totalFileSize.ToString();

            if (0 < m_mailCount)
            {
                long aveSize = (long)(m_totalFileSize / m_mailCount);
                txtAveSize.Text = aveSize.ToString();
            }

            double mailPerSec = m_mailCount / timeSpan.TotalSeconds;
            txtMailPerSec.Text = mailPerSec.ToString();
            txtFailCall.Text = m_failCount.ToString();

            EnableControls(true);

            string msg = "Thread ID " + thdID.ToString() + "Retrieval Duration: " + txtDuration.Text + "\r\n"
                    + "Total Sent Files: " + txtNumMail.Text + "\r\n"
                    + "Total Files Size: " + txtMailsSize.Text + "\r\n"
                    + "Ave.  File  Size: " + txtAveSize.Text + "\r\n"
                    + "Mails per second: " + txtMailPerSec.Text + "\r\n"
                    + "+Fail retrieve +: " + txtFailCall.Text;

            commObj.LogToFile( m_retrieveLog, msg );

        }//end of ThdDoneHandler

        /// <summary>
        /// Point to the root folder for storing retrieve files.
        /// Folder per thread will create. Default thread folder is Thread_0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkStoreLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            if (txtStoreFolder.Text != "")
                fbDlg.SelectedPath = txtStoreFolder.Text;  // set the default folder

            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                txtStoreFolder.Text = fbDlg.SelectedPath;
            }
        }//end of lnkStoreLocation_LinkClicked

        /// <summary>
        /// Enable/Disable controls 
        /// </summary>
        /// <param name="value">true: enable; false: disable</param>
        public void EnableControls(bool value)
        {
            txtKeyFile.Enabled = value;
            txtStoreFolder.Enabled = value;
            cboDomainName.Enabled = value;

            if( !chkZAPIPerf.Checked)
                nudThread.Enabled = value;
            
            chkZAPIPerf.Enabled = value;
            btnRetrieve.Enabled = value;
        }//end of EnableControls

        private bool ValidateUserInput()
        {
            bool rv = true; // assume everything is OK
            if (txtKeyFile.Text == "")
            {
                txtKeyFile.Focus();
                txtKeyFile.BackColor = Color.YellowGreen;
                rv = false;
            }//end of if - check txtFolder
            else
                if (txtStoreFolder.Text == "")
                {
                    txtStoreFolder.Focus();
                    txtStoreFolder.BackColor = Color.YellowGreen;
                    rv = false;
                }//end of if
                else
                    if( cboDomainName.Text == "")
                    {
                        cboDomainName.Focus();
                        cboDomainName.BackColor = Color.YellowGreen;
                        rv = false;
                    }

            return (rv);
        }//end of ValidateUserInput

        /// <summary>
        /// 1) Get UI data
        /// 2) Reset internal data such as counter etc...
        /// </summary>
        private void SetRetrievalData()
        {
            // initial data from UI
            m_domainName = cboDomainName.Text;
            m_zKeyFile = txtKeyFile.Text;
            m_dataFolder = txtStoreFolder.Text; 

            // reset internal data
            m_initialThread = (int)nudThread.Value;
            m_delay = (int)nudDelay.Value;

            // reset value
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            txtNumMail.Text = "";
            txtAveSize.Text = "";
            txtDuration.Text = "";
            txtMailPerSec.Text = "";
            txtMailsSize.Text = "";
            txtFailCall.Text = "0";
            m_thdCount = 0;
            m_mailCount = 0;
            m_failCount = 0;
            m_thdList = null;
            m_totalFileSize = 0;

            m_bMIME = (rdoMIME.Checked) ? true : false;

        }//end of SetRetrievalData()

        private void Thd_RetrieveFromDS()
        {
            Debug.WriteLine("UcRetrieve.cs - Thd_RetrieveFromDS()");

            int i = 0;
            String strRetrieve = "";
            string line = ""; // a line from key file
            string outFile = ""; // full path file name
            string zkey = "";
            string duration = "";
            string fullPathRetrieveLog = commObj.logFullPath + "\\" + m_retrieveLog;

            DateTime startTime;
            TimeSpan timeSpan;

            ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
            if(ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) != null)
                zApi.ClientCertificates.Add( ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) );

            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start the retrieval-job Count: " + m_thdCount);
                }

                StreamReader sr = new StreamReader(m_zKeyFile);
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tmp = line.Split(new Char[] { ';' });
                    zkey = tmp[0];
                    //         
                    //outFile = m_dataFolder + "\\" + Thread.CurrentThread.Name + "\\" + i.ToString() + "_" + commObj.GetShortFileName(tmp[1]);
                    if (tmp.Length < 1) // zdk + ; + file name
                        outFile = m_dataFolder + "\\" + Thread.CurrentThread.Name + "\\" + commObj.GetShortFileName(tmp[1]);
                    else
                        outFile = m_dataFolder + "\\" + Thread.CurrentThread.Name + "\\" + i.ToString() + ".msg";

                    Debug.WriteLine("\t Start Calling store API");


                    // kentest : try-catch within loop
                    try
                    {
                        if(chkZAPIPerf.Checked)
                        {
                            startTime = DateTime.Now; // start the timer

                            if(m_bMIME)
                                strRetrieve = zApi.retrieveMime( zkey, m_domainName );
                            else
                                strRetrieve = zApi.retrieveDocument( zkey, m_domainName ); // defaul is DIME

                            timeSpan = DateTime.Now - startTime;
                            duration = timeSpan.TotalSeconds.ToString();
                        }
                        else
                        {
                            if(m_bMIME)
                                strRetrieve = zApi.retrieveMime( zkey, m_domainName );
                            else
                                strRetrieve = zApi.retrieveDocument( zkey, m_domainName ); // defaul is DIME
                        }
                    }
                    catch (Exception innerEx)
                    {
                        string msg = innerEx.Message + "\n" + innerEx.GetType().ToString() + innerEx.StackTrace;
                        commObj.LogToFile( m_retrieveLog, "Retrieve API Fail: " + msg );
                        lock (this)
                        {
                            m_failCount++;
                            commObj.LogToFile( m_retrieveLog, "Retrieve API Fail: " + msg );
                        }
                    }
                    finally
                    {
                        commObj.LogToFile( m_retrieveLog, "After Calling retrieveDocument:  " + strRetrieve );
                    }                    
                    // test code here - delete later
                    SoapContext inSOAPContext = zApi.ResponseSoapContext;
                    int attachCount = inSOAPContext.Attachments.Count;
                    if (attachCount <= 0)
                    {
                        lock (this)
                        {
                            commObj.WriteLineByLine( m_retrieveLog, "*** ERROR BREAK: Attach count = " + attachCount.ToString() );
                        }                        
                        continue; // skip the rest, do next zkey
                    }//end of if - attachCount <= 0

                    //outFile = m_dataFolder + "\\" + Thread.CurrentThread.Name + "\\mail_" + i.ToString() + ".txt";
                    using (FileStream fs = new FileStream(outFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        //long bufSize = zApi.ResponseSoapContext.Attachments[0].Stream.Length;
                        long bufSize = inSOAPContext.Attachments[0].Stream.Length;
                        m_totalFileSize += bufSize;

                        byte[] buffer = new byte[bufSize];
                        // byte[] buffer = new byte[10];
                        int chunkLength;

                        int count = 0;
                        while ((chunkLength = zApi.ResponseSoapContext.Attachments[0].Stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, chunkLength);
                            count++;
                        }

                        // save for reference - add zdk into file
                        //string str = "\r\n\r\n";
                        //buffer = commObj.StrToByteArray(str);
                        //fs.Write(buffer, 0, str.Length);
                        //buffer = commObj.StrToByteArray(zkey);
                        //fs.Write(buffer, 0, zkey.Length);

                        lock (this)
                        {
                            string msg = "Attach count = " + attachCount 
                                       + "\r\nBuffer count = " + count.ToString() 
                                       + "\r\nbufSize from Response Soap: " + bufSize.ToString();
                            commObj.LogToFile( m_retrieveLog, msg );
                        }//end of lock

                        if (chkZAPIPerf.Checked)
                        {
                            string msg = outFile + ';' + bufSize.ToString() + ';' + duration;
                            commObj.WriteLineByLine(m_apiRetriLog, msg);
                        }

                        if (chkLogIntegrity.Checked)
                        {
                            string msg = outFile + ';' + zkey;
                            commObj.WriteLineByLine(m_integrityLog, msg);
                        }
                    }//end of using - File Stream - finish retrieve

                    //zApi.retrieveComplete(zkey, m_domainName); // signal WSP the retrieval complete.

                    lock (this)
                    {
                        m_mailCount++;
                    }
                    i++; // new file name

                    IAsyncResult r = BeginInvoke(m_delegateNumMailCtrl, new object[] { m_mailCount });
                    Thread.Sleep(m_delay * 1000); // real time
                }//end of while

                sr.Close();
                sr.Dispose();
                          
                // Comment @03-20-05: Decrement move to finally
                //lock (this)
                //{
                //    --m_thdCount; // decrement
                //    Debug.WriteLine("\t End the Retrieval job Count: " + m_thdCount);
                //}

                //if (m_thdCount == 0) // all thread done: Enable controls
                //{
                //    IAsyncResult iAsync = BeginInvoke(m_delegateThdDoneNotification, new object[] { true });
                //}
                // archEventEnd(this, new ArchiveEventArgs(m_thdCount));
            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Trace.WriteLine("\t Thread Abort Msg \n" + thdEx.Message);
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                string msg = "\nMessage Count" + i.ToString() + "\n" + ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace.ToString();
                commObj.LogToFile( m_retrieveLog, "\t Exception: " + msg );
                //MessageBox.Show(msg, "Generic Exception");
            }//end of catch - generic exception
            finally
            {
                lock (this)
                {
                    // Change from m_thdCount to --m_thdCount
                    IAsyncResult iAsync = BeginInvoke(m_delegateThdDoneNotification, new object[] { --m_thdCount });
                    Debug.WriteLine("\t Finally: Invoke ThdDoneNotification");
                }                    
            }//end of finally

        }//end of Thd_RetrieveFromDS

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcRetrieve.cs - btnRetrieve_Click");
            //RegisterArchiveEvent(new ArchiveEventHandler(OnArchiveEnd));

            string tmpPath = "";

            if (!ValidateUserInput())
                return;
            EnableControls(false);
            SetRetrievalData(); // avoiding cross-thread operation - timer start inside this function
            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                // create folder here
                tmpPath = txtStoreFolder.Text + "\\Thd_Retrieve_" + i.ToString();
                if (!Directory.Exists(tmpPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(tmpPath);
                }
                
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_RetrieveFromDS));
                m_thdList[i].Name = "Thd_Retrieve_" + i.ToString();
                m_thdList[i].Start();
                Debug.WriteLine("\t Start Thread: " + m_thdList[i].Name);
            }//end of for
        }//end of btnRetrieve_Click

        private void KillRetrievalThread(ref Thread oneThd)
        {
            try
            {
                oneThd.Abort(); // abort
                oneThd.Join(); // require for ensure the thread kill

            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Debug.WriteLine(thdEx.Message);
                commObj.LogToFile("Thread.log", "\t Exception ocurr in KillSendMailThread:" + oneThd.Name);
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }//end of catch
        }//end of KillRetrievalThread

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcRetrieve.cs - btnAbort_Click");
            string fullPathRetrieveLog = commObj.logFullPath + "\\" + m_retrieveLog;
            try
            {
                OnUpdateStatusBar(new StatusEventArgs("Retrieval Panel: Abort"));

                for (int i = 0; i < m_initialThread; i++)
                {
                    if (m_thdList[i] != null && m_thdList[i].IsAlive)
                        KillRetrievalThread(ref m_thdList[i]);
                }//end of for

                // reset mouse cursor and enable control
                IAsyncResult r = BeginInvoke(m_delegateThdDoneNotification, new object[] { m_thdCount });
            }//end of try
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace);
                commObj.LogToFile( m_retrieveLog, "UcRetrieve.cs - btnAbort_Click " + ex.Message );
                
            }//end of catch
        }//end of btnAbort_Click

        private void chkZAPIPerf_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("UcRetrieve.cs - chkZAPIPerf_CheckedChanged");
            if (chkZAPIPerf.Checked)
            {
                Debug.WriteLine("\t Checked");
                nudThread.Value = 1;
                nudThread.Enabled = false;
            }//end of if
            else
            {
                Debug.WriteLine("\t UnChecked");
                nudThread.Enabled = true;
            }//end of else - unchecked

        }//end of chkZAPIPerf_CheckedChanged

        private void lnkViewRetriLog_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcRetrieve.cs - lnkViewRetriLog_Click");
            try
            {
                string logFullPathFileName = commObj.logFullPath + "\\" + m_retrieveLog;
                Debug.WriteLine(logFullPathFileName);
                myRetriLogTitle = commObj.ViewLogFromNotepad(logFullPathFileName, myRetriLogTitle);
            }//emd of try
            catch (Win32Exception win32Ex)
            {
                Trace.WriteLine(win32Ex.Message + "\n" + win32Ex.GetType().ToString() + win32Ex.StackTrace);
            }//end of catch - win32 exception
        }//end of lnkViewRetriLog_Click

        private void txtStartTime_MouseEnter(object sender, EventArgs e)
        {
            ttpRetrieve.SetToolTip(txtStartTime, txtStartTime.Text);
        }

        private void txtEndTime_TextChanged(object sender, EventArgs e)
        {
            ttpRetrieve.SetToolTip(txtEndTime, txtEndTime.Text);
        }

#region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcRetrieve.cs - SaveComboBoxItem");
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\" + m_xmlFile;
                if (!File.Exists(cboPath))
                {
                    using (StreamWriter sw = File.CreateText(cboPath))
                    {
                    }//end of using - for auto close etc... yes... empty
                }

                // Save the combox
                tw = new XmlTextWriter(Application.StartupPath + "\\" + m_xmlFile, System.Text.Encoding.UTF8);

                Debug.WriteLine("\t ComboBox Item file" + Application.StartupPath + "\\" + m_xmlFile);

                tw.WriteStartDocument();
                tw.WriteStartElement("InitData");

                //The order is important
                WriteComboBoxEntries(cboDomainName, "cboDomainName", cboDomainName.Text, tw);
                //WriteComboBoxEntries(cboMailFrom, "cboMailFrom", cboMailFrom.Text, tw);
                //WriteComboBoxEntries(cboRcptTo, "cboRcptTo", cboRcptTo.Text, tw);

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
            Debug.WriteLine("UcRetrieve.cs - WriteComboBoxEntries");
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
            Debug.WriteLine("UcRetrieve.cs - LoadComboBoxes");
            try
            {
                cboDomainName.Items.Clear();
                
                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\" + m_xmlFile;
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
                    }//end of switch
                }//end of for

                if(0 < cboDomainName.Items.Count)
                    cboDomainName.Text = cboDomainName.Items[0].ToString();
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "Exception");
                MessageBox.Show(msg, "LoadComboBoxes()");
            }//end of catch
        }//end of //end of LoadComboBoxes
#endregion

        /// <summary>
        /// Initial the text box controls
        /// Default values are set in app config file
        /// </summary>
        private void InitTextboxCtrl()
        {
            txtStoreFolder.Text = WsClient.Program.appSetting.StoreFolder;
            txtKeyFile.Text = WsClient.Program.appSetting.zdkFile;
        }//end of InitTextboxCtrl

        private void UcRetrieve_Load(object sender, EventArgs e)
        {
            SplashScreen.SetStatus("Loading Retrieval Panel");
            LoadComboBoxes(); // cannot do in constructor
            InitTextboxCtrl();
        }

#region Handle Key down and press
        private void cboDomainName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboDomainName_KeyDown

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

        private void cboDomainName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - cboDomainName_KeyPress");
            AutoCompleteCombo(sender, e);
        }//end of cboDomainName_KeyPress
#endregion
    }//end of class 

    //// Custom Event - send notification to main form to update the status bar
    //public delegate void UpdateStatusEventHandler(Object sender, StatusEventArgs eArg);
    //public class StatusEventArgs : EventArgs
    //{
    //    private string _msg = "";
    //    public StatusEventArgs(string msg)
    //    {
    //        _msg = msg;
    //    }

    //    public string strMsg
    //    {
    //        get
    //        {
    //            return _msg;
    //        }
    //        set
    //        {
    //            _msg = value;
    //        }
    //    }//end of msg
    //}//end of class - StatusEventArgs
}