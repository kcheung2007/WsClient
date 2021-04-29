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

// Morgan Stanley Record
//# 101, people, test organization
//dn: msfwid=101, ou=people,o=testdomain2
//msfwid: 101
//employeenumber: 00001
//departmentnumber: B098
//cn: AJWWTB
//uid: AJWWTB
//mail: tagfrom@exchange
//maildrop: tagfrom.maildrop@exchange
//mailalternateaddress;int: tagfrom.maaint@exchange
//mailalternateaddress;int: Sophie.Temple@nuukiemail.com
//mailalternateaddress;int: iskjefqkqh@eyeideas.com
//mailalternateaddress;ext: tagfrom.maaext@exchange
//msbloombergid: AJWWTB
//mssuperdivisioncode: 10100
//msdivisioncode: 94000
//c: FRA
//msccdesc: FOR TESTING

// DB record
//# 102, people, test organization
//dn: dbdirid=102, ou=people,o=testdomain1
//dbdirid: 102
//employeenumber: 00002
//departmentnumber: 8760
//cn: TailWaggingOffer
//uid: TailWaggingOffer
//mailalternateaddress;int: TailWaggingOffer@mx1754.gg02.com
//mailalternateaddress;int: lcaddoh@iname.com
//mailalternateaddress;int: skyegreeram@bemused.com
//mailalternateaddress;ext: vikings_1fan@yahoo.com
//dbbloombergid: TailWaggingOffer
//mssuperdivisioncode: 10100
//msdivisioncode: 94210
//retentioncode: GBR

namespace WsClient
{
    public partial class WinGenLdifData : Form
    {
        private int m_recordCnt;
        private Thread m_thdGenData;
        private delegate void DelegateJobDoneNotification(int count); // all thread done - indicate Thread index
        private DelegateJobDoneNotification m_delegateJobDoneNotification;
        private delegate void DelegateUpdate_txtDisplay(int recCnt);
        private DelegateUpdate_txtDisplay m_delegateUpdateDisplay;
        private CommObj commObj = new CommObj();
        private RandomPassword randPwd = new RandomPassword();
        private string fileName;
        private string domainName;
        private string dnName;
        private decimal numRec;
        private static string SPECIAL_CHARS1 = "~!@#$%^&*()_+=-[]}{;:/.,<>?";
        private static string SPECIAL_CHARS2 = "~!#$%^&*()+=[]}{;:/,<>?";

        private const int TIMER_INTERVAL = 100;

        public WinGenLdifData()
        {
            InitializeComponent();
            m_delegateJobDoneNotification = new DelegateJobDoneNotification( JobDoneHandler );
            m_delegateUpdateDisplay = new DelegateUpdate_txtDisplay( Update_txtDisplay );
            
            timer1.Interval = TIMER_INTERVAL;            
        }

        public void JobDoneHandler(int count)
        {
            Debug.WriteLine( "WinGenLdifData.cs - +++++++ JobDoneHandler ++++++++" );

            EnableControls( true );

            string thdName = m_thdGenData == null ? "" : m_thdGenData.Name;
            string msg = "Thread " + thdName + "Done\r\n"
                + "Total LDIF Record Generate: " + count;

            txtDisplay.Text = msg;
            commObj.LogToFile( "ldifLog.txt", msg );
        }//end of JobDoneHandler

        private void Update_txtDisplay(int recCnt)
        {
            txtDisplay.Text = "LDIF Record Count: " + recCnt;
            txtDisplay.Refresh();

        }//end of Update_txtDisplay

        private void EnableControls(bool bValue)
        {
            cboDomain.Enabled = bValue;
            btnDoIt.Enabled = bValue;
            //btnAbort.Enabled = !bValue;
            lnkOutFile.Enabled = bValue;
            txtOutFile.Enabled = bValue;
        }//end of EnableCOntrols
        
        /// <summary>
        /// After validation - everything is good
        /// Initial the XML generation variables.
        /// </summary>
        private void InitUserData()
        {
            fileName = txtOutFile.Text;
            domainName = cboDomain.Text;
            dnName = cboDnValue.Text;
            numRec = nudRecord.Value;
        }//end of InitUserData

        /// <summary>
        /// Make sure user input ok before generate the data.
        /// Only validate the input xml template and output xml data text box.
        /// </summary>
        /// <returns>true - OK; false - fail</returns>
        private bool ValidateUserInput()
        {
            bool rv = true; // assume everything is OK
            if(txtOutFile.Text == "")
            {
                rv = false;
                txtOutFile.BackColor = Color.YellowGreen;
            }
            else
            if(cboDomain.Text == "")     
            {                        
                rv = false;
                cboDomain.BackColor = Color.YellowGreen;                    
            }
            return (rv);
        }//end of ValidateUserInput

        private void btnDoIt_Click(object sender, EventArgs e)
        {
            try
            {
                if(!ValidateUserInput())
                    return;

                InitUserData();
                EnableControls( false ); // disable control - but not the abort button

                timer1.Start();

                m_thdGenData = new Thread( new ThreadStart( this.Thd_GenLdapData ) );
                m_thdGenData.Name = "Thd_GenLdapData";
                m_thdGenData.Start();

                commObj.LogToFile( "ldapLog.txt", "++ Thd_GenLdapData Start ++" );
            }
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message );
                commObj.LogToFile( "Data Generation thread Error : " + ex.Message.ToString() );
            }
        }//end of btnDoIt_Click

        private void Thd_GenLdapData()
        {
            Random autoRand = new Random();
            CommObj commObj = new CommObj();

            try
            {
                using(StreamWriter sw = File.CreateText( fileName ))
                {
                    //sw.WriteLine( "This is my file." );
                    //sw.WriteLine( "I can write ints {0} or floats {1}, and so on.", 1, 4.2 );

                    sw.WriteLine("dn: o=" + domainName);
                    sw.WriteLine("objectclass: top");
                    sw.WriteLine("objectclass: organization");
                    sw.WriteLine("o:" + domainName);
                    sw.WriteLine();
                    
                    sw.WriteLine("dn: ou=Groups,o=" + domainName);
                    sw.WriteLine("objectclass: top");
                    sw.WriteLine("objectclass: organizationalUnit");
                    sw.WriteLine("ou: Groups");
                    sw.WriteLine();

                    sw.WriteLine("dn: ou=people,o=" + domainName);
                    sw.WriteLine("objectclass: top");
                    sw.WriteLine("objectclass: organizationalUnit");
                    sw.WriteLine("ou: people");
                    sw.WriteLine();

                    sw.WriteLine("dn: ou=Aliases,o=" + domainName);
                    sw.WriteLine("objectclass: top");
                    sw.WriteLine("objectclass: organizationalUnit");
                    sw.WriteLine("ou: Aliases");
                    sw.WriteLine();

                    sw.WriteLine( "dn: ou=ISGaliases,ou=Aliases,o=" + domainName );
                    sw.WriteLine("objectclass: top");
                    sw.WriteLine("objectclass: organizationalUnit");
                    sw.WriteLine("ou: ISGaliases");
                    sw.WriteLine();

                    sw.WriteLine("dn: ou=Mail Routing,ou=Aliases,o=" + domainName);
                    sw.WriteLine("objectclass: top");
                    sw.WriteLine("objectclass: organizationalUnit");
                    sw.WriteLine("ou: Mail Routing");
                    sw.WriteLine();

                    sw.WriteLine("dn: ou=prodids,o=" + domainName);
                    sw.WriteLine("objectclass: top");
                    sw.WriteLine("objectclass: organizationalUnit");
                    sw.WriteLine("ou: prodids");
                    sw.WriteLine();

                    for(m_recordCnt = 0; m_recordCnt < (int)nudRecord.Value; m_recordCnt++)
                    {
                        //Debug.WriteLine( "record count: " + m_recordCnt );
                        //dn: msfwid=101, ou=people,o=testdomain2
                        sw.WriteLine( "dn: " + dnName + "=" + m_recordCnt + ", ou=people, o=" + domainName );
                        sw.WriteLine( dnName + ": " + m_recordCnt );
                        sw.WriteLine( "employeenumber: " + m_recordCnt );
                        sw.WriteLine( "departmentnumber: " + autoRand.Next().ToString() );
                        sw.WriteLine( "cn: " + RandomPassword.Generate( 6 )) ;
                        sw.WriteLine( "uid: " + RandomPassword.Generate( 6 ) );
                        sw.WriteLine( "mail: " + "E" + m_recordCnt.ToString() + "@zantaz.com" ); // trim begining and end only. Cannot remove the middle one
                        sw.WriteLine( "mailalternateaddress;int: " + commObj.TrimString( RandomPassword.Generate( 15 ).Trim( SPECIAL_CHARS1.ToCharArray() ), SPECIAL_CHARS2) + "@" + "abc.zantaz.com" );
                        sw.WriteLine( "mailalternateaddress;int: " + commObj.TrimString( RandomPassword.Generate( 15 ).Trim( SPECIAL_CHARS1.ToCharArray() ), SPECIAL_CHARS2 ) + "@" + "XyZ.zantaz.com" );
                        sw.WriteLine( "mailalternateaddress;int: " + commObj.TrimString( RandomPassword.Generate( 15 ).Trim( SPECIAL_CHARS1.ToCharArray() ), SPECIAL_CHARS2 ) + "@" + "QA.zantaz.com" );
                        sw.WriteLine( "mailalternateaddress;ext: " + commObj.TrimString( RandomPassword.Generate( 15 ).Trim( SPECIAL_CHARS1.ToCharArray() ), SPECIAL_CHARS2 ) + "@" + "123.zantaz.com" );
                        sw.WriteLine( "msbloombergid: " + RandomPassword.Generate( 6 ).ToUpper());
                        sw.WriteLine( "dbbloombergid: " + RandomPassword.Generate( 6 ).ToUpper() );
                        sw.WriteLine( "mssuperdivisioncode: " + autoRand.Next( 10000, 99999 ) );
                        sw.WriteLine( "msdivisioncode: " + autoRand.Next( 10000, 99999 ) );
                        sw.WriteLine( "c: " + RandomPassword.Generate( 5 ).ToUpper() );
                        sw.WriteLine( "retentioncode: " + RandomPassword.Generate( 5 ).ToUpper() );
                        sw.WriteLine( "msccdesc: FOR TESTING" );
                        sw.WriteLine();

                        //lock(this)
                        //{
                        //    BeginInvoke( m_delegateUpdateDisplay, new object[] { m_recordCnt } );
                        //}
                    }//end of for

                    sw.Close();                 
                }//end of steam writer
                timer1.Stop();
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message );
            }//end of catch
            finally
            {
                lock(this)
                {
                    BeginInvoke( m_delegateJobDoneNotification, new object[] { m_recordCnt } );
                }
            }//end of finally
        }//end of Thd_GenLdapData

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Trace.WriteLine( "WinGenLdifData.cs - btnAbort_Click" );
            try
            {
                txtDisplay.Text = "Abort called. Please wait for the thread terminate!!";
                if(m_thdGenData != null && m_thdGenData.IsAlive)
                    KillGenDataThread();

                lock(this)
                {
                    // reset mouse cursor and enable control
                    BeginInvoke( m_delegateJobDoneNotification, new object[] { m_recordCnt } );
                }
                timer1.Stop();
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( "WinGenLdifData.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                commObj.LogToFile( "WinGenLdifData.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                MessageBox.Show( ex.Message + "\n" + ex.StackTrace, "Abort Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch               
            finally
            {
                timer1.Stop();
            }
        }//end of btnAbort_Click

        private void KillGenDataThread()
        {
            Trace.WriteLine( "WinGenLdifData.cs - KillGenDataThread()" );
            try
            {
                commObj.LogToFile( "ldapLog.txt", "Kill KillGenDataThread Start" );
                timer1.Stop(); // close or abort will come here
                m_thdGenData.Abort(); // abort
                m_thdGenData.Join();  // require for ensure the thread kill
            }//end of try 
            catch(ThreadAbortException thdEx)
            {
                Trace.WriteLine( thdEx.Message );
                commObj.LogToFile( "Aborting the Count thread : " + thdEx.Message.ToString() );
            }//end of catch				
        }//end of KillGenDataThread

        private void timer1_Tick(object sender, EventArgs e)
        {
            lock(this)
            {
                BeginInvoke( m_delegateUpdateDisplay, new object[] { m_recordCnt } );
            }
        }        
    }
}
