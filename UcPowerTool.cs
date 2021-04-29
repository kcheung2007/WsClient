using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WsClient
{
    public partial class UcPowerTool : UserControl
    {
        public UcPowerTool()
        {
            InitializeComponent();
        }

        public byte[] StrToUTF8ByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes( str );
        }//end of StrToByteArray

        private void btnToBase64_Click(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrEmpty( txtInput.Text ))
                {
                    txtInput.BackColor = Color.GreenYellow;
                }
                else
                {
                    txtOutput.Text = System.Convert.ToBase64String( StrToUTF8ByteArray( txtInput.Text ) );
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }
        }//end of btnToBase64_Click

        private void btnToString_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] byteArray;
                if(String.IsNullOrEmpty( txtInput.Text ))
                {
                    txtInput.BackColor = Color.GreenYellow;
                }
                else
                {
                    byteArray = System.Convert.FromBase64String( txtInput.Text );
                    txtOutput.Text = System.Text.Encoding.UTF8.GetString( byteArray );
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }

        }
    }
}
