using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Dime;
//using Microsoft.Web.Services2.Security.X509;
//using DimeClient;


namespace WsClient
{
    public partial class UcSimpleTest : UserControl
    {
//        private ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
//        private WsClient.Properties.Settings appSetting = new WsClient.Properties.Settings();

        private CommObj commObj = new CommObj();
        private string m_logFileName = "SimpleTestLog.txt";
        private string m_xmlFile = "Simple.xml";

        // "UpdateStatusEventHandler" same as delegate above
        public event EventHandler<StatusEventArgs> statusChanged;
        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual

        public UcSimpleTest()
        {
            InitializeComponent();
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnArchive.Enabled = false;
            btnRetrieve.Enabled = false;
            rdoDIME.Enabled = false;
            rdoMIME.Enabled = false;
            chkSkipCompleteCall.Enabled = false;
            btnMdUpdate.Enabled = false;
            btnDelete.Enabled = false;

            if (chkDllTest.Checked)
            {
                archiveByDll(); // Do archive by DLL
            }
            else
            {
                archiveByWS(); // Do archive by WS
            }

            btnArchive.Enabled = true;
            btnRetrieve.Enabled = true;
            rdoDIME.Enabled = true;
            rdoMIME.Enabled = true;
            chkSkipCompleteCall.Enabled = true;
            btnMdUpdate.Enabled = true;
            btnDelete.Enabled = true;
            this.Cursor = Cursors.Default;
        }//end of btnArchive_Click

        /// <summary>
        /// Synchronize function... please wait...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            // Test of new implement of SMTP connection test.
            // bool rv = commObj.TestSMTPConnection("10.1.41.122", "25");
            Debug.WriteLine("ucTestPanel.cs - btnRetrieve_Click");                 

            this.Cursor = Cursors.WaitCursor;
            btnArchive.Enabled = false;
            btnRetrieve.Enabled = false;
            rdoDIME.Enabled = false;
            rdoMIME.Enabled = false;
            chkSkipCompleteCall.Enabled = false;
            btnMdUpdate.Enabled = false;
            btnDelete.Enabled = false;

            if (chkDllTest.Checked)
            {
                retrieveByDll();
            }
            else
            {
                if(rdoDIME.Checked)
                    retrieveByWS();
                else
                    if(rdoMIME.Checked)
                        retrieveMIMEByWS();
            }//end of else

            btnArchive.Enabled = true;
            btnRetrieve.Enabled = true;
            rdoDIME.Enabled = true;
            rdoMIME.Enabled = true;
            chkSkipCompleteCall.Enabled = true;
            btnMdUpdate.Enabled = true;
            btnDelete.Enabled = true;
            this.Cursor = Cursors.Default;
        }//end of btnRetrieve_Click

        private void btnMdUpdate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnArchive.Enabled = false;
            btnRetrieve.Enabled = false;
            rdoDIME.Enabled = false;
            rdoMIME.Enabled = false;
            chkSkipCompleteCall.Enabled = false;
            btnMdUpdate.Enabled = false;
            btnDelete.Enabled = false;

            if (chkDllTest.Checked)
            {
                updateByDll(); // Do update by DLL
            }
            else
            {
                updateByWS(); // Do update by WS
            }

