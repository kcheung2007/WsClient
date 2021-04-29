using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
    ////////////////////////////////////////////////////////////////////////
    // C L A S S   -   UcArchive:UserControl
    ////////////////////////////////////////////////////////////////////////
    // 3) Class that will send notification message
    public partial class UcArchive : UserControl
    {
        //// Define public event member in the class that raise the event
        //public event ArchiveEventHandler archEventEnd;

        // Move to the local thread proc
        //private ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
        private CommObj commObj = new CommObj();
        private string myArchiveLogTitle = "xaZ ArChIvE Log impossible title"; // this is for killing windows;
        private Thread [] m_thdList;
        private static int m_thdCount = 0;
        private FilesListObj m_filesListObj = null; // remember to initialize it..
        private DateTime m_procStartTime;
        private DateTime m_procEndTime;
        private string zKeyFileName = "zKey.txt";
        private string m_apiPerfLog = "ApiArchLog.txt";
        private string m_archiveLog = "ArchiveLog.txt";
        private string m_dataFolder = "";
        private string m_storeTime = "";
        private string m_domainName = "";
        private string m_mailFrom = "";
        private string m_rcptTo = "";
        private string m_mdeFile = "";
        private string m_xmlFile = "Archive.xml";
        private int m_initialThread = 0;
        private int m_delay = 0;
        private int m_sentMail = 0;
        private long m_totalFileSize = 0;


        private Byte[] fsByte;
        private FileStream fs2;
        private DimeAttachment mdeAttachment;


        //private DimeAttachment m_mdeAttachment;

        // ++++ update GUI control delegate
        //private delegate void DelegateEnableControls(bool bValue);
        //private DelegateEnableControls m_delegateEnableControls;

        private delegate void DelegateJobDoneNotification(int thdId); // all thread done - indicate Thread index
        private DelegateJobDoneNotification m_delegateJobDoneNotification;

        private delegate void DelegateUpdate_txtNumMail(int numMail);
        private DelegateUpdate_txtNumMail m_delegateNumMailCtrl;

        // "UpdateStatusEventHandler" same as delegate above
        public event EventHandler<StatusEventArgs> statusChanged;
        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual

        public UcArchive()
        {
            InitializeComponent();
            //RegisterArchiveEvent(new ArchiveEventHandler(OnArchiveEnd));
            //m_delegateEnableControls = new DelegateEnableControls(this.EnableControls);
            m_delegateJobDoneNotification = new DelegateJobDoneNotification(JobDoneHandler);
            m_delegateNumMailCtrl = new DelegateUpdate_txtNumMail(Update_txtNumMail);
            
        }

        ///// <summary>
        ///// Register archive event handler (delegate) to the event invocation list.
        ///// Any method has the same signature with delegate can register to this event.
        ///// </summary>
        ///// <param name="eventHandler"></param>
        //public void RegisterArchiveEvent(ArchiveEventHandler eventHandler)
        //{
        //    archEventEnd += eventHandler;

        //}//end of RegisterArchiveEvent

        ///// <summary>
        ///// Unegister archive event handler (delegate) from the event invocation list.
        ///// </summary>
        ///// <param name="eventHandler"></param>
        //public void UnregisterArchiveEvent(ArchiveEventHandler eventHandler)
        //{
        //    archEventEnd -= eventHandler;
        //}//end of UnregisterArchiveEvent

        private void Update_txtNumMail(int numMail)
        {
            txtNumMail.Text = numMail.ToString( CultureInfo.CurrentCulture );
        }//end of Update_txtNumMail

        /// <summary>
        /// Validate user input
        /// </summary>
        /// <returns>bool: OK - true; Fail - false</returns>
        private bool ValidateUserInput()
        {
            bool rv = true; // assume everything is OK
            //if(txtFolder.Text == "")
            if( String.IsNullOrEmpty(txtFolder.Text) )
            {
                txtFolder.Focus();
                txtFolder.BackColor = Color.YellowGreen;
                rv = false;
            }//end of if - check txtFolder
            else
                if(String.IsNullOrEmpty(cboDomainName.Text))
                {
                    cboDomainName.Focus();
                    cboDomainName.BackColor = Color.YellowGreen;
                    rv = false;
                }//end of if
                else
                    if( String.IsNullOrEmpty(cboMailFrom.Text))
                    {
                        cboMailFrom.Focus();
                        cboMailFrom.BackColor = Color.YellowGreen;
                        rv = false;
                    }//end of if
                    else
                        if( String.IsNullOrEmpty(cboRcptTo.Text))
                        {
                            cboRcptTo.Focus();
                            cboRcptTo.BackColor = Color.YellowGreen;
                            rv = false;
                        }//end of if
                        else
                            if( nudCycle.Value < 1 )
                            {
                                nudCycle.Focus();
                                nudCycle.BackColor = Color.YellowGreen;
                                rv = false;
                            }//end of if
                            else
                                if( nudThread.Value < 1 )
                                {
                                    nudThread.Focus();
                                    nudThread.BackColor = Color.YellowGreen;
                                    rv = false;
                                }//end of if
                                else
                                    if (chkDoUpdate.Checked && (String.IsNullOrEmpty(cboMdeFile.Text)))
                                    {
                                        cboMdeFile.Focus();
                                        cboMdeFile.BackColor = Color.YellowGreen;
                                        rv = false;
                                    }

            // TO DO: check if mde file exist and folder exist

            return (rv);
        }//end of ValidateUserInput

        /// <summary>
        /// Set the GUI data to object member variables for avoiding cross-thread operation issue
        /// </summary>
        private void SetArchiveData()
        {
            m_dataFolder = txtFolder.Text;
            //m_storeTime = dtpArchive.ToString(); add the EML time format - 01-21-08
            m_storeTime = dtpArchive.Value.ToLocalTime().ToString( "R" );
            m_domainName = cboDomainName.Text;
            m_mailFrom = cboMailFrom.Text;
            m_rcptTo = cboRcptTo.Text;
            m_mdeFile = cboMdeFile.Text;

            m_initialThread = (int)nudThread.Value;
            m_delay = (int)nudDelay.Value;

            // reset value
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            txtNumMail.Text = "";
            txtMailPerSec.Text = "";
            txtMailsSize.Text = "";
            txtAveSize.Text = "";
            txtDuration.Text = "";
            m_thdCount = 0;
            m_sentMail = 0;
            m_totalFileSize = 0;
            m_thdList = null;
            m_filesListObj = null;

            if(chkDoUpdate.Checked) // get the MDE file stream first
            {
                if(!String.IsNullOrEmpty( m_mdeFile ))
                {
                    //FileStream fs2 = File.OpenRead( m_mdeFile );
                    fs2 = new FileStream( m_mdeFile, FileMode.Open, FileAccess.Read, FileShare.Read );
                    fsByte = new Byte[fs2.Length];
                    fs2.Read( fsByte, 0, (int)fs2.Length );
                }//end of if
            }//end of if - chkDoUpdate

        }//end of SetArchiveData

        /// <summary>
        /// Creating Archive thread for archiving files into DS...
        /// Method Thd_ArchiveToDS() is the one do the job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArchive_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - btnArchive_Click");
            //RegisterArchiveEvent(new ArchiveEventHandler(OnArchiveEnd));

// Move after ValidateUserInput() on Jan 13.
//            SetArchiveData(); // avoiding cross-thread operation - timer start inside this function
            if( !ValidateUserInput() )
                return;

            SetArchiveData(); // avoiding cross-thread operation - timer start inside this function
           
            m_filesListObj = new FilesListObj(txtFolder.Text); // get a list of file name
            if (m_filesListObj.numFile <= 0)
            {
                MessageBox.Show( "File doesn't exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
                txtFolder.Focus();
                txtFolder.BackColor = Color.YellowGreen;
                return; // exit
            }// end of if

            EnableControls(false); // user input good... 
            
            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_ArchiveToDS));
                m_thdList[i].Name = "Thd_Archive_" + i.ToString(CultureInfo.CurrentCulture);
                m_thdList[i].Start();
                Debug.WriteLine("\t Start Thread: " + m_thdList[i].Name);
            }//end of for
        }//end of btnArchive_Click

        /// <summary>
        /// ThreadStart callback function: Really do the work.
        /// 1) Update Thread count
        /// 2) Archive to DS
        /// 3) Record the zKey into a file... 
        /// </summary>
        private void Thd_ArchiveToDS()
        {
            Debug.WriteLine("UcArchive.cs - ArchiveToDS");
            Debug.WriteLine("\t URL = " + WsClient.Program.appSetting.WsServer_URL.ToString());
            string zKey = ""; // default null
            string strTmp = "";
            DateTime startTime;
            TimeSpan timeSpan;


            Random autoRand = new Random();
            ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
            zApi.Timeout = -1; // ensure large file transfer in time

            if(ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) != null)
                zApi.ClientCertificates.Add( ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) );

            
                                                      
            try
            {
                //Byte[] fsByte;
                //FileStream fs2;
                //DimeAttachment mdeAttachment;

                //if(chkDoUpdate.Checked) // get the MDE file stream first
                //{
                //    if(!String.IsNullOrEmpty( m_mdeFile ))
                //    {
                //        //FileStream fs2 = File.OpenRead( m_mdeFile );
                //        fs2 = new FileStream( m_mdeFile, FileMode.Open, FileAccess.Read, FileShare.Read );
                //        fsByte = new Byte[fs2.Length];
                //        fs2.Read( fsByte, 0, (int)fs2.Length );
                //    }//end of if
                //}//end of if - chkDoUpdate

                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start the archive-job Count: " + m_thdCount);
                }

                int numCycle = (int)nudCycle.Value;                
                for (int i = 0; i < numCycle; i++)
                {
                    Debug.WriteLine( "\t In Cycle " + i.ToString( CultureInfo.CurrentCulture ) );

                    for (int j = 0; j < m_filesListObj.numFile; j++)         
                    {
                        Debug.WriteLine( "\t\t files index = " + m_filesListObj.idxFile.ToString( CultureInfo.CurrentCulture ) );
                        Debug.WriteLine("\t\t files name  = " + m_filesListObj.fullFileName);                                            

                        SoapContext outSOAPContext = zApi.RequestSoapContext;
                        DimeAttachment outAttachment = new DimeAttachment("text/plain", TypeFormat.None, File.OpenRead(m_filesListObj.fullFileName)); // file name
                        outAttachment.Id = m_filesListObj.fullFileName + "_" + autoRand.Next().ToString( CultureInfo.CurrentCulture ); // use file name for Attachment ID
                        outSOAPContext.Attachments.Add(outAttachment);
                        Debug.WriteLine("\t Done: Adding Attachment\n" + outSOAPContext.ToString());

                        // 2nd attachment for mde - new for soap archive
                        if(chkDoUpdate.Checked)
                        {
                            if(!String.IsNullOrEmpty( m_mdeFile ))
                            {                                
                                Byte[] info = new UTF8Encoding( true ).GetBytes( "\r\nFILE ID: " + outAttachment.Id );

                                MemoryStream ms = new MemoryStream( (int)fs2.Length + info.Length + 128);
                                ms.Write( fsByte, 0, fsByte.Length );
                                ms.Write( info, 0, info.Length );
                                                                
                                mdeAttachment = new DimeAttachment( "text/plain", TypeFormat.None, ms ); // file name
                                outSOAPContext.Attachments.Add( mdeAttachment ); // 2nd attachment

                                //fs2.Close();
                                //ms.Close();
                            }//end of if - mde file is ok
                        }//end of if

                        m_totalFileSize += m_filesListObj.currentFileSize;

                        Debug.WriteLine("\t Start calling zAPI");
                        if (chkZAPIPerf.Checked)
                        {
                            startTime = DateTime.Now;
                            zKey = zApi.storeDocument(m_filesListObj.currentFileSize, m_storeTime, m_domainName, m_mailFrom, m_rcptTo);
                            timeSpan = DateTime.Now - startTime;
                            strTmp = m_filesListObj.fullFileName + ';' 
                                   + m_filesListObj.currentFileSize + ';'
                                   + timeSpan.TotalSeconds.ToString( CultureInfo.CurrentCulture ) + ';' 
                                   + zKey;
                        }
                        else
                            zKey = zApi.storeDocument(m_filesListObj.currentFileSize, m_storeTime, m_domainName, m_mailFrom, m_rcptTo);

                        #region Replaced Section - delete later
                        //if (chkDoUpdate.Checked) // call mde update now
                        //{
                        //    // TO DO - call update api after the archive api
                        //    SoapContext mdeSOAPContext = zApi.RequestSoapContext;
                        //    DimeAttachment mdeAttachment = new DimeAttachment("text/plain", TypeFormat.None, File.OpenRead(m_mdeFile)); // file name
                        //    mdeAttachment.Id = m_mdeFile + "_" + autoRand.Next().ToString(); // use file name for Attachment ID
                        //    mdeSOAPContext.Attachments.Add(mdeAttachment);

                        //    rv = zApi.updateDocument(zKey, m_domainName);
                        //    if (!rv)
                        //        commObj.LogToFile("++ Update Document call fail " + outAttachment.Id);
                        //}//end of if
                        #endregion

                        lock (this)
                        {
                            m_sentMail++; // multiple thread access this variable.
                        }//end of lock

                        if (chkZAPIPerf.Checked)
                            commObj.WriteLineByLine(m_apiPerfLog, strTmp);

                        commObj.WriteLineByLine(zKeyFileName, zKey + ';' + m_filesListObj.fullFileName);

                        m_filesListObj.idxFile++; // point to next file
                        if (m_filesListObj.numFile <= m_filesListObj.idxFile)
                            m_filesListObj.idxFile = 0; //reset file index
                        
                        IAsyncResult r = BeginInvoke(m_delegateNumMailCtrl, new object[] { m_sentMail });
                        Thread.Sleep(m_delay * 1000); // real time delay per mail

                        outAttachment.Stream.Close();
                        outAttachment = null;
                    }//end of for - send everything in the folder
                }//end of for - number of cycle for the folder
                
            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Trace.WriteLine(thdEx.Message);
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                commObj.LogToFile("\t Exception" + ex.Message);
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace.ToString();
                MessageBox.Show( msg, "Generic Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch - generic exception
            finally
            {
                lock (this)
                {                    
                    if (--m_thdCount == 0)
                        BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                }                    
            }//end of finally - enable control
        }//end of Thd_ArchiveToDS

        /// <summary>
        /// Enable/Disable controls 
        /// </summary>
        /// <param name="value">true: enable; false: disable</param>
        public void EnableControls( bool value )
        {
            txtFolder.Enabled = value;
            dtpArchive.Enabled = value;
            cboDomainName.Enabled = value;
            cboMailFrom.Enabled = value;
            cboRcptTo.Enabled = value;            
            chkDoUpdate.Enabled = value;

            nudCycle.Enabled = value;
            if( !chkZAPIPerf.Checked )
                nudThread.Enabled = value;

            cboMdeFile.Enabled = (chkDoUpdate.Checked && value) ? true : false;
            //if(chkDoUpdate.Checked && value)
            //    cboMdeFile.Enabled = true;
            //else
            //    cboMdeFile.Enabled = false;

            chkZAPIPerf.Enabled = value;
            btnArchive.Enabled = value;
        }//end of EnableControls

        /// <summary>
        /// Handle everything after all archive threads done....
        /// Enable controls, set end time etc.
        /// </summary>
        /// <param name="value"></param>
        public void JobDoneHandler(int thdId)
        {
            Debug.WriteLine("UcArchive.cs - +++++++ JobDoneHandler ++++++++");
            m_procEndTime = DateTime.Now;
            txtEndTime.Text = m_procEndTime.ToString(CultureInfo.CurrentCulture);
            txtNumMail.Text = m_sentMail.ToString(CultureInfo.CurrentCulture);

            TimeSpan timeSpan = m_procEndTime - m_procStartTime;
            txtDuration.Text = timeSpan.TotalSeconds.ToString();

            txtMailsSize.Text = m_totalFileSize.ToString(CultureInfo.CurrentCulture);

            if (0 < m_sentMail)
            {
                long aveSize = (long)(m_totalFileSize / m_sentMail);
                txtAveSize.Text = aveSize.ToString( CultureInfo.CurrentCulture );
            }//end of if

            double mailPerSec = m_sentMail / timeSpan.TotalSeconds;
            txtMailPerSec.Text = mailPerSec.ToString( CultureInfo.CurrentCulture );
            EnableControls(true);

            string msg = "Thread " + thdId.ToString( CultureInfo.CurrentCulture ) + " - Archive Duration: " + txtDuration.Text + "\r\n" 
                + "Total Sent Files: " + txtNumMail.Text + "\r\n"
                + "Total Files Size: " + txtMailsSize.Text + "\r\n"
                + "Ave.  File  Size: " + txtAveSize.Text + "\r\n" 
                + "Mails per second: " + txtMailPerSec.Text;

            Debug.WriteLine("UcArchive.cs - Log Path = " + commObj.logFullPath);
            commObj.LogToFile(m_archiveLog, msg);
        }//end of JobDoneHandler

        private void nudDelay_ValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine( "UcArchive.cs - nudDelay_ValueChanged: " + nudDelay.Value.ToString( CultureInfo.CurrentCulture ) );
            m_delay = (int)nudDelay.Value;
        }//end of nudDelay_ValueChanged

        /// <summary>
        /// Stop the thread... loop through whole array... 
        /// ie: use m_initialThread, but not m_thdCount...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbort_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - btnAbort_Click");
            try
            {
                OnUpdateStatusBar(new StatusEventArgs("Archive Panel: Abort"));

                Debug.WriteLine("\t m_initialThread = " + m_initialThread);
                for (int i = 0; i < m_initialThread; i++)
                {
                    if (m_thdList[i] != null && m_thdList[i].IsAlive)
                    {
                        Debug.WriteLine("\t KillArchiveThread : " + m_thdList[i].Name);
                        KillArchiveThread(ref m_thdList[i]);
                    }
                }//end of for

                // reset mouse cursor and enable control
                IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { -99 });
            }//end of try
            catch (Exception ex)
            {
                commObj.LogToFile("UcArchive.cs - btnAbort_Click " + ex.Message);
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "Abort", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );                
            }//end of catch
        }//end of btnAbort_Click

        private void KillArchiveThread( ref Thread oneThd)
        {
            try
            {
                oneThd.Abort(); // abort
                oneThd.Join(); // require for ensure the thread kill

            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Debug.WriteLine("KillArchiveThread - " + thdEx.Message);
                commObj.LogToFile("Thread.log", "\t Exception ocurr in KillArchiveThread:" + oneThd.Name);
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
        }//end of KillArchiveThread

        private void lnkFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            if (txtFolder.Text != "")
                fbDlg.SelectedPath = txtFolder.Text;  // set the default folder

            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fbDlg.SelectedPath;
            } 
        }//end of lnkFolder_LinkClicked

        ///// <summary>
        ///// Custom event delegate
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void OnArchiveEnd(object sender, ArchiveEventArgs e)
        //{
        //    Debug.WriteLine("\t OnArchiveEnd " + e.objCount.ToString());
        //    if (e.objCount != 0)
        //        return;
            
        //    // EnableControls(true);
        //    UnregisterArchiveEvent(new ArchiveEventHandler(OnArchiveEnd));
        //}//end of OnArchiveEnd

        private void chkDoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            cboMdeFile.Enabled = chkDoUpdate.Checked ? true : false;
            //if (chkDoUpdate.Checked)
            //    cboMdeFile.Enabled = true;
            //else
            //    cboMdeFile.Enabled = false;
        }

        private void chkZAPIPerf_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - chkZAPIPerf_CheckedChanged");
            if (chkZAPIPerf.Checked)
            {
                Debug.WriteLine("\t Checked");
                nudThread.Value = 1;
                nudDelay.Value = 0;
                nudThread.Enabled = false;
                nudDelay.Enabled = false;
            }//end of if
            else
            {
                Debug.WriteLine("\t UnChecked");
                nudThread.Enabled = true;
                nudDelay.Enabled = true;
            }//end of else - unchecked
        }//end of chkZAPIPerf_CheckedChanged

        private void lnkViewArchiveLog_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - lnkViewRetriLog_Click");
            try
            {
                string logFullPathFileName = commObj.logFullPath + "\\" + m_archiveLog;
                Debug.WriteLine(logFullPathFileName);
                myArchiveLogTitle = commObj.ViewLogFromNotepad(logFullPathFileName, myArchiveLogTitle);
            }//emd of try
            catch (Win32Exception win32Ex)
            {
                Trace.WriteLine(win32Ex.Message + "\n" + win32Ex.GetType().ToString() + win32Ex.StackTrace);
            }//end of catch - win32 exception
        }//end of lnkViewArchiveLog_Click

        private void txtEndTime_MouseEnter(object sender, EventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - txtEndTime_MouseEnter");
            ttpArchive.SetToolTip(txtEndTime, txtEndTime.Text);
        }

        private void txtStartTime_MouseEnter(object sender, EventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - txtStartTime_MouseEnter");
            ttpArchive.SetToolTip(txtStartTime, txtStartTime.Text);
        }

#region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcArchive.cs - SaveComboBoxItem");
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\" + m_xmlFile;
                if (!File.Exists(cboPath))
                {
                    StreamWriter sw = File.CreateText(cboPath);
                    sw.Close();
                }

                // Save the combox
                tw = new XmlTextWriter(Application.StartupPath + "\\" + m_xmlFile, System.Text.Encoding.UTF8);

                Debug.WriteLine("\t ComboBox Item file" + Application.StartupPath + "\\" + m_xmlFile);

                tw.WriteStartDocument();
                tw.WriteStartElement("InitData");

                //The order is important
                WriteComboBoxEntries(cboDomainName, "cboDomainName", cboDomainName.Text, tw);
                WriteComboBoxEntries(cboMailFrom, "cboMailFrom", cboMailFrom.Text, tw);
                WriteComboBoxEntries(cboRcptTo, "cboRcptTo", cboRcptTo.Text, tw);
                WriteComboBoxEntries(cboMdeFile, "cboMdeFile", cboMdeFile.Text, tw);

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
            Debug.WriteLine("UcArchive.cs - WriteComboBoxEntries");
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
            Debug.WriteLine("UcArchive.cs - LoadComboBoxes");
            try
            {
                cboDomainName.Items.Clear();
                cboMailFrom.Items.Clear();
                cboRcptTo.Items.Clear();
                cboMdeFile.Items.Clear();

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
                        case "cboMailFrom":
                            for (x = 0; x < nodeList.Item(1).ChildNodes.Count; ++x)
                            {
                                cboMailFrom.Items.Add(nodeList.Item(1).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboRcptTo":
                            for (x = 0; x < nodeList.Item(2).ChildNodes.Count; ++x)
                            {
                                cboRcptTo.Items.Add(nodeList.Item(2).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboMdeFile":
                            for (x = 0; x < nodeList.Item(3).ChildNodes.Count; ++x)
                            {
                                cboMdeFile.Items.Add(nodeList.Item(3).ChildNodes.Item(x).InnerText);
                            }
                            break;
                    }//end of switch
                }//end of for

                if (0 < cboDomainName.Items.Count)
                    cboDomainName.Text = cboDomainName.Items[0].ToString();
                if (0 < cboMailFrom.Items.Count) 
                    cboMailFrom.Text = cboMailFrom.Items[0].ToString();
                if (0 < cboRcptTo.Items.Count) 
                    cboRcptTo.Text = cboRcptTo.Items[0].ToString();
                if (0 < cboMdeFile.Items.Count)
                    cboMdeFile.Text = cboMdeFile.Items[0].ToString();

            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "Exception");
                MessageBox.Show(msg, "LoadComboBoxes()", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }//end of catch
        }//end of //end of LoadComboBoxes
#endregion

        /// <summary>
        /// Initial the text box controls
        /// Default values are set in app config file
        /// </summary>
        private void InitTextboxCtrl()
        {
            txtFolder.Text = WsClient.Program.appSetting.DataFolder;
        }//end of InitTextboxCtrl

        /// <summary>
        /// Initial the combo box controls and some text controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcArchive_Load(object sender, EventArgs e)
        {
            SplashScreen.SetStatus("Loading Archive Panel");
            LoadComboBoxes(); // cannot do in constructor
            InitTextboxCtrl();
        }//end of UcArchive_Load

        private void lnkMde_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.Filter = "mde files (*.mde)|*mde|text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                if (ofDlg.FileName != "")
                {
                    cboMdeFile.Text = ofDlg.FileName;
                }//end of if - open file name
            }// end of if - open file dialog
        }

