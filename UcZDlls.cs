using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WsClient
{
    public partial class UcZDlls : UserControl
    {
        private CommObj commObj = new CommObj();
//        private WsClient.Properties.Settings appSetting = new WsClient.Properties.Settings();
        private string myDllLogTitle = "xaZ D1l Log ImPoSsIbLe title"; // this is for killing windows;
        private Thread[] m_thdList;
        private static int m_thdCount = 0;
        private FilesListObj m_filesListObj = null; // remember to initialize it..
        private DateTime m_procStartTime;
        private DateTime m_procEndTime;
        private string m_zKeyFileName = "DllKey.txt";
        private string m_dllPerfLog = "DllPerfLog.txt";
        private string m_DllsLog = "DllsLog.txt";
        private string m_dataFolder = "";
        private string m_storeTime = "";
        private string m_domainName = "";
        private string m_mailFrom = "";
        private string m_rcptTo = "";
        private string m_mdeFile = ""; // for archive only
        private int m_initialThread = 0;
        private int m_delay = 0;
        private int m_numMail = 0; // number of mail archive or retrieve
        private int m_failCount = 0;
        private long m_totalFileSize = 0;

        private delegate void DelegateJobDoneNotification(int thdId); // all thread done
        private DelegateJobDoneNotification m_delegateJobDoneNotification;

        private delegate void DelegateUpdate_txtNumMail(int numMail);
        private DelegateUpdate_txtNumMail m_delegateNumMailCtrl;

        #region Dynamic Load DLL
        // test for dynamic load... save for reference
        //private delegate uint savetods( IStream fStream, 
        //                                string serverURL, 
        //                                string domain, 
        //                                string sender, 
        //                                string rcpt, 
        //                                string createTime, 
        //                                StringBuilder bufZDK);
        #endregion
        
        public UcZDlls()
        {
            InitializeComponent();
            m_delegateJobDoneNotification = new DelegateJobDoneNotification(JobDoneHandler);
            m_delegateNumMailCtrl = new DelegateUpdate_txtNumMail(Update_txtNumMail);
        }

        // "UpdateStatusEventHandler" same as delegate above
        public event EventHandler<StatusEventArgs> statusChanged;
        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual OnUpdateStatusBar

        private void Update_txtNumMail(int numMail)
        {
            txtNumMail.Text = numMail.ToString();
        }//end of Update_txtNumMail        

        /// <summary>
        /// Validate user input
        /// </summary>
        /// <returns>bool: OK - true; Fail - false</returns>
        private bool CheckArchiveUserInput()
        {
            bool rv = true; // assume everything is OK
            if (txtFolder.Text == "")
            {
                txtFolder.Focus();
                txtFolder.BackColor = Color.YellowGreen;
                rv = false;
            }//end of if - check txtFolder
            else
                if (cboArchDomain.Text == "")
                {
                    cboArchDomain.Focus();
                    cboArchDomain.BackColor = Color.YellowGreen;
                    rv = false;
                }//end of if
                else
                    if (cboMailFrom.Text == "")
                    {
                        cboMailFrom.Focus();
                        cboMailFrom.BackColor = Color.YellowGreen;
                        rv = false;
                    }//end of if
                    else
                        if (cboRcptTo.Text == "")
                        {
                            cboRcptTo.Focus();
                            cboRcptTo.BackColor = Color.YellowGreen;
                            rv = false;
                        }//end of if
                        else
                            if (nudThread.Value < 1)
                            {
                                nudThread.Focus();
                                nudThread.BackColor = Color.YellowGreen;
                                rv = false;
                            }//end of if
                            else
                                if( (chkDoUpdate.Checked) && (cboMdeFile.Text == "") )
                                {
                                    cboMdeFile.Focus();
                                    cboMdeFile.BackColor = Color.YellowGreen;
                                    rv = false;
                                }
            return (rv);
        }//end of CheckArchiveUserInput

        /// <summary>
        /// Validate user input
        /// </summary>
        /// <returns>bool: OK - true; Fail - false</returns>
        private bool CheckUpdateUserInput()
        {
            bool rv = true; //assume everything is OK
            if (txtUpdateKeyFile.Text == "")
            {
                txtUpdateKeyFile.Focus();
                txtUpdateKeyFile.BackColor = Color.YellowGreen;
                rv = false;
            }//end of if
            else
                if (cboUpdateRcptTo.Text == "")
                {
                    cboUpdateRcptTo.Focus();
                    cboUpdateRcptTo.BackColor = Color.YellowGreen;
                    rv = false;
                }
                else
                    if (cboUpdateDomain.Text == "")
                    {
                        cboUpdateDomain.Focus();
                        cboUpdateDomain.BackColor = Color.YellowGreen;
                        rv = false;
                    }
                    else
                        if (cboMdeFolder.Text == "")
                        {
                            cboMdeFolder.Focus();
                            cboMdeFolder.BackColor = Color.YellowGreen;
                            rv = false;
                        }
            return (rv);
        }//end of CheckUpdateUserInput

        /// <summary>
        /// Validate user input
        /// </summary>
        /// <returns>bool: OK - true; Fail - false</returns>
        private bool CheckRetrieveUserInput()
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
                    if (cboRetrDomain.Text == "")
                    {
                        cboRetrDomain.Focus();
                        cboRetrDomain.BackColor = Color.YellowGreen;
                        rv = false;
                    }

            return (rv);
        }//end of CheckRetrieveUserInput

        /// <summary>
        /// Set/reset common counters in the upper right corner.
        /// </summary>
        private void ResetCounters()
        {
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
            m_numMail = 0;
            m_failCount = 0;
            m_totalFileSize = 0;
            m_thdList = null;
            m_filesListObj = null;

        }//end of ResetCounters

        /// <summary>
        /// Set the GUI data to object member variables for avoiding cross-thread operation issue
        /// </summary>
        private void SetArchiveData()
        {
            m_dataFolder = txtFolder.Text;
            //m_storeTime = dtpArchive.ToString(); add the EML time format - 01-21-08
            m_storeTime = dtpArchive.Value.ToLocalTime().ToString( "R" );
            m_domainName = cboArchDomain.Text;
            m_mailFrom = cboMailFrom.Text;
            m_rcptTo = cboRcptTo.Text;
            m_zKeyFileName = "DllKey.txt";

            m_mdeFile = cboMdeFile.Text;

            ResetCounters();
        }//end of SetArchiveData

        private void SetUpdateData()
        {
            // initial data from UI
            m_domainName = cboUpdateDomain.Text;
            m_zKeyFileName = txtUpdateKeyFile.Text;
            m_rcptTo = cboUpdateRcptTo.Text;

            ResetCounters();
        }//end of SetUpdateData

        /// <summary>
        /// 1) Get UI data
        /// 2) Reset internal data such as counter etc...
        /// </summary>
        private void SetRetrievalData()
        {
            // initial data from UI
            m_domainName = cboRetrDomain.Text;
            m_zKeyFileName = txtKeyFile.Text;
            m_dataFolder = txtStoreFolder.Text;

            ResetCounters();
        }//end of SetRetrievalData()

        /// <summary>
        /// Enable update function. Disable archive and retrival controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoDllUpdate_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcZDlls.cs - rdoDllUpdate_Click");

            rdoDllArchive.Checked = false;
            rdoDllRetrieve.Checked = false;
            
            EnableArchiveGroupCtrl(false);
            EnableRetrieveGroupCtrl(false);
            EnableUpdateGroupCtrl(true);

        }//end of rdoDllUpdate_Click


        /// <summary>
        /// Enable archive function. Disable retrieve controls.
        /// Mutual exclusive to each other.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoDllArchive_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcZDlls.cs - rdoDllArchive_Click");

            //reset default zdk file - in case someone change it...
            m_zKeyFileName = "DllKey.txt";
            rdoDllRetrieve.Checked = false;
            rdoDllUpdate.Checked = false;

            EnableArchiveGroupCtrl(true);
            EnableRetrieveGroupCtrl(false);
            EnableUpdateGroupCtrl(false);
            
        }//end ofrdoDllArchive_Click

        /// <summary>
        /// Enable retrieve function. Disable archive controls.
        /// Mutual exclusive to each other.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoDllRetrieve_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcZDlls.cs - rdoRetrieve_Click");

            rdoDllArchive.Checked = false;
            rdoDllUpdate.Checked = false;

            EnableArchiveGroupCtrl(false);
            EnableRetrieveGroupCtrl(true);
            EnableUpdateGroupCtrl(false);
        }//end of rdoRetrieve_Click

        /// <summary>
        /// Enable or disable update controls.
        /// </summary>
        /// <param name="value">bool: true - enable; false - disable</param>
        private void EnableUpdateGroupCtrl(bool value)
        {
            lnkUpdateKeyFile.Enabled = value;
            txtUpdateKeyFile.Enabled = value;
            lblUpdateRcptTo.Enabled = value;
            cboUpdateRcptTo.Enabled = value;
            lblUpdateDomain.Enabled = value;
            cboUpdateDomain.Enabled = value;
            cboMdeFolder.Enabled = value;
            lnkMdeFolder.Enabled = value;
        }//end of EnableUpdateGroupCtrl


        /// <summary>
        /// Enable or disable archive controls.
        /// </summary>
        /// <param name="value">bool: true - enable; false - disable</param>
        private void EnableArchiveGroupCtrl( bool value )
        {
            lnkFolder.Enabled = value;
            txtFolder.Enabled = value;
            lblDate.Enabled = value;
            dtpArchive.Enabled = value;
            lblArchDomain.Enabled = value;
            cboArchDomain.Enabled = value;
            lblMailFrom.Enabled = value;
            cboMailFrom.Enabled = value;
            lblRcptTo.Enabled = value;
            cboRcptTo.Enabled = value;

            cboMdeFile.Enabled = (chkDoUpdate.Checked && value) ? true : false;
        }//end of EnableArchiveGroupCtrl

        /// <summary>
        /// Enable or disable retrieve controls.
        /// </summary>
        /// <param name="value">bool: true - enable; false - disable</param>
        private void EnableRetrieveGroupCtrl(bool value)
        {            
            lnkBrowseKeyFile.Enabled = value;
            lnkStoreLocation.Enabled = value;
            txtKeyFile.Enabled = value;
            txtStoreFolder.Enabled = value;
            lblRetrDomain.Enabled = value;
            cboRetrDomain.Enabled = value;
        }//end of EnableRetrieveGroupCtrl

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

        private void lnkBrowseKeyFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                if (ofDlg.FileName != "")
                {
                    txtKeyFile.Text = ofDlg.FileName;
                }//end of if - open file name
            }// end of if - open file dialog
        }//end of lnkBrowseKeyFile_LinkClicked

        /// <summary>
        /// Execute the function based on the radio button selection: archive or retrieve.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcZDlls.cs - btnRun_Click");
            try
            {
                if (rdoDllArchive.Checked)
                {
                    RunArchive();
                }
                else
                    if (rdoDllUpdate.Checked)
                    {
                        RunUpdate();
                    }
                else
                    if( rdoDllRetrieve.Checked)
                    {
                        RunRetrieve();
                    }
            }
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show( msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }
        }//end of btnRun_Click

        private void RunUpdate()
        {
            Debug.WriteLine("UcZDlls.cs - RunUpdate()");
            if (!CheckUpdateUserInput())
                return;

            SetUpdateData();

            m_filesListObj = new FilesListObj(cboMdeFolder.Text); // get a list of file name
            if (m_filesListObj.numFile <= 0)
            {
                MessageBox.Show( "File doesn't exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
                cboMdeFolder.Focus();
                cboMdeFolder.BackColor = Color.YellowGreen;
                return; // exit
            }// end of if

            EnableUpdateGroupCtrl(false);

            btnRun.Enabled = false;
            btnAbort.Enabled = true;
            rdoDllArchive.Enabled = false;
            rdoDllRetrieve.Enabled = false;

            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_DllUpdateToDS));
                m_thdList[i].Name = "Thd_Update_" + i.ToString();
                m_thdList[i].Start();
                Debug.WriteLine("\t Start Thread: " + m_thdList[i].Name);
            }//end of for
        }//end of RunUpdate

        /// <summary>
        /// Creating archive thread for archiving files into DS by using DLL
        /// Method Thd_DllArchiveToDS() is the one do the job
        /// </summary>
        private void RunArchive()
        {
            Debug.WriteLine("UcZDlls.cs - RunArchive()");
            if (!CheckArchiveUserInput())
                return;

            SetArchiveData();

            m_filesListObj = new FilesListObj(txtFolder.Text); // get a list of file name
            if (m_filesListObj.numFile <= 0)
            {
                MessageBox.Show( "File doesn't exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
                txtFolder.Focus();
                txtFolder.BackColor = Color.YellowGreen;
                return; // exit
            }// end of if

            EnableArchiveGroupCtrl(false); // user input good            

            btnRun.Enabled = false;
            btnAbort.Enabled = true;
            rdoDllRetrieve.Enabled = false;
            rdoDllUpdate.Enabled = false;

            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_DllArchiveToDS));
                m_thdList[i].Name = "Thd_Archive_" + i.ToString();
                m_thdList[i].Start();
                Debug.WriteLine("\t Start Thread: " + m_thdList[i].Name);
            }//end of for
        }//end of RunArchive

        /// <summary>
        /// ThreadStart callback function: Really do the work.
        /// 1) Update Thread count
        /// 2) Update MDE in DS
        /// 3) Reading from the zKey ... 
        /// </summary>
        private void Thd_DllUpdateToDS()
        {
            Debug.WriteLine("UcZDlls.cs - Thd_DllUpdateToDS");

            #region Dynamic Load DLL
            // test code for dynamic load - safe for reference
            //int hLib = Win32Api.LoadLibrary("ZLib\\dsnadll.dll");
            //IntPtr ptr = Win32Api.GetProcAddress(hLib, "savetods");
            //savetods mySaveToDS = (savetods)Marshal.GetDelegateForFunctionPointer(ptr, typeof(savetods));
            #endregion // save for reference


            bool bRC = true; // TO DO : WHAT THIS MEAN???
            uint rc = 0; // default assume good
            string line = ""; // a line from key file
            string zkey = "";
            string strTmp = "";
            DateTime startTime;
            TimeSpan timeSpan;
            
            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start the retrieval-job Count: " + m_thdCount);
                }

                StreamReader sr = new StreamReader(m_zKeyFileName);
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tmp = line.Split(new Char[] { ';' });
                    zkey = tmp[0];

                    Debug.WriteLine("\t Start Calling update API");

                    StringBuilder sbMetadata = new StringBuilder(); // new mde

                    // Create an instance of StreamReader to read from a file.
                    // The using statement also closes the StreamReader.
                    using (StreamReader srMde = new StreamReader(m_filesListObj.fullFileName))
                    {
                        String tmpStr;
                        // Buid the file string until the end of the file is reached.
                        while ((tmpStr = srMde.ReadLine()) != null)
                        {
                            sbMetadata.Append(tmpStr);
                            sbMetadata.Append("\n");
                        }
                    }//end of using

                    if (chkDllTiming.Checked)
                    {
                        startTime = DateTime.Now;
                        rc = ZDllWrapper.metaupdate(zkey, WsClient.Program.appSetting.WsServer_URL, m_domainName, sbMetadata.ToString(), ref bRC);
                        timeSpan = DateTime.Now - startTime;
                        strTmp = m_filesListObj.fullFileName + ';'
                               + m_filesListObj.currentFileSize + ';'
                               + timeSpan.TotalSeconds.ToString() + ';'
                               + zkey.ToString();
                    }
                    else
                    {
                        rc = ZDllWrapper.metaupdate(zkey, WsClient.Program.appSetting.WsServer_URL, m_domainName, sbMetadata.ToString(), ref bRC);
                    }

                    if (rc == 0) // 0 = SOAP archive successful
                    {
                        m_totalFileSize += m_filesListObj.currentFileSize;
                    }
                    else
                    {
                        lock (this)
                        {
                            m_failCount++;
                        }
                        strTmp += "+++" + ZDllWrapper.getgSOAPErrorCode(rc);
                        commObj.LogToFile(m_DllsLog, strTmp);
                        goto ErrJumpHere;
                    }

                    lock (this)
                    {
                        m_numMail++; // multiple thread access this variable.
                    }//end of lock

                    if (chkDllTiming.Checked)
                        commObj.WriteLineByLine(m_dllPerfLog, strTmp);

                ErrJumpHere:
                    sbMetadata = null;
                    m_filesListObj.idxFile++; // point to next file
                    if (m_filesListObj.numFile <= m_filesListObj.idxFile)                    
                        m_filesListObj.idxFile = 0; //reset file index

                    IAsyncResult r = BeginInvoke(m_delegateNumMailCtrl, new object[] { m_numMail });
                    Thread.Sleep(m_delay * 1000); // real time
                }//end of while
                sr.Close();
                sr.Dispose();

            // Comment @03-21-05: decrement in finally
            //    lock (this)
            //    {
            //        --m_thdCount; // decrement
            //        Debug.WriteLine("\t End the Retrieval job Count: " + m_thdCount);
            //    }
            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Trace.WriteLine("\t Thread Abort Msg \n" + thdEx.Message);
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace.ToString();
                commObj.LogToFile(m_DllsLog, "\t Exception " + msg);
                MessageBox.Show( msg, "Generic Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch - generic exception
            finally
            {
                lock (this)
                {
                    // change from m_thdCount to --m_thdCount
                    IAsyncResult iAsync = BeginInvoke(m_delegateJobDoneNotification, new object[] { --m_thdCount });
                    Debug.WriteLine("\t Finally: Invoke ThdDoneNotification");
                }
            }//end of finally

        }//end of Thd_DllUpdateToDS

        /// <summary>
        /// ThreadStart callback function: Really do the work.
        /// 1) Update Thread count
        /// 2) Archive to DS
        /// 3) Record the zKey into a file... 
        /// </summary>
        private void Thd_DllArchiveToDS()
        {
            Debug.WriteLine("UcZDlls.cs - Thd_DllArchiveToDS");


            #region Dynamic Load DLL
            // test code for dynamic load - safe for reference
            //int hLib = Win32Api.LoadLibrary("ZLib\\dsnadll.dll");
            //IntPtr ptr = Win32Api.GetProcAddress(hLib, "savetods");
            //savetods mySaveToDS = (savetods)Marshal.GetDelegateForFunctionPointer(ptr, typeof(savetods));
            #endregion // save for reference


            StringBuilder sbZDK = new StringBuilder(2000); // 2000 defined by Ricky and Greg

            string strTmp = "";
            DateTime startTime;
            TimeSpan timeSpan;
            uint rv = 0;
            bool bRC = true; // TO DO : WHAT THIS MEAN???

            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start the archive-job Count: " + m_thdCount);
                }

                for (int j = 0; j < m_filesListObj.numFile; j++)
                {
                    Debug.WriteLine("\t\t files index = " + m_filesListObj.idxFile.ToString());
                    Debug.WriteLine("\t\t files name  = " + m_filesListObj.fullFileName);
                    
                    FileStream fileStream = new FileStream(m_filesListObj.fullFileName, FileMode.Open, FileAccess.Read);
                    ZIStreamWrapper zIStream = new ZIStreamWrapper(fileStream);

                    Debug.WriteLine("\t Start calling zAPI");
                    if (chkDllTiming.Checked)
                    {
                        startTime = DateTime.Now;
                        rv = ZDllWrapper.savetods(zIStream, WsClient.Program.appSetting.WsServer_URL, m_domainName, m_mailFrom, m_rcptTo, m_storeTime, sbZDK);

                        timeSpan = DateTime.Now - startTime;
                        strTmp = m_filesListObj.fullFileName + ';'
                               + m_filesListObj.currentFileSize + ';'
                               + timeSpan.TotalSeconds.ToString() + ';'
                               + sbZDK.ToString();
                    }
                    else
                    {
                        rv = ZDllWrapper.savetods(zIStream, WsClient.Program.appSetting.WsServer_URL, m_domainName, m_mailFrom, m_rcptTo, m_storeTime, sbZDK);
                        #region Dynamic Load DLL
                        //rv = mySaveToDS(zIStream, WsClient.Program.appSetting.WsServer_URL, m_domainName, m_mailFrom, m_rcptTo, m_storeTime, sbZDK);
                        #endregion // save for reference
                    }

                    if (rv == 0) // 0 = SOAP archive successful
                    {
                        m_totalFileSize += m_filesListObj.currentFileSize;

                        // Do metadata update                        
                        StringBuilder sbMetadata = new StringBuilder(); // new mde

                        // Create an instance of StreamReader to read from a file.
                        // The using statement also closes the StreamReader.
                        using (StreamReader srMde = new StreamReader(m_mdeFile))
                        {
                            String tmpStr;
                            // Buid the file string until the end of the file is reached.
                            while ((tmpStr = srMde.ReadLine()) != null)
                            {
                                sbMetadata.Append(tmpStr);
                                sbMetadata.Append("\n");
                            }
                        }//end of using

                        uint rc = ZDllWrapper.metaupdate(sbZDK.ToString(), WsClient.Program.appSetting.WsServer_URL, m_domainName, sbMetadata.ToString(), ref bRC);
                        if( rc != 0 )
                            commObj.LogToFile(m_DllsLog, "1st mde update fail");
                    }
                    else
                    {
                        lock (this)
                        {
                            m_failCount++;
                        }                        
                        strTmp += "+++" + ZDllWrapper.getgSOAPErrorCode(rv);
                        commObj.LogToFile(m_DllsLog, strTmp);
                        
                        goto ErrJumpHere;
                    }

                    lock (this)
                    {
                        m_numMail++; // multiple thread access this variable.
                    }//end of lock

                    if (chkDllTiming.Checked)
                        commObj.WriteLineByLine(m_dllPerfLog, strTmp);

                    commObj.WriteLineByLine("DllKey.txt", sbZDK.ToString() + ';' + m_filesListObj.fullFileName);

                ErrJumpHere:
                    m_filesListObj.idxFile++; // point to next file
                    if (m_filesListObj.numFile <= m_filesListObj.idxFile)
                        m_filesListObj.idxFile = 0; //reset file index

                    IAsyncResult r = BeginInvoke(m_delegateNumMailCtrl, new object[] { m_numMail });
                    Thread.Sleep(m_delay * 1000); // real time delay per mail

                    zIStream = null; //release the stream???
                    fileStream.Close();
                    fileStream.Dispose();

                    // Debug section - no need in future
                    // int hLibModule = Win32Api.GetModuleHandleA("dsnadll.dll");
                    // commObj.WriteLineByLine("LibModule.txt", hLibModule.ToString());
                    // Win32Api.FreeLibrary(hLibModule);
                }//end of for - send everything in the folder

                //lock (this)
                //{
                //    --m_thdCount; // decrement
                //    Debug.WriteLine("\t End the archive-job Count: " + m_thdCount);
                //}

                //if (m_thdCount == 0) // all thread done: Enable controls
                //{
                //    IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { true });
                //}
                // archEventEnd(this, new ArchiveEventArgs(m_thdCount));
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
                    //IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                    if (--m_thdCount == 0)
                        BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                }
            }//end of finally - enable control

            Debug.WriteLine("\t Release resource - Thd_DllArchiveToDS");
            sbZDK = null; //release string builder object???

        }//end of Thd_DllArchiveToDS


        /// <summary>
        /// Creating retrieve thread for archiving files into DS by using DLL
        /// Method Thd_DllRetrieveFromDS() is the one do the job
        /// </summary>
        private void RunRetrieve()
        {
            Debug.WriteLine("UcZDlls.cs - RunRetrieve()");
            if (!CheckRetrieveUserInput())
                return;

            SetRetrievalData();
            EnableRetrieveGroupCtrl(false); // user input good, then handle the GUI
            btnRun.Enabled = false;
            btnAbort.Enabled = true;
            rdoDllArchive.Enabled = false;
            rdoDllUpdate.Enabled = false;

            // create folder here
            string tmpPath;
            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                tmpPath = txtStoreFolder.Text + "\\Thd_DllRetrieve_" + i.ToString();
                if (!Directory.Exists(tmpPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(tmpPath);
                }
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_DllRetrieveFromDS));
                m_thdList[i].Name = "Thd_DllRetrieve_" + i.ToString();
                m_thdList[i].Start();
                Debug.WriteLine("\t Start Dll Retrieval Thread: " + m_thdList[i].Name);
            }//end of for
        }//end of RunRetrieve

        private void Thd_DllRetrieveFromDS()
        {
            Debug.WriteLine("UcRetrieve.cs - Thd_RetrieveFromDS()");

            int i = 0;
            uint rc = 0; // default assume good
            String strRC = "";
            string line = ""; // a line from key file
            string outFile = ""; // full path file name
            string zkey = "";
            string duration = "";
            DateTime startTime;
            TimeSpan timeSpan;
            IStream istm = null;

            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start the retrieval-job Count: " + m_thdCount);
                }

                StreamReader sr = new StreamReader(m_zKeyFileName);
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tmp = line.Split(new Char[] { ';' });
                    zkey = tmp[0];
                    //outFile = m_dataFolder + "\\" + Thread.CurrentThread.Name + "\\" + i.ToString() + "_" + commObj.GetShortFileName(tmp[1]);
                    outFile = m_dataFolder + "\\" + Thread.CurrentThread.Name + "\\" + commObj.GetShortFileName(tmp[1]);

                    Debug.WriteLine("\t Start Calling store API");

                    using (FileStream fs = new FileStream(outFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        istm = new ZIStreamWrapper(fs);
                        if (chkDllTiming.Checked)
                        {
                            startTime = DateTime.Now; // start the timer
                            rc = ZDllWrapper.restorefromds(istm, zkey, WsClient.Program.appSetting.WsServer_URL, m_domainName);
                            timeSpan = DateTime.Now - startTime;
                            duration = timeSpan.TotalSeconds.ToString();
                        }
                        else
                            rc = ZDllWrapper.restorefromds(istm, zkey, WsClient.Program.appSetting.WsServer_URL, m_domainName);
                       
                        if (rc == 0) // 0 == SOAP action successful
                        {
                            strRC = "SOAP OK";
                            m_totalFileSize += fs.Length;
                        }
                        else
                        {
                            strRC = ZDllWrapper.getgSOAPErrorCode(rc);
                            lock (this)
                            {
                                m_failCount++;
                                commObj.LogToFile(m_DllsLog, "\r\n*** ERROR BREAK: " + strRC + "\r\n" + zkey);
                            }
                            continue; // skip the rest, do next zkey
                        }
                    }//end of using

                    lock (this)
                    {
                        m_numMail++;
                    }
                    i++; // new file name

                    IAsyncResult r = BeginInvoke(m_delegateNumMailCtrl, new object[] { m_numMail });
                    Thread.Sleep(m_delay * 1000); // real time
                }//end of while
                sr.Close();
                sr.Dispose();

                //lock (this)
                //{
                //    --m_thdCount; // decrement
                //    Debug.WriteLine("\t End the Retrieval job Count: " + m_thdCount);
                //}                
            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Trace.WriteLine("\t Thread Abort Msg \n" + thdEx.Message);
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace.ToString();
                commObj.LogToFile(m_DllsLog, "\t Exception " + msg);
                MessageBox.Show( msg, "Generic Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch - generic exception
            finally
            {
                lock (this)
                {
                    if (--m_thdCount == 0)
                        BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                    //IAsyncResult iAsync = BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                    Debug.WriteLine("\t Finally: Invoke ThdDoneNotification");
                }
            }//end of finally
        }//end of Thd_DllRetrieveFromDS

        /// <summary>
        /// Kill the Archive Thread.
        /// </summary>
        /// <param name="oneThd"></param>
        private void Kill_Dll_Thread(ref Thread oneThd)
        {
            try
            {
                oneThd.Abort(); // abort
                oneThd.Join(); // require for ensure the thread kill

            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Trace.WriteLine(thdEx.Message);
                commObj.LogToFile("Thread.log", "\t Exception ocurr in Kill_Dll_Thread:" + oneThd.Name);
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show( msg, "Kill_Dll_Thread", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
        }//end of Kill_Dll_Thread

        /// <summary>
        /// Abort Archive Job
        /// </summary>
        private void AbortArchiveJob()
        {
            Debug.WriteLine("UcZDlls.cs - AbortArchiveJob");
            try
            {
                for (int i = 0; i < m_initialThread; i++)
                {
                    if (m_thdList[i] != null && m_thdList[i].IsAlive)
                        Kill_Dll_Thread(ref m_thdList[i]);
                }//end of for

                // reset mouse cursor and enable control
                IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
            }//end of try
            catch (Exception ex)
            {
                commObj.LogToFile("UcZDlls.cs - AbortArchiveJob() " + ex.Message);
            }//end of catch

        }//end of AbortArchiveJob

        private void AbortRetrievalJob()
        {
            Debug.WriteLine("UcZDlls.cs - AbortRetrievalJob");
            AbortArchiveJob(); // reuse the same function????
        }//end of AbortRetrievalJob

        private void AbortUpdateJob()
        {
            Debug.WriteLine("UcZDlls.cs - AbortUpdateJob");
            AbortArchiveJob(); // reuse the same function????
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcZDlls.cs - btnAbort_Click");
            if (rdoDllArchive.Checked)
            {
                AbortArchiveJob();
                OnUpdateStatusBar(new StatusEventArgs("Z. Dll Archive: Abort"));
            }
            else
                if( rdoDllRetrieve.Checked)
                {
                    AbortRetrievalJob();
                    OnUpdateStatusBar(new StatusEventArgs("Z. Dll Retrieve: Abort"));
                }
                else
                    if (rdoDllUpdate.Checked)
                    {
                        AbortUpdateJob();
                        OnUpdateStatusBar(new StatusEventArgs("Z. Dll Update: Abort"));
                    }
        }//end of btnAbort_Click

        /// <summary>
        /// Handle everything after all archive threads done....
        /// Enable controls, set end time etc.
        /// </summary>
        /// <param name="value"></param>
        public void JobDoneHandler(int thdId)
        {
            Debug.WriteLine("UcArchive.cs - JobDoneHandler");
            string tmpStr = "";

            m_procEndTime = DateTime.Now;
            txtEndTime.Text = m_procEndTime.ToString();
            txtNumMail.Text = m_numMail.ToString();

            TimeSpan timeSpan = m_procEndTime - m_procStartTime;
            txtDuration.Text = timeSpan.TotalSeconds.ToString();

            txtMailsSize.Text = m_totalFileSize.ToString();

            if (0 < m_numMail)
            {
                long aveSize = (long)(m_totalFileSize / m_numMail);
                txtAveSize.Text = aveSize.ToString();
            }//end of if

            double mailPerSec = m_numMail / timeSpan.TotalSeconds;
            txtMailPerSec.Text = mailPerSec.ToString();
            if (rdoDllArchive.Checked)
            {
                EnableArchiveGroupCtrl(true);
                EnableRetrieveGroupCtrl(false);
                EnableUpdateGroupCtrl(false);
                tmpStr = "Archive Duration: ";
            }
            else
                if (rdoDllRetrieve.Checked)
                {
                    EnableRetrieveGroupCtrl(true);
                    EnableArchiveGroupCtrl(false);
                    EnableUpdateGroupCtrl(false);
                    tmpStr = "Retrieval Duration: ";
                }
                else
                    if (rdoDllUpdate.Checked)
                    {
                        EnableRetrieveGroupCtrl(false);
                        EnableArchiveGroupCtrl(false);
                        EnableUpdateGroupCtrl(true);
                        tmpStr = "Update MD Duration: ";
                    }

            btnRun.Enabled = true; // enable the Run button
            btnAbort.Enabled = false;
            rdoDllArchive.Enabled = true;
            rdoDllRetrieve.Enabled = true;
            rdoDllUpdate.Enabled = true;

            string msg = "Thread ID" + thdId.ToString() + "\r\n" 
                + tmpStr + txtDuration.Text + "\r\n"
                + "Total Sent Files: " + txtNumMail.Text + "\r\n"
                + "Total Files Size: " + txtMailsSize.Text + "\r\n"
                + "Ave.  File  Size: " + txtAveSize.Text + "\r\n"
                + "Mails per second: " + txtMailPerSec.Text;

            commObj.LogToFile(m_DllsLog, msg);

            // debug section
            //int hLibModule = Win32Api.GetModuleHandleA("dsnadll.dll");
            //commObj.WriteLineByLine("LibModule.txt", "Before free:" + hLibModule.ToString());
            //Win32Api.FreeLibrary(hLibModule);
            //commObj.WriteLineByLine("LibModule.txt", "After free:" + Win32Api.GetModuleHandleA("dsnadll.dll"));

        }//end of JobDoneHandler

        /// <summary>
        /// Hidden feature :)        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenFile_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcZDlls.cs - btnGenFile_Click");
            string sourceFile = @"C:\wsMails\100K\mail.msg";
            string targetPath = @"C:\wsMails\100K\MSG_1K\";
            btnGenFile.Enabled = false;

            try
            {
                // Create the file and clean up handles.
                using (FileStream fs = File.Open(sourceFile, FileMode.Open)) { }

                // Ensure that the target does not exist.
                // File.Delete(path2);

                for (int i = 0; i < 1000; i++)
                {
                    // Copy the file.
                    File.Copy(sourceFile, targetPath + i.ToString() + ".msg");
                }//end of for
                Debug.WriteLine("\t Done 1K copy");
            }
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show( msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }
            btnGenFile.Enabled = true;
        }

        /// <summary>
        /// View the Dll Logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkViewDllTestLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Debug.WriteLine("UcZDlls.cs - lnkViewDllTestLog_LinkClicked");
            try
            {
                string logFullPathFileName = commObj.logFullPath + "\\" + m_DllsLog;
                Debug.WriteLine(logFullPathFileName);
                myDllLogTitle = commObj.ViewLogFromNotepad(logFullPathFileName, myDllLogTitle);
            }//emd of try
            catch (Win32Exception win32Ex)
            {
                Trace.WriteLine(win32Ex.Message + "\n" + win32Ex.GetType().ToString() + win32Ex.StackTrace);
            }//end of catch - win32 exception
        }

        private void UcZDlls_Load(object sender, EventArgs e)
        {
            SplashScreen.SetStatus("Loading Zantaz DLL");
            Debug.WriteLine("FFFFFF");
        }

        private void lnkUpdateKeyFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                if (ofDlg.FileName != "")
                {
                    txtUpdateKeyFile.Text = ofDlg.FileName;
                }//end of if - open file name
            }// end of if - open file dialog
        }

        private void lnkMdeFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            if (cboMdeFolder.Text != "")
                fbDlg.SelectedPath = cboMdeFolder.Text;  // set the default folder

            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                cboMdeFolder.Text = fbDlg.SelectedPath;
            }
        }

        // Show Tooltip
        #region MouseEnterEvent
        private void txtFolder_MouseEnter(object sender, EventArgs e)
        {            
            ttpZDlls.SetToolTip(txtFolder, "Location of Archive Data\r\n" + txtFolder.Text);
        }

        private void txtUpdateKeyFile_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtUpdateKeyFile, "Location of ZDK file\r\n" + txtUpdateKeyFile.Text);
        }

        private void txtKeyFile_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtKeyFile, "Location of ZDK file\r\n" + txtKeyFile.Text);
        }

        private void txtStoreFolder_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtStoreFolder, "Store retrieve files\r\n" + txtStoreFolder.Text);
        }

        private void txtStartTime_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtStartTime, txtStartTime.Text);
        }

        private void txtEndTime_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtEndTime, txtEndTime.Text);
        }

        private void txtDuration_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtDuration, txtDuration.Text);
        }

        private void txtNumMail_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtNumMail, txtNumMail.Text);
        }

        private void txtMailPerSec_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtMailPerSec, txtMailPerSec.Text);
        }

        private void txtMailsSize_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtMailsSize, txtMailsSize.Text);
        }

        private void txtAveSize_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(txtAveSize, txtAveSize.Text);
        }

        private void cboMdeFolder_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(cboMdeFolder, "Point to MDE files location. \r\nHit Enter to save the current value.\r\n" + cboMdeFolder.Text);
        }

        private void cboArchDomain_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(cboArchDomain, "Hit Enter to save the current value.\r\n" + cboArchDomain.Text);
        }

        private void cboMailFrom_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(cboMailFrom, "Hit Enter to save the current value.\r\n" + cboMailFrom.Text);
        }

        private void cboRcptTo_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(cboRcptTo, "Hit Enter to save the current value.\r\n" + cboRcptTo.Text);
        }

        private void cboUpdateRcptTo_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(cboUpdateRcptTo, "Hit Enter to save the current value.\r\n" + cboUpdateRcptTo.Text);
        }

        private void cboUpdateDomain_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(cboUpdateDomain, "Hit Enter to save the current value.\r\n" + cboUpdateDomain.Text);
        }

        private void cboRetrDomain_MouseEnter(object sender, EventArgs e)
        {
            ttpZDlls.SetToolTip(cboRetrDomain, "Hit Enter to save the current value.\r\n" + cboRetrDomain.Text);
        }
        #endregion

        private void chkDoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            cboMdeFile.Enabled = chkDoUpdate.Checked ? true : false;

        }
        
    }
}