            btnArchive.Enabled = true;
            btnRetrieve.Enabled = true;
            rdoDIME.Enabled = true;
            rdoMIME.Enabled = true;
            chkSkipCompleteCall.Enabled = true;
            btnMdUpdate.Enabled = true;
            btnDelete.Enabled = true;
            this.Cursor = Cursors.Default;
        }//end of btnMdUpdate_Click

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnArchive.Enabled = false;
            btnRetrieve.Enabled = false;
            rdoDIME.Enabled = false;
            rdoMIME.Enabled = false;
            chkSkipCompleteCall.Enabled = false;
            btnMdUpdate.Enabled = false;
            btnDelete.Enabled = false;

            if(chkDllTest.Checked)
            {
                // DeleteByDll(); // under construction
            }
            else
            {
                DeleteByWS(); // Do delete by WS
            }

            btnArchive.Enabled = true;
            btnRetrieve.Enabled = true;
            rdoDIME.Enabled = true;
            rdoMIME.Enabled = true;
            chkSkipCompleteCall.Enabled = true;
            btnMdUpdate.Enabled = true;
            btnDelete.Enabled = true;
            this.Cursor = Cursors.Default;
        }//end of btnDelete_Click

        private void rdoFileName_Click(object sender, EventArgs e)
        {
            lnkLabel.Text = "File Name";
        }// end of rdoFileName_Click

        private void rdoString_Click(object sender, EventArgs e)
        {
            lnkLabel.Text = "String";
        }

        private void lnkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.Filter = "msg files (*.msg)|*.msg|doc files (*.doc)|*.doc|All files (*.*)|*.*";
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                if (ofDlg.FileName != "")
                {
                    txtDataHandler.Text = ofDlg.FileName;
                    lnkLabel.Text = "File Name";
                    rdoFileName.Checked = true;
                }//end of if - open file name
            }// end of if - open file dialog
        }//end of lnkLabel_LinkClicked

        private void chkDllTest_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - chkDllTest_Click");
            if (chkDllTest.Checked)
            {
                Debug.WriteLine("\t chkDllTest_Click");
                lnkLabel.Text = "File Name";
                rdoFileName.Checked = true;
            }
            else
            {
                Debug.WriteLine("\t chkDllTest UN Click");
                // rdoFileName and lnkLabel state unchange
            }
        }//end of lnkLabel_LinkClicked

        /// <summary>
        /// Archive files by calling web service API directly
        /// </summary>
        /// <returns></returns>
        private bool archiveByWS()
        {
            txtZDK.Text = "Archiving... Please wait...";
            txtZDK.Refresh();

            ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();

            bool rv = true; // GOOD
            long fileSize = 0;
            string storeTime = dtpArchive.Value.ToLocalTime().ToString( "R" );
            string domainName = cboDomainName.Text;
            string mailFrom = cboMailFrom.Text;
            string rcptTo = cboRcptTo.Text;
            string aString = txtDataHandler.Text;
            string mdeInFile = txtMdeFile.Text;

            try
            {
                SoapContext outSOAPContext = zApi.RequestSoapContext;

                zApi.Timeout = -1; // ensure large file transfer in time

                //UTF8Encoding encoder = new UTF8Encoding();
                //byte[] bytes = encoder.GetBytes(aString);
                //MemoryStream aStream = new MemoryStream(bytes);
                //DimeAttachment outAttachment = new DimeAttachment("text/plain", TypeFormat.None, aStream);
                //outAttachment.Id = "attachment1";
                //outSOAPContext.Attachments.Add(outAttachment);


                DimeAttachment outAttachment;
                if (rdoString.Checked) // input as text operation
                {
                    UTF8Encoding encoder = new UTF8Encoding(); // encoding for simple text string
                    byte[] bytes = encoder.GetBytes(aString);
                    MemoryStream memStream = new MemoryStream(bytes);
                    outAttachment = new DimeAttachment("text/plain", TypeFormat.None, memStream);
                    outAttachment.Id = "plain_text";
                    fileSize = aString.Length; // get the size of the string
                    outSOAPContext.Attachments.Add( outAttachment ); // inline for simplify logic
                }
                else // input as file - no encoding
                {
                    FileStream fs = File.OpenRead(aString);
                    fileSize = fs.Length;

                    outAttachment = new DimeAttachment("text/plain", TypeFormat.None, fs); // file name
                    outAttachment.Id = aString;
                    outSOAPContext.Attachments.Add( outAttachment ); // inline for simplify logic

                    // 2nd attachment for mde - new for soap archive
                    if(chkMde.Checked )
                    {
                        if(!String.IsNullOrEmpty( mdeInFile ))
                        {
                            DimeAttachment mdeAttachment;
                            FileStream fs2 = File.OpenRead( mdeInFile );
                            fileSize = fs2.Length;

                            mdeAttachment = new DimeAttachment( "text/plain", TypeFormat.None, fs2 ); // file name
                            mdeAttachment.Id = "mde";

                            outSOAPContext.Attachments.Add( mdeAttachment ); // 2nd attachment
                        }//end of if - mde file is ok
                        else
                        {
                            MessageBox.Show( "MDE File empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
                            return (false);
                        }                       
                    }
                    
                }//end of else                

                // save for reference
                // X509Certificate cert = new X509Certificate( "c:\\tmp\\SE_cert.p12", "5+3v3j065" );
                // zApi.ClientCertificates.Add( cert );

                if(ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) != null)
                    zApi.ClientCertificates.Add( ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) );


                //commObj.LogToFile( m_logFileName, fileSize + " " + storeTime + " " + domainName + " " + mailFrom + " " + rcptTo );
                // Invoke Service                
                string strZDK = zApi.storeDocument(fileSize, storeTime, domainName, mailFrom, rcptTo);
                txtZDK.Text = strZDK;
            }//end of try
            catch (Exception ex)
            {
                rv = false; //BAD
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                txtZDK.Text = msg;
                commObj.LogToFile(m_logFileName, msg);
                MessageBox.Show( msg, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
            return (rv);
        }//end of archiveByWS

        /// <summary>
        /// Retrieve files by calling web service API directly from WSP
        /// </summary>
        /// <returns></returns>
        private bool retrieveByWS()
        {
            bool rv = true; // Good
            string strRetrieve = "";
            string tmpZDK = ""; // quick and dirty for retrieve complete

            ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
            try
            {

                //X509CertificateStore store = X509CertificateStore.CurrentUserStore( X509CertificateStore.MyStore );
                //bool open = store.OpenRead();

                //X509Certificate cert = null;
                ////Subject Key Identifier from Client cert.
                //byte[] certKeyID = new byte[] { 0x46, 0xc4, 0x8f, 0x21, 0x48, 0x41, 0x5f, 0xf6, 0x46, 0x75, 0xa6, 0x93, 0x6b, 0x92, 0xb4, 0x24, 0xfe, 0xc8, 0x62, 0x13 };
                                               
                //X509CertificateCollection certs = store.FindCertificateByKeyIdentifier( certKeyID );

                //if(0 < certs.Count)
                //{
                //    string msg = "... Cert count more than 1. Using the first one from the collection ...";
                //    commObj.LogToFile( m_logFileName, msg );

                //    cert = certs[0]; // assume there is only one client cert
                //    zApi.ClientCertificates.Add( cert );
                //}
                //else
                //    cert = null;


                //X509Certificate cert = new X509Certificate( "c:\\tmp\\SE_cert.p12", "5+3v3j065" );
                //zApi.ClientCertificates.Add( cert );


                if(ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) != null)
                    zApi.ClientCertificates.Add( ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) );


                tmpZDK = txtZDK.Text;
                strRetrieve = zApi.retrieveDocument(txtZDK.Text, cboDomainName.Text);
                // txtZDK.Text = strRetrieve;
                txtZDK.Text = "\r\n After retrieveDocument:  " + strRetrieve;

                // test code here - delete later
                SoapContext inSOAPContext = zApi.ResponseSoapContext;
                int attachCount = inSOAPContext.Attachments.Count;
                if (attachCount <= 0)
                {
                    txtZDK.Text += "\r\n Attach count = " + attachCount.ToString();
                    return( false ); // BAD - EXIT
                }

                string fn = @"C:\tmp\DSfile.msg";
                using (FileStream fs = new FileStream(fn, FileMode.Create))
                {
                    long bufSize = zApi.ResponseSoapContext.Attachments[0].Stream.Length;
                    byte[] buffer = new byte[bufSize];
                    // byte[] buffer = new byte[10];
                    int chunkLength;

                    int count = 0;
                    while ((chunkLength = zApi.ResponseSoapContext.Attachments[0].Stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, chunkLength);
                        count++;
                    }
                    txtZDK.Text += "\r\n count = " + count.ToString();
                }

                if(!chkSkipCompleteCall.Checked) // skip if checked
                {
                    zApi.retrieveComplete( tmpZDK, cboDomainName.Text ); // signal WSP the retrieval complete.
                }
                
            }//end of try
            catch (Exception ex)
            {
                rv = false; // BAD
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                txtZDK.Text = msg;
                commObj.LogToFile(m_logFileName, msg);
                MessageBox.Show( msg, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
            return (rv);
        }//end of retrieveByWS

        /// <summary>
        /// Retrieve files by calling web service API directly from WSP
        /// </summary>
        /// <returns></returns>
        private bool retrieveMIMEByWS()
        {
            Debug.WriteLine( "UcSimpleTest.cs - retrieveMIMEByWS");
            bool rv = true; // Good
            string strRetrieve = "";
            string tmpZDK = ""; // quick and dirty for retrieve complete

            ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
            try
            {
                if(ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) != null)
                    zApi.ClientCertificates.Add( ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) );

                tmpZDK = txtZDK.Text;
                strRetrieve = zApi.retrieveMime( txtZDK.Text, cboDomainName.Text );
                txtZDK.Text = "\r\n After retrieveDocument:  " + strRetrieve;

                // test code here - delete later
                SoapContext inSOAPContext = zApi.ResponseSoapContext;
                int attachCount = inSOAPContext.Attachments.Count;
                if(attachCount <= 0)
                {
                    txtZDK.Text += "\r\n Attach count = " + attachCount.ToString();
                    return (false); // BAD - EXIT
                }

                string fn = @"C:\tmp\DSfile.msg";
                using(FileStream fs = new FileStream( fn, FileMode.Create ))
                {
                    long bufSize = zApi.ResponseSoapContext.Attachments[0].Stream.Length;
                    byte[] buffer = new byte[bufSize];
                    // byte[] buffer = new byte[10];
                    int chunkLength;

                    int count = 0;
                    while((chunkLength = zApi.ResponseSoapContext.Attachments[0].Stream.Read( buffer, 0, buffer.Length )) > 0)
                    {
                        fs.Write( buffer, 0, chunkLength );
                        count++;
                    }
                    txtZDK.Text += "\r\n count = " + count.ToString();
                }//end of using

                if(!chkSkipCompleteCall.Checked) // skip if checked
                {
                    Debug.WriteLine( "\t Skip the completion call" );
                    zApi.retrieveComplete( tmpZDK, cboDomainName.Text ); // signal WSP the retrieval complete.
                }
                
            }//end of try
            catch(Exception ex)
            {
                rv = false; // BAD
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                txtZDK.Text = msg;
                commObj.LogToFile( m_logFileName, msg );
                MessageBox.Show( msg, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
            return (rv);
        }//end of retrieveMIMEByWS

        /// <summary>
        /// Update the metadata by calling web service API (This is temp solution
        /// </summary>
        /// <returns></returns>
        private bool updateByWS()
        {
            bool rv = true; // GOOD
            string aString = txtDataHandler.Text;
            ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();

            try
            {
                SoapContext outSOAPContext = zApi.RequestSoapContext;
                zApi.Timeout = 600000; // ensure large file transfer in time

                DimeAttachment outAttachment;

                FileStream fs = File.OpenRead(aString);
                outAttachment = new DimeAttachment("text/plain", TypeFormat.None, fs); // file name
                outAttachment.Id = aString;

                outSOAPContext.Attachments.Add(outAttachment);

                //zApi.updateDocument(txtZDK.Text, txtMailboxID.Text, txtFolderID.Text, cboRcptTo.Text, cboDomainName.Text);
                rv = zApi.updateDocument(txtZDK.Text, cboDomainName.Text);
                if( rv )
                    txtZDK.Text = "Metadata Update OK";
                else
                    txtZDK.Text = "Metadata Update FAIL";
            }//end of try
            catch (Exception ex)
            {
                rv = false; //BAD
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                txtZDK.Text = msg;
                commObj.LogToFile(m_logFileName, msg);
                MessageBox.Show( msg, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
            return (rv);
        }//end of updateByWS

        /// <summary>
        /// Delete the archived message based on the input zdk and domain name
        /// </summary>
        /// <returns></returns>
        private bool DeleteByWS()
        {
            bool rv = true; // GOOD
            string strStatus; //return status of API Call
            string tmpZDK = txtZDK.Text; // save the copy of zdk

            ZANTAZ_StoreAndRetrieveService zApi = new ZANTAZ_StoreAndRetrieveService();
            try
            {
                strStatus = zApi.destroyDocument( tmpZDK, cboDomainName.Text );
                if(strStatus.Length == 0)
                    txtZDK.Text = "Service State = OK";
                else
                    txtZDK.Text = "Service State = " + strStatus;

                string msg = "Delete message of " + tmpZDK;
                OnUpdateStatusBar( new StatusEventArgs( msg ) );

            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                txtZDK.Text = msg;
                commObj.LogToFile( m_logFileName, "DeleteByWS: " + msg );
                MessageBox.Show( msg, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
                rv = false;
            }//end of catch
            return (rv);
        }//end of DeleteByWS

        /// <summary>
        /// Archive files by calling ZANTAZ dll API. This is middle layer between WSP and Client.
        /// Retire on 2009
        /// </summary>
        /// <returns>gSOAP return code</returns>
        private uint archiveByDll()
        {
            //txtZDK.Text = "Archiving... Please wait...";
            //txtZDK.Refresh();

            uint rv = 0; // GOOD
            //string storeTime = dtpArchive.ToString();
            //string storeTime = dtpArchive.Value.ToLocalTime().ToString( "R" );
            //string domainName = cboDomainName.Text;
            //string mailFrom = cboMailFrom.Text;
            //string rcptTo = cboRcptTo.Text;
            //string fileName = txtDataHandler.Text;

            //try
            //{
            //    StringBuilder sbZDK = new StringBuilder(2000); // 2000 from Ricky and Greg

            //    try
            //    {
            //        FileStream fileStream = new FileStream(fileName, FileMode.Open);
            //        ZIStreamWrapper zIStmWrapper = new ZIStreamWrapper(fileStream);
            //        rv = ZDllWrapper.savetods(zIStmWrapper, WsClient.Program.appSetting.WsServer_URL, domainName, mailFrom, rcptTo, storeTime, sbZDK);
            //        if (rv == 0) // 0 = SOAP archive successful
            //        {
            //            txtZDK.Text = sbZDK.ToString();
            //        }
            //        else
            //        {
            //            txtZDK.Text = ZDllWrapper.getgSOAPErrorCode(rv);
            //        }
            //        fileStream.Close();
            //        fileStream.Dispose();
            //        zIStmWrapper = null;
            //    }//end of try
            //    catch (Exception ex)
            //    {
            //        string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
            //        MessageBox.Show(msg + "\r\n" + txtZDK.Text);
            //    }//end of catch
            //}//end of try
            //catch (Exception ex)
            //{
            //    string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
            //    txtZDK.Text = msg;
            //    commObj.LogToFile(m_logFileName, msg);
            //    MessageBox.Show(msg, "Exception");
            //}//end of catch
            return (rv);
        }//end of archiveByDll

        /// <summary>
        /// Retrieve files by calling ZANTAZ DLL API. This is middle layer between WSP and Client.
        /// Retired on 2009
        /// </summary>
        /// <returns>bool : true - GOOD, false - BAD</returns>
        private bool retrieveByDll()
        {
            bool rv = true; // GOOD
            // hard code for simple test
            //FileStream file = new FileStream("C:\\tmp\\dllRestore.msg", FileMode.Create);
            //IStream istm = new ZIStreamWrapper(file);
            //try
            //{
            //    uint rc = ZDllWrapper.restorefromds(istm, txtZDK.Text, WsClient.Program.appSetting.WsServer_URL, cboDomainName.Text);
            //    if (rc == 0) // 0 = SOAP archive successful
            //    {
            //        txtZDK.Text = rv.ToString();
            //    }
            //    else
            //    {
            //        txtZDK.Text = ZDllWrapper.getgSOAPErrorCode(rc);
            //        rv = false;
            //    }                
            //}//end of try
            //catch (Exception ex)
            //{
            //    string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
            //    MessageBox.Show(msg + "\r\n" + txtZDK.Text);
            //}//end of catch

            //file.Close();
            //file.Dispose();
            //istm = null;
            return (rv);
        }//end of retrieveByDll

        /// <summary>
        /// Update the meta data by calling ZANTAZ DLL API.
        /// </summary>
        /// <returns>bool : true - GOOD, false - BAD</returns>
        private bool updateByDll()
        {
            bool bRC = true; // assume GOOD
            uint rv = 0; // assume G-SOAP return OK
            StringBuilder sbMetadata = new StringBuilder();
            try
            {
                // TO DO : Update the metadata upate
                // rv = ZDllWrapper.metaupdate(txtZDK.Text, WsClient.Program.appSetting.WsServer_URL, cboDomainName.Text, txtMailboxID.Text, txtFolderID.Text, cboRcptTo.Text, ref rc);
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader srMde = new StreamReader(txtDataHandler.Text))
                {
                    String tmpStr;
                    // Buid the file string until the end of the file is reached.
                    while ((tmpStr = srMde.ReadLine()) != null)
                    {
                        sbMetadata.Append(tmpStr);
                        sbMetadata.Append("\n");
                    }
                }//end of using

                rv = ZDllWrapper.metaupdate(txtZDK.Text, WsClient.Program.appSetting.WsServer_URL, cboDomainName.Text, sbMetadata.ToString(), ref bRC);

                
                if (rv == 0) // 0 = SOAP archive successful
                {
                    txtZDK.Text = "Metadata Update OK";
                }
                else
                {
                    txtZDK.Text = ZDllWrapper.getgSOAPErrorCode(rv);
                }                    
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                txtZDK.Text = msg;
                commObj.LogToFile(m_logFileName, msg);
                MessageBox.Show( msg, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch

            return (bRC);
        }//end of updateByDll

#region Initial combo box control Code

        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcSimpleTest.cs - SaveComboBoxItem");
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
                tw = new XmlTextWriter(cboPath, System.Text.Encoding.UTF8);

                Debug.WriteLine("\t ComboBox Item file" + cboPath);

                tw.WriteStartDocument();
                tw.WriteStartElement("InitData");

                //The order is important
                WriteComboBoxEntries(cboDomainName, "cboDomainName", cboDomainName.Text, tw);
                WriteComboBoxEntries(cboMailFrom, "cboMailFrom", cboMailFrom.Text, tw);
                WriteComboBoxEntries(cboRcptTo, "cboRcptTo", cboRcptTo.Text, tw);
                WriteComboBoxEntries(cboWsUrl, "cboWsUrl", cboWsUrl.Text, tw);                

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
            Debug.WriteLine("UcSimpleTest.cs - WriteComboBoxEntries");
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
            Debug.WriteLine("UcSimpleTest.cs - LoadComboBoxes");
            try
            {
                cboDomainName.Items.Clear();
                cboMailFrom.Items.Clear();
                cboRcptTo.Items.Clear();
                cboWsUrl.Items.Clear();

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
                        case "cboWsUrl":
                            for (x = 0; x < nodeList.Item(3).ChildNodes.Count; ++x)
                            {
                                cboWsUrl.Items.Add(nodeList.Item(3).ChildNodes.Item(x).InnerText);
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
                if (0 < cboWsUrl.Items.Count)
                    cboWsUrl.Text = cboWsUrl.Items[0].ToString();

            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "Exception");
                MessageBox.Show( msg, "LoadComboBoxes()", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
        }//end of LoadComboBoxes
        #endregion // Initial ComboBox

        /// <summary>
        /// Initial the combo box selection text and edit box default text value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcSimpleTest_Load(object sender, EventArgs e)
        {
            SplashScreen.SetStatus("Loading Simple Test");
            txtDataHandler.Text = WsClient.Program.appSetting.DataFile;
            txtMdeFile.Text = WsClient.Program.appSetting.MdeFile;
            LoadComboBoxes(); // cannot do in constructor
        }//end of UcSimpleTest_Load

#region Handle key down key pass
        private void cboDomainName_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - cboDomainName_KeyDown");
            switch (e.KeyValue)
            {
                case 13: // enter key down - save the current value
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboDomainName_KeyDown

        private void cboMailFrom_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - cboMailFrom_KeyDown");
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }//end of cboMailFrom_KeyDown

        private void cboRcptTo_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - cboRcptTo_KeyDown");
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboRcptTo_KeyDown

        private void cboWsUrl_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - cboWsUrl_KeyDown");
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboWsUrl_KeyDown

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
            Debug.WriteLine("UcSimpleTest.cs - cboDomainName_KeyPress");
            AutoCompleteCombo(sender, e);
        }//end of cboDomainName_KeyPress

        private void cboMailFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - cboMailFrom_KeyPress");
            AutoCompleteCombo(sender, e);
        }//end of cboMailFrom_KeyPress

        private void cboRcptTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - cboRcptTo_KeyPress");
            AutoCompleteCombo(sender, e);
        }

        private void cboWsUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - cboWsUrl_KeyPress");
            AutoCompleteCombo(sender, e);
        }

#endregion

        private void btnIsAlive_Click(object sender, EventArgs e)
        {
            try
            {
                ZANTAZ_StoreAndRetrieveService dimeApi = new ZANTAZ_StoreAndRetrieveService(cboWsUrl.Text);

                if(ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) != null)
                    dimeApi.ClientCertificates.Add( ClientCertObj.GetClientCertBySubjectName( WsClient.Program.appSetting.CertSubjectName ) );

                bool isAlive = dimeApi.isAlive(cboDomainName.Text);
                txtZDK.Text = "Service State = " + isAlive.ToString();

                OnUpdateStatusBar(new StatusEventArgs(WsClient.Program.appSetting.WsServer_URL));
            }
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                txtZDK.Text = msg;
                commObj.LogToFile( m_logFileName, msg );
                MessageBox.Show( msg, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }
        }
        private void cboWsUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - cboWsUrl_SelectedIndexChanged");
            WsClient.Program.appSetting.WsServer_URL = cboWsUrl.Text;
        }

        private void cboWsUrl_Layout(object sender, LayoutEventArgs e)
        {
            Debug.WriteLine("UcSimpleTest.cs - cboWsUrl_Layout");
        }

        private void rdoDIME_Click(object sender, EventArgs e)
        {

        }

        private void rdoMIME_Click(object sender, EventArgs e)
        {

        }

        private void lnkMdeFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.Filter = "mde files (*.mde)|*.mde|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if(ofDlg.ShowDialog() == DialogResult.OK)
            {
                if(ofDlg.FileName != "")
                {
                    txtMdeFile.Text = ofDlg.FileName;
                }//end of if - open file name
            }// end of if - open file dialog
        }

        private void chkMde_Click(object sender, EventArgs e)
        {
            if(chkMde.Checked)
            {
                lnkMdeFile.Enabled = true;
                txtMdeFile.Enabled = true;
            }
            else
            {
                lnkMdeFile.Enabled = false;
                txtMdeFile.Enabled = false;
            }
        }
    }
}
