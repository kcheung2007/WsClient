using System.Diagnostics;

namespace WsClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                Trace.WriteLine("MainForm.Designer.cs - components deposing");
                components.Dispose();               
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tssLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.vSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftTabCtrl = new System.Windows.Forms.TabControl();
            this.tabData = new System.Windows.Forms.TabPage();
            this.btnGenAddr = new System.Windows.Forms.Button();
            this.btnLidf = new System.Windows.Forms.Button();
            this.btnDXLData = new System.Windows.Forms.Button();
            this.tabMailClient = new System.Windows.Forms.TabPage();
            this.hSplitContainer = new System.Windows.Forms.SplitContainer();
            this.topTabCtrl = new System.Windows.Forms.TabControl();
            this.tabSimpleTest = new System.Windows.Forms.TabPage();
            this.tabArchive = new System.Windows.Forms.TabPage();
            this.tabRetrieve = new System.Windows.Forms.TabPage();
            this.tabZApi = new System.Windows.Forms.TabPage();
            this.tabZDlls = new System.Windows.Forms.TabPage();
            this.tabMailSender = new System.Windows.Forms.TabPage();
            this.tabMailCounter = new System.Windows.Forms.TabPage();
            this.tabPgDSTest = new System.Windows.Forms.TabPage();
            this.tabRemoteCmd = new System.Windows.Forms.TabPage();
            this.botTabCtrl = new System.Windows.Forms.TabControl();
            this.tabMonitor = new System.Windows.Forms.TabPage();
            this.tabNotepad = new System.Windows.Forms.TabPage();
            this.tabExplorer = new System.Windows.Forms.TabPage();
            this.tabSetDate = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer( this.components );
            this.ttpWsClient = new System.Windows.Forms.ToolTip( this.components );
            this.tabToys = new System.Windows.Forms.TabPage();
            this.statusStrip.SuspendLayout();
            this.vSplitContainer.Panel1.SuspendLayout();
            this.vSplitContainer.Panel2.SuspendLayout();
            this.vSplitContainer.SuspendLayout();
            this.leftTabCtrl.SuspendLayout();
            this.tabData.SuspendLayout();
            this.hSplitContainer.Panel1.SuspendLayout();
            this.hSplitContainer.Panel2.SuspendLayout();
            this.hSplitContainer.SuspendLayout();
            this.topTabCtrl.SuspendLayout();
            this.botTabCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tssLabel} );
            this.statusStrip.Location = new System.Drawing.Point( 0, 496 );
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size( 727, 22 );
            this.statusStrip.TabIndex = 0;
            // 
            // tssLabel
            // 
            this.tssLabel.AutoToolTip = true;
            this.tssLabel.Name = "tssLabel";
            this.tssLabel.Size = new System.Drawing.Size( 109, 17 );
            this.tssLabel.Text = "toolStripStatusLabel1";
            this.tssLabel.ToolTipText = "Here is tool tip";
            // 
            // vSplitContainer
            // 
            this.vSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vSplitContainer.Location = new System.Drawing.Point( 0, 0 );
            this.vSplitContainer.Name = "vSplitContainer";
            // 
            // vSplitContainer.Panel1
            // 
            this.vSplitContainer.Panel1.Controls.Add( this.leftTabCtrl );
            // 
            // vSplitContainer.Panel2
            // 
            this.vSplitContainer.Panel2.Controls.Add( this.hSplitContainer );
            this.vSplitContainer.Size = new System.Drawing.Size( 727, 496 );
            this.vSplitContainer.SplitterDistance = 128;
            this.vSplitContainer.TabIndex = 1;
            this.vSplitContainer.Text = "splitContainer1";
            this.vSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler( this.vSplitContainer_SplitterMoved );
            // 
            // leftTabCtrl
            // 
            this.leftTabCtrl.Controls.Add( this.tabData );
            this.leftTabCtrl.Controls.Add( this.tabMailClient );
            this.leftTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftTabCtrl.Location = new System.Drawing.Point( 0, 0 );
            this.leftTabCtrl.Name = "leftTabCtrl";
            this.leftTabCtrl.SelectedIndex = 0;
            this.leftTabCtrl.Size = new System.Drawing.Size( 128, 496 );
            this.leftTabCtrl.TabIndex = 0;
            this.leftTabCtrl.SelectedIndexChanged += new System.EventHandler( this.leftTabCtrl_SelectedIndexChanged );
            // 
            // tabData
            // 
            this.tabData.Controls.Add( this.btnGenAddr );
            this.tabData.Controls.Add( this.btnLidf );
            this.tabData.Controls.Add( this.btnDXLData );
            this.tabData.Location = new System.Drawing.Point( 4, 22 );
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabData.Size = new System.Drawing.Size( 120, 470 );
            this.tabData.TabIndex = 1;
            this.tabData.Text = "Data";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // btnGenAddr
            // 
            this.btnGenAddr.Location = new System.Drawing.Point( 6, 61 );
            this.btnGenAddr.Name = "btnGenAddr";
            this.btnGenAddr.Size = new System.Drawing.Size( 108, 23 );
            this.btnGenAddr.TabIndex = 2;
            this.btnGenAddr.Text = "Addresses";
            this.ttpWsClient.SetToolTip( this.btnGenAddr, "Generate control file for mailing" );
            this.btnGenAddr.UseVisualStyleBackColor = true;
            this.btnGenAddr.Click += new System.EventHandler( this.btnGenAddr_Click );
            // 
            // btnLidf
            // 
            this.btnLidf.Location = new System.Drawing.Point( 6, 32 );
            this.btnLidf.Name = "btnLidf";
            this.btnLidf.Size = new System.Drawing.Size( 108, 23 );
            this.btnLidf.TabIndex = 1;
            this.btnLidf.Text = "LDIF Data";
            this.ttpWsClient.SetToolTip( this.btnLidf, "Generate LDIF File" );
            this.btnLidf.UseVisualStyleBackColor = true;
            this.btnLidf.Click += new System.EventHandler( this.btnLidf_Click );
            // 
            // btnDXLData
            // 
            this.btnDXLData.Location = new System.Drawing.Point( 6, 3 );
            this.btnDXLData.Name = "btnDXLData";
            this.btnDXLData.Size = new System.Drawing.Size( 108, 23 );
            this.btnDXLData.TabIndex = 0;
            this.btnDXLData.Text = "DXL Data";
            this.btnDXLData.UseVisualStyleBackColor = true;
            this.btnDXLData.Click += new System.EventHandler( this.btnDXLData_Click );
            // 
            // tabMailClient
            // 
            this.tabMailClient.Location = new System.Drawing.Point( 4, 22 );
            this.tabMailClient.Name = "tabMailClient";
            this.tabMailClient.Size = new System.Drawing.Size( 120, 470 );
            this.tabMailClient.TabIndex = 2;
            this.tabMailClient.Text = "Mail Client";
            this.tabMailClient.UseVisualStyleBackColor = true;
            // 
            // hSplitContainer
            // 
            this.hSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hSplitContainer.Location = new System.Drawing.Point( 0, 0 );
            this.hSplitContainer.Name = "hSplitContainer";
            this.hSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // hSplitContainer.Panel1
            // 
            this.hSplitContainer.Panel1.Controls.Add( this.topTabCtrl );
            // 
            // hSplitContainer.Panel2
            // 
            this.hSplitContainer.Panel2.Controls.Add( this.botTabCtrl );
            this.hSplitContainer.Size = new System.Drawing.Size( 595, 496 );
            this.hSplitContainer.SplitterDistance = 354;
            this.hSplitContainer.TabIndex = 0;
            this.hSplitContainer.Text = "splitContainer1";
            this.hSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler( this.hSplitContainer_SplitterMoved );
            // 
            // topTabCtrl
            // 
            this.topTabCtrl.Controls.Add( this.tabSimpleTest );
            this.topTabCtrl.Controls.Add( this.tabArchive );
            this.topTabCtrl.Controls.Add( this.tabRetrieve );
            this.topTabCtrl.Controls.Add( this.tabZApi );
            this.topTabCtrl.Controls.Add( this.tabZDlls );
            this.topTabCtrl.Controls.Add( this.tabMailSender );
            this.topTabCtrl.Controls.Add( this.tabMailCounter );
            this.topTabCtrl.Controls.Add( this.tabPgDSTest );
            this.topTabCtrl.Controls.Add( this.tabRemoteCmd );
            this.topTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTabCtrl.Location = new System.Drawing.Point( 0, 0 );
            this.topTabCtrl.Name = "topTabCtrl";
            this.topTabCtrl.SelectedIndex = 0;
            this.topTabCtrl.Size = new System.Drawing.Size( 595, 354 );
            this.topTabCtrl.TabIndex = 0;
            this.topTabCtrl.SelectedIndexChanged += new System.EventHandler( this.topTabCtrl_SelectedIndexChanged );
            // 
            // tabSimpleTest
            // 
            this.tabSimpleTest.Location = new System.Drawing.Point( 4, 22 );
            this.tabSimpleTest.Name = "tabSimpleTest";
            this.tabSimpleTest.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabSimpleTest.Size = new System.Drawing.Size( 587, 328 );
            this.tabSimpleTest.TabIndex = 0;
            this.tabSimpleTest.Text = "Simple Test";
            this.tabSimpleTest.UseVisualStyleBackColor = true;
            // 
            // tabArchive
            // 
            this.tabArchive.Location = new System.Drawing.Point( 4, 22 );
            this.tabArchive.Name = "tabArchive";
            this.tabArchive.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabArchive.Size = new System.Drawing.Size( 587, 328 );
            this.tabArchive.TabIndex = 1;
            this.tabArchive.Text = "Archive Panel";
            this.tabArchive.UseVisualStyleBackColor = true;
            // 
            // tabRetrieve
            // 
            this.tabRetrieve.Location = new System.Drawing.Point( 4, 22 );
            this.tabRetrieve.Name = "tabRetrieve";
            this.tabRetrieve.Size = new System.Drawing.Size( 587, 328 );
            this.tabRetrieve.TabIndex = 2;
            this.tabRetrieve.Text = "Retrieve Panel";
            this.tabRetrieve.UseVisualStyleBackColor = true;
            // 
            // tabZApi
            // 
            this.tabZApi.Location = new System.Drawing.Point( 4, 22 );
            this.tabZApi.Name = "tabZApi";
            this.tabZApi.Size = new System.Drawing.Size( 587, 328 );
            this.tabZApi.TabIndex = 6;
            this.tabZApi.Text = "ZAPIs";
            this.tabZApi.UseVisualStyleBackColor = true;
            // 
            // tabZDlls
            // 
            this.tabZDlls.Location = new System.Drawing.Point( 4, 22 );
            this.tabZDlls.Name = "tabZDlls";
            this.tabZDlls.Size = new System.Drawing.Size( 587, 328 );
            this.tabZDlls.TabIndex = 5;
            this.tabZDlls.Text = "Z. Dlls Test";
            this.tabZDlls.UseVisualStyleBackColor = true;
            // 
            // tabMailSender
            // 
            this.tabMailSender.Location = new System.Drawing.Point( 4, 22 );
            this.tabMailSender.Name = "tabMailSender";
            this.tabMailSender.Size = new System.Drawing.Size( 587, 328 );
            this.tabMailSender.TabIndex = 4;
            this.tabMailSender.Text = "Mail Sender";
            this.tabMailSender.UseVisualStyleBackColor = true;
            // 
            // tabMailCounter
            // 
            this.tabMailCounter.Location = new System.Drawing.Point( 4, 22 );
            this.tabMailCounter.Name = "tabMailCounter";
            this.tabMailCounter.Size = new System.Drawing.Size( 587, 328 );
            this.tabMailCounter.TabIndex = 8;
            this.tabMailCounter.Text = "Counter";
            this.tabMailCounter.UseVisualStyleBackColor = true;
            // 
            // tabPgDSTest
            // 
            this.tabPgDSTest.Location = new System.Drawing.Point( 4, 22 );
            this.tabPgDSTest.Name = "tabPgDSTest";
            this.tabPgDSTest.Size = new System.Drawing.Size( 587, 328 );
            this.tabPgDSTest.TabIndex = 7;
            this.tabPgDSTest.Text = "DS Test";
            this.tabPgDSTest.UseVisualStyleBackColor = true;
            // 
            // tabRemoteCmd
            // 
            this.tabRemoteCmd.Location = new System.Drawing.Point( 4, 22 );
            this.tabRemoteCmd.Name = "tabRemoteCmd";
            this.tabRemoteCmd.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabRemoteCmd.Size = new System.Drawing.Size( 587, 328 );
            this.tabRemoteCmd.TabIndex = 9;
            this.tabRemoteCmd.Text = "Cmd";
            this.ttpWsClient.SetToolTip( this.tabRemoteCmd, "Issue ssh cmd to DS" );
            this.tabRemoteCmd.ToolTipText = "Issue ssh cmd to DS";
            this.tabRemoteCmd.UseVisualStyleBackColor = true;
            // 
            // botTabCtrl
            // 
            this.botTabCtrl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.botTabCtrl.Controls.Add( this.tabMonitor );
            this.botTabCtrl.Controls.Add( this.tabNotepad );
            this.botTabCtrl.Controls.Add( this.tabExplorer );
            this.botTabCtrl.Controls.Add( this.tabSetDate );
            this.botTabCtrl.Controls.Add( this.tabToys );
            this.botTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.botTabCtrl.Location = new System.Drawing.Point( 0, 0 );
            this.botTabCtrl.Name = "botTabCtrl";
            this.botTabCtrl.SelectedIndex = 0;
            this.botTabCtrl.Size = new System.Drawing.Size( 595, 138 );
            this.botTabCtrl.TabIndex = 0;
            // 
            // tabMonitor
            // 
            this.tabMonitor.Location = new System.Drawing.Point( 4, 4 );
            this.tabMonitor.Name = "tabMonitor";
            this.tabMonitor.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabMonitor.Size = new System.Drawing.Size( 587, 112 );
            this.tabMonitor.TabIndex = 0;
            this.tabMonitor.Text = "Monitor";
            this.tabMonitor.ToolTipText = "Monitor Panel";
            this.tabMonitor.UseVisualStyleBackColor = true;
            // 
            // tabNotepad
            // 
            this.tabNotepad.Location = new System.Drawing.Point( 4, 4 );
            this.tabNotepad.Name = "tabNotepad";
            this.tabNotepad.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabNotepad.Size = new System.Drawing.Size( 587, 112 );
            this.tabNotepad.TabIndex = 1;
            this.tabNotepad.Text = "Notepad";
            this.tabNotepad.ToolTipText = "Simple Notepad";
            this.tabNotepad.UseVisualStyleBackColor = true;
            // 
            // tabExplorer
            // 
            this.tabExplorer.Location = new System.Drawing.Point( 4, 4 );
            this.tabExplorer.Name = "tabExplorer";
            this.tabExplorer.Size = new System.Drawing.Size( 587, 112 );
            this.tabExplorer.TabIndex = 2;
            this.tabExplorer.Text = "Explorer";
            this.tabExplorer.UseVisualStyleBackColor = true;
            // 
            // tabSetDate
            // 
            this.tabSetDate.Location = new System.Drawing.Point( 4, 4 );
            this.tabSetDate.Name = "tabSetDate";
            this.tabSetDate.Size = new System.Drawing.Size( 587, 112 );
            this.tabSetDate.TabIndex = 3;
            this.tabSetDate.Text = "Set Date";
            this.tabSetDate.UseVisualStyleBackColor = true;
            // 
            // tabToys
            // 
            this.tabToys.Location = new System.Drawing.Point( 4, 4 );
            this.tabToys.Name = "tabToys";
            this.tabToys.Size = new System.Drawing.Size( 587, 112 );
            this.tabToys.TabIndex = 5;
            this.tabToys.Text = "Toys";
            this.ttpWsClient.SetToolTip( this.tabToys, "Power Toys" );
            this.tabToys.ToolTipText = "Power Toys";
            this.tabToys.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 727, 518 );
            this.Controls.Add( this.vSplitContainer );
            this.Controls.Add( this.statusStrip );
            this.Icon = ((System.Drawing.Icon)(resources.GetObject( "$this.Icon" )));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "WsClient";
            this.Load += new System.EventHandler( this.MainForm_Load );
            this.Layout += new System.Windows.Forms.LayoutEventHandler( this.MainForm_Layout );
            this.ResizeEnd += new System.EventHandler( this.MainForm_ResizeEnd );
            this.statusStrip.ResumeLayout( false );
            this.statusStrip.PerformLayout();
            this.vSplitContainer.Panel1.ResumeLayout( false );
            this.vSplitContainer.Panel2.ResumeLayout( false );
            this.vSplitContainer.ResumeLayout( false );
            this.leftTabCtrl.ResumeLayout( false );
            this.tabData.ResumeLayout( false );
            this.hSplitContainer.Panel1.ResumeLayout( false );
            this.hSplitContainer.Panel2.ResumeLayout( false );
            this.hSplitContainer.ResumeLayout( false );
            this.topTabCtrl.ResumeLayout( false );
            this.botTabCtrl.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer vSplitContainer;
        private System.Windows.Forms.SplitContainer hSplitContainer;
        private System.Windows.Forms.TabControl leftTabCtrl;
        private System.Windows.Forms.TabControl topTabCtrl;
        private System.Windows.Forms.TabControl botTabCtrl;
        private System.Windows.Forms.TabPage tabData;        
        private System.Windows.Forms.TabPage tabSimpleTest;
        private System.Windows.Forms.TabPage tabArchive;        
        private System.Windows.Forms.TabPage tabMonitor;
        private System.Windows.Forms.TabPage tabNotepad;
        private System.Windows.Forms.TabPage tabRetrieve;        
        private System.Windows.Forms.ToolStripStatusLabel tssLabel;
        private System.Windows.Forms.TabPage tabMailClient;
        private System.Windows.Forms.TabPage tabMailSender;
        private System.Windows.Forms.TabPage tabZDlls;
        private System.Windows.Forms.TabPage tabExplorer;
        private System.Windows.Forms.TabPage tabZApi;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnDXLData;
        private System.Windows.Forms.TabPage tabSetDate;
        private System.Windows.Forms.Button btnLidf;
        private System.Windows.Forms.TabPage tabPgDSTest;
        private System.Windows.Forms.TabPage tabMailCounter;
        private System.Windows.Forms.Button btnGenAddr;
        private System.Windows.Forms.ToolTip ttpWsClient;
        private System.Windows.Forms.TabPage tabRemoteCmd;
        private System.Windows.Forms.TabPage tabToys;
    }
}

