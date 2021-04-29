using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Dime;



namespace WsClient
{
    public partial class UcMonitor : UserControl
    {
        /// <summary>
        /// ZANTAZ web service variable: - Global? 
        /// </summary>
        
//        private WsClient.Properties.Settings appSetting = new WsClient.Properties.Settings();
//        private ZANTAZ_StoreAndRetrieveService zAPI = new ZANTAZ_StoreAndRetrieveService();
        private CommObj commObj = new CommObj();
        private string logPath = Application.StartupPath + "\\Logs";
        private string m_logFileName = "MonitorLog.txt";   // should set from user preference
        private string myProcessTitle = ""; // for Log Detail 1 process        
        private bool m_isPortalAlive = false; // default no...
        private string m_xmlFile = "Monitor.xml";

        public UcMonitor()
        {
            InitializeComponent();
            myProcessTitle = "xAz M0nit0r Log impossible title"; // this is for killing windows
            commObj.SetCurrentDirectory(Application.StartupPath + "\\logs");
            ttpMonitor.SetToolTip(cboDomainName1, WsClient.Program.appSetting.WsServer_URL);
        }

        // "UpdateStatusEventHandler" same as delegate above
        public event EventHandler<StatusEventArgs> statusChanged;

        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual

        private void lnkDetail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Debug.WriteLine("UcMonitor.cs - lnkDetail_LinkClicked");
            try
            {
                string logFullPathFileName = logPath + "\\" + m_logFileName;
                Debug.WriteLine(logFullPathFileName);
                myProcessTitle = commObj.ViewLogFromNotepad(logFullPathFileName, myProcessTitle);
                
                // save for reference - delete later
                //string logFullPathFileName = logPath + "\\" + m_logFileName;
                //Process myProcess = new Process();

                //int iHandle = Win32Api.FindWindowEx(0, 0, null, myProcessTitle);
                //if (iHandle != 0) // log file opened - bug for != and return
                //{
                //    Win32Api.SetForegroundWindow(iHandle);
                //    return;
                //}//end of if

                //// Get the path that stores user documents.
                //myProcess.StartInfo.FileName = logFullPathFileName;
                //myProcess.Start();
                
                //// Allow the process to finish starting. But what if loop forever?
                //while( !myProcess.WaitForInputIdle() );
                //myProcessTitle = myProcess.MainWindowTitle;

            }//emd of try
            catch (Win32Exception win32Ex)
            {
                Trace.WriteLine(win32Ex.Message + "\n" + win32Ex.GetType().ToString() + win32Ex.StackTrace);
            }//end of catch - win32 exception
        }// end of lnkDetail_LinkClicked

        /// <summary>
        /// Actually, check the following:
        /// 1) Is Soap Portal alive
        /// 2) Are ALL smart cell alive. ie: total 5 pairs, return false if ALL died
        /// </summary>
        private bool CheckIsPortalAlive(string domainName)
        {
            Debug.WriteLine("UcMonitor.cs - CheckIsPortalAlive()");
            string logFullPathFileName = commObj.logFullPath + "\\" + m_logFileName;
            bool rv = false; // default not alive
            ZANTAZ_StoreAndRetrieveService zAPI = new ZANTAZ_StoreAndRetrieveService();
            try
            {
                rv = zAPI.isAlive(domainName);
                if (!rv)// not alive
                    commObj.LogToFile(m_logFileName, "+++ isAlive fail +++");
            }//end of try
            catch (Exception ex) // what to do?
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine("Exception:" + msg);

                // commObj.SetCurrentDirectory(Application.StartupPath + "\\logs");
                commObj.LogToFile(m_logFileName, msg);
            }//end of catch 

