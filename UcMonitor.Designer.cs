using System.Diagnostics;

namespace WsClient
{
    partial class UcMonitor
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
                Trace.WriteLine("UcMonitor.Designer.cs - components deposing");
                components.Dispose();                
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcMonitor));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPointOfInterest = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblLastCheckTime = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblLog = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStatusText1 = new System.Windows.Forms.Label();
            this.lblStatusIcon1 = new System.Windows.Forms.Label();
            this.ledImageList = new System.Windows.Forms.ImageList(this.components);
            this.lblDateCheck1 = new System.Windows.Forms.Label();
            this.btnCheck1 = new System.Windows.Forms.Button();
            this.lnkDetail1 = new System.Windows.Forms.LinkLabel();
            this.cboDomainName1 = new System.Windows.Forms.ComboBox();
            this.monTimer = new System.Windows.Forms.Timer(this.components);
            this.ttpMonitor = new System.Windows.Forms.ToolTip(this.components);
            this.bgdWorkerMonitor = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.00507F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.99492F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel1.Controls.Add(this.lblPointOfInterest, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLastCheckTime, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAction, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLog, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDateCheck1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCheck1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lnkDetail1, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboDomainName1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.38462F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.61538F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(520, 53);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblPointOfInterest
            // 
            this.lblPointOfInterest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPointOfInterest.AutoSize = true;
            this.lblPointOfInterest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPointOfInterest.Location = new System.Drawing.Point(6, 4);
            this.lblPointOfInterest.Name = "lblPointOfInterest";
            this.lblPointOfInterest.Size = new System.Drawing.Size(98, 13);
            this.lblPointOfInterest.TabIndex = 0;
            this.lblPointOfInterest.Text = "Point of Interest";
            this.lblPointOfInterest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(116, 4);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Status";
            // 
            // lblLastCheckTime
            // 
            this.lblLastCheckTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLastCheckTime.AutoSize = true;
            this.lblLastCheckTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastCheckTime.Location = new System.Drawing.Point(187, 4);
            this.lblLastCheckTime.Name = "lblLastCheckTime";
            this.lblLastCheckTime.Size = new System.Drawing.Size(116, 13);
            this.lblLastCheckTime.TabIndex = 2;
            this.lblLastCheckTime.Text = "Last Checked Time";
            // 
            // lblAction
            // 
            this.lblAction.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(336, 4);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(43, 13);
            this.lblAction.TabIndex = 3;
            this.lblAction.Text = "Action";
            // 
            // lblLog
            // 
            this.lblLog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.Location = new System.Drawing.Point(440, 4);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(28, 13);
            this.lblLog.TabIndex = 4;
            this.lblLog.Text = "Log";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblStatusText1);
            this.panel1.Controls.Add(this.lblStatusIcon1);
            this.panel1.Location = new System.Drawing.Point(114, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(47, 24);
            this.panel1.TabIndex = 5;
            // 
            // lblStatusText1
            // 
            this.lblStatusText1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusText1.AutoSize = true;
            this.lblStatusText1.Location = new System.Drawing.Point(22, 6);
            this.lblStatusText1.Name = "lblStatusText1";
            this.lblStatusText1.Size = new System.Drawing.Size(35, 13);
            this.lblStatusText1.TabIndex = 1;
            this.lblStatusText1.Text = "WAIT";
            // 
            // lblStatusIcon1
            // 
            this.lblStatusIcon1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusIcon1.ImageList = this.ledImageList;
            this.lblStatusIcon1.Location = new System.Drawing.Point(4, 4);
            this.lblStatusIcon1.Name = "lblStatusIcon1";
            this.lblStatusIcon1.Size = new System.Drawing.Size(20, 16);
            this.lblStatusIcon1.TabIndex = 0;
            // 
            // ledImageList
            // 
            this.ledImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ledImageList.ImageStream")));
            this.ledImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ledImageList.Images.SetKeyName(0, "LedGreen.ico");
            this.ledImageList.Images.SetKeyName(1, "LedRed.ico");
            this.ledImageList.Images.SetKeyName(2, "LedYellow.ico");
            // 
            // lblDateCheck1
            // 
            this.lblDateCheck1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDateCheck1.AutoSize = true;
            this.lblDateCheck1.Location = new System.Drawing.Point(228, 30);
            this.lblDateCheck1.Name = "lblDateCheck1";
            this.lblDateCheck1.Size = new System.Drawing.Size(35, 13);
            this.lblDateCheck1.TabIndex = 6;
            this.lblDateCheck1.Text = "label1";
            this.ttpMonitor.SetToolTip(this.lblDateCheck1, "Last Checked Time");
            // 
            // btnCheck1
            // 
            this.btnCheck1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCheck1.Enabled = false;
            this.btnCheck1.Location = new System.Drawing.Point(334, 27);
            this.btnCheck1.Name = "btnCheck1";
            this.btnCheck1.Size = new System.Drawing.Size(48, 19);
            this.btnCheck1.TabIndex = 7;
            this.btnCheck1.Text = "Check";
            this.ttpMonitor.SetToolTip(this.btnCheck1, "Check SOAP Portal Status");
            this.btnCheck1.Click += new System.EventHandler(this.btnCheck1_Click);
            // 
            // lnkDetail1
            // 
            this.lnkDetail1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lnkDetail1.AutoSize = true;
            this.lnkDetail1.Location = new System.Drawing.Point(437, 30);
            this.lnkDetail1.Name = "lnkDetail1";
            this.lnkDetail1.Size = new System.Drawing.Size(34, 13);
            this.lnkDetail1.TabIndex = 8;
            this.lnkDetail1.TabStop = true;
            this.lnkDetail1.Text = "Detail";
            this.ttpMonitor.SetToolTip(this.lnkDetail1, "Launch log file");
            this.lnkDetail1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDetail_LinkClicked);
            // 
            // cboDomainName1
            // 
            this.cboDomainName1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDomainName1.FormattingEnabled = true;
            this.cboDomainName1.Location = new System.Drawing.Point(4, 25);
            this.cboDomainName1.Name = "cboDomainName1";
            this.cboDomainName1.Size = new System.Drawing.Size(103, 21);
            this.cboDomainName1.TabIndex = 9;
            this.cboDomainName1.Text = "Stop";
            this.ttpMonitor.SetToolTip(this.cboDomainName1, "Default Domain Name");
            this.cboDomainName1.MouseEnter += new System.EventHandler(this.cboDomainName1_MouseEnter);
            this.cboDomainName1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboDomainName1_KeyPress);
            this.cboDomainName1.SelectedValueChanged += new System.EventHandler(this.cboDomainName1_SelectedValueChanged);
            this.cboDomainName1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboDomainName1_KeyDown);
            this.cboDomainName1.TextUpdate += new System.EventHandler(this.cboDomainName1_TextUpdate);
            // 
            // monTimer
            // 
            this.monTimer.Enabled = true;
            this.monTimer.Interval = 50000;
            this.monTimer.Tick += new System.EventHandler(this.monTimer_Tick);
            // 
            // bgdWorkerMonitor
            // 
            this.bgdWorkerMonitor.WorkerReportsProgress = true;
            this.bgdWorkerMonitor.WorkerSupportsCancellation = true;
            this.bgdWorkerMonitor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgdWorkerMonitor_DoWork);
            this.bgdWorkerMonitor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgdWorkerMonitor_RunWorkerCompleted);
            this.bgdWorkerMonitor.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgdWorkerMonitor_ProgressChanged);
            // 
            // UcMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UcMonitor";
            this.Size = new System.Drawing.Size(535, 151);
            this.Load += new System.EventHandler(this.UcMonitor_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblPointOfInterest;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLastCheckTime;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblStatusIcon1;
        private System.Windows.Forms.Label lblStatusText1;
        private System.Windows.Forms.ImageList ledImageList;
        private System.Windows.Forms.Label lblDateCheck1;
        private System.Windows.Forms.Button btnCheck1;
        private System.Windows.Forms.LinkLabel lnkDetail1;
        private System.Windows.Forms.ComboBox cboDomainName1;
        private System.Windows.Forms.Timer monTimer;
        private System.Windows.Forms.ToolTip ttpMonitor;
        private System.ComponentModel.BackgroundWorker bgdWorkerMonitor;
    }
}
