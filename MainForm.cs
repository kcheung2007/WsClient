using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WsClient
{
    public partial class MainForm : Form
    {
        // Right top tab control
        public WsClient.UcMonitor pgMonitor;
        private WsClient.UcSimpleTest   pgSimpleTest;        
        private WsClient.UcExplorer     pgExplorer;
        private WsClient.UcNotepad      pgNotepad;
        private WsClient.UcArchive      pgArchive;
        private WsClient.UcRetrieve     pgRetrieve;
        private WsClient.UcZAPIs        pgZApi;
        private WsClient.UcMailSender   pgMailSender;
        private WsClient.UcZDlls        pgZDlls;
        private WsClient.UcSetEmlDate   pgSetEmlDate;
        private WsClient.UcDSTest       pgDSTest;
        private WsClient.UcMailCounter  pgMailCounter;
        private WsClient.UcSshCmd       pgSshCmd;
        private WsClient.UcPowerTool    pgPowerTool;
        
        #region save for future reference - messaging between controls
        // private WsClient.UcWebSearch ucWebSearch = new WsClient.UcWebSearch();        
        // private System.Windows.Forms.TabPage tabSearchTest = new System.Windows.Forms.TabPage();
        #endregion

        private int defaultWidth;
        private int defaultHeight;
        private int defaultSpliterWidth;
//        private int defaultSpliterHeight;

        // Left tab control
        private WsClient.UcMailClient   pgBatchMail;

        public MainForm()
        {
            // Tabbed Page Order in MainForm.Designer.cs topTabCtrl section
            Debug.WriteLine("MainForm.cs - Constructor");
            InitializeComponent();
            try
            {
                // SplashScreen.ShowSplashScreen();
                #region Right bottom Tab Control
                pgMonitor = new UcMonitor();
                pgMonitor.Dock = DockStyle.Fill;
                this.tabMonitor.Controls.Add( pgMonitor );
                //pgMonitor.statusChanged += new UpdateStatusEventHandler( pgMonitor_statusChanged );
                pgMonitor.statusChanged += new EventHandler<StatusEventArgs>( pgMonitor_statusChanged );

                pgExplorer = new UcExplorer();
                pgExplorer.Dock = DockStyle.Fill;
                this.tabExplorer.Controls.Add( pgExplorer );

                pgNotepad = new UcNotepad();
                pgNotepad.Dock = DockStyle.Fill;
                this.tabNotepad.Controls.Add( pgNotepad );

                pgSetEmlDate = new UcSetEmlDate();
                pgSetEmlDate.Dock = DockStyle.Fill;
                this.tabSetDate.Controls.Add( pgSetEmlDate );

                pgPowerTool = new UcPowerTool();
                pgPowerTool.Dock = DockStyle.Fill;
                this.tabToys.Controls.Add( pgPowerTool );
                
                #endregion

                #region Right Top Tab Control
                pgSimpleTest = new UcSimpleTest();
                pgSimpleTest.Dock = DockStyle.Fill;
                this.tabSimpleTest.Controls.Add(pgSimpleTest);
                pgSimpleTest.statusChanged += new EventHandler<StatusEventArgs>( pgSimpleTest_statusChanged );

                pgArchive = new UcArchive();
                pgArchive.Dock = DockStyle.Fill;
                this.tabArchive.Controls.Add(pgArchive);
                pgArchive.statusChanged += new EventHandler<StatusEventArgs>( pgArchive_statusChanged );

                pgRetrieve = new UcRetrieve();
                pgRetrieve.Dock = DockStyle.Fill;
                this.tabRetrieve.Controls.Add(pgRetrieve);
                pgRetrieve.statusChanged += new EventHandler<StatusEventArgs>( pgRetrieve_statusChanged );

                pgZApi = new UcZAPIs();
                pgZApi.Dock = DockStyle.Fill;
                this.tabZApi.Controls.Add(pgZApi);
                
                pgZDlls = new UcZDlls();
                pgZDlls.Dock = DockStyle.Fill;
                this.tabZDlls.Controls.Add(pgZDlls);
                pgZDlls.statusChanged += new EventHandler<StatusEventArgs>( pgZDlls_statusChanged );

                pgMailSender = new UcMailSender();
                pgMailSender.Dock = DockStyle.Fill;
                this.tabMailSender.Controls.Add(pgMailSender);
                pgMailSender.statusChanged += new EventHandler<StatusEventArgs>( pgMailSender_statusChanged );

                pgMailCounter = new UcMailCounter();
                pgMailCounter.Dock = DockStyle.Fill;
                this.tabMailCounter.Controls.Add( pgMailCounter );
                pgMailCounter.statusChanged += new EventHandler<StatusEventArgs>( pgMailCounter_statusChanged );

                pgDSTest = new UcDSTest();
                pgDSTest.Dock = DockStyle.Fill;
                this.tabPgDSTest.Controls.Add( pgDSTest );
                pgDSTest.statusChanged += new EventHandler<StatusEventArgs>( pgDSTest_statusChanged );
                pgDSTest.displayMsgChanged += new EventHandler<DisplayMsgEventArgs>( OnNotepad_displayMsgChanged );

                pgSshCmd = new UcSshCmd();
                pgSshCmd.Dock = DockStyle.Fill;
                this.tabRemoteCmd.Controls.Add( pgSshCmd );
                pgSshCmd.statusChanged += new EventHandler<StatusEventArgs>( pgSshCmd_statusChanged );
                pgSshCmd.displayMsgChanged += new EventHandler<DisplayMsgEventArgs>( OnNotepad_displayMsgChanged );

                #endregion


                #region Save for future reference
                // Save for future reference - send message to other control
                //pgDSTest.Controls[0].Controls.Add( tabSearchTest );
                //tabSearchTest.Controls.Add( this.ucWebSearch );
                //tabSearchTest.Location = new System.Drawing.Point( 4, 4 );
                //tabSearchTest.Name = "tabSearchTest";
                //tabSearchTest.Padding = new System.Windows.Forms.Padding( 3 );
                //tabSearchTest.Size = new System.Drawing.Size( 556, 315 );
                //tabSearchTest.TabIndex = 1;
                //tabSearchTest.Text = "Search";
                //tabSearchTest.ToolTipText = "DS Web Search";
                //tabSearchTest.UseVisualStyleBackColor = true;

                //ucWebSearch.Dock = System.Windows.Forms.DockStyle.Top;
                //ucWebSearch.Location = new System.Drawing.Point( 3, 3 );
                //ucWebSearch.Name = "ucWebSearch";
                //ucWebSearch.Size = new System.Drawing.Size( 550, 256 );
                //ucWebSearch.TabIndex = 0;
                //ucWebSearch.displayMsgChanged += new EventHandler<DisplayMsgEventArgs>( pgDSTest_displayMsgChanged );
                #endregion

                #region Left Hand-side Tab Control
                pgBatchMail = new UcMailClient();
                pgBatchMail.DefaultWidth  = pgBatchMail.Size.Width;
                pgBatchMail.DefaultHeight = pgBatchMail.Size.Height;
                pgBatchMail.Dock = DockStyle.Fill;
                this.tabMailClient.Controls.Add(pgBatchMail);
                pgBatchMail.statusChanged += new EventHandler<StatusEventArgs>( pgBatchMail_statusChanged );
                #endregion

                //tssLabel.Text = WsClient.Program.appSetting.WsServer_URL;
                tssLabel.Text = "Ready";

                defaultWidth = this.Width;
                defaultHeight = this.Height;
                defaultSpliterWidth = vSplitContainer.SplitterDistance;

                Debug.WriteLine( "Default Width = " + defaultWidth + " : Default height = " + defaultHeight );

            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show(msg, "MainForm - Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }
        }

        void OnNotepad_displayMsgChanged(object sender, DisplayMsgEventArgs e)
        {
            // Need to cast to TextBox in order to set the SelectionStart
            TextBox tb = (TextBox)pgNotepad.Controls[0]; // controls[0] is the notepad control
            tb.Text = e.strMsg;
            tb.SelectionStart = tb.Text.Length;
            tb.ScrollToCaret();
            tb.Refresh();

            //pgNotepad.Controls[0].Text = e.strMsg; // controls[0] is the notepad control
            //pgNotepad.Controls[0].Refresh();
            //throw new NotImplementedException();
        }//end of OnNotepad_displayMsgChanged

        public void UpdateStatusBar( string msg )
        {
            tssLabel.Text = msg;            
        }//end of UpdateStatusBar

        #region Status Change EVENT from user control
        private void pgSimpleTest_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("MainForm.cs - pgSimpleTest_statusChanged");
            tssLabel.Text = e.strMsg;
        }

        private void pgMonitor_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("MainForm.cs - pgMonitor_statusChanged");
            tssLabel.Text = e.strMsg;
        }

        private void pgBatchMail_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("MainForm.cs - pgBatchMail_statusChanged");
            tssLabel.Text = e.strMsg;
        }

        private void pgZDlls_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("MainForm.cs - pgZDlls_statusChanged");
            tssLabel.Text = e.strMsg;
        }

        private void pgArchive_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("MainForm.cs - pgArchive_statusChanged");
            tssLabel.Text = e.strMsg;
        }
        private void pgRetrieve_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("MainForm.cs - pgRetrieve_statusChanged");
            tssLabel.Text = e.strMsg;
        }//end of pgRetrieve_statusChanged

        private void pgMailSender_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("MainForm.cs - pgMailSender_statusChanged");
            tssLabel.Text = e.strMsg;
        }//end of pgMailSender_statusChanged

        private void pgDSTest_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine( "MainForm.cs - pgDSTest_statusChanged" );
            tssLabel.Text = e.strMsg;
            this.Refresh();
        }//end of pgDSTest_statusChanged

        private void pgMailCounter_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine( "MainForm.cs - pgMailCounter_statusChanged" );
            tssLabel.Text = e.strMsg;
        }
        private void pgClient_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("MainForm.cs - pgClient_statusChanged");
            tssLabel.Text = e.strMsg;
        }//end of pgClient_statusChanged

        private void pgSshCmd_statusChanged(object sender, StatusEventArgs e)
        {
            Debug.WriteLine( "MainForm.cs - pgRemoteCmd_statusChanged" );
            tssLabel.Text = e.strMsg;
        }//end of pgRemoteCmd_statusChanged
        #endregion

        private void topTabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("MainForm.cs - topTabCtrl_SelectedIndexChanged");
            UpdateStatusBar(topTabCtrl.SelectedTab.Text);

            switch(topTabCtrl.SelectedIndex)
            {
                case 7: // DS Test tab
                case 8: // SSH Command tab
                    botTabCtrl.SelectedTab = tabNotepad;
                    break;
                //default:
                //    botTabCtrl.SelectedTab = tabMonitor;
                //    break;
            }//end of switch
        }//end of topTabCtrl_SelectedIndexChanged

        private void leftTabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("MainForm.cs - leftTabCtrl_SelectedIndexChanged");
            UpdateStatusBar(leftTabCtrl.SelectedTab.Text);

            switch (leftTabCtrl.SelectedIndex) // kill the instance here ??
            {
                case 0: // First Page
                    {
                        vSplitContainer.SplitterDistance = defaultSpliterWidth;// 150;
                    }
                    break;
                case 1: // Batch Mail Page
                    {
                        vSplitContainer.SplitterDistance = pgBatchMail.DefaultWidth;
                    }
                    break;
            }// end of switch - Selected Index		
        }//end of leftTabCtrl_SelectedIndexChanged

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("MainForm.cs - MainForm_Load");
                this.Text = Application.ExecutablePath;

                SplashScreen.CloseForm();
                this.Activate();
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show( msg, "MainForm - Load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }

        }//end of MainForm_Load

        private void vSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            // Debug.WriteLine("MainForm.cs - vSplitContainer_SplitterMoved");
            // Debug.WriteLine("\t vSplitContainer.SplitterDistance = " + vSplitContainer.SplitterDistance);

            if (defaultWidth <= this.Width)
            {
                switch (leftTabCtrl.SelectedIndex) // kill the instance here ??
                {
                    case 0: // First Page
                        {
                            vSplitContainer.SplitterDistance = defaultSpliterWidth;//150;
                        }
                        break;
                    case 1: // Batch Mail Page
                        {
                            //Debug.WriteLine("MainForm.cs - vSplitContainer.SplitterDistance " + vSplitContainer.SplitterDistance);
                            if (vSplitContainer.SplitterDistance < pgBatchMail.DefaultWidth)
                                vSplitContainer.SplitterDistance = pgBatchMail.DefaultWidth;
                        }
                        break;
                }// end of switch - Selected Index
            }

        }//end of vSplitContainer_SplitterMoved

        private void hSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (defaultHeight <= this.Height)
            {
                switch (leftTabCtrl.SelectedIndex) // kill the instance here ??
                {
                    case 0: // First Page
                        {
                            //hSplitContainer.SplitterDistance = 150;
                        }
                        break;
                    case 1: // Batch Mail Page
                        {
                            // Debug.WriteLine("MainForm.cs - hSplitContainer.SplitterDistance " + hSplitContainer.SplitterDistance);
                            //if (hSplitContainer.SplitterDistance < pgBatchMail.DefaultHeight)
                            //    hSplitContainer.SplitterDistance = pgBatchMail.DefaultHeight;
                        }
                        break;
                }// end of switch - Selected Index
            }
        }//end of hSplitContainer_SplitterMoved

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            Debug.WriteLine("MainForm.cs - MainForm_ResizeEnd");
            this.Width = this.Width < defaultWidth ? defaultWidth : this.Width;
            //if (this.Size.Width < defaultWidth)
            //    this.Size.Width = defaultWidth;

            if (this.Height < defaultHeight)
                this.Height = defaultHeight;
        }//end of MainForm_ResizeEnd

        private void DoSplash()
        {
            Debug.WriteLine("MainForm.cs - DoSplash");
        }//end of DoSplash

        private void MainForm_Layout(object sender, LayoutEventArgs e)
        {
            Debug.WriteLine("MainForm.cs - MainForm_Layout");
            Debug.WriteLine("\t " + e.AffectedControl.Name);
            SplashScreen.SetStatus( "Loading " + e.AffectedControl.Name);
        }//end of MainForm_Layout

        private void btnDXLData_Click(object sender, EventArgs e)
        {
            WinGenDXLData genDXLData = new WinGenDXLData();
            //DialogResult rv = genDXLData.ShowDialog();
            genDXLData.ShowDialog();
        }//end of btnDXLData_Click

        private void btnLidf_Click(object sender, EventArgs e)
        {
            WinGenLdifData genLdifData = new WinGenLdifData();
            //DialogResult rv = genLdifData.ShowDialog();
            genLdifData.ShowDialog();
        }//end of btnLidf_Click

        private void btnGenAddr_Click(object sender, EventArgs e)
        {

        }

        

    }//end of class - MainForm

    // Custom Event - send notification to main form to update the status bar
    public delegate void EventHandler<StatusEventArgs>(Object sender, StatusEventArgs eArg);
    public class StatusEventArgs : EventArgs
    {
        private string _msg = "";
        public StatusEventArgs(string msg)
        {
            _msg = msg;
        }

        public string strMsg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
            }
        }//end of msg
    }//end of class - StatusEventArgs

    /// <summary>
    /// Custom event - Display message in Notepad
    /// </summary>
    public class DisplayMsgEventArgs : EventArgs
    {
        private string _msg;
        public DisplayMsgEventArgs(string msg)
        {
            _msg = msg;
        }

        public string strMsg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
            }
        }//end of msg
    }//end of class - DisplayMsgEventArgs 

}