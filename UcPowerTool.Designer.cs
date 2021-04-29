namespace WsClient
{
    partial class UcPowerTool
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
            this.lblInput = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnToBase64 = new System.Windows.Forms.Button();
            this.btnToString = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point( 12, 18 );
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size( 61, 13 );
            this.lblInput.TabIndex = 0;
            this.lblInput.Text = "Input String";
            this.lblInput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point( 4, 40 );
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size( 69, 13 );
            this.lblOutput.TabIndex = 1;
            this.lblOutput.Text = "Output String";
            this.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add( this.btnToString );
            this.groupBox1.Controls.Add( this.btnToBase64 );
            this.groupBox1.Controls.Add( this.txtOutput );
            this.groupBox1.Controls.Add( this.txtInput );
            this.groupBox1.Controls.Add( this.lblInput );
            this.groupBox1.Controls.Add( this.lblOutput );
            this.groupBox1.Location = new System.Drawing.Point( 3, 0 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size( 529, 61 );
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base64 Conversion";
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Location = new System.Drawing.Point( 76, 14 );
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size( 362, 20 );
            this.txtInput.TabIndex = 2;
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point( 76, 37 );
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size( 362, 20 );
            this.txtOutput.TabIndex = 3;
            // 
            // btnToBase64
            // 
            this.btnToBase64.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToBase64.Location = new System.Drawing.Point( 444, 14 );
            this.btnToBase64.Name = "btnToBase64";
            this.btnToBase64.Size = new System.Drawing.Size( 75, 22 );
            this.btnToBase64.TabIndex = 4;
            this.btnToBase64.Text = "To Base64";
            this.btnToBase64.UseVisualStyleBackColor = true;
            this.btnToBase64.Click += new System.EventHandler( this.btnToBase64_Click );
            // 
            // btnToString
            // 
            this.btnToString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToString.Location = new System.Drawing.Point( 444, 36 );
            this.btnToString.Name = "btnToString";
            this.btnToString.Size = new System.Drawing.Size( 75, 22 );
            this.btnToString.TabIndex = 5;
            this.btnToString.Text = "To String";
            this.btnToString.UseVisualStyleBackColor = true;
            this.btnToString.Click += new System.EventHandler( this.btnToString_Click );
            // 
            // UcPowerTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.groupBox1 );
            this.Name = "UcPowerTool";
            this.Size = new System.Drawing.Size( 535, 151 );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnToString;
        private System.Windows.Forms.Button btnToBase64;
    }
}
