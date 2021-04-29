using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

namespace WsClient
{
    public partial class WinGenDXLData : Form
    {
        private string m_inNsfFile; // Full Path Name
        private string m_outDxlFolder;
        private string m_NsfInfo;
        private string m_password;
        private int m_dxlCount;
        private Thread m_thdGenData;

        private delegate void DelegateJobDoneNotification(int count); // all thread done - indicate Thread index
        private DelegateJobDoneNotification m_delegateJobDoneNotification;

        private delegate void DelegateUpdate_rtbDisplay(int dxlCount);
        private DelegateUpdate_rtbDisplay m_delegateUpdateDisplay;

        private CommObj commObj = new CommObj();

        public WinGenDXLData()
        {
            InitializeComponent();
            m_delegateJobDoneNotification = new DelegateJobDoneNotification( JobDoneHandler );
            m_delegateUpdateDisplay = new DelegateUpdate_rtbDisplay( Update_rtbDisplay );

            LoadComboBoxes();
        }

        public void JobDoneHandler(int count)
        {
            Debug.WriteLine( "WinGenDXLData.cs - +++++++ JobDoneHandler ++++++++" );

            EnableControls( true );

            string thdName = m_thdGenData == null ? "" : m_thdGenData.Name;
            string msg = "Thread " + thdName + "Done\r\n"
                + "Total DXL File Generate: " + count;

            rtbDisplay.Text = msg;
            commObj.LogToFile("dxlLog.txt", msg );
        }//end of JobDoneHandler

        private void Update_rtbDisplay(int dxlCount)
        {
            rtbDisplay.Text = m_NsfInfo + "\r\nDXL Message Count " + dxlCount;
        }//end of Update_txtDisplay

        private void EnableControls( bool bValue )
        {
            cboInNsfFile.Enabled = bValue;
            cboOutFolder.Enabled = bValue;
            lnkNSFFile.Enabled = bValue;
            lnkOutFolder.Enabled = bValue;
            btnCreate.Enabled = bValue;
            lblPassword.Enabled = bValue;
            txtPassword.Enabled = bValue;
        }//end of EnableCOntrols

        /// <summary>
        /// After validation - everything is good
        /// Initial the XML generation variables.
        /// </summary>
        private void InitUserData()
        {
            m_dxlCount = 0; // reset
            m_inNsfFile = cboInNsfFile.Text;
            cboInNsfFile.BackColor = TextBox.DefaultBackColor;

            m_outDxlFolder = cboOutFolder.Text;
            cboOutFolder.BackColor = TextBox.DefaultBackColor;

            m_password = txtPassword.Text;
            txtPassword.BackColor = TextBox.DefaultBackColor;
        }//end of InitUserData

        /// <summary>
        /// Make sure user input ok before generate the data.
        /// Only validate the input xml template and output xml data text box.
        /// </summary>
        /// <returns>true - OK; false - fail</returns>
        private bool ValidateUserInput()
        {
            bool rv = true; // assume everything is OK
            if(cboInNsfFile.Text == "")
            {
                rv = false;
                cboInNsfFile.BackColor = Color.YellowGreen;
            }
            else
                if(!File.Exists( cboInNsfFile.Text ))
                {
                    rv = false;
                    cboInNsfFile.BackColor = Color.YellowGreen;
                    rtbDisplay.Text = "FILE DOES NOT EXISTS";
                }
                else
                    if(cboOutFolder.Text == "")
                    {
                        rv = false;
                        cboOutFolder.BackColor = Color.YellowGreen;
                    }
                    else
                        if(txtPassword.Text == "")
                        {
                            rv = false;
                            txtPassword.BackColor = Color.YellowGreen;
                        }

            return (rv);
        }//end of ValidateUserInput

        /// <summary>
        /// Generate the DXL data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if(!ValidateUserInput())
                    return;
                InitUserData();
                EnableControls( false ); // disable control

                m_thdGenData = new Thread( new ThreadStart( this.Thd_GenDxlData ) );
                m_thdGenData.Name = "Thd_GenDxlData";
                m_thdGenData.Start();

