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

using Tamir.SharpSsh;

namespace WsClient
{
    /// <summary>
    /// Test procedure MUST Follow:
    /// 1. Clean up the Smart Cell
    /// 2. Get the email address from the ldap
    /// ldapsearch -h 10.0.93.2 -p 389 -x -D "cn=Admin,ou=zusers,o=testdomain1" -w skyline -b "o=testdomain1" -z 700000 "(objectClass=*)" | grep mail?* | awk {'print $2'} > address.txt
    /// 3. Send mail based on the address.txt
    /// 4. Extract the MDE from SC by using outputMDE.sh
    /// #!/bin/bash
    /// rm -f outMDE.txt
    /// while read line
    /// do
    /// echo +++++++++++++++++++++++++++++ >> outMDE.txt
    /// echo $line
    /// echo $line >> outMDE.txt
    /// unzip -p $line Data >> outMDE.txt
    /// done
    /// </summary>
    public partial class UcDSTest : UserControl
    {
        private string m_xmlFile = "DSTest.xml";
        private string m_logFile = "mdeData.txt";
        private string myMdeDataLogTitle = "mDe 0f SeaRcH loG file with Imp0ssible Name"; // uniqe name for killing windows
        private WsClient.LdapDataObj m_ldapDataObj;
        private WsClient.MdeDataObj m_mdeDataObj;
        private int m_Pass;
        private int m_Fail;
        private int m_Total;
        private CommObj commObj = new CommObj();
        private Thread m_thdMdeCheck;
        private SshExec exec;
      
        private delegate void DelegateJobDoneNotification(int count); // all thread done - indicate Thread index
        private DelegateJobDoneNotification m_delegateJobDoneNotification;

        public event EventHandler<StatusEventArgs> statusChanged;
        public event EventHandler<DisplayMsgEventArgs> displayMsgChanged;

        public UcDSTest()
        {
            InitializeComponent();
            m_delegateJobDoneNotification = new DelegateJobDoneNotification( JobDoneHandler );
        }

        public void JobDoneHandler(int count)
        {
            Debug.WriteLine( "UcDSTest.cs - +++++++ JobDoneHandler ++++++++" );

            EnableControls( true );

            string thdName = m_thdMdeCheck == null ? "" : m_thdMdeCheck.Name;
            string msg = "Thread " + thdName + "Done\r\n"
                + "Total Mde Record Check: " + count;
            
            commObj.LogToFile( msg );
            OnUpdateStatusBar( new StatusEventArgs( "Mde Check Done" ) );
        }//end of JobDoneHandler

        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged( this, eArgs );
        }//end of virtual

        // "DisplayMsgEventArgs" - argument in EventArgs class
        protected virtual void OnDisplayMsgInNotepad(DisplayMsgEventArgs eArgs)
        {
            displayMsgChanged( this, eArgs );
        }//end of virtual

        private void EnableControls(bool bValue)
        {
            cboNatIP.Enabled = bValue;
            cboBase.Enabled = bValue;
            cboLdapIP.Enabled = bValue;
            cboFilter.Enabled = bValue;
            cboCn.Enabled = bValue;
            cboOu.Enabled = bValue;
            txtInFile.Enabled = bValue;
            lnkInFile.Enabled = bValue;
            btnRun.Enabled = bValue;
            txtInFile.Enabled = bValue;
        }//end of EnableCOntrols

        /// <summary>
        /// Make sure user input ok before generate the data.
        /// </summary>
        /// <returns>true - OK; false - fail</returns>
        private bool ValidateUserInput()
        {
            bool rv = true; // assume everything is OK
            if(String.IsNullOrEmpty(txtInFile.Text))
            {
                rv = false;
                txtInFile.BackColor = Color.YellowGreen;
            }
            return (rv);
        }//end of ValidateUserInput