            return (rv);
            
        }//end of checkIsPortalAlive


        /// <summary>
        /// Update the time stamp on the Monitor control
        /// </summary>
        /// <param name="lblCtrl"></param>
        private void UpdateTimeStamp(Label lblCtrl)
        {
            Debug.WriteLine("UcMonitor.cs - UpdateTimeStamp( lblCtrl ) " + "lblCtrl=" + lblCtrl.Text);
            DateTime dt = DateTime.Now;
            lblCtrl.Text = dt.ToString();
//            lblCtrl.Refresh();
        }//end of UpdateTimeStamp

        private void UcMonitor_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMonitor.cs - UcMonitor_Load");

            SplashScreen.SetStatus("Loading Monitor");
            LoadComboBoxes(true); // cannot do in constructor
            //cboDomainName1.Text = "stop";
            if (cboDomainName1.Text.ToLower() == "stop")
            {
                lblStatusIcon1.ImageIndex = 2;
                lblStatusText1.Text = "N/A";
                UpdateTimeStamp(lblDateCheck1);
                return;
            }
                            
            //monTimer.Interval = 50000; // get it from numberic updown - hardcode
            //monTimer.Start();            
            bgdWorkerMonitor.RunWorkerAsync(cboDomainName1.Text);
            UpdateTimeStamp(lblDateCheck1);            
        }//end of UcMonitor_Load

        private void monTimer_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMonitor.cs - monTimer_Tick");
            try
            {

                if (cboDomainName1.Text.ToLower() == "stop")
                {
                    return;
                }
                
                UpdateTimeStamp(lblDateCheck1);

                Debug.WriteLine("\t Check is portal alive");
                bgdWorkerMonitor.RunWorkerAsync(cboDomainName1.Text); // we can pass parameter in (here)
            }
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                //MessageBox.Show( msg, "Kentest - multi-work");
                Debug.WriteLine(msg);
            }
        }//end of monTimer_Tick

        /// <summary>
        /// Actual time-consuming work here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgdWorkerMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            // This method will run on a thread other than the UI thread.
            // Be sure not to manipulate any Windows Forms controls created
            // on the UI thread from this method.

            Debug.WriteLine("UcMonitor.cs - bgdWorkerMonitor_DoWork");
            monTimer.Enabled = false;
            m_isPortalAlive = CheckIsPortalAlive((string)e.Argument);
        }//end of bgdWorkerMonitor_DoWork

        /// <summary>
        /// This event updates the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgdWorkerMonitor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Debug.WriteLine("UcMonitor.cs - bgdWorkerMonitor_ProgressChanged");
            // example
            // this.progressBar1.value = e.ProgressPercentage;
        }//end of bgdWorkerMonitor_ProgressChanged

        /// <summary>
        /// This event handler deals with the results of the background operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgdWorkerMonitor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("UcMonitor.cs - bgdWorkerMonitor_RunWorkerCompleted");            

            if (m_isPortalAlive)
            {
                Debug.WriteLine("\t Finally - m_isPortalAlive is true");
                lblStatusIcon1.ImageIndex = 0;
                lblStatusText1.Text = "OK";
            }//end of if - isAlive
            else
            {
                Debug.WriteLine("\t Finally - m_isPortalAlive is false");
                lblStatusIcon1.ImageIndex = 1;
                lblStatusText1.Text = "FAIL";
            }//end of else
            btnCheck1.Enabled = true;
            monTimer.Enabled = true;
        }

        private void btnCheck1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMonitor - btnCheck1_Click");
            try
            {
                monTimer.Interval = 50000; // get it from numberic updown - hardcode
                monTimer.Start();  

                btnCheck1.Enabled = false;

                OnUpdateStatusBar(new StatusEventArgs(WsClient.Program.appSetting.WsServer_URL));

                bgdWorkerMonitor.RunWorkerAsync(cboDomainName1.Text);
                UpdateTimeStamp(lblDateCheck1);
            }
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                //MessageBox.Show( msg, "Kentest - multi-work");
                Debug.WriteLine(msg);
            }
        }//end of btnCheck1_Click

        private void cboDomainName1_SelectedValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMonitor.cs - cboDomainName1_SelectedValueChanged");
            try
            {
                if (cboDomainName1.Text.ToLower() == "stop")
                {
                    lblStatusIcon1.ImageIndex = 2;
                    lblStatusText1.Text = "N/A";
                    UpdateTimeStamp(lblDateCheck1);
                    btnCheck1.Enabled = false;
                    return;
                }

                OnUpdateStatusBar(new StatusEventArgs(WsClient.Program.appSetting.WsServer_URL));
                bgdWorkerMonitor.RunWorkerAsync(cboDomainName1.Text);
                UpdateTimeStamp(lblDateCheck1);
            }
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                //MessageBox.Show( msg, "Kentest - multi-work");
                Debug.WriteLine(msg);
            }
        }//end of cboDomainName1_SelectedValueChanged

        private void cboDomainName1_TextUpdate(object sender, EventArgs e)
        {
        }

        private void cboDomainName1_MouseEnter(object sender, EventArgs e)
        {
            ttpMonitor.SetToolTip(cboDomainName1, WsClient.Program.appSetting.WsServer_URL);
        }

        #region Initial combo box control Code

        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcMonitor.cs - SaveComboBoxItem");
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
                tw = new XmlTextWriter(cboPath, System.Text.Encoding.UTF8);

                Debug.WriteLine("\t ComboBox Item file " + cboPath);

                tw.WriteStartDocument();
                tw.WriteStartElement("InitData");

                //The order is important
                WriteComboBoxEntries(cboDomainName1, "cboDomainName1", cboDomainName1.Text, tw);

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

            LoadComboBoxes(false);
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
            Debug.WriteLine("UcMonitor.cs - WriteComboBoxEntries");
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
        private void LoadComboBoxes(bool iniFlag)
        {
            Debug.WriteLine("UcMonitor.cs - LoadComboBoxes");
            try
            {
                cboDomainName1.Items.Clear();

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
                        case "cboDomainName1":
                            for (x = 0; x < nodeList.Item(0).ChildNodes.Count; ++x)
                            {
                                cboDomainName1.Items.Add(nodeList.Item(0).ChildNodes.Item(x).InnerText);
                            }
                            break;
                    }//end of switch
                }//end of for

                if (iniFlag)
                {
                    cboDomainName1.Text = "stop";
                }
                else
                {
                    if (0 < cboDomainName1.Items.Count)
                        cboDomainName1.Text = cboDomainName1.Items[0].ToString();
                }
                

            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "Exception");
                MessageBox.Show(msg, "LoadComboBoxes()");
            }//end of catch
        }//end of LoadComboBoxes
        #endregion // Initial ComboBox

        #region Handle key down key pass        
        private void cboDomainName1_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcMonitor.cs - cboDomainName_KeyDown");
            switch (e.KeyValue)
            {
                case 13: // enter key down - save the current value
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboDomainName1_KeyDown

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

        private void cboDomainName1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcMonitor.cs - cboDomainName_KeyPress");
            AutoCompleteCombo(sender, e);
        }//end of cboDomainName_KeyPress
        #endregion

        

    }
}