                commObj.LogToFile( "dxlLog.txt", "++ Thd_GenDxlData Start ++" );
            }
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message );
                commObj.LogToFile( "Data Generation thread Error : " + ex.Message.ToString() );
            }
            // Do the job
        }//end of btnCreate_Click

        private void Thd_GenDxlData()
        {
            Domino.NotesSession domSession;
            Domino.NotesDatabase notesDB;
            Domino.NotesDbDirectory notesDBDir;
            Domino.NotesDXLExporter dxlExporter;
            Domino.NotesDocumentCollection docCollect;
            Domino.NotesStream notesStream;

            try
            {
                domSession = new Domino.NotesSession();
                domSession.Initialize( m_password );
                notesDBDir = domSession.GetDbDirectory( "" );
                //notesDB = notesDBDir.OpenDatabase( "c:\\tmp\\ua0021.nsf", false );
                notesDB = notesDBDir.OpenDatabase( m_inNsfFile, false );
                if(!notesDB.IsOpen)
                {
                    Debug.WriteLine( "DB Open fail" );
                    commObj.WriteLineByLine( "dxlLog.txt", "DB Open fail" );
                    return;
                }

                m_NsfInfo = "DB Title: " + notesDB.Title;
                docCollect = notesDB.AllDocuments;
                m_NsfInfo += "\r\nDoc count: " + docCollect.Count;
                Debug.WriteLine( m_NsfInfo );
                commObj.LogToFile( "dxlLog.txt", m_NsfInfo );

                Domino.NotesDocument notesDoc;
                string outFileName;
                int i = docCollect.Count;
                for(m_dxlCount = 0; m_dxlCount < i; m_dxlCount++)
                {
                    notesDoc = docCollect.GetNthDocument( m_dxlCount );
                    outFileName = m_outDxlFolder + "\\notesDXL_" + m_dxlCount + ".xml";
                    notesStream = domSession.CreateStream();
                    //if(notesStream.Open( outFileName, "ASCII" ))
                    if(notesStream.Open( outFileName, "UTF-8" ))                    
                    {
                        notesStream.Truncate(); // erased any existing file
                        dxlExporter = domSession.CreateDXLExporter();
                        commObj.LogToFile( "dxlLog.txt", "Exported " + notesStream.WriteText( dxlExporter.Export( notesDoc ), Domino.EOL_TYPE.EOL_NONE ) );
                    }//end of if
                    notesStream.Close();

                    lock(this)
                    {
                        IAsyncResult rm = BeginInvoke( m_delegateUpdateDisplay, new object[] { m_dxlCount } );
                    }
                }//end of for
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message );
                MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
            finally
            {
                lock(this)
                {
                    BeginInvoke( m_delegateJobDoneNotification, new object[] { m_dxlCount } );
                }
            }//end of finally
        }//end of Thd_GenDxlData

        /// <summary>
        /// Abort and clean up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbort_Click(object sender, EventArgs e)
        {
            Trace.WriteLine( "WinGenDXLData.cs - btnAbort_Click" );
            try
            {
                if(m_thdGenData != null && m_thdGenData.IsAlive)
                    KillGenDataThread();

                // reset mouse cursor and enable control
                BeginInvoke( m_delegateJobDoneNotification, new object[] { -99 } );
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( "WinGenDXLData.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                commObj.LogToFile( "WinGenDXLData.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                MessageBox.Show( ex.Message + "\n" + ex.StackTrace, "Abort Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch               
        }//end of btnAbort_Click

        private void KillGenDataThread()
        {
            Trace.WriteLine( "WinGenDXLData.cs - KillGenDataThread()" );
            try
            {
                commObj.LogToFile( "dxlLog.txt", "Kill KillGenDataThread Start" );
                m_thdGenData.Abort(); // abort
                m_thdGenData.Join();  // require for ensure the thread kill
            }//end of try 
            catch(ThreadAbortException thdEx)
            {
                Trace.WriteLine( thdEx.Message );
                commObj.LogToFile( "Aborting the Count thread : " + thdEx.Message.ToString() );
            }//end of catch				
        }//end of KillGenDataThread

        /// <summary>
        /// Point to the input NSF file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkNSFFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenFileDialog ofDlg = new OpenFileDialog();
                ofDlg.ShowReadOnly = true;
                ofDlg.RestoreDirectory = true;
                if(ofDlg.ShowDialog() == DialogResult.OK)
                {
                    cboInNsfFile.Text = ofDlg.FileName;
                }//end of if
            }
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }
        }//end of lnkNSFFile_LinkClicked

        /// <summary>
        /// Output DXL per document location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkOutFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                cboOutFolder.Text = folderBrowserDlg.SelectedPath;
            }//end of if
        }

        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine( "WinGenDXLData.cs - SaveComboBoxItem" );
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\InitDxlData.xml";
                if(!File.Exists( cboPath ))
                {
                    using(StreamWriter sw = File.CreateText( cboPath ))
                    {
                    }//end of using - for auto close etc... yes... empty
                }

                // Save the combox
                tw = new XmlTextWriter( Application.StartupPath + "\\InitDxlData.xml", System.Text.Encoding.UTF8 );

                Debug.WriteLine( "\t ComboBox Item file" + Application.StartupPath + "\\InitDxlData.xml" );

                tw.WriteStartDocument();
                tw.WriteStartElement( "InitData" );

                //The order is important - match with InitMailSender.xml and switch case in load combo box
                WriteComboBoxEntries( cboInNsfFile, "cboInNsfFile", cboInNsfFile.Text, tw ); //nodeList.Item(0)
                WriteComboBoxEntries( cboOutFolder, "cboOutFolder", cboOutFolder.Text, tw ); //nodeList.Item(1)
                // Text Box but not combo box
                // WriteTextBoxEntries(txtInputFile, "txtInputFile", txtInputFile.Text, tw);
                // WriteComboBoxEntries(txtMailFolder, "txtFolder", txtMailFolder.Text, tw);

                tw.WriteEndElement();
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg, "SaveComboBoxItem()" );
            }//end of catch
            finally
            {
                if(tw != null)
                {
                    tw.Flush();
                    tw.Close();
                }
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
            Debug.WriteLine( "WinGenDXLData.cs - WriteComboBoxEntries" );
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
            Debug.WriteLine( "WinGenDXLData.cs - LoadComboBoxes" );
            try
            {
                cboInNsfFile.Items.Clear();
                cboOutFolder.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\InitDxlData.xml";
                if(!File.Exists( cboPath ))
                {
                    //File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load( cboPath );
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                int numComboBox = nodeList.Count; // count text box also
                int x;
                for(int i = 0; i < numComboBox; i++) // Order is important here
                {
                    switch(nodeList.Item( i ).Attributes.GetNamedItem( "name" ).InnerText)
                    {
                        case "cboInNsfFile":
                            for(x = 0; x < nodeList.Item( 0 ).ChildNodes.Count; ++x)
                            {
                                cboInNsfFile.Items.Add( nodeList.Item( 0 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboOutFolder":
                            for(x = 0; x < nodeList.Item( 1 ).ChildNodes.Count; ++x)
                            {
                                cboOutFolder.Items.Add( nodeList.Item( 1 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                    }//end of switch
                }//end of for

                if(0 < cboInNsfFile.Items.Count)
                    cboInNsfFile.Text = cboInNsfFile.Items[0].ToString();
                if(0 < cboOutFolder.Items.Count)
                    cboOutFolder.Text = cboOutFolder.Items[0].ToString();
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg, "Exception" );
                MessageBox.Show( msg, "LoadComboBoxes()", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
        }//end of //end of LoadComboBoxes
        #endregion

        #region Handle Key Down and Press
        private void cboInNsfFile_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboOutFolder_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboInNsfFile_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboOutFolder_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }
        
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
        #endregion
    }
}