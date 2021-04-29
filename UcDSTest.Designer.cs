namespace WsClient
{
    partial class UcDSTest
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
            if(disposing && (components != null))
            {
                if(m_thdMdeCheck != null && m_thdMdeCheck.IsAlive)
                {
                    this.KillMdeCheckThread();
                    commObj.LogToFile( "Thread.log", "   KillMdeCheckThread Killed" );
                }
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dsTabCtrl = new System.Windows.Forms.TabControl();
            this.tabPgSearchTest = new System.Windows.Forms.TabPage();
            this.ucWebSearch = new WsClient.UcWebSearch();
            this.tabPgLdapTest = new System.Windows.Forms.TabPage();
            this.cboOu = new System.Windows.Forms.ComboBox();
            this.cboCn = new System.Windows.Forms.ComboBox();
            this.lblou = new System.Windows.Forms.Label();
            this.lblcn = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.cboBase = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblBase = new System.Windows.Forms.Label();
            this.lblLdap = new System.Windows.Forms.Label();
            this.cboLdapIP = new System.Windows.Forms.ComboBox();
            this.cboNatIP = new System.Windows.Forms.ComboBox();
            this.lblNAT = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblFail = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lnkInFile = new System.Windows.Forms.LinkLabel();
            this.txtInFile = new System.Windows.Forms.TextBox();
            this.ttpDSTest = new System.Windows.Forms.ToolTip( this.components );
            this.lnkMdeLog = new System.Windows.Forms.LinkLabel();
            this.dsTabCtrl.SuspendLayout();
            this.tabPgSearchTest.SuspendLayout();
            this.tabPgLdapTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // dsTabCtrl
            // 
            this.dsTabCtrl.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.dsTabCtrl.Controls.Add( this.tabPgSearchTest );
            this.dsTabCtrl.Controls.Add( this.tabPgLdapTest );
            this.dsTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dsTabCtrl.Location = new System.Drawing.Point( 0, 0 );
            this.dsTabCtrl.Multiline = true;
            this.dsTabCtrl.Name = "dsTabCtrl";
            this.dsTabCtrl.SelectedIndex = 0;
            this.dsTabCtrl.Size = new System.Drawing.Size( 583, 323 );
            this.dsTabCtrl.TabIndex = 0;
            this.ttpDSTest.SetToolTip( this.dsTabCtrl, "Whatever" );
            // 
            // tabPgSearchTest
            // 
            this.tabPgSearchTest.Controls.Add( this.ucWebSearch );
            this.tabPgSearchTest.Location = new System.Drawing.Point( 4, 4 );
            this.tabPgSearchTest.Name = "tabPgSearchTest";
            this.tabPgSearchTest.Size = new System.Drawing.Size( 556, 315 );
            this.tabPgSearchTest.TabIndex = 1;
            this.tabPgSearchTest.Text = "Search";
            this.tabPgSearchTest.UseVisualStyleBackColor = true;
            // 
            // ucWebSearch
            // 
            this.ucWebSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucWebSearch.Location = new System.Drawing.Point( 0, 0 );
            this.ucWebSearch.Name = "ucWebSearch";
            this.ucWebSearch.Size = new System.Drawing.Size( 556, 315 );
            this.ucWebSearch.TabIndex = 0;
            // 
            // tabPgLdapTest
            // 
            this.tabPgLdapTest.Controls.Add( this.lnkMdeLog );
            this.tabPgLdapTest.Controls.Add( this.cboOu );
            this.tabPgLdapTest.Controls.Add( this.cboCn );
            this.tabPgLdapTest.Controls.Add( this.lblou );
            this.tabPgLdapTest.Controls.Add( this.lblcn );
            this.tabPgLdapTest.Controls.Add( this.btnTest );
            this.tabPgLdapTest.Controls.Add( this.btnAbort );
            this.tabPgLdapTest.Controls.Add( this.btnRun );
            this.tabPgLdapTest.Controls.Add( this.cboFilter );
            this.tabPgLdapTest.Controls.Add( this.cboBase );
            this.tabPgLdapTest.Controls.Add( this.lblFilter );
            this.tabPgLdapTest.Controls.Add( this.lblBase );
            this.tabPgLdapTest.Controls.Add( this.lblLdap );
            this.tabPgLdapTest.Controls.Add( this.cboLdapIP );
            this.tabPgLdapTest.Controls.Add( this.cboNatIP );
            this.tabPgLdapTest.Controls.Add( this.lblNAT );
            this.tabPgLdapTest.Controls.Add( this.lblTotal );
            this.tabPgLdapTest.Controls.Add( this.lblFail );
            this.tabPgLdapTest.Controls.Add( this.lblPass );
            this.tabPgLdapTest.Controls.Add( this.txtTotal );
            this.tabPgLdapTest.Controls.Add( this.txtFail );
            this.tabPgLdapTest.Controls.Add( this.txtPass );
            this.tabPgLdapTest.Controls.Add( this.lnkInFile );
            this.tabPgLdapTest.Controls.Add( this.txtInFile );
            this.tabPgLdapTest.Location = new System.Drawing.Point( 4, 4 );
            this.tabPgLdapTest.Name = "tabPgLdapTest";
            this.tabPgLdapTest.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPgLdapTest.Size = new System.Drawing.Size( 556, 315 );
            this.tabPgLdapTest.TabIndex = 0;
            this.tabPgLdapTest.Text = "LDAP";
            this.ttpDSTest.SetToolTip( this.tabPgLdapTest, "LDAP Test" );
            this.tabPgLdapTest.ToolTipText = "LDAP Test ???";
            this.tabPgLdapTest.UseVisualStyleBackColor = true;
            // 
            // cboOu
            // 
            this.cboOu.FormattingEnabled = true;
            this.cboOu.Location = new System.Drawing.Point( 232, 52 );
            this.cboOu.Name = "cboOu";
            this.cboOu.Size = new System.Drawing.Size( 149, 21 );
            this.cboOu.TabIndex = 22;
            this.ttpDSTest.SetToolTip( this.cboOu, "eg:zusers\r\ncn=Admin,ou=zusers,o=testdomain1" );
            this.cboOu.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboOu_KeyPress );
            this.cboOu.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboOu_KeyDown );
            // 
            // cboCn
            // 
            this.cboCn.FormattingEnabled = true;
            this.cboCn.Location = new System.Drawing.Point( 232, 29 );
            this.cboCn.Name = "cboCn";
            this.cboCn.Size = new System.Drawing.Size( 149, 21 );
            this.cboCn.TabIndex = 21;
            this.ttpDSTest.SetToolTip( this.cboCn, "eg:Admin\r\ncn=Admin,ou=zusers,o=testdomain1" );
            this.cboCn.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboCn_KeyPress );
            this.cboCn.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboCn_KeyDown );
            // 
            // lblou
            // 
            this.lblou.AutoSize = true;
            this.lblou.Location = new System.Drawing.Point( 209, 55 );
            this.lblou.Name = "lblou";
            this.lblou.Size = new System.Drawing.Size( 19, 13 );
            this.lblou.TabIndex = 20;
            this.lblou.Text = "ou";
            this.ttpDSTest.SetToolTip( this.lblou, "eg: <o=testdomain1>" );
            // 
            // lblcn
            // 
            this.lblcn.AutoSize = true;
            this.lblcn.Location = new System.Drawing.Point( 209, 32 );
            this.lblcn.Name = "lblcn";
            this.lblcn.Size = new System.Drawing.Size( 19, 13 );
            this.lblcn.TabIndex = 19;
            this.lblcn.Text = "cn";
            this.ttpDSTest.SetToolTip( this.lblcn, "eg: <o=testdomain1>" );
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point( 470, 5 );
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size( 75, 21 );
            this.btnTest.TabIndex = 18;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler( this.btnTest_Click );
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point( 470, 108 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 75, 21 );
            this.btnAbort.TabIndex = 9;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point( 470, 84 );
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size( 75, 21 );
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler( this.btnRun_Click );
            // 
            // cboFilter
            // 
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point( 48, 50 );
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size( 149, 21 );
            this.cboFilter.TabIndex = 17;
            this.ttpDSTest.SetToolTip( this.cboFilter, "eg: mail=E111@autonome.com" );
            this.cboFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboFilter_KeyPress );
            this.cboFilter.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboFilter_KeyDown );
            // 
            // cboBase
            // 
            this.cboBase.FormattingEnabled = true;
            this.cboBase.Location = new System.Drawing.Point( 232, 6 );
            this.cboBase.Name = "cboBase";
            this.cboBase.Size = new System.Drawing.Size( 149, 21 );
            this.cboBase.TabIndex = 16;
            this.ttpDSTest.SetToolTip( this.cboBase, "eg:testdomain1\r\ncn=Admin,ou=zusers,o=testdomain1" );
            this.cboBase.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboBase_KeyPress );
            this.cboBase.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboBase_KeyDown );
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point( 16, 54 );
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size( 29, 13 );
            this.lblFilter.TabIndex = 15;
            this.lblFilter.Text = "Filter";
            this.ttpDSTest.SetToolTip( this.lblFilter, "mail=E0000@autonome.com" );
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new System.Drawing.Point( 197, 9 );
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size( 31, 13 );
            this.lblBase.TabIndex = 14;
            this.lblBase.Text = "Base";
            this.ttpDSTest.SetToolTip( this.lblBase, "eg: <o=testdomain1>" );
            // 
            // lblLdap
            // 
            this.lblLdap.AutoSize = true;
            this.lblLdap.Location = new System.Drawing.Point( 10, 31 );
            this.lblLdap.Name = "lblLdap";
            this.lblLdap.Size = new System.Drawing.Size( 35, 13 );
            this.lblLdap.TabIndex = 13;
            this.lblLdap.Text = "LDAP";
            this.ttpDSTest.SetToolTip( this.lblLdap, "Front NAT" );
            // 
            // cboLdapIP
            // 
            this.cboLdapIP.FormattingEnabled = true;
            this.cboLdapIP.Location = new System.Drawing.Point( 48, 28 );
            this.cboLdapIP.Name = "cboLdapIP";
            this.cboLdapIP.Size = new System.Drawing.Size( 149, 21 );
            this.cboLdapIP.TabIndex = 12;
            this.ttpDSTest.SetToolTip( this.cboLdapIP, "Identity server IP" );
            this.cboLdapIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboLdapIP_KeyPress );
            this.cboLdapIP.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboLdapIP_KeyDown );
            // 
            // cboNatIP
            // 
            this.cboNatIP.FormattingEnabled = true;
            this.cboNatIP.Location = new System.Drawing.Point( 48, 6 );
            this.cboNatIP.Name = "cboNatIP";
            this.cboNatIP.Size = new System.Drawing.Size( 149, 21 );
            this.cboNatIP.TabIndex = 11;
            this.ttpDSTest.SetToolTip( this.cboNatIP, "Front NAT IP address" );
            this.cboNatIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboNatIP_KeyPress );
            this.cboNatIP.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboNatIP_KeyDown );
            // 
            // lblNAT
            // 
            this.lblNAT.AutoSize = true;
            this.lblNAT.Location = new System.Drawing.Point( 16, 9 );
            this.lblNAT.Name = "lblNAT";
            this.lblNAT.Size = new System.Drawing.Size( 29, 13 );
            this.lblNAT.TabIndex = 10;
            this.lblNAT.Text = "NAT";
            this.ttpDSTest.SetToolTip( this.lblNAT, "Front NAT" );
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTotal.Location = new System.Drawing.Point( 268, 113 );
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size( 34, 13 );
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total:";
            // 
            // lblFail
            // 
            this.lblFail.AutoSize = true;
            this.lblFail.Location = new System.Drawing.Point( 143, 113 );
            this.lblFail.Name = "lblFail";
            this.lblFail.Size = new System.Drawing.Size( 26, 13 );
            this.lblFail.TabIndex = 6;
            this.lblFail.Text = "Fail:";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point( 12, 113 );
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size( 33, 13 );
            this.lblPass.TabIndex = 5;
            this.lblPass.Text = "Pass:";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point( 306, 109 );
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size( 75, 20 );
            this.txtTotal.TabIndex = 4;
            this.txtTotal.Text = "0";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtFail
            // 
            this.txtFail.Location = new System.Drawing.Point( 173, 109 );
            this.txtFail.Name = "txtFail";
            this.txtFail.ReadOnly = true;
            this.txtFail.Size = new System.Drawing.Size( 75, 20 );
            this.txtFail.TabIndex = 3;
            this.txtFail.Text = "0";
            this.txtFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point( 48, 109 );
            this.txtPass.Name = "txtPass";
            this.txtPass.ReadOnly = true;
            this.txtPass.Size = new System.Drawing.Size( 75, 20 );
            this.txtPass.TabIndex = 2;
            this.txtPass.Text = "0";
            this.txtPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lnkInFile
            // 
            this.lnkInFile.AutoSize = true;
            this.lnkInFile.Location = new System.Drawing.Point( 10, 88 );
            this.lnkInFile.Name = "lnkInFile";
            this.lnkInFile.Size = new System.Drawing.Size( 35, 13 );
            this.lnkInFile.TabIndex = 0;
            this.lnkInFile.TabStop = true;
            this.lnkInFile.Text = "In File";
            this.ttpDSTest.SetToolTip( this.lnkInFile, "Result file from SC MDE" );
            this.lnkInFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkInFile_LinkClicked );
            // 
            // txtInFile
            // 
            this.txtInFile.Location = new System.Drawing.Point( 48, 85 );
            this.txtInFile.Name = "txtInFile";
            this.txtInFile.Size = new System.Drawing.Size( 333, 20 );
            this.txtInFile.TabIndex = 1;
            this.ttpDSTest.SetToolTip( this.txtInFile, "Point to the extraced MDE info file" );
            // 
            // lnkMdeLog
            // 
            this.lnkMdeLog.AutoSize = true;
            this.lnkMdeLog.Location = new System.Drawing.Point( 393, 113 );
            this.lnkMdeLog.Name = "lnkMdeLog";
            this.lnkMdeLog.Size = new System.Drawing.Size( 64, 13 );
            this.lnkMdeLog.TabIndex = 23;
            this.lnkMdeLog.TabStop = true;
            this.lnkMdeLog.Text = "mdeData.txt";
            this.lnkMdeLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkMdeLog_LinkClicked );
            // 
            // UcDSTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.dsTabCtrl );
            this.Name = "UcDSTest";
            this.Size = new System.Drawing.Size( 583, 323 );
            this.Load += new System.EventHandler( this.UcDSTest_Load );
            this.dsTabCtrl.ResumeLayout( false );
            this.tabPgSearchTest.ResumeLayout( false );
            this.tabPgLdapTest.ResumeLayout( false );
            this.tabPgLdapTest.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TabControl dsTabCtrl;
        private System.Windows.Forms.TabPage tabPgLdapTest;
        private System.Windows.Forms.ToolTip ttpDSTest;
        private System.Windows.Forms.TextBox txtInFile;
        private System.Windows.Forms.LinkLabel lnkInFile;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblFail;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.ComboBox cboNatIP;
        private System.Windows.Forms.Label lblNAT;
        private System.Windows.Forms.Label lblLdap;
        private System.Windows.Forms.ComboBox cboLdapIP;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.ComboBox cboBase;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TabPage tabPgSearchTest;
        private UcWebSearch ucWebSearch;
        private System.Windows.Forms.ComboBox cboOu;
        private System.Windows.Forms.ComboBox cboCn;
        private System.Windows.Forms.Label lblou;
        private System.Windows.Forms.Label lblcn;
        private System.Windows.Forms.LinkLabel lnkMdeLog;
    }
}