#region Handle Key down and press
        private void cboDomainName_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - cboDomainName_KeyDown");
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboDomainName_KeyDown

        private void cboMailFrom_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - cboMailFrom_KeyDown");
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }//end of cboMailFrom_KeyDown

        private void cboRcptTo_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - cboRcptTo_KeyDown");
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboRcptTo_KeyDown

        private void cboMdeFile_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - cboMdeFile_KeyDown");
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboMdeFile_KeyDown


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

        private void cboMailFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - cboMailFrom_KeyPress");
            AutoCompleteCombo(sender, e);
        }//end of cboMailFrom_KeyPress

        private void cboRcptTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - cboRcptTo_KeyPress");
            AutoCompleteCombo(sender, e);
        }

        private void cboMdeFile_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcArchive.cs - cboMdeFile_KeyPress");
            AutoCompleteCombo(sender, e);
        }
#endregion

       
        



    }//end of partial class UcArchive 

    //// Custom Event - send notification to main form to update the status bar
    //// D E L E G A T E    D E C L A R A T I O N
    //public delegate void UpdateStatusEventHandler(Object sender, StatusEventArgs eArg);

    //// E V E N T   D A T A    C L A S S
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
    //}

    //// +++++ start custome EVENT ++++ 
    //// 1) Create a class that provides data for the event.
    //// 2) Declare a delegate for the event
    //// 3) Create a class that will send the notification messages
    ////    Define a public event member in this class that raises the event
    //// 4) Create a class to handle the event.

    /////////////////////////////////////////////////////////////////////////
    //// 2) Delegate declaration
    //public delegate void ArchiveEventHandler(object sender, ArchiveEventArgs eArgs);


    ///////////////////////////////////////////////////////////////////////
    //// E V E N T   D A T A   C L A S S 
    ///////////////////////////////////////////////////////////////////////
    //// 1) Provide data for the event, derives from System.EventArgs
    //public class ArchiveEventArgs : EventArgs
    //{
    //    private int _objCount;
    //    private string _thdName = "";

    //    public string thdName
    //    {
    //        get
    //        {
    //            return _thdName;
    //        }
    //        set
    //        {
    //            _thdName = value;
    //        }
    //    }//end of thdName

    //    public int objCount
    //    {
    //        get
    //        {
    //            return _objCount;
    //        }
    //        set
    //        {
    //            _objCount = value;
    //        }
    //    }
    //    // constructor
    //    public ArchiveEventArgs(int count)
    //    {
    //        Debug.WriteLine("UcArchive.cs - ArchiveEventArgs: count = " + count.ToString());
    //        _objCount = count;
    //    }//end of constructor

    //    public ArchiveEventArgs(string str)
    //    {
    //        Debug.WriteLine("UcArchive.cs - ArchiveEventArgs: thdName = " + str);
    //        _thdName = str;
    //    }
    //}//end of class - ThdEventArgs (Event Data Class)

}
