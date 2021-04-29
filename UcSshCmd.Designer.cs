namespace WsClient
{
    partial class UcSshCmd
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
            this.lnkTCFolder = new System.Windows.Forms.LinkLabel();
            this.ttpSshCmd = new System.Windows.Forms.ToolTip( this.components );
            this.cboTCFolder = new System.Windows.Forms.ComboBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboNatIP = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTotalTC = new System.Windows.Forms.TextBox();
            this.lblTotalTC = new System.Windows.Forms.Label();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblTotalEml = new System.Windows.Forms.Label();
            this.lblFail = new System.Windows.Forms.Label();
            this.txtTotalEml = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.btnAbort = new System.Windows.Forms.Button();
            this.treeGridView = new AdvancedDataGridView.TreeGridView();
            this.colTestCase = new AdvancedDataGridView.TreeGridColumn();
            this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNAT = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkTCFolder
            // 
            this.lnkTCFolder.AutoSize = true;
            this.lnkTCFolder.Location = new System.Drawing.Point( 4, 6 );
            this.lnkTCFolder.Name = "lnkTCFolder";
            this.lnkTCFolder.Size = new System.Drawing.Size( 40, 13 );
            this.lnkTCFolder.TabIndex = 0;
            this.lnkTCFolder.TabStop = true;
            this.lnkTCFolder.Text = "TC File";
            this.ttpSshCmd.SetToolTip( this.lnkTCFolder, "Browser TC Folder which contains a list of TC cmd files" );
            this.lnkTCFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lnkTCFolder_LinkClicked );
            // 
            // cboTCFolder
            // 
            this.cboTCFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTCFolder.BackColor = System.Drawing.SystemColors.Info;
            this.cboTCFolder.FormattingEnabled = true;
            this.cboTCFolder.Location = new System.Drawing.Point( 46, 2 );
            this.cboTCFolder.Name = "cboTCFolder";
            this.cboTCFolder.Size = new System.Drawing.Size( 324, 21 );
            this.cboTCFolder.TabIndex = 6;
            this.ttpSshCmd.SetToolTip( this.cboTCFolder, "Command File\r\nHit Enter to save current value" );
            this.cboTCFolder.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.cboCmdFile_KeyPress );
            this.cboTCFolder.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cboCmdFile_KeyDown );
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point( 608, 4 );
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size( 57, 22 );
            this.btnRun.TabIndex = 13;
            this.btnRun.Text = "Run";
            this.ttpSshCmd.SetToolTip( this.btnRun, "Execute cmd file" );
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler( this.btnRun_Click );
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point( 374, 1 );
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size( 29, 23 );
            this.btnRefresh.TabIndex = 16;
            this.ttpSshCmd.SetToolTip( this.btnRefresh, "Refresh Test Case Info" );
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler( this.btnRefresh_Click );
            // 
            // cboNatIP
            // 
            this.cboNatIP.FormattingEnabled = true;
            this.cboNatIP.Items.AddRange( new object[] {
            "10.1.41.51"} );
            this.cboNatIP.Location = new System.Drawing.Point( 46, 27 );
            this.cboNatIP.Name = "cboNatIP";
            this.cboNatIP.Size = new System.Drawing.Size( 111, 21 );
            this.cboNatIP.TabIndex = 19;
            this.cboNatIP.Text = "10.1.41.51";
            this.ttpSshCmd.SetToolTip( this.cboNatIP, "Front NAT IP address" );
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add( this.txtTotalTC );
            this.groupBox1.Controls.Add( this.lblTotalTC );
            this.groupBox1.Controls.Add( this.txtFail );
            this.groupBox1.Controls.Add( this.txtPass );
            this.groupBox1.Controls.Add( this.lblTotalEml );
            this.groupBox1.Controls.Add( this.lblFail );
            this.groupBox1.Controls.Add( this.txtTotalEml );
            this.groupBox1.Controls.Add( this.lblPass );
            this.groupBox1.Location = new System.Drawing.Point( 410, -4 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 192, 56 );
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // txtTotalTC
            // 
            this.txtTotalTC.Location = new System.Drawing.Point( 73, 10 );
            this.txtTotalTC.Name = "txtTotalTC";
            this.txtTotalTC.ReadOnly = true;
            this.txtTotalTC.Size = new System.Drawing.Size( 39, 20 );
            this.txtTotalTC.TabIndex = 14;
            // 
            // lblTotalTC
            // 
            this.lblTotalTC.Location = new System.Drawing.Point( 6, 13 );
            this.lblTotalTC.Name = "lblTotalTC";
            this.lblTotalTC.Size = new System.Drawing.Size( 62, 13 );
            this.lblTotalTC.TabIndex = 13;
            this.lblTotalTC.Text = "Total TC";
            this.lblTotalTC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFail
            // 
            this.txtFail.Location = new System.Drawing.Point( 147, 32 );
            this.txtFail.Name = "txtFail";
            this.txtFail.ReadOnly = true;
            this.txtFail.Size = new System.Drawing.Size( 39, 20 );
            this.txtFail.TabIndex = 12;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point( 147, 10 );
            this.txtPass.Name = "txtPass";
            this.txtPass.ReadOnly = true;
            this.txtPass.Size = new System.Drawing.Size( 39, 20 );
            this.txtPass.TabIndex = 12;
            // 
            // lblTotalEml
            // 
            this.lblTotalEml.Location = new System.Drawing.Point( 6, 36 );
            this.lblTotalEml.Name = "lblTotalEml";
            this.lblTotalEml.Size = new System.Drawing.Size( 62, 13 );
            this.lblTotalEml.TabIndex = 7;
            this.lblTotalEml.Text = "Total EML";
            this.lblTotalEml.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFail
            // 
            this.lblFail.AutoSize = true;
            this.lblFail.Location = new System.Drawing.Point( 120, 36 );
            this.lblFail.Name = "lblFail";
            this.lblFail.Size = new System.Drawing.Size( 23, 13 );
            this.lblFail.TabIndex = 9;
            this.lblFail.Text = "Fail";
            // 
            // txtTotalEml
            // 
            this.txtTotalEml.Location = new System.Drawing.Point( 73, 32 );
            this.txtTotalEml.Name = "txtTotalEml";
            this.txtTotalEml.ReadOnly = true;
            this.txtTotalEml.Size = new System.Drawing.Size( 39, 20 );
            this.txtTotalEml.TabIndex = 10;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point( 113, 13 );
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size( 30, 13 );
            this.lblPass.TabIndex = 8;
            this.lblPass.Text = "Pass";
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point( 608, 29 );
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size( 57, 22 );
            this.btnAbort.TabIndex = 14;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler( this.btnAbort_Click );
            // 
            // treeGridView
            // 
            this.treeGridView.AllowUserToAddRows = false;
            this.treeGridView.AllowUserToDeleteRows = false;
            this.treeGridView.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.colTestCase,
            this.colPath,
            this.colStartTime,
            this.colEndTime,
            this.colStatus} );
            this.treeGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.treeGridView.ImageList = null;
            this.treeGridView.Location = new System.Drawing.Point( 3, 54 );
            this.treeGridView.Name = "treeGridView";
            this.treeGridView.RowHeadersVisible = false;
            this.treeGridView.Size = new System.Drawing.Size( 662, 333 );
            this.treeGridView.TabIndex = 17;
            // 
            // colTestCase
            // 
            this.colTestCase.DefaultNodeImage = null;
            this.colTestCase.HeaderText = "Test Case";
            this.colTestCase.Name = "colTestCase";
            this.colTestCase.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTestCase.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPath
            // 
            this.colPath.HeaderText = "Path";
            this.colPath.Name = "colPath";
            this.colPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colStartTime
            // 
            this.colStartTime.HeaderText = "Start Time";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.ReadOnly = true;
            this.colStartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colEndTime
            // 
            this.colEndTime.HeaderText = "End Time";
            this.colEndTime.Name = "colEndTime";
            this.colEndTime.ReadOnly = true;
            this.colEndTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblNAT
            // 
            this.lblNAT.AutoSize = true;
            this.lblNAT.Location = new System.Drawing.Point( 15, 30 );
            this.lblNAT.Name = "lblNAT";
            this.lblNAT.Size = new System.Drawing.Size( 29, 13 );
            this.lblNAT.TabIndex = 18;
            this.lblNAT.Text = "NAT";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point( 167, 31 );
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size( 53, 13 );
            this.lblPassword.TabIndex = 20;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point( 223, 28 );
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size( 147, 20 );
            this.txtPassword.TabIndex = 21;
            this.txtPassword.Text = "skyline";
            // 
            // UcSshCmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.txtPassword );
            this.Controls.Add( this.lblPassword );
            this.Controls.Add( this.cboNatIP );
            this.Controls.Add( this.lblNAT );
            this.Controls.Add( this.treeGridView );
            this.Controls.Add( this.btnRefresh );
            this.Controls.Add( this.btnAbort );
            this.Controls.Add( this.btnRun );
            this.Controls.Add( this.groupBox1 );
            this.Controls.Add( this.cboTCFolder );
            this.Controls.Add( this.lnkTCFolder );
            this.Name = "UcSshCmd";
            this.Size = new System.Drawing.Size( 668, 390 );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeGridView)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkTCFolder;
        private System.Windows.Forms.ToolTip ttpSshCmd;
        private System.Windows.Forms.ComboBox cboTCFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTotalTC;
        private System.Windows.Forms.Label lblTotalTC;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblTotalEml;
        private System.Windows.Forms.Label lblFail;
        private System.Windows.Forms.TextBox txtTotalEml;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnRefresh;
        private AdvancedDataGridView.TreeGridView treeGridView;
        private AdvancedDataGridView.TreeGridColumn colTestCase;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.ComboBox cboNatIP;
        private System.Windows.Forms.Label lblNAT;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
    }
}
