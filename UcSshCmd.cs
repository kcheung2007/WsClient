using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using AdvancedDataGridView;
using Tamir.SharpSsh;


namespace WsClient
{
    public partial class UcSshCmd : UserControl
    {
        private FilesListObj fListObj;
        private TreeGridNode node;

        public event EventHandler<StatusEventArgs> statusChanged;
        public event EventHandler<DisplayMsgEventArgs> displayMsgChanged;

        public UcSshCmd()
        {
            InitializeComponent();
        }

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

        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        /// 
        string m_xmlFileName = "InitCmdFile.xml";
        private void SaveComboBoxItem()
        {
            Debug.WriteLine( "UcSshCmd.cs - SaveComboBoxItem" );
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\" + m_xmlFileName;
                if(!File.Exists( cboPath ))
                {
                    using(StreamWriter sw = File.CreateText( cboPath ))
                    {
                    }//end of using - for auto close etc... yes... empty
                }

                // Save the combox
                tw = new XmlTextWriter( Application.StartupPath + "\\" + m_xmlFileName, System.Text.Encoding.UTF8 );

                Debug.WriteLine( "\t ComboBox Item file" + Application.StartupPath + "\\" + m_xmlFileName );

                tw.WriteStartDocument();
                tw.WriteStartElement( "InitData" );

                //The order is important - match with InitMailSender.xml and switch case in load combo box
                WriteComboBoxEntries( cboTCFolder, "cboCmdFile", cboTCFolder.Text, tw ); //nodeList.Item(0)
                //WriteComboBoxEntries( cboMDrive, "cboMDrive", cboMDrive.Text, tw ); //nodeList.Item(1)

                tw.WriteEndElement();
            }//end of try
            catch(IOException ioEx)
            {
                string msg = ioEx.Message + "\n" + ioEx.GetType().ToString() + ioEx.StackTrace;
                Debug.WriteLine( msg, "SaveComboBoxItem()" );
            }
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
            Debug.WriteLine( "UcSshCmd.cs - WriteComboBoxEntries" );
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
            Debug.WriteLine( "UcSshCmd.cs - LoadComboBoxes" );
            try
            {
                cboTCFolder.Items.Clear();
                //cboMDrive.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\" + m_xmlFileName;
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
                        case "cboCmdFile":
                            for(x = 0; x < nodeList.Item( 0 ).ChildNodes.Count; ++x)
                            {
                                cboTCFolder.Items.Add( nodeList.Item( 0 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        //case "cboMDrive":
                        //    for(x = 0; x < nodeList.Item( 1 ).ChildNodes.Count; ++x)
                        //    {
                        //        cboMDrive.Items.Add( nodeList.Item( 1 ).ChildNodes.Item( x ).InnerText );
                        //    }
                        //    break;
                    }//end of switch
                }//end of for

                if(0 < cboTCFolder.Items.Count)
                    cboTCFolder.Text = cboTCFolder.Items[0].ToString();
                //if(0 < cboMDrive.Items.Count)
                //    cboMDrive.Text = cboMDrive.Items[0].ToString();
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
        private void cboCmdFile_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboCmdFile_KeyPress(object sender, KeyPressEventArgs e)
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


        private void lnkTCFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Debug.WriteLine( "UcSshCmd - lnkTCFolder_LinkClicked" );
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            
            if(!String.IsNullOrEmpty( cboTCFolder.Text ))
            {
                if(Directory.Exists( cboTCFolder.Text ))
                    fbDlg.SelectedPath = cboTCFolder.Text;  // set the default folder
            }

            if(fbDlg.ShowDialog() == DialogResult.OK)
            {
                cboTCFolder.Text = fbDlg.SelectedPath;
            }

            Debug.WriteLine( "\t - lnkTCFolder_LinkClicked" );
            LoadTestCase( cboTCFolder.Text );
        }//end of lnkCmdFile_LinkClicked

        /// <summary>
        /// Load Test Case from test case command file to tree view grid
        /// TO DO: Define Test case command file
        /// </summary>
        /// <param name="tcFile"></param>
        private void LoadTestCase(string tcFolder)
        {
            Debug.WriteLine( "TC Folder: " + tcFolder );
            fListObj = new FilesListObj( tcFolder );
            
            Font boldFont = new Font( treeGridView.DefaultCellStyle.Font, FontStyle.Bold );
            for(int i = 0; i < fListObj.numFile; i++)
            {
                node = treeGridView.Nodes.Add( fListObj.fullFileName[i], "Col 2", "col 3", "col 4", "col 5" );
                node.DefaultCellStyle.Font = boldFont;
            }//end of for

            int numCol = treeGridView.ColumnCount;

            
            //TreeGridNode node = treeGridView.Nodes.Add( "Col 1", "Col 2", "col 3", "col 4", "col 5" );
            //node.ImageIndex = 0;
            node.DefaultCellStyle.Font = boldFont;

            node = node.Nodes.Add( null, "Re: Using DataView filter when binding to DataGridView", "tab", @"10/19/2005 1:02 AM", @"10/19/2005 1:02 AM" );
            ////node.ImageIndex = 1;
        }//end of LoadTestCase

        private void btnRun_Click(object sender, EventArgs e)
        {
            DisplayMsgEventArgs dMsgArg = new DisplayMsgEventArgs( "This message display in notepad" );

            //OnDisplayMsgInNotepad( new DisplayMsgEventArgs( "This message display in notepad") );
            OnDisplayMsgInNotepad( dMsgArg );
            OnUpdateStatusBar( new StatusEventArgs( "This is test message." ) );

            try
            {
                //SshExec exec = new SshExec( "10.1.41.99", "db2udb", "fastdb" );
                SshExec exec = new SshExec( cboNatIP.Text, "root", txtPassword.Text );
                
                //if(input.Pass != null) exec.Password = input.Pass;
                // exec.Password = "skyline";
                //if(input.IdentityFile != null) exec.AddIdentityFile( input.IdentityFile );

                Debug.Write( "Connecting..." );
                dMsgArg.strMsg = "Connecting...\r\n";
                OnDisplayMsgInNotepad( dMsgArg );
                exec.Connect();
                Debug.WriteLine( "OK" );
                dMsgArg.strMsg += "OK...\r\n";
                OnDisplayMsgInNotepad( dMsgArg );

                //string output = exec.RunCommand( "//root/DQTest//qXMBeanInfo.sh" );
                //Debug.WriteLine( output );
                //output = output.Replace( "\n", "\r\n" );
                //Debug.WriteLine( output );
                //dMsgArg.strMsg += "//root/DQTest//qXMBeanInfo.sh" + output.ToString();
                //OnDisplayMsgInNotepad( dMsgArg );
                
                //SAVED for future
                //string output = exec.RunCommand( "ssh 10.0.98.2 \"//opt//bin//viewCloudState --verbose\"" );
                //string output = exec.RunCommand( "ssh db2udb@10.0.99.1 \"KentAuto//auditQueryInfo.sh 040000011fa95aa59500000030487951fa0000011fa95aa59500000000\"" );

                string output = exec.RunCommand( "test.sh" );

                Debug.WriteLine( output );
                output = output.Replace( "\n", "\r\n" );
                Debug.WriteLine( output );
                dMsgArg.strMsg += "return message: " + output.ToString();
                OnDisplayMsgInNotepad( dMsgArg );
                //rtbDisplay.Text += output;

                //output = exec.RunCommand( "./helloWorld.sh PARAMMMMMMA" );
                //Debug.WriteLine( output );
                //rtbDisplay.Text += output;

                //output = exec.RunCommand( "ls -ltr" );
                //rtbDisplay.Text += output;

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
            }
        }//end of btnRun_Click

        private void btnAbort_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if(!String.IsNullOrEmpty( cboTCFolder.Text ))
                {
                    if(Directory.Exists( cboTCFolder.Text ))
                    {
                        node = null;
                        treeGridView.Rows.Clear();
                        LoadTestCase( cboTCFolder.Text );
                    }
                }
                else
                    cboTCFolder.BackColor = Color.GreenYellow;
            }//end of try
            catch(IOException ioex)
            {
                string msg = ioex.Message + "\n" + ioex.GetType().ToString() + ioex.StackTrace;
                MessageBox.Show( msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
            
        }

    }
}
