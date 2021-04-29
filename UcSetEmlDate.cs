using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WsClient
{
    public partial class UcSetEmlDate : UserControl
    {
        private FilesListObj m_filesListObj = null; // remember to initialize it..
        private int numModified = 0;
        private Thread m_ThdObj;

        //private delegate void DelegateUpdate_txtModified(int numModified);
        //private DelegateUpdate_txtModified m_delegateNumModifiedCtrl;

        public UcSetEmlDate()
        {
            InitializeComponent();
            //m_delegateNumModifiedCtrl = new DelegateUpdate_txtModified( Update_txtModified );
        }

        private void Update_txtModified(int num)
        {
            txtModified.Text = numModified.ToString();
        }//end of Update_txtNumMail

        private void lnkSource_Click(object sender, EventArgs e)
        {
            Debug.WriteLine( "UcSetEmlDate.cs - lnkSource_Click" );
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();

            if(txtSource.Text != null)
                fbDlg.SelectedPath = txtSource.Text;  // set the default folder

            if(fbDlg.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = fbDlg.SelectedPath;
            }
        }//end of lnkSource_Click

        private void lnkTarget_Click(object sender, EventArgs e)
        {
            Debug.WriteLine( "UcSetEmlDate.cs - lnkTarget_Click" );
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();

            if(txtTarget.Text != null)
                fbDlg.SelectedPath = txtTarget.Text;  // set the default folder

            if(fbDlg.ShowDialog() == DialogResult.OK)
            {
                txtTarget.Text = fbDlg.SelectedPath;
            }
        }//end of lnkSource_Click

        private void Thd_SetEmlDate()
        {
            m_filesListObj = new FilesListObj( txtSource.Text ); // get a list of file name
            if(m_filesListObj.numFile <= 0)
            {
                MessageBox.Show( "No File in this directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
                txtSource.Focus();
                txtSource.BackColor = Color.YellowGreen;
                return; // exit
            }// end of if

            txtTotal.Text = m_filesListObj.numFile.ToString();
            for(int i = 0; i < m_filesListObj.numFile; i++)
            {
                Debug.WriteLine( "\tbtnDoit_Click - file name: " + m_filesListObj.fullFileName );
                numModified += ModifyMsgDate( m_filesListObj.fullFileName );
                m_filesListObj.idxFile++; // move to next one
                //IAsyncResult r = BeginInvoke( m_delegateNumModifiedCtrl, new object[] { numModified } );
                txtModified.Text = numModified.ToString();
                txtModified.Refresh();
            }//end of for

        }//end of Thd_SetEmlDate

        private void btnDoit_Click(object sender, EventArgs e)
        {
            if(!ValidateUserInput())
                return;

            m_ThdObj = new Thread( new ThreadStart( this.Thd_SetEmlDate ) );
            m_ThdObj.Name = "Thd_SetEmlDate";
            m_ThdObj.Start();

        }//end of btnDoit_Click

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Trace.WriteLine( "UcSetEmlDate.cs - btnAbort_Click" );
            try
            {
                if(m_ThdObj != null && m_ThdObj.IsAlive)
                    KillSetEmlDateThread();
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( "UcSetEmlDate.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                MessageBox.Show( ex.Message + "\n" + ex.StackTrace, "Abort Exception" );
            }//end of catch               
        }//end of btnAbort_Click

        private void KillSetEmlDateThread()
        {
            Trace.WriteLine( "UcSetEmlDate.cs - KillSetEmlDate()" );
            try
            {
                m_ThdObj.Abort(); // abort
                m_ThdObj.Join();  // require for ensure the thread kill
            }//end of try 
            catch(ThreadAbortException thdEx)
            {
                Trace.WriteLine( thdEx.Message );
            }//end of catch				
        }//end of KillSetEmlDateThread

        /// <summary>
        /// Validate user input
        /// </summary>
        /// <returns>bool: OK - true; Fail - false</returns>
        private bool ValidateUserInput()
        {
            bool rv = true; // assume everything is OK
            if( (txtSource.Text == "") || (!Directory.Exists( txtSource.Text )) )
            {
                txtSource.Focus();
                txtSource.BackColor = Color.YellowGreen;
                rv = false;
            }//end of if - check txtFolder
            else
                if(txtTarget.Text == "" || (!Directory.Exists( txtSource.Text )))
                {
                    txtTarget.Focus();
                    txtTarget.BackColor = Color.YellowGreen;
                    rv = false;
                }//end of if
            return (rv);
        }//end of ValidateUserInput

        private int ModifyMsgDate(string fn)
        {
            int rv = 0;
            string origFile = fn + "_ORIG";

            // Rename to a new file name FileName_ORIG.
            FileInfo fi = new FileInfo( fn );
            string origFileName = fi.Name;

            if(File.Exists( txtTarget.Text + "\\" + origFileName ))
                File.Delete( txtTarget.Text + "\\" + origFileName );
            fi.MoveTo( origFile );

            // Read line by line and replace the Date line.
            // Write back to the original name
            string line = "";
            StreamReader sr = new StreamReader( origFile );
            StreamWriter sw = File.CreateText( txtTarget.Text + "\\" + origFileName );
            while((line = sr.ReadLine()) != null)
            {
                // Debug.WriteLine( "ModifyMsgDate: " + line );
                if(!line.StartsWith( "Date" ))
                    sw.WriteLine( line );
                else
                {
                    sw.WriteLine( "Date: " + dateTimePicker1.Value.ToLocalTime().ToString( "R" ) );
                    rv = 1;
                }
            }//end of while
            sr.Close();
            sw.Close();

            return (rv);
        }


    }//end of partial class - UcSetEmlDate
}