        private void lnkInFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.Filter = "result files (*.txt)|*.txt|All files (*.*)|*.*";
            if(ofDlg.ShowDialog() == DialogResult.OK)
            {
                txtInFile.Text = ofDlg.FileName;
            }// end of if - open file dialog
            else
            {
                txtInFile.BackColor = Color.YellowGreen;
                txtInFile.Focus();
            }
        }//end of lnkInFile_LinkClicked

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if(!ValidateUserInput())
                    return;

                m_Pass = m_Fail = m_Total = 0;
                txtFail.Text = txtPass.Text = txtTotal.Text = "0";
                commObj.DeleteFile( commObj.logFullPath + "\\" + m_logFile );
                EnableControls( false ); // disable control - but not the abort button

                m_thdMdeCheck = new Thread( new ThreadStart( this.Thd_MdeCheck ) );
                m_thdMdeCheck.Name = "Thd_MdeCheck";
                m_thdMdeCheck.Start();

                commObj.LogToFile( "++ Thd_MdeCheck Start ++" );
            }
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message );
                commObj.LogToFile( "Data Generation thread Error : " + ex.Message.ToString() );
            }
          
            //GetLdapData( cboFilter.Text ); // testing purpose
        }//end of btnRun_Click

        private void Thd_MdeCheck()
        {
            try
            {
                int REC_LEN = 100;
                int counter = 0;
                string[] mdeRec = new string[REC_LEN];
                string str;
                StreamReader sr = new StreamReader( txtInFile.Text );

                while((str = sr.ReadLine()) != null)
                {
                    //if(str.Contains( "++++++" ))
                    //    commObj.LogToFile( counter + "+++++" );

                    //if(counter < 29)
                    if(!str.Contains( "This" ) && counter < REC_LEN)
                        mdeRec[counter++] = str;
                    else
                    {
                        mdeRec[counter++] = "This";
                        CreateDataObjAndVerify( mdeRec );
                        counter = 0; //reset
                    }//end of else - outer
                }//end of while
//                CreateDataObjAndVerify( mdeRec ); // verify the last one
                sr.Close();
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg );
                commObj.LogToFile( m_logFile, msg );
            }
            finally
            {
                lock(this)
                {
                    BeginInvoke( m_delegateJobDoneNotification, new object[] { m_Total } );
                }
            }//end of finally            
        }//end of Thd_MdeCheck

        /// <summary>
        /// Create both MDE data object and ldap Data object for verification
        /// OK... also update the UI counters.
        /// Calling VerifyIdentityStamping function
        /// </summary>
        private void CreateDataObjAndVerify(string[] mdeRec)
        {
             m_Total++; // every time create mde data object
             m_mdeDataObj = new MdeDataObj( mdeRec );
             if((m_ldapDataObj = GetLdapData( "mail=" + m_mdeDataObj.From )) != null)
                 VerifyIdentityStamping();
             else
                 if((m_ldapDataObj = GetLdapData( "mailalternateaddress=" + m_mdeDataObj.From )) != null)
                     VerifyIdentityStamping();
                 else
                 {
                     // print out error
                     commObj.LogToFile( m_logFile, m_mdeDataObj.MdeFullFileName );
                     commObj.WriteLineByLine( m_logFile, "UcDSTest.cs - Warning: ldap search of From field return nothing - " + m_mdeDataObj.From );
                     commObj.WriteLineByLine( m_logFile, "Handle Recipient cases" );
                     HandleRcptCase();
                 }

             //debug only
             // commObj.LogToFile( "mdeData.txt", m_mdeDataObj.From );
             txtTotal.Text = m_Total.ToString( CultureInfo.CurrentCulture );
             txtPass.Text = m_Pass.ToString( CultureInfo.CurrentCulture );
             txtFail.Text = m_Fail.ToString( CultureInfo.CurrentCulture );
        }//end of CreateDataObjAndVerify

        /// <summary>
        /// Handle the sender (From) cacse first, then Recipients case (To, CC, BCC)
        /// Basically repeat whatever from sender checking...
        /// </summary>
        private void HandleRcptCase()
        {
            string rcpt = getMDERcptAddr();
            if(!String.IsNullOrEmpty( rcpt ))
            {
                if((m_ldapDataObj = GetLdapData( "mail=" + rcpt )) != null)
                    VerifyRcptStamping();
                else
                    if((m_ldapDataObj = GetLdapData( "mailalternateaddress=" + rcpt )) != null)
                        VerifyRcptStamping();
                    else
                    {
                        m_Fail++;
                        // print out error
                        commObj.LogToFile( m_logFile, "UcDSTest.cs - Error - ldap search RCPT return nothing - " + rcpt );
                    }
            }//end of if
            else
            {
                m_Fail++;
                // print out error
                commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                commObj.WriteLineByLine( "mdeData.txt", "UcDSTest.cs - Error: Empty Recipient." );
            }
        }//end of HandleRcptCase

        private void VerifyRcptStamping()
        {
            try
            {
                if(m_ldapDataObj.dn.Contains( "dbdirid" ))
                    HandleMSRcptCase(); // Using retention code as Region code                    
                else                        
                    if(m_ldapDataObj.dn.Contains( "msfwid" ))                            
                        HandleMSRcptCase();                        
                    else                            
                        if(m_ldapDataObj.dn.Contains( "uid" ))                                
                            HandleJPMCRcptCase();
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg );
                commObj.LogToFile( "mdeData.txt", msg );
            }//end of catch
        }//end of VerifyRcptStamping

        private void HandleJPMCRcptCase()
        {
            if(!m_mdeDataObj.RcptID.Contains( m_ldapDataObj.uId ))
            {
                commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-RECIPIENTS-ID: " + "mde:" + m_mdeDataObj.RcptID + " X " + "ldap:" + m_ldapDataObj.uId );
                m_Fail++;
            }
            else
                if(!m_mdeDataObj.RcptS6GroupID.Contains( m_ldapDataObj.s6groupid ))
                {
                    commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                    commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-RECIPIENTS-S6-GROUP-ID: " + "mde:" + m_mdeDataObj.RcptS6GroupID + " X " + "ldap:" + m_ldapDataObj.s6groupid );
                    m_Fail++;
                }
                else
                    if(!m_mdeDataObj.RcptS6UsrGrpMapping.Contains( m_ldapDataObj.s6usergroupmapping ))
                    {
                        commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                        commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-RECIPIENTS-S6-USER-GROUP-MAPPING: " + "mde:" + m_mdeDataObj.RcptS6UsrGrpMapping + " X " + "ldap:" + m_ldapDataObj.s6usergroupmapping );
                        m_Fail++;
                    }
                    else
                        if(!m_mdeDataObj.RegionCode.Contains( m_ldapDataObj.ST ))
                        {
                            commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                            commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-REGION-CODE: " + "mde:" + m_mdeDataObj.RegionCode + " X " + "ldap:" + m_ldapDataObj.ST );
                            m_Fail++;
                        }
                        else
                            m_Pass++;

        }//end of HandleJPMCRcptCase

        private void HandleMSRcptCase()
        {
            if(!m_mdeDataObj.RcptID.Contains( m_ldapDataObj.msfwId ))
            {
                commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-RECIPIENTS-ID: " + "mde:" + m_mdeDataObj.RcptID + " X " + "ldap:" + m_ldapDataObj.msfwId );
                m_Fail++;
            }
            else
                if(!m_mdeDataObj.RcptS6GroupID.Contains( m_ldapDataObj.s6groupid ))
                {
                    commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                    commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-RECIPIENTS-S6-GROUP-ID: " + "mde:" + m_mdeDataObj.RcptS6GroupID + " X " + "ldap:" + m_ldapDataObj.s6groupid );
                    m_Fail++;
                }
                else
                    if(!m_mdeDataObj.RcptS6UsrGrpMapping.Contains( m_ldapDataObj.s6usergroupmapping ))
                    {
                        commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                        commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-RECIPIENTS-S6-USER-GROUP-MAPPING: " + "mde:" + m_mdeDataObj.RcptS6UsrGrpMapping + " X " + "ldap:" + m_ldapDataObj.s6usergroupmapping );
                        m_Fail++;
                    }
                    else
                        if(!m_mdeDataObj.RegionCode.Contains( m_ldapDataObj.ST ))
                        {
                            commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                            commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-REGION-CODE: " + "mde:" + m_mdeDataObj.RegionCode + " X " + "ldap:" + m_ldapDataObj.ST );
                            m_Fail++;
                        }
                        else
                            m_Pass++;
        }//end of HandleMSRcptCase

        private string getMDERcptAddr()
        {
            string rv = null;
            if(!String.IsNullOrEmpty( m_mdeDataObj.To ))
                rv = m_mdeDataObj.To;
            else
                if(!String.IsNullOrEmpty( m_mdeDataObj.CC ))
                    rv = m_mdeDataObj.CC;
                else
                    if(!String.IsNullOrEmpty( m_mdeDataObj.BCC ))
                        rv = m_mdeDataObj.BCC;
            return (rv);
        }//end of getMDERcptAddr
        /// <summary>
        /// Basically verify the sender stamping
        /// </summary>
        private void VerifyIdentityStamping()
        {
            Debug.WriteLine( "UcDSTest.cs - VerifyIdentityStamping" );
            try
            {                
                // First check the status completion, Incomplete, print out the entire record.

                if((m_mdeDataObj.IdentityLookupStatus != "Complete") ||
//                    (m_mdeDataObj.RetentionStatus != "Complete") ||
                    (m_mdeDataObj.StampingStatus != "Complete"))
                {
                    commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                    commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-STAMPING-STATUS: " + m_mdeDataObj.StampingStatus );
                    commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-RETENTION-STATUS: " + m_mdeDataObj.RetentionStatus );
                    commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-IDENTITY-LOOKUP-STATUS: " + m_mdeDataObj.IdentityLookupStatus );

                }//end of if - status check
                else // Status Complete
                {
//save                    commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                    if(m_ldapDataObj.dn.Contains( "dbdirid" ))
                        HandleDBCase(); // Using retention code as Region code
                    else
                        if(m_ldapDataObj.dn.Contains( "msfwid" ))
                            HandleMSCase();
                        else
                            if(m_ldapDataObj.dn.Contains( "uid" ))
                                HandleJPMCCase();
                }//end of else
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg );
                commObj.LogToFile( "mdeData.txt", msg );
            }//end of catch
            
        }// end of VerifyIdentityStamping


        /// <summary>
        /// Similar to Handle
        /// </summary>        
        private void HandleJPMCCase()
        {
            if(m_mdeDataObj.SenderID != m_ldapDataObj.uId)
            {
                commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-SENDER-ID: " + "mde:" + m_mdeDataObj.SenderID + " X " + "ldap:" + m_ldapDataObj.uId );
                m_Fail++;
            }
            else
                if(m_mdeDataObj.SenderS6GrpID != m_ldapDataObj.s6groupid)
                {
                    commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                    commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-SENDER-S6-GROUP-ID: " + "mde:" + m_mdeDataObj.SenderS6GrpID + " X " + "ldap:" + m_ldapDataObj.s6groupid );
                    m_Fail++;
                }
                else
                    if(m_mdeDataObj.SentS6UsrGrpMapping != m_ldapDataObj.s6usergroupmapping)
                    {
                        commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                        commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-SENDER-S6-USER-GROUP-MAPPING: " + "mde:" + m_mdeDataObj.SentS6UsrGrpMapping + " X " + "ldap:" + m_ldapDataObj.s6usergroupmapping );
                        m_Fail++;
                    }
                    else
                        //if(m_mdeDataObj.RegionCode != m_ldapDataObj.ST)
                        if( !m_mdeDataObj.RegionCode.Contains( m_ldapDataObj.ST ) )
                        {
                            commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                            commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-REGION-CODE: " + "mde:" + m_mdeDataObj.RegionCode + " X " + "ldap:" + m_ldapDataObj.ST );
                            m_Fail++;
                        }
                        m_Pass++;
        }//end of JPMC Case

        /// <summary>
        /// Using the retention code as region code
        /// </summary>
        private void HandleDBCase()
        {
            if(m_mdeDataObj.RegionCode != m_ldapDataObj.retentionCode)
            {
                commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-REGION-CODE: " + "mde:" + m_mdeDataObj.RegionCode + " X " + "ldap:" + m_ldapDataObj.retentionCode );
                m_Fail++;
            }
            else
                if(m_mdeDataObj.SenderID != m_ldapDataObj.dbdirid)
                {
                    commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                    commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-SENDER-ID: " + "mde:" + m_mdeDataObj.SenderID + " X " + "ldap:" + m_ldapDataObj.dbdirid );
                    m_Fail++;
                }
                else
                    if(m_mdeDataObj.MsBloombergID != m_ldapDataObj.dbBloombergId)
                    {
                        commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                        commObj.WriteLineByLine( "mdeData.txt", "X-MSBLOOMBERGID: " + "mde:" + m_mdeDataObj.MsBloombergID + " X " + "ldap:" + m_ldapDataObj.dbBloombergId );
                        m_Fail++;
                    }
                    else
                        m_Pass++;
        }//end of HandleDBCase

        /// <summary>
        /// Using the retention code as region code
        /// Actually, it is the multi-instance S6 for supervision
        /// </summary>
        private void HandleMSCase()
        {
            if(m_mdeDataObj.RegionCode != m_ldapDataObj.C)
            {
                commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-REGION-CODE: " + "mde:" + m_mdeDataObj.RegionCode + " X " + "ldap:" + m_ldapDataObj.C );
                m_Fail++;
            }
            else
                if(m_mdeDataObj.SenderID != m_ldapDataObj.msfwId)
                {
                    commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                    commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-SENDER-ID: " + "mde:" + m_mdeDataObj.SenderID + " X " + "ldap:" + m_ldapDataObj.msfwId );
                    m_Fail++;
                }
                else
                    if(m_mdeDataObj.MsBloombergID != m_ldapDataObj.dbBloombergId)
                    {
                        commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                        commObj.WriteLineByLine( "mdeData.txt", "X-MSBLOOMBERGID: " + "mde:" + m_mdeDataObj.MsBloombergID + " X " + "ldap:" + m_ldapDataObj.msBloombergId );
                        m_Fail++;
                    }
                    else
                        if(m_mdeDataObj.SenderS6GrpID != m_ldapDataObj.s6groupid)
                        {
                            commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                            commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-SENDER-S6-GROUP-ID: " + "mde:" + m_mdeDataObj.SenderS6GrpID + " X " + "ldap:" + m_ldapDataObj.s6groupid );
                            m_Fail++;
                        }
                        else
                            if(m_mdeDataObj.SentS6UsrGrpMapping != m_ldapDataObj.s6usergroupmapping)
                            {
                                commObj.LogToFile( "mdeData.txt", m_mdeDataObj.MdeFullFileName );
                                commObj.WriteLineByLine( "mdeData.txt", "X-ZANTAZ-SENDER-S6-USER-GROUP-MAPPING: " + "mde:" + m_mdeDataObj.SentS6UsrGrpMapping + " X " + "ldap:" + m_ldapDataObj.s6usergroupmapping );
                                m_Fail++;
                            }
                            else
                                 m_Pass++;
        }//end of HandleMSCase


        /// <summary>
        /// Get the ldap data from identity server by issuing ldapsearch command through SSH
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Pre-defined ldap data or null if no result return</returns>
        private LdapDataObj GetLdapData(string filter)
        {
            LdapDataObj ldapObj = null;
            DisplayMsgEventArgs dMsgArg = new DisplayMsgEventArgs( "This message display in notepad" );
            OnDisplayMsgInNotepad( dMsgArg );
            OnUpdateStatusBar( new StatusEventArgs( "Test LDAP connect and searching..." ) );
            
            try
            {
                exec = new SshExec( cboNatIP.Text, "root", "skyline" );
                //Debug.Write( "Connecting..." );
                dMsgArg.strMsg = "Connecting...\r\n";
                OnDisplayMsgInNotepad( dMsgArg );
                exec.Connect();
                //Debug.WriteLine( "OK" );
                dMsgArg.strMsg += "OK...\r\n";
                OnDisplayMsgInNotepad( dMsgArg );

                //ssh 10.0.93.2 'ldapsearch -h 10.0.93.2 -p 389 -x -D "cn=Admin,ou=zusers,o=testdomain1" -w skyline -b "o=testdomain1" -z 1 "(E478@zantaz.com)"'
                //string cmd = "ssh " + cboLdapIP.Text + " 'ldapsearch -h " + cboLdapIP.Text + " -p 389 -x -D \"cn=Admin,ou=zusers,o=" + cboBase.Text + "\" -w skyline -b \"o=" + cboBase.Text + "\" -z 1 \"(" + cboFilter.Text + ")\"'";
                string cmd = "ssh " + cboLdapIP.Text + " 'ldapsearch -h " + cboLdapIP.Text + " -p 389 -x -D \"cn=" + cboCn.Text + ",ou=" + cboOu.Text + ",o=" + cboBase.Text + "\" -w skyline -b \"o=" + cboBase.Text + "\" -z 1 \"(" + filter + ")\"'";
                string output = exec.RunCommand( cmd );

                Debug.WriteLine( output );
                if(output.Contains( "numEntries" ))
                    ldapObj = new LdapDataObj( output );

                Debug.Write( "Disconnecting..." );
                dMsgArg.strMsg += "Disconnecting...\r\n";
                OnDisplayMsgInNotepad( dMsgArg );

                Debug.WriteLine( "OK" );
                dMsgArg.strMsg += "OK";
                OnDisplayMsgInNotepad( dMsgArg );
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg );
                commObj.WriteLineByLine( "mdeData.txt", msg );
            }
            finally
            {
                if( exec != null )
                    exec.Close();
            }
            return (ldapObj);
        }//end of GetLdapData

        /// <summary>
        /// Test the ldap connection. SSH to NAT server and issue remote command (SSH + ldap search)
        /// Result will display in the notepad tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            DisplayMsgEventArgs dMsgArg = new DisplayMsgEventArgs( "This message display in notepad" );
            OnDisplayMsgInNotepad( dMsgArg );
            OnUpdateStatusBar( new StatusEventArgs( "Test LDAP connect and searching..." ) );

            try
            {
                SshExec exec = new SshExec( cboNatIP.Text, "root", "skyline" );
                //Debug.Write( "Connecting..." );
                dMsgArg.strMsg = "Connecting...\r\n";
                OnDisplayMsgInNotepad( dMsgArg );
                exec.Connect();
                //Debug.WriteLine( "OK" );
                dMsgArg.strMsg += "OK...\r\n";
                OnDisplayMsgInNotepad( dMsgArg );

                //ssh 10.0.93.2 'ldapsearch -h 10.0.93.2 -p 389 -x -D "cn=Admin,ou=zusers,o=testdomain1" -w skyline -b "o=testdomain1" -z 1 "(E478@zantaz.com)"'
                //string cmd = "ssh " + cboLdapIP.Text + " 'ldapsearch -h " + cboLdapIP.Text + " -p 389 -x -D \"cn=Admin,ou=zusers,o=" + cboBase.Text + "\" -w skyline -b \"o=" + cboBase.Text + "\" -z 1 \"(" + cboFilter.Text + ")\"'";
                string cmd = "ssh " + cboLdapIP.Text + " 'ldapsearch -h " + cboLdapIP.Text + " -p 389 -x -D \"cn=" + cboCn.Text + ",ou=" + cboOu.Text + ",o=" + cboBase.Text + "\" -w skyline -b \"o=" + cboBase.Text + "\" -z 1 \"(" + cboFilter.Text + ")\"'";
                string output = exec.RunCommand( cmd );

                Debug.WriteLine( output );
                output = output.Replace( "\n", "\r\n" );
                Debug.WriteLine( output );
                dMsgArg.strMsg += output.ToString();
                OnDisplayMsgInNotepad( dMsgArg );

                Debug.Write( "Disconnecting..." );
                dMsgArg.strMsg += "Disconnecting...\r\n";
                OnDisplayMsgInNotepad( dMsgArg );

                exec.Close();
                Debug.WriteLine( "OK" );
                dMsgArg.strMsg += "OK";
                OnDisplayMsgInNotepad( dMsgArg );
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg );
                commObj.LogToFile( "mdeData.txt", msg );

            }
        }//end of btnTest_Click

        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine( "UcDSTest.cs - SaveComboBoxItem" );
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\" + m_xmlFile;
                if(!File.Exists( cboPath ))
                {
                    StreamWriter sw = File.CreateText( cboPath );
                    sw.Close();
                }

                // Save the combox
                tw = new XmlTextWriter( Application.StartupPath + "\\" + m_xmlFile, System.Text.Encoding.UTF8 );

                Debug.WriteLine( "\t ComboBox Item file" + Application.StartupPath + "\\" + m_xmlFile );

                tw.WriteStartDocument();
                tw.WriteStartElement( "InitData" );

                //The order is important
                WriteComboBoxEntries( cboNatIP, "cboNatIP", cboNatIP.Text, tw );
                WriteComboBoxEntries( cboLdapIP, "cboLdapIP", cboLdapIP.Text, tw );
                WriteComboBoxEntries( cboBase, "cboBase", cboBase.Text, tw );
                WriteComboBoxEntries( cboFilter, "cboFilter", cboFilter.Text, tw );
                WriteComboBoxEntries( cboCn, "cboCn", cboCn.Text, tw );
                WriteComboBoxEntries( cboCn, "cboOu", cboOu.Text, tw );

                tw.WriteEndElement();
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg, "SaveComboBoxItem()" );
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
            Debug.WriteLine( "UcDSTest.cs - WriteComboBoxEntries" );
            int maxEntriesToStore = 10;

            tw.WriteStartElement( "combobox" );
            tw.WriteStartAttribute( "name", string.Empty );
            tw.WriteString( cboBoxName );
            tw.WriteEndAttribute();

            // Write the item from the text box first.
            if(txtBoxText.Length != 0)
            {
                tw.WriteStartElement( "entry" );
                tw.WriteString( txtBoxText );
                tw.WriteEndElement();
                maxEntriesToStore -= 1;
            }//end of if

            // Write the rest of the entries (up to 10).
            for(int i = 0; i < cboBox.Items.Count && i < maxEntriesToStore; ++i)
            {
                if(txtBoxText != cboBox.Items[i].ToString())
                {
                    tw.WriteStartElement( "entry" );
                    tw.WriteString( cboBox.Items[i].ToString() );
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
            Debug.WriteLine( "UcDSTest.cs - LoadComboBoxes" );
            try
            {
                cboNatIP.Items.Clear();
                cboLdapIP.Items.Clear();
                cboBase.Items.Clear();
                cboFilter.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\" + m_xmlFile;
                if(!File.Exists( cboPath ))
                {
                    //File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load( cboPath );
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                int numComboBox = nodeList.Count;
                int x;
                for(int i = 0; i < numComboBox; i++)
                {
                    switch(nodeList.Item( i ).Attributes.GetNamedItem( "name" ).InnerText)
                    {
                        case "cboNatIP":
                            for(x = 0; x < nodeList.Item( 0 ).ChildNodes.Count; ++x)
                            {
                                cboNatIP.Items.Add( nodeList.Item( 0 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboLdapIP":
                            for(x = 0; x < nodeList.Item( 1 ).ChildNodes.Count; ++x)
                            {
                                cboLdapIP.Items.Add( nodeList.Item( 1 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboBase":
                            for(x = 0; x < nodeList.Item( 2 ).ChildNodes.Count; ++x)
                            {
                                cboBase.Items.Add( nodeList.Item( 2 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboFilter":
                            for(x = 0; x < nodeList.Item( 3 ).ChildNodes.Count; ++x)
                            {
                                cboFilter.Items.Add( nodeList.Item( 3 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboCn":
                            for(x = 0; x < nodeList.Item( 4 ).ChildNodes.Count; ++x)
                            {
                                cboCn.Items.Add( nodeList.Item( 4 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboOu":
                            for(x = 0; x < nodeList.Item( 5 ).ChildNodes.Count; ++x)
                            {
                                cboOu.Items.Add( nodeList.Item( 5 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;

                    }//end of switch
                }//end of for

                if(0 < cboNatIP.Items.Count)
                    cboNatIP.Text = cboNatIP.Items[0].ToString();
                if(0 < cboLdapIP.Items.Count)
                    cboLdapIP.Text = cboLdapIP.Items[0].ToString();
                if(0 < cboBase.Items.Count)
                    cboBase.Text = cboBase.Items[0].ToString();
                if(0 < cboFilter.Items.Count)
                    cboFilter.Text = cboFilter.Items[0].ToString();
                if(0 < cboCn.Items.Count)
                    cboCn.Text = cboCn.Items[0].ToString();
                if(0 < cboOu.Items.Count)
                    cboOu.Text = cboOu.Items[0].ToString();

            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg, "Exception" );
                MessageBox.Show( msg, "LoadComboBoxes()", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
        }//end of //end of LoadComboBoxes
        #endregion

        #region Handle Key down and press
        private void cboNatIP_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboNatIP_KeyDown" );
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboDomainName_KeyDown

        private void cboLdapIP_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboLdapIP_KeyDown" );
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }//end of cboMailFrom_KeyDown

        private void cboBase_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboBase_KeyDown" );
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboRcptTo_KeyDown

        private void cboFilter_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboFilter_KeyDown" );
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboMdeFile_KeyDown

        private void cboCn_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboCn_KeyDown" );
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboCn_KeyDown

        private void cboOu_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboOu_KeyDown" );
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }//end of cboOu_KeyDown

        /// <summary>
        /// Handle the auto completion for user input in combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCompleteCombo(object sender, KeyPressEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;//this.ComboBox1
            if(Char.IsControl( e.KeyChar ))
                return;

            String ToFind = cb.Text.Substring( 0, cb.SelectionStart ) + e.KeyChar;
            int index = cb.FindStringExact( ToFind );
            if(index == -1)
                index = cb.FindString( ToFind );

            if(index == -1)
                return;

            cb.SelectedIndex = index;
            cb.SelectionStart = ToFind.Length;
            cb.SelectionLength = cb.Text.Length - cb.SelectionStart;
            e.Handled = true;

        }//end of AutoCompleteCombo

        private void cboNatIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboNatIP_KeyPress" );
            AutoCompleteCombo( sender, e );
        }//end of cboDomainName_KeyPress

        private void cboLdapIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboLdapIP_KeyPress" );
            AutoCompleteCombo( sender, e );
        }//end of cboMailFrom_KeyPress

        private void cboBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboBase_KeyPress" );
            AutoCompleteCombo( sender, e );
        }

        private void cboFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboFilter_KeyPress" );
            AutoCompleteCombo( sender, e );
        }

        private void cboCn_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboCn_KeyPress" );
            AutoCompleteCombo( sender, e );
        }//end of cboCn_KeyPress

        private void cboOu_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine( "UcDSTest.cs - cboOu_KeyPress" );
            AutoCompleteCombo( sender, e );
        }//end of cboOu_KeyPress

        #endregion

        /// <summary>
        /// Initail data:
        /// 1. Load Combo boxes
        /// 2. Validate User Input - under construction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDSTest_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
        }//end of UcDSTest_Load

        private void btnAbort_Click(object sender, EventArgs e)
        {
            try
            {
                OnDisplayMsgInNotepad( new DisplayMsgEventArgs("Abort called. Please wait for the thread terminate!!") );
                if(exec != null)
                    exec.Close();

                if(m_thdMdeCheck != null && m_thdMdeCheck.IsAlive)
                    KillMdeCheckThread();

                lock(this)
                {
                    // reset mouse cursor and enable control
                    BeginInvoke( m_delegateJobDoneNotification, new object[] { m_Total } );
                }
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( "UcDSTest.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                commObj.LogToFile( "UcDSTest.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                MessageBox.Show( ex.Message + "\n" + ex.StackTrace, "Abort Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
        }//end of btnAbort_Click

        private void KillMdeCheckThread()
        {
            try
            {
                commObj.LogToFile( "Kill KillMdeCheckThread Start" );
                m_thdMdeCheck.Abort(); // abort
                m_thdMdeCheck.Join();  // require for ensure the thread kill
            }//end of try 
            catch(ThreadAbortException thdEx)
            {
                Trace.WriteLine( thdEx.Message );
                commObj.LogToFile( "Aborting the Count thread : " + thdEx.Message.ToString() );
            }//end of catch
        }//end of KillMdeCheckThread

        private void lnkMdeLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string logFullPathFileName = commObj.logFullPath + "\\" + m_logFile;
                Debug.WriteLine( logFullPathFileName );
                myMdeDataLogTitle = commObj.ViewLogFromNotepad( logFullPathFileName, myMdeDataLogTitle );
            }//emd of try
            catch(Win32Exception win32Ex)
            {
                Trace.WriteLine( win32Ex.Message + "\n" + win32Ex.GetType().ToString() + win32Ex.StackTrace );
            }//end of catch - win32 exception
        }
    }
}
