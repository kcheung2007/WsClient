using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

namespace WsClient
{
    public partial class UcMailSender : UserControl
    {
        private CommObj commObj = new CommObj();
        private AttachObj attachObj = null; // for attachment
        private String msgCaption = "Mail Sender Page";
        private Thread[] m_thdList;
        private static int m_thdCount;
        private DateTime m_procStartTime;
        private DateTime m_procEndTime;
        private int m_initialThread;
        private int m_delay;
        private int m_sentMail;        

        private string m_MailFrom = "";
        private string m_RcptTo = ""; // share with both smtp and ms api
        private string m_CC = "";
        private string m_BCC = "";
        private string m_Subject = "";
        private string m_smtpServer = "";
        private string m_portNumber = "";
        private string m_txtInputFile = ""; // use by either smtp case or normal case
        private string m_ZANTAZ_Header = "";
        private string m_ZANTAZ_Value = "";
        private string m_Encoding = "";

        public event EventHandler<StatusEventArgs> statusChanged;

        private delegate void DelegateJobDoneNotification(int thdId); // all thread done - indicate Thread index
        private DelegateJobDoneNotification m_delegateJobDoneNotification;

        private delegate void DelegateUpdate_txtNumMail(int numMail);
        private DelegateUpdate_txtNumMail m_delegateNumMailCtrl;

        private delegate void DelegateUpdate_txtSubject(string szSubject);
        private DelegateUpdate_txtSubject m_delegateShowSubject;        

        public UcMailSender()
        {
            InitializeComponent();

            m_delegateJobDoneNotification = new DelegateJobDoneNotification(JobDoneHandler);
            m_delegateNumMailCtrl = new DelegateUpdate_txtNumMail(Update_txtNumMail);
            m_delegateShowSubject = new DelegateUpdate_txtSubject(Update_txtSubject);

            // save for reference - how to use the ini file
            //commObj.InitComboBoxItem(cboTo, "[To Address]");
        }

        private void Update_txtSubject(string szSubject)
        {
            txtSubject.Text = szSubject;
        }//end of Update_txtSubject

        private void Update_txtNumMail(int numMail)
        {
            txtNumMail.Text = numMail.ToString();
        }//end of Update_txtNumMail

        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual

        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcMailSender.cs - SaveComboBoxItem");
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\InitMailSender.xml";
                if (!File.Exists(cboPath))
                {
                    using (StreamWriter sw = File.CreateText(cboPath))
                    {
                    }//end of using - for auto close etc... yes... empty
                }

                // Save the combox
                tw = new XmlTextWriter(Application.StartupPath + "\\InitMailSender.xml", System.Text.Encoding.UTF8);

                Debug.WriteLine("\t ComboBox Item file" + Application.StartupPath + "\\InitMailSender.xml");

                tw.WriteStartDocument();
                tw.WriteStartElement("InitData");

                //The order is important - match with InitMailSender.xml and switch case in load combo box
                WriteComboBoxEntries(cboFileFrom, "cboFileFrom", cboFileFrom.Text, tw); //nodeList.Item(0)
                WriteComboBoxEntries(cboTo, "cboTo", cboTo.Text, tw); //nodeList.Item(1)
                WriteComboBoxEntries(cboCC, "cboCC", cboCC.Text, tw); //nodeList.Item(2)
                WriteComboBoxEntries(cboBCC, "cboBCC", cboBCC.Text, tw); //nodeList.Item(3)
                WriteComboBoxEntries(cboRcptTo, "cboRcptTo", cboRcptTo.Text, tw); //nodeList.Item(4)
                WriteComboBoxEntries(cboMailFrom, "cboMailFrom", cboMailFrom.Text, tw); //nodeList.Item(5)

                WriteComboBoxEntries(cboHeader, "cboHeader", cboHeader.Text, tw); //nodeList.Item(6)
                WriteComboBoxEntries(cboHeaderVal, "cboHeaderVal", cboHeaderVal.Text, tw); //nodeList.Item(7)

                WriteComboBoxEntries(cboSMTP, "cboSMTP", cboSMTP.Text, tw); //nodeList.Item(8)
                WriteComboBoxEntries(cboPort, "cboPort", cboPort.Text, tw); //nodeList.Item(9)

                // Text Box but not combo box
                // WriteTextBoxEntries(txtInputFile, "txtInputFile", txtInputFile.Text, tw);
                // WriteComboBoxEntries(txtMailFolder, "txtFolder", txtMailFolder.Text, tw);

                tw.WriteEndElement();
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "SaveComboBoxItem()");
            }//end of catch
            finally
            {
                if (tw != null)
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
            Debug.WriteLine("UcMailSender.cs - WriteComboBoxEntries");
            int maxEntriesToStore = 10;

            tw.WriteStartElement("combobox");
            tw.WriteStartAttribute("name", string.Empty);
            tw.WriteString(cboBoxName);
            tw.WriteEndAttribute();

            // Write the item from the text box first.
            if (txtBoxText.Length != 0)
            {
                tw.WriteStartElement("entry");
                tw.WriteString(txtBoxText);
                tw.WriteEndElement();
                maxEntriesToStore -= 1;
            }//end of if

            // Write the rest of the entries (up to 10).
            for (int i = 0; i < cboBox.Items.Count && i < maxEntriesToStore; ++i)
            {
                if (txtBoxText != cboBox.Items[i].ToString())
                {
                    tw.WriteStartElement("entry");
                    tw.WriteString(cboBox.Items[i].ToString());
                    tw.WriteEndElement();
                }
            }//end of for
            tw.WriteEndElement();
        }//end of WriteComboBoxEntries

        //private void WriteTextBoxEntries(TextBox txtBox, string txtBoxName, string txtBoxText, XmlTextWriter tw)
        //{
        //    Debug.WriteLine("UcMailSender.cs - WriteTextBoxEntries");
        //    int maxEntriesToStore = 1;

        //    tw.WriteStartElement("textbox");
        //    tw.WriteStartAttribute("name", string.Empty);
        //    tw.WriteString(txtBoxName);
        //    tw.WriteEndAttribute();

        //    // Write the item from the text box first.
        //    if (txtBoxText.Length != 0)
        //    {
        //        tw.WriteStartElement("entry");
        //        tw.WriteString(txtBoxText);
        //        tw.WriteEndElement();
        //        maxEntriesToStore -= 1;
        //    }//end of if

        //     Write the rest of the entries (up to 10).
        //    for (int i = 0; i < cboBox.Items.Count && i < maxEntriesToStore; ++i)
        //    {
        //        if (txtBoxText != cboBox.Items[i].ToString())
        //        {
        //            tw.WriteStartElement("entry");
        //            tw.WriteString(cboBox.Items[i].ToString());
        //            tw.WriteEndElement();
        //        }
        //    }//end of for
        //    tw.WriteEndElement();
        //}//end of WriteTextBoxEntries
        /// <summary>
        /// Load the text value into combo boxes. (OK... hardcode)
        /// </summary>
        private void LoadComboBoxes()
        {
            Debug.WriteLine("UcMailSender.cs - LoadComboBoxes");
            try
            {
                cboFileFrom.Items.Clear();
                cboTo.Items.Clear();
                cboCC.Items.Clear();
                cboBCC.Items.Clear();
                cboHeader.Items.Clear();
                cboHeaderVal.Items.Clear();
                cboMailFrom.Items.Clear();
                cboRcptTo.Items.Clear();
                cboSMTP.Items.Clear();
                cboPort.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\InitMailSender.xml";
                if (!File.Exists(cboPath))
                {
                    //File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load(cboPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                int numComboBox = nodeList.Count; // count text box also
                int x;
                for (int i = 0; i < numComboBox; i++) // Order is important here
                {
                    switch (nodeList.Item(i).Attributes.GetNamedItem("name").InnerText)
                    {
                        case "cboFileFrom":
                            for (x = 0; x < nodeList.Item(0).ChildNodes.Count; ++x)
                            {
                                cboFileFrom.Items.Add(nodeList.Item(0).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboTo":
                            for (x = 0; x < nodeList.Item(1).ChildNodes.Count; ++x)
                            {
                                cboTo.Items.Add(nodeList.Item(1).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboCC":
                            for (x = 0; x < nodeList.Item(2).ChildNodes.Count; ++x)
                            {
                                cboCC.Items.Add(nodeList.Item(2).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboBCC":
                            for (x = 0; x < nodeList.Item(3).ChildNodes.Count; ++x)
                            {
                                cboBCC.Items.Add(nodeList.Item(3).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboRcptTo":
                            for (x = 0; x < nodeList.Item(4).ChildNodes.Count; ++x)
                            {
                                cboRcptTo.Items.Add(nodeList.Item(4).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboMailFrom":
                            for (x = 0; x < nodeList.Item(5).ChildNodes.Count; ++x)
                            {
                                cboMailFrom.Items.Add(nodeList.Item(5).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboHeader":
                            for (x = 0; x < nodeList.Item(6).ChildNodes.Count; ++x)
                            {
                                cboHeader.Items.Add(nodeList.Item(6).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboHeaderVal":
                            for (x = 0; x < nodeList.Item(7).ChildNodes.Count; ++x)
                            {
                                cboHeaderVal.Items.Add(nodeList.Item(7).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboSMTP":
                            for (x = 0; x < nodeList.Item(8).ChildNodes.Count; ++x)
                            {
                                cboSMTP.Items.Add(nodeList.Item(8).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboPort":
                            for (x = 0; x < nodeList.Item(9).ChildNodes.Count; ++x)
                            {
                                cboPort.Items.Add(nodeList.Item(9).ChildNodes.Item(x).InnerText);
                            }
                            break;
                    }//end of switch
                }//end of for

                if(0 < cboFileFrom.Items.Count)
                    cboFileFrom.Text = cboFileFrom.Items[0].ToString();
                if (0 < cboTo.Items.Count)
                    cboTo.Text = cboTo.Items[0].ToString();
                if (0 < cboCC.Items.Count)
                    cboCC.Text = cboCC.Items[0].ToString();
                if (0 < cboBCC.Items.Count)
                    cboBCC.Text = cboBCC.Items[0].ToString();
                if (0 < cboRcptTo.Items.Count) 
                    cboRcptTo.Text = cboRcptTo.Items[0].ToString();
                if (0 < cboMailFrom.Items.Count) 
                    cboMailFrom.Text = cboMailFrom.Items[0].ToString();
                if (0 < cboHeader.Items.Count)
                    cboHeader.Text = cboHeader.Items[0].ToString();
                if (0 < cboHeaderVal.Items.Count)
                    cboHeaderVal.Text = cboHeaderVal.Items[0].ToString();
                if (0 < cboSMTP.Items.Count)
                    cboSMTP.Text = cboSMTP.Items[0].ToString();
                if (0 < cboPort.Items.Count)
                    cboPort.Text = cboPort.Items[0].ToString();

            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "Exception");
                MessageBox.Show(msg, "LoadComboBoxes()", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }//end of catch
        }//end of //end of LoadComboBoxes
        #endregion

        #region Handle Key Down and Press
        private void cboFileFrom_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboTo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboCC_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboBCC_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboHeader_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboHeaderVal_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboMailFrom_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboRcptTo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch

        }

        private void cboSMTP_KeyDown(object sender, KeyEventArgs e)
        {
           switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboPort_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboFileFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboBCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboMailFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboRcptTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboHeader_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboHeaderVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        private void cboSMTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo(sender, e);
        }

        /// <summary>
        /// Handle the auto completion for user input in combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCompleteCombo(object sender, KeyPressEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;//this.ComboBox1
            if (Char.IsControl(e.KeyChar))
                return;

            String ToFind = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            int index = cb.FindStringExact(ToFind);
            if (index == -1)
                index = cb.FindString(ToFind);

            if (index == -1)
                return;

            cb.SelectedIndex = index;
            cb.SelectionStart = ToFind.Length;
            cb.SelectionLength = cb.Text.Length - cb.SelectionStart;
            e.Handled = true;

        }//end of AutoCompleteCombo
        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - lnkTest_LinkClicked");
            this.Cursor = Cursors.WaitCursor;
            richBox.Text = commObj.TestSmtpConnection(cboSMTP.Text, cboPort.Text) ? "Connection OK" : "Connection FAIL";
            this.Cursor = Cursors.Default;
        }//end of btnTest_Click

        private void EnableFileCaseCtrl(bool value)
        {
            lnkFile.Enabled = value;
            txtMailAddrFile.Enabled = value;
        }//end of EnableFileCase

        private void EnableNormalCaseCtrl(bool value)
        {
            //rdoNormal.Checked = value;
            lnkSender.Enabled = value;
            lnkTo.Enabled = value;
            lnkCC.Enabled = value;
            lnkBCC.Enabled = value;
            cboFileFrom.Enabled = value;
            cboTo.Enabled = value;
            cboCC.Enabled = value;
            cboBCC.Enabled = value;
            lblHeader.Enabled = value;
            cboHeader.Enabled = value;
            lblValue.Enabled = value;
            cboHeaderVal.Enabled = value;
        }//end of EnableNormalCaseCtrl

        private void EnableSmtpCaseCtrl(bool value)
        {
            //rdoSMTP.Checked = value;
            lblRcptTo.Enabled = value;
            lblMailFrom.Enabled = value;
            cboRcptTo.Enabled = value;
            cboMailFrom.Enabled = value;
            txtInputFile.Enabled = value;
            txtMailFolder.Enabled = value;
            rdoInputFile.Enabled = value;
            rdoMsgFolder.Enabled = value;
            rdoDefault.Enabled = value;
            dtPicker.Enabled = value;
            
            if (value == true)
            {
                if (rdoInputFile.Checked)
                {
                    txtInputFile.Enabled = true;
                    txtMailFolder.Enabled = false;

                    btnBroFolder.Enabled = false;
                    btnBroMailFile.Enabled = true;

                    dtPicker.Enabled = false;
                }
                else
                    if( rdoMsgFolder.Checked )
                    {
                        txtInputFile.Enabled = false;
                        txtMailFolder.Enabled = true;
                        btnBroFolder.Enabled = true;
                        btnBroMailFile.Enabled = false;

                        dtPicker.Enabled = false;
                    }
                    else
                        if(rdoDefault.Checked)
                        {
                            dtPicker.Enabled = true;
                            txtInputFile.Enabled = false;
                            txtMailFolder.Enabled = false;
                            btnBroFolder.Enabled = false;
                            btnBroMailFile.Enabled = false;
                        }
            }//end of value == true
            else
            {   // value == false
                txtInputFile.Enabled = false;
                txtMailFolder.Enabled = false;
                btnBroFolder.Enabled = false;
                btnBroMailFile.Enabled = false;
                rdoDefault.Enabled = false;
                dtPicker.Enabled = false;
            }
        }//end of EnableSmtpCaseCtrl

        private void EnableOtherCtrl(bool value)
        {
            chkGUID.Enabled = value;
            chkAttach.Enabled = value;
            lblSubject.Enabled = value;
            lnkFolder.Enabled = value;
            txtSubject.Enabled = value;
            txtFolder.Enabled = value;
        }//end of EnableOtherCtrl

        /// <summary>
        /// Handle enable or disable controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoFileCase_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMailSender.cs - rdoFileCase_Click");
            rdoNormal.Checked = false;
            rdoSMTP.Checked = false;
            rdoFileCase.Checked = true;

            // enable normal group control
            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(true);
            EnableOtherCtrl(true);
            
            panel2.Enabled = false;            

            OnUpdateStatusBar(new StatusEventArgs("Dynamic send simple mail by using MS mail API."));
        }//end of rdoFileCase_Click

        /// <summary>
        /// Handle enable or disable of the controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoNormal_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMailSender.cs - rdoNormal_Click" );
            rdoNormal.Checked = true;
            rdoSMTP.Checked = false;
            rdoFileCase.Checked = false;

            // enable normal group control
            EnableNormalCaseCtrl(true);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(true);

            panel2.Enabled = true;
            if(!chkAttach.Checked)
            {
                txtFolder.Enabled = false;
                lnkFolder.Enabled = false;
            }

            OnUpdateStatusBar(new StatusEventArgs("Dynamic send simple mail by using MS mail API."));
        }//end of rdoNormal_Click

        /// <summary>
        /// Handle enable or disable of the controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoSMTP_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMailSender.cs - rdoSMTP_Click");
            rdoNormal.Checked = false;
            rdoSMTP.Checked = true;
            rdoFileCase.Checked = false;

            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(true);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(true);

            panel2.Enabled = false;

            OnUpdateStatusBar(new StatusEventArgs("Stream a eml file from one address to another address"));
        }//end of rdoSMTP_Click

        private void chkAttach_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAttach.Checked)
            {
                lnkFolder.Enabled = true;
                txtFolder.Enabled = true;
            }
            else
            {
                lnkFolder.Enabled = false;
                txtFolder.Enabled = false;
            }//end of else - disable
        }// end of chkAttach_CheckedChanged

        private void btnSend_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - btnSend_Click");

            //if (chkAttach.Checked)
            //{
            //    attachObj = new AttachObj(txtFolder.Text);
            //}// end of if - attachment check

            if (rdoNormal.Checked)
            {
                doMsApiMailing();
            }//end of if - do the normal mailing
            else                    
                if (rdoSMTP.Checked)
                {
                    doSocketMailing();
                }// end of if - stream file into socket
                else
                    if (rdoFileCase.Checked)
                    {
                        doFileCaseMailing();
                    }
        }//end of btnSend_Click

        private void doFileCaseMailing()
        {
            if (!ValidateFileCaseInput())
                return;

            SetFileCaseData(); // avoiding cross-thread operation - timer start inside this function

            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(false);
            btnSend.Enabled = false; // exclusive between Send button and abort button
            //btnAbort.Enabled = true;

            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_SendFileMail));
                m_thdList[i].Name = "Thd_SendFileMail_" + i.ToString(CultureInfo.InvariantCulture);
                m_thdList[i].Start();
#if(DEBUG)
                commObj.LogToFile("Thread.log", "Start Thread: " + m_thdList[i].Name);
#endif
            }
        }//end of doFileCaseMailing

        private void doMsApiMailing()
        {
            if (!ValidateMsApiInput())
                return;

            SetMsApiData(); // avoiding cross-thread operation - timer start inside this function

            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(false);
            btnSend.Enabled = false; // exclusive between Send button and abort button
            //btnAbort.Enabled = true;

            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_SendGuidMail));
                m_thdList[i].Name = "Thd_SendGuidMail_" + i.ToString(CultureInfo.InvariantCulture);
                m_thdList[i].Start();
#if(DEBUG)
                commObj.LogToFile("Thread.log", "Start Thread: " + m_thdList[i].Name);
#endif
            }//end of for
        }//end of doMsApiMailing

        private void SetFileCaseData()
        {
            m_initialThread = (int)nudThread.Value;
            m_delay = (int)nudDelay.Value;

            m_Subject = txtSubject.Text;
            m_smtpServer = cboSMTP.Text;
            m_portNumber = cboPort.Text;

            // reset value
            m_thdCount = 0;
            m_sentMail = 0;
            m_thdList = null;
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            txtNumMail.Text = "";
            txtMailPerSec.Text = "";
            txtMailsSize.Text = "";
            txtAveSize.Text = "";
            txtDuration.Text = "";
        }//end of SetFileCaseData

        private void SetMsApiData()
        {
            m_initialThread = (int)nudThread.Value;
            m_delay = (int)nudDelay.Value;

//            m_MailFrom = cboMailFrom.Text;
            m_txtInputFile = cboFileFrom.Text; // file that contains sender addresses
            m_RcptTo = cboTo.Text;
            m_CC = cboCC.Text;
            m_BCC = cboBCC.Text;
            m_Subject = txtSubject.Text;
            m_smtpServer = cboSMTP.Text;
            m_portNumber = cboPort.Text;
            m_ZANTAZ_Header = cboHeader.Text;
            m_ZANTAZ_Value = cboHeaderVal.Text;

            // reset value
            m_thdCount = 0;
            m_sentMail = 0;
            m_thdList = null;
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            txtNumMail.Text = "";
            txtMailPerSec.Text = "";
            txtMailsSize.Text = "";
            txtAveSize.Text = "";
            txtDuration.Text = "";

            if(chkAttach.Checked)
            {
                attachObj = new AttachObj( txtFolder.Text );
            }// end of if - attachment check
        }//end of SetApiData

        private void SetSmtpData()
        {
            m_initialThread = (int)nudThread.Value;
            m_delay = (int)nudDelay.Value;

            m_MailFrom = cboMailFrom.Text;
            m_RcptTo = cboRcptTo.Text;
            m_smtpServer = cboSMTP.Text;
            m_portNumber = cboPort.Text;
            m_Encoding = cboEncoding.Text;

            if (rdoInputFile.Checked)
                m_txtInputFile = txtInputFile.Text;
            // reset value
            m_thdCount = 0;
            m_sentMail = 0;
            m_thdList = null;            
            m_procStartTime = DateTime.Now;
            txtStartTime.Text = m_procStartTime.ToString();
            txtEndTime.Text = "";
            txtNumMail.Text = "";
            txtMailPerSec.Text = "";
            txtMailsSize.Text = "";
            txtAveSize.Text = "";
            txtDuration.Text = "";
        }//end of SetSmtpData

        private bool ValidateFileCaseInput()
        {
            bool rv = true; // assume everything is ok
            if (txtMailAddrFile.Text == "")
            {
                txtMailAddrFile.Focus();
                txtMailAddrFile.BackColor = Color.YellowGreen;
                rv = false;
            }
            return (rv);
        }//end of ValidateFileCaseInput

        private bool ValidateMsApiInput()
        {
            bool rv = true; // assume everything is ok
            if (cboFileFrom.Text == "")
            {
                cboFileFrom.Focus();
                cboFileFrom.BackColor = Color.YellowGreen;
                rv = false;
            }
            else
                if((cboTo.Text == "") && (cboCC.Text == "") && (cboBCC.Text == "")) // MS API Must have To field
                {
                    cboTo.Focus();
                    cboTo.BackColor = Color.YellowGreen;
                    rv = false;
                }                
            return (rv);
        }//end of ValidateMsApiInput

        /// <summary>
        /// Validate user input
        /// </summary>
        /// <returns>bool: OK - true; Fail - false</returns>
        private bool ValidateSmtpInput()
        {
            bool rv = true; // assume everything is ok
            if (cboMailFrom.Text == "")
            {
                cboMailFrom.Focus();
                cboMailFrom.BackColor = Color.YellowGreen;
                rv = false;
            }//end of check Mail From
            else
                if (cboRcptTo.Text == "")
                {
                    cboRcptTo.Focus();
                    cboRcptTo.BackColor = Color.YellowGreen;
                    rv = false;
                }
                else
                {
                    if (rdoInputFile.Checked)
                    {
                        if (txtInputFile.Text == "")
                        {
                            txtInputFile.Focus();
                            txtInputFile.BackColor = Color.YellowGreen;
                            rv = false;
                        }
                    }
                    else
                        if (rdoMsgFolder.Checked)
                        {
                            if (txtMailFolder.Text == "")
                            {
                                txtMailFolder.Focus();
                                txtMailFolder.BackColor = Color.YellowGreen;
                                rv = false;
                            }
                        }//end of rdoMsgFolder
                }//end of else
            return (rv);
        }//end of ValidateSmtpInput

        private void doSocketMailing()
        {
            if (!ValidateSmtpInput())
                return;

            SetSmtpData(); // avoiding cross-thread operation - timer start inside this function
            
            EnableNormalCaseCtrl(false);
            EnableSmtpCaseCtrl(false);
            EnableFileCaseCtrl(false);
            EnableOtherCtrl(false);
            btnSend.Enabled = false; // exclusive between Send button and abort button
            //btnAbort.Enabled = true;

            m_thdList = new Thread[m_initialThread];
            for (int i = 0; i < m_initialThread; i++)
            {
                m_thdList[i] = new Thread(new ThreadStart(this.Thd_SendSmtpMail));
                m_thdList[i].Name = "Thd_SendSmtpMail_" + i.ToString(CultureInfo.InvariantCulture);
                m_thdList[i].Start();
#if(DEBUG)
                commObj.LogToFile("Thread.log", "Start Thread: " + m_thdList[i].Name);
#endif
            }//end of for
        }//end of doSocketMailing

        private void lnkSender_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - lnkFrom_LinkClicked");

            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.RestoreDirectory = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                cboFileFrom.Text = ofDlg.FileName;
            }//end of if
        }//end of lnkSender_LinkClicked

        private void lnkFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - lnkFolder_LinkClicked");
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();

            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            if (txtFolder.Text != null)
                fbDlg.SelectedPath = txtFolder.Text;  // set the default folder

            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fbDlg.SelectedPath;
            }
        }//end of lnkFolder_LinkClicked

        public void Thd_SendFileMail()
        {
            Debug.WriteLine("UcMailSender.cs - Thd_SendFileMail");
            string strGUID = "";
            bool bSent = true;
            StreamReader sr = null;
            DateTime startTime = DateTime.Now;

            //////////////////////////////////////////////////
            // Building connection string and creating OleDB 
            //////////////////////////////////////////////////
            //
            // Assemble the connection string for the 
            // Excel spreadsheet.
            // The actual connection string is:
            // "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyExcelSpreadsheet.XLS;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=2"""
            // For a better explanation of the connection string
            // go to http://www.connectionstrings.com/
            //
            StringBuilder sbConn = new StringBuilder();
            sbConn.Append( @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=");
            sbConn.Append( txtMailAddrFile.Text );
            sbConn.Append( ";Extended Properties=" );
            sbConn.Append( Convert.ToChar( 34 ) );
            sbConn.Append( "Excel 8.0;HDR=Yes;IMEX=2" ); // The first roll consider header and skip
            // sbConn.Append( "Excel 8.0;HDR=No;IMEX=2" ); // No header and start from the first roll
            sbConn.Append( Convert.ToChar( 34 ) );
            //
            // Open the spreadsheet and query the data.
            //
            OleDbConnection cnExcel = new OleDbConnection( sbConn.ToString() );
            cnExcel.Open();
            OleDbCommand cmdExcel = new OleDbCommand( "Select * From [Sheet1$]", cnExcel );
//            OleDbCommand cmdExcel = new OleDbCommand( "Select * From [S2_TD1$]", cnExcel );
            OleDbDataReader drExcel = cmdExcel.ExecuteReader();

            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start send FILE mail thread Count: " + m_thdCount);
                    OnUpdateStatusBar(new StatusEventArgs("Start send FILE mail : Thread Count = " + m_thdCount.ToString()));
                }
                for (int i = 0; i < nudCycle.Value; i++)
                {
                    Debug.WriteLine("\t In Cycle " + i.ToString());                    
                    try
                    {
                        //sr = new StreamReader(txtMailAddrFile.Text); // address book - put here for exception catch
                        //while ((strLine = sr.ReadLine()) != null) // file name from txtFrom field
                        while( drExcel.Read() )
                        {
                            try
                            {
                                //counter++;
                                Debug.WriteLine( "\t - AutoSendMail - inside while loop : " + m_sentMail.ToString() );
                                strGUID = System.Guid.NewGuid().ToString();
                                if(!drExcel[0].ToString().StartsWith( "#" ))
                                {
                                    Debug.WriteLine( "Not start with #" );
                                    SMTPSender smtpSender = new SMTPSender( m_Encoding );
                                    smtpSender.smtpServer = m_smtpServer;
                                    smtpSender.smtpPortNum = m_portNumber;

                                    smtpSender.RcptTo = drExcel[0].ToString();
                                    smtpSender.mailFrom = drExcel[1].ToString();
                                    smtpSender.mailTo = drExcel[2].ToString();
                                    smtpSender.mailCC = drExcel[3].ToString();
                                    smtpSender.mailBCC = drExcel[4].ToString();
                                    smtpSender.mailSubject = drExcel[5].ToString();

                                    DateTime dt = new DateTime( int.Parse( drExcel[6].ToString() ),
                                                                int.Parse( drExcel[7].ToString() ),
                                                                int.Parse( drExcel[8].ToString() ) );
                                    smtpSender.mailSentDate = dt.ToString( "D" );

                                    smtpSender.mailBody = dt.ToLongDateString() + "\r\n" + drExcel[5].ToString() + strGUID;
                                    bSent = smtpSender.SmtpSend();
                                    if(bSent)
                                    {
                                        lock(this)
                                        {
                                            //m_sentMail++;
                                            IAsyncResult r = BeginInvoke( m_delegateNumMailCtrl, new object[] { ++m_sentMail } );
                                        }
                                    }//end of if
                                }// end of if
                                else
                                {
                                    Debug.WriteLine( "Start with #" );
                                }
                                Thread.Sleep( (int)nudDelay.Value );
                            }//end of try
                            catch(SMTPException ex)
                            {
                                Trace.WriteLine( "\tSMTPClientSender() Exception: " + ex.SmtpMessage.ToString() );
                                commObj.LogToFile( "SMTPClientSender() Exception: " + ex.SmtpMessage.ToString() );
                                // MessageBox.Show(ex.SmtpMessage.ToString(), msgCaption);
                            }//end of catch
                            catch(FormatException fmtEx)
                            {
                                Trace.WriteLine( "\tSMTPClientSender() FormatException: " + fmtEx.Message.ToString() );
                                commObj.LogToFile( "SMTPClientSender() FormatException: " + fmtEx.Message.ToString() );
                                // MessageBox.Show(ex.SmtpMessage.ToString(), msgCaption);
                            }//end of catch
                        }//end of while
                    }//end of try
                    catch (Exception gex)
                    {
                        Debug.WriteLine("\t Generic Exception: " + gex.ToString());
                        commObj.LogToFile( "Generic Exception: " + gex.ToString() );
                    }//end of catch generic exception
                    finally
                    {
                        if (sr != null)
                        {
                            Trace.WriteLine("Finally - close the Stream Reader");
                            commObj.LogToFile("Finally - close the Stream Reader");
                            sr.Close();
                        }//end of if

                        lock (this)
                        {
                            if (--m_thdCount == 0)
                            {
                                BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                                commObj.LogToFile("Job Done Notification - FILE mail thread : " + m_thdCount);
                                OnUpdateStatusBar(new StatusEventArgs("Thread Count = " + m_thdCount.ToString()));
                            }                            
                        }

                        OnUpdateStatusBar(new StatusEventArgs("Thread Count = " + m_thdCount.ToString()));
                    }//end of finally
                }//end of for
            }//end of outer try
            catch (Exception finalEx)
            {
                Debug.WriteLine("\t Generic File Exception: " + finalEx.ToString());
                MessageBox.Show(finalEx.Message, msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }//end of Thd_SendFileMail

/*** Save for back up 
        public void Thd_SendFileMail()
        {
            Debug.WriteLine( "UcMailSender.cs - Thd_SendFileMail" );
            string strGUID = "";
            bool bSent = true;
            StreamReader sr = null;
            DateTime startTime = DateTime.Now;
            try
            {
                lock(this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine( "\t Start send FILE mail thread Count: " + m_thdCount );
                    OnUpdateStatusBar( new StatusEventArgs( "Start send FILE mail : Thread Count = " + m_thdCount.ToString() ) );
                }
                for(int i = 0; i < nudCycle.Value; i++)
                {
                    Debug.WriteLine( "\t In Cycle " + i.ToString() );
                    string strLine; // read from file

                    try
                    {
                        sr = new StreamReader( txtMailAddrFile.Text ); // address book - put here for exception catch
                        while((strLine = sr.ReadLine()) != null) // file name from txtFrom field
                        {
                            try
                            {
                                //counter++;
                                Debug.WriteLine( "\t - AutoSendMail - inside while loop : " + m_sentMail.ToString() );

                                strGUID = System.Guid.NewGuid().ToString();
                                if(strLine[0] != '#') // skip all comment
                                {
                                    //richBox.Text += "\r\nRead line : " + strLine;
                                    //if (chkGUID.Checked)
                                    //{
                                    //    strGUID = System.Guid.NewGuid().ToString();
                                    //    txtSubject.Text = savSubj + counter.ToString() + " " + strGUID;
                                    //    commObj.LogGUID("GUID.LOG", strGUID);
                                    //}//end of if - GUID			

                                    // parse each line from the file, which defines a specific field:
                                    // Total 9 fields: RcptTo, FROM, TO, CC, BCC, Subject, Year, Month, Day and separated by commer.
                                    // Fill can be null, and store in an array.
                                    string[] splitStr = new string[4];
                                    splitStr = strLine.Split( new Char[] { ',' } );
                                    for(int k = 0; k < splitStr.Length; k++) // trim leading and ending space
                                        splitStr[k] = splitStr[k].Trim( ' ' );

                                    //m_MailFrom = splitStr[0];
                                    //m_RcptTo = splitStr[1];
                                    //m_CC = splitStr[2];
                                    //m_BCC = splitStr[3];

                                    SMTPSender smtpSender = new SMTPSender( m_Encoding );
                                    smtpSender.smtpServer = m_smtpServer;
                                    smtpSender.smtpPortNum = m_portNumber;

                                    smtpSender.RcptTo = splitStr[0];
                                    smtpSender.mailFrom = splitStr[1];
                                    smtpSender.mailTo = splitStr[2];
                                    smtpSender.mailCC = splitStr[3];
                                    smtpSender.mailBCC = splitStr[4];
                                    smtpSender.mailSubject = splitStr[5];

                                    DateTime dt = new DateTime( int.Parse( splitStr[6] ),
                                                                int.Parse( splitStr[7] ),
                                                                int.Parse( splitStr[8] ) );
                                    smtpSender.mailSentDate = dt.ToString( "D" );

                                    smtpSender.mailBody = dt.ToLongDateString() + "\r\n" + splitStr[5] + strGUID;
                                    bSent = smtpSender.SmtpSend();
                                    if(bSent)
                                    {
                                        lock(this)
                                        {
                                            //m_sentMail++;
                                            IAsyncResult r = BeginInvoke( m_delegateNumMailCtrl, new object[] { ++m_sentMail } );
                                        }
                                    }
                                    #region Comment out save for reference
                                    //// Create mail address
                                    //MailMessage mailMsg = new MailMessage();
                                    ////if (splitStr[0] != "") // add from
                                    //if( !String.IsNullOrEmpty(splitStr[0]))
                                    //{
                                    //    MailAddress from = new MailAddress(splitStr[0]);
                                    //    mailMsg.From = from;
                                    //}
                                    ////if (splitStr[1] != "") // add to
                                    //if( !String.IsNullOrEmpty(splitStr[1]))
                                    //{
                                    //    MailAddress to = new MailAddress(splitStr[1]);
                                    //    mailMsg.To.Add(to);
                                    //}

                                    ////if (splitStr[2] != "") // add CC
                                    //if(!String.IsNullOrEmpty( splitStr[2] ))
                                    //{
                                    //    MailAddress cc = new MailAddress(splitStr[2]);
                                    //    mailMsg.CC.Add(cc);
                                    //}
                                    ////if (splitStr[3] != "") // add BCC
                                    //if(!String.IsNullOrEmpty( splitStr[3] ))
                                    //{
                                    //    MailAddress bcc = new MailAddress(splitStr[3]);
                                    //    mailMsg.Bcc.Add(bcc);
                                    //}

                                    //if (chkGUID.Checked)
                                    //{
                                    //    mailMsg.Subject = m_sentMail.ToString()
                                    //                    + " " + m_Subject + " : "
                                    //                    + strGUID;

                                    //    commObj.LogGUID("GUID.LOG", strGUID);
                                    //}
                                    //else
                                    //{
                                    //    mailMsg.Subject = m_Subject;
                                    //}

                                    //mailMsg.Body = "Sent Mail: " + m_sentMail.ToString()
                                    //         + "\nFrom: " + splitStr[0]		// start from here change
                                    //         + "\n TO:  " + splitStr[1]
                                    //         + "\n CC:  " + splitStr[2]
                                    //         + "\n BCC: " + splitStr[3]
                                    //         + "\n Body_Subject: " + mailMsg.Subject
                                    //         + "\n" + DateTime.Now;


                                    //if (chkAttach.Checked)
                                    //{
                                    //    if (attachObj.IdxAttach == attachObj.NumFile)
                                    //        attachObj.IdxAttach = 0; //reset

                                    //    Debug.WriteLine(attachObj.IdxAttach, "\t - idxAttach");
                                    //    Debug.WriteLine(attachObj.AttachFullName, "\t - filename");
                                    //    // Create  the file attachment for this e-mail message.
                                    //    Attachment attachData = new Attachment(attachObj.AttachFullName, MediaTypeNames.Application.Octet);
                                    //    // Add time stamp information for the file.
                                    //    ContentDisposition disposition = attachData.ContentDisposition;
                                    //    disposition.CreationDate = System.IO.File.GetCreationTime(attachObj.AttachFullName);
                                    //    disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachObj.AttachFullName);
                                    //    disposition.ReadDate = System.IO.File.GetLastAccessTime(attachObj.AttachFullName);
                                    //    // Add the file attachment to this e-mail message.
                                    //    mailMsg.Attachments.Add(attachData);

                                    //    mailMsg.Body += "\r\nAttach file index = " + attachObj.IdxAttach
                                    //                  + "\r\nAttach file name = " + attachObj.AttachFullName;

                                    //    attachObj.IdxAttach++; // point to next file
                                    //}//end of if

                                    //Debug.WriteLine("Send mail la");
                                    //SmtpClient smtpClient = new SmtpClient(m_smtpServer);
                                    //smtpClient.Port = int.Parse(m_portNumber);

                                    //smtpClient.Send(mailMsg);

                                    //lock (this)
                                    //{
                                    //    // m_sentMail++; // inc counter
                                    //    IAsyncResult rm = BeginInvoke(m_delegateNumMailCtrl, new object[] { m_sentMail++ });
                                    //    IAsyncResult rs = BeginInvoke(m_delegateShowSubject, new object[] { mailMsg.Subject });
                                    //}
                                    #endregion

                                }//end of if - skip all commtn

                                Thread.Sleep( (int)nudDelay.Value );

                            }//end of try
                            catch(SMTPException ex)
                            {
                                Trace.WriteLine( "\tSMTPClientSender() Exception: " + ex.SmtpMessage.ToString() );
                                commObj.LogToFile( "SMTPClientSender() Exception: " + ex.SmtpMessage.ToString() );
                                // MessageBox.Show(ex.SmtpMessage.ToString(), msgCaption);
                            }//end of catch
                            catch(FormatException fmtEx)
                            {
                                Trace.WriteLine( "\tSMTPClientSender() FormatException: " + fmtEx.Message.ToString() );
                                commObj.LogToFile( "SMTPClientSender() FormatException: " + fmtEx.Message.ToString() );
                                // MessageBox.Show(ex.SmtpMessage.ToString(), msgCaption);
                            }//end of catch
                        }//end of while
                    }//end of try
                    catch(Exception gex)
                    {
                        Debug.WriteLine( "\t Generic Exception: " + gex.ToString() );
                        commObj.LogToFile( "Generic Exception: " + gex.ToString() );
                    }//end of catch generic exception
                    finally
                    {
                        if(sr != null)
                        {
                            Trace.WriteLine( "Finally - close the Stream Reader" );
                            commObj.LogToFile( "Finally - close the Stream Reader" );
                            sr.Close();
                        }//end of if

                        lock(this)
                        {
                            if(--m_thdCount == 0)
                            {
                                BeginInvoke( m_delegateJobDoneNotification, new object[] { m_thdCount } );
                                commObj.LogToFile( "Job Done Notification - FILE mail thread : " + m_thdCount );
                                OnUpdateStatusBar( new StatusEventArgs( "Thread Count = " + m_thdCount.ToString() ) );
                            }
                        }

                        OnUpdateStatusBar( new StatusEventArgs( "Thread Count = " + m_thdCount.ToString() ) );
                    }//end of finally
                }//end of for
            }//end of outer try
            catch(Exception finalEx)
            {
                Debug.WriteLine( "\t Generic File Exception: " + finalEx.ToString() );
                MessageBox.Show( finalEx.Message, msgCaption );
            }
        }//end of Thd_SendFileMail
***/

        public void Thd_SendGuidMail()
        {
            Trace.WriteLine("UcMailSender.cs - Thd_SendGuidMail");
            string strGUID = "";
            StreamReader sr = null;
            DateTime startTime = DateTime.Now;

            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    Debug.WriteLine("\t Start send GUI mail thread Count: " + m_thdCount);
                }
                for (int i = 0; i < nudCycle.Value; i++)
                {
                    Debug.WriteLine("\t In Cycle " + i.ToString());                    
                    string strFrom;

                    // save for reference
                    // IPHostEntry hostInfo = Dns.GetHostByName("localhost");
                    // strHostName = hostInfo.HostName;
                    
                    sr = new StreamReader(m_txtInputFile); // address book - put here for exception catch
                    while ((strFrom = sr.ReadLine()) != null) // file name from txtFrom field
                    {
                        //  counter++;
                        Debug.WriteLine("\t - HandleSendMail - inside while loop : " + m_sentMail.ToString());

                        // save for reference
                        // cboHeaderVal.Text = strHostName + ";" + numSent.ToString("00000000000000000000");
                        strGUID = System.Guid.NewGuid().ToString();                        
		
                        // Create mail address
                        MailMessage mailMsg = new MailMessage();

                        try // catch invalid email address
                        {
                            //if(strFrom != "") // add from
                            if(!String.IsNullOrEmpty(strFrom))
                            {
                                // TO DO: Rework this portion
                                string[] tmpFrom = strFrom.Split( new char[] { '"', '<', '>' }, StringSplitOptions.RemoveEmptyEntries );
                                if(tmpFrom.Length == 1)
                                {
                                    MailAddress from = new MailAddress( strFrom, "dummy FromName" );
                                    mailMsg.From = from;
                                }
                                else
                                    if(tmpFrom.Length == 3)
                                    {
                                        MailAddress from = new MailAddress( tmpFrom[2], tmpFrom[0] );
                                        mailMsg.From = from;
                                    }                                
                            }
                            //if(m_RcptTo != "") // add to
                            if(!String.IsNullOrEmpty( m_RcptTo ))
                            {
                                MailAddress to = new MailAddress( m_RcptTo, "RcptTo Name" );
                                mailMsg.To.Add( to );
                            }

                            //if(m_CC != "") // add CC
                            if(!String.IsNullOrEmpty( m_CC ))
                            {
                                MailAddress cc = new MailAddress( m_CC, "CC Name" );
                                mailMsg.CC.Add( cc );
                            }
                            //if(m_BCC != "") // add BCC
                            if(!String.IsNullOrEmpty( m_BCC ))
                            {
                                MailAddress bcc = new MailAddress( m_BCC, "BCC display Name" );
                                mailMsg.Bcc.Add( bcc );
                            }
                            if (chkGUID.Checked)
                            {
                                mailMsg.Subject = m_sentMail.ToString()
                                                + " " + m_Subject + " : "
                                                + strGUID;

                                commObj.LogGUID("GUID.LOG", strGUID);
                            }
                            else
                            {
                                mailMsg.Subject = m_Subject;
                            }                       

                            mailMsg.Body = "Sent Mail: " + m_sentMail.ToString()
                                     + "\nFrom: " + strFrom		// start from here change
                                     + "\n TO:  " + m_RcptTo
                                     + "\n CC:  " + m_CC
                                     + "\n BCC: " + m_BCC
                                     + "\n Body_Subject: " + mailMsg.Subject
                                     + "\n" + DateTime.Now;

                            // save for reference - this one is needed for creating new doc type
                            // mailMsg.Headers.Add("X-ZANTAZDOCCLASS", "CONFIRMATIONS");
                            if(!String.IsNullOrEmpty( m_ZANTAZ_Header ))
                                mailMsg.Headers.Add(m_ZANTAZ_Header, m_ZANTAZ_Value);

                            // Create attachment
                            if (chkAttach.Checked)
                            {
                                if (attachObj.IdxAttach == attachObj.NumFile)
                                    attachObj.IdxAttach = 0; //reset

                                Debug.WriteLine(attachObj.IdxAttach, "\t - idxAttach");
                                Debug.WriteLine(attachObj.AttachFullName, "\t - filename");

                                // Create  the file attachment for this e-mail message.
                                Attachment attachData = new Attachment(attachObj.AttachFullName, MediaTypeNames.Application.Octet);
                                // Add time stamp information for the file.
                                ContentDisposition disposition = attachData.ContentDisposition;
                                disposition.CreationDate = System.IO.File.GetCreationTime(attachObj.AttachFullName);
                                disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachObj.AttachFullName);
                                disposition.ReadDate = System.IO.File.GetLastAccessTime(attachObj.AttachFullName);
                                // Add the file attachment to this e-mail message.
                                mailMsg.Attachments.Add(attachData);

                                mailMsg.Body += "\r\nAttach file index = " + attachObj.IdxAttach
                                              + "\r\nAttach file name = " + attachObj.AttachFullName;

                                attachObj.IdxAttach++; // point to next file
                            }//end of if

                            Debug.WriteLine( "Send mail la" );
                            SmtpClient smtpClient = new SmtpClient( m_smtpServer );
                            smtpClient.Port = int.Parse( m_portNumber );

                            smtpClient.Send( mailMsg );

                            lock(this)
                            {
                                // m_sentMail++; // inc counter
                                IAsyncResult rm = BeginInvoke( m_delegateNumMailCtrl, new object[] { m_sentMail++ } );
                                IAsyncResult rs = BeginInvoke( m_delegateShowSubject, new object[] { mailMsg.Subject } );
                            }
                        }//end of try
                        catch(System.Net.WebException webEx)
                        {
                            Debug.WriteLine( webEx.Message.ToString() );
                            commObj.LogToFile( "\tWebException occur" + strGUID.ToString() );
                            Thread.Sleep( 50000 );
                            //	MessageBox.Show(ex.Message.ToString(), msgCaption);
                        }// end of catch
                        catch(System.FormatException fmtEx)
                        {
                            Trace.WriteLine( "\tFormatException: " + fmtEx.Message.ToString() );
                            commObj.LogToFile( "FormatException occur" + fmtEx.Message.ToString() + " -> " + strFrom);
                        }
                        catch(System.Net.Mail.SmtpException ex)
                        {
                            Trace.WriteLine( "\tSMTPException: " + ex.Message.ToString() );
                            commObj.LogToFile( "SMTPException occur " + ex.Message.ToString() + " -> " + strFrom );
                            Thread.Sleep( 50000 );
                            //MessageBox.Show( ex.SmtpMessage.ToString(), "SMTPException" );
                        }//end of catch

                        //m_Subject = ""; // reset subject
                        Thread.Sleep((int)nudDelay.Value);
                    }//end of while
                                                               
                }//end of for				
            }//end of outer try
            //catch (SMTPException ex)
            //{
            //    Trace.WriteLine("\tSMTPClientSender() Exception: " + ex.SmtpMessage.ToString());
            //    MessageBox.Show(ex.SmtpMessage.ToString(), "SMTPException");
            //}//end of catch
            catch (Exception gex)
            {
                Trace.WriteLine("\t Generic Exception: " + gex.ToString());
                MessageBox.Show(gex.Message, msgCaption);
                //MessageBox.Show("Thread Abort!", msgCaption);
            }//end of catch generic exception
            finally
            {
                if (sr != null)
                {
                    Trace.WriteLine("Finally - close the Stream Reader");
                    commObj.LogToFile("Finally - close the Stream Reader");
                    sr.Close();
                }//end of if

                lock (this)
                {
                    if (--m_thdCount == 0)
                    {
                        BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                        commObj.LogToFile("Job Done Notification - Send GUI Mail thread : " + m_thdCount);
                        OnUpdateStatusBar(new StatusEventArgs("Thread Count = " + m_thdCount.ToString()));
                    }
                } 
            }//end of finally            
        }//end of Thd_SendGuidMail

        /// <summary>
        /// Send mail by streaming the message into SMTP socket.
        /// Option 1: Stream a eml file.
        /// Option 2: Specify a folder that contain a list of eml files and send them all.
        /// </summary>
        public void Thd_SendSmtpMail()
        {
            Trace.WriteLine("UcMailSender.cs - Thd_SendSmtpMail");
            bool bSent = true;
            DateTime startTime = DateTime.Now;

            try
            {
                lock (this)
                {
                    m_thdCount++; // increment thread count
                    //Debug.WriteLine("\t Start send smtp mail thread Count: " + m_thdCount);
                }
                for (int i = 0; i < nudCycle.Value; i++)
                {
                    Debug.WriteLine("\t In Cycle " + i.ToString());
                    try // SMTP exception
                    {
                        if(rdoMsgFolder.Checked)
                        {
                            Debug.WriteLine( "\t Send mail in Folder" );
                            AttachObj mailFiles = new AttachObj( txtMailFolder.Text ); // TO DO : Better design Memory intensive
                            for(mailFiles.IdxAttach = 0; mailFiles.IdxAttach < mailFiles.NumFile; mailFiles.IdxAttach++)
                            {
                                Debug.WriteLine( "\t mailFile index = " + mailFiles.IdxAttach.ToString() );
                                //richBox.Text = mailFile.AttchFullName;
                                //richBox.Text = mailFile.idxAttach.ToString();
                                SMTPSender smtpSender = new SMTPSender(m_Encoding);
                                smtpSender.mailFrom = m_MailFrom;
                                smtpSender.RcptTo = m_RcptTo;
                                smtpSender.mailTo = m_RcptTo;
                                smtpSender.smtpServer = m_smtpServer;
                                smtpSender.smtpPortNum = m_portNumber;

                                bSent = smtpSender.SmtpSend( mailFiles.AttachFullName, true ); // skip Kentest lookup
                                if(bSent)
                                {
                                    lock(this)
                                    {
                                        //m_sentMail++;
                                        IAsyncResult r = BeginInvoke( m_delegateNumMailCtrl, new object[] { ++m_sentMail } );
                                    }
                                }
                            }//end of for - loop through the file in folder
                        }//end of if - message folder
                        else
                            if(rdoInputFile.Checked)
                            {   // message file
                                Debug.WriteLine( "\t Send mail from input file" );
                                SMTPSender smtpSender = new SMTPSender(m_Encoding);
                                smtpSender.mailFrom = m_MailFrom;
                                smtpSender.RcptTo = m_RcptTo;
                                smtpSender.mailTo = m_RcptTo;

                                smtpSender.smtpServer = m_smtpServer;
                                smtpSender.smtpPortNum = m_portNumber;

                                //Debug.WriteLine( "Date Time Picker Value: " + dtPicker.Value.ToLongDateString() );
                                //smtpSender.mailSentDate = dtPicker.Value.ToLongDateString();

                                bSent = smtpSender.SmtpSend( m_txtInputFile, true );
                                if(bSent)
                                {
                                    lock(this)
                                    {
                                        //m_sentMail++;
                                        IAsyncResult r = BeginInvoke( m_delegateNumMailCtrl, new object[] { ++m_sentMail } );
                                    }
                                }
                            }//end of else 
                            else
                                if(rdoDefault.Checked)
                                {
                                    SMTPSender smtpSender = new SMTPSender(m_Encoding);
                                    smtpSender.mailFrom = m_MailFrom;
                                    smtpSender.RcptTo = m_RcptTo;
                                    smtpSender.mailTo = m_RcptTo;

                                    smtpSender.smtpServer = m_smtpServer;
                                    smtpSender.smtpPortNum = m_portNumber;

                                    smtpSender.mailSentDate = dtPicker.Value.ToUniversalTime().ToString( "d MMM yyyy hh:mm:ss" ) + " -0700";
                                    //smtpSender.mailSentDate = dtPicker.Value.ToUniversalTime().ToString( "r" );

                                    string guidSubject = txtSubject.Text + " : " + System.Guid.NewGuid().ToString();
                                    if(chkGUID.Checked)
                                        smtpSender.mailSubject = guidSubject;
                                    else
                                        smtpSender.mailSubject = txtSubject.Text;

                                    smtpSender.mailBody = dtPicker.Value.ToLongTimeString() + "\r\n" + guidSubject;
                                    bSent = smtpSender.SmtpSend();
                                    if(bSent)
                                    {
                                        lock(this)
                                        {
                                            //m_sentMail++;
                                            IAsyncResult r = BeginInvoke( m_delegateNumMailCtrl, new object[] { ++m_sentMail } );
                                        }
                                    }
                                }//end of if                    
                    }//end of try - SMTP exceotion
                    catch(SMTPException smtpex)
                    {
                        Trace.WriteLine( "\tSMTPClientSender() Exception: " + smtpex.SmtpMessage.ToString() );
                        commObj.LogToFile( "\tSMTPClientSender() Exception: " + smtpex.SmtpMessage.ToString() );
                        //MessageBox.Show( smtpex.SmtpMessage.ToString(), "SMTP Exception" );
                    }//end of catch
                    catch(IOException ioex)
                    {
                        commObj.LogToFile( "\tSMTPClientSender() IOException: " + ioex.Message.ToString() );
                        //MessageBox.Show( ioex.Message.ToString(), "IO Exception" );
                    }
                    catch(Exception ex)
                    {
                        commObj.LogToFile( "\tSMTPClientSender() Generic Exception: " + ex.Message.ToString() );
                        //MessageBox.Show( ex.Message, msgCaption );
                    }
                }//end of for				
            }//end of outer try            
            catch (Exception gex)
            {
                Trace.WriteLine("\t Generic Exception: " + gex.ToString());
                MessageBox.Show(gex.Message, msgCaption);
                //MessageBox.Show("Thread Abort!", msgCaption);
            }//end of catch generic exception
            finally
            {
                lock (this)
                {
                    //IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { --m_thdCount });
                    if (--m_thdCount == 0)
                    {
                        BeginInvoke(m_delegateJobDoneNotification, new object[] { m_thdCount });
                        commObj.LogToFile("Job Done Notification - SMTP mail thread : " + m_thdCount);
                        OnUpdateStatusBar(new StatusEventArgs("Thread Count = " + m_thdCount.ToString()));
                    }
                } 
            }//end of finally           
        }//end of Thd_SendSmtpMail

        private void rdoDefault_Click(object sender, EventArgs e)
        {
            dtPicker.Enabled = true;

            txtInputFile.Enabled = false;
            txtMailFolder.Enabled = false;

            btnBroMailFile.Enabled = false;
            btnBroFolder.Enabled = false;

        }//end of rdoDefault_Click

        private void rdoInputFile_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - rdoInputFile_Click");
            txtInputFile.Enabled = true;
            txtMailFolder.Enabled = false;

            btnBroMailFile.Enabled = true;
            btnBroFolder.Enabled = false;

            dtPicker.Enabled = false;

            //if (File.Exists(txtInputFile.Text))
            //    return;

        }//end of rdoInputFile_Click

        private void rdoMsgFolder_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("UcMailSender.cs - rdoMsgFolder_Click");
            txtInputFile.Enabled = false;
            txtMailFolder.Enabled = true;
            btnBroMailFile.Enabled = false;
            btnBroFolder.Enabled = true;

            dtPicker.Enabled = false;
        }//end of rdoMsgFolder_Click

        private void UcMailSender_Load(object sender, EventArgs e)
        {
            SplashScreen.SetStatus("Loading Mail Sender");
            LoadComboBoxes(); // cannot do in constructor
        }//end of UcMailSender_Load

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMailSender.cs - btnAbort_Click");
            try
            {
                OnUpdateStatusBar(new StatusEventArgs("Mail Sender Panel: Abort! Thread Count = " + m_thdCount.ToString(CultureInfo.CurrentCulture)));

                for (int i = 0; i < m_initialThread; i++)
                {
                    if (m_thdList[i] != null && m_thdList[i].IsAlive)
                        KillMailSenderThread(ref m_thdList[i]);
                }//end of for

                // reset mouse cursor and enable control
                IAsyncResult r = BeginInvoke(m_delegateJobDoneNotification, new object[] { -99 });
            }//end of try
            catch (Exception ex)
            {
                commObj.LogToFile("UcmailSender.cs - btnAbort_Click " + ex.Message);
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace,"Abort", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }//end of catch
        }//end of btnAbort_Click

        private void KillMailSenderThread(ref Thread oneThd)
        {
            Debug.WriteLine("KillMailSenderThread");
            try
            {
                oneThd.Abort(); // abort
                oneThd.Join(); // require for ensure the thread kill

            }//end of try
            catch (ThreadAbortException thdEx)
            {
                Debug.WriteLine(thdEx.Message);
                commObj.LogToFile("Thread.log", "\t Exception ocurr in KillMailSenderThread:" + oneThd.Name);                
            }//end of catch - ThreadAbortException thdEx
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }//end of catch
        }//end of KillMailSenderThread

        public void JobDoneHandler(int thdId)
        {
            Debug.WriteLine("UcMailSender.cs - +++++++ JobDoneHandler ++++++++");

            m_procEndTime = DateTime.Now;
            txtEndTime.Text = m_procEndTime.ToString(CultureInfo.CurrentCulture);
            txtNumMail.Text = m_sentMail.ToString(CultureInfo.CurrentCulture);

            TimeSpan timeSpan = m_procEndTime - m_procStartTime;
            txtDuration.Text = timeSpan.TotalSeconds.ToString(CultureInfo.CurrentCulture);
            
            double mailPerSec = m_sentMail / timeSpan.TotalSeconds;
            txtMailPerSec.Text = mailPerSec.ToString(CultureInfo.CurrentCulture);

            if (rdoSMTP.Checked)
            {
                EnableNormalCaseCtrl(false);
                EnableSmtpCaseCtrl(true);
                EnableFileCaseCtrl(false);
            }
            else
                if (rdoNormal.Checked)
                {
                    EnableNormalCaseCtrl(true);
                    EnableSmtpCaseCtrl(false);
                    EnableFileCaseCtrl(false);
                }
                else
                    if (rdoFileCase.Checked)
                    {
                        EnableNormalCaseCtrl(false);
                        EnableSmtpCaseCtrl(false);
                        EnableFileCaseCtrl(true);
                    }

            EnableOtherCtrl(true);
            btnSend.Enabled = true;
            //btnAbort.Enabled = false;


            string msg = "Thread " + thdId.ToString() + " - Mail Send Duration: " + txtDuration.Text + "\r\n"
                + "Total Sent Files: " + txtNumMail.Text + "\r\n"
                + "Mails per second: " + txtMailPerSec.Text;

            commObj.LogToFile(msg);
        }//end of JobDoneHandler

        private void btnBroMailFile_Click(object sender, EventArgs e)
        {
            string inFile = "";
            if( File.Exists(txtInputFile.Text) )
                inFile = txtInputFile.Text;

            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.DefaultExt = "eml";
            ofDlg.Filter = "eml files (*.eml)|*.eml|All files (*.*)|*.*";
            //ofDlg.CheckFileExists = File.Exists(txtInputFile.Text);
            ofDlg.RestoreDirectory = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                txtInputFile.Text = ofDlg.FileName;
            }//end of if
            else
            {
                txtInputFile.Text = inFile;
            }
        }//end of btnBroMailFile_Click

        private void btnBroFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.RootFolder = Environment.SpecialFolder.MyComputer; // set the default root folder
            if (!String.IsNullOrEmpty(txtMailFolder.Text))
            {
                if (Directory.Exists(txtMailFolder.Text))
                    fbDlg.SelectedPath = txtMailFolder.Text;  // set the default folder
            }

            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                txtMailFolder.Text = fbDlg.SelectedPath;
            }

        }// emd pf btnBroFolder_Click

        private void lnkFile_Click(object sender, EventArgs e)
        {
            string inFile = "";
            if (File.Exists(txtMailAddrFile.Text))
                inFile = txtMailAddrFile.Text;

            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.Filter = "excel files (*.xls)|*.xls|csv files (*.csv)|*.csv|text files (*.txt)|*.txt|All files (*.*)|*.*";
            //ofDlg.CheckFileExists = File.Exists(txtInputFile.Text);
            ofDlg.RestoreDirectory = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                txtMailAddrFile.Text = ofDlg.FileName;
            }//end of if
            else
            {
                txtMailAddrFile.Text = inFile;
            }
        }

        // For dynamic tool tip
        #region Mouse Event
        private void cboFileFrom_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboFileFrom, "File contains FROM address.\r\nHit Enter to save the current value.\r\n" + cboFileFrom.Text);
        }

        private void cboTo_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboTo, "Hit Enter to save the current value.\r\n" + cboTo.Text);
        }

        private void cboCC_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboCC, "Hit Enter to save the current value.\r\n" + cboCC.Text);
        }

        private void cboBCC_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboBCC, "Hit Enter to save the current value.\r\n" + cboBCC.Text);
        }

        private void cboHeader_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboHeader, "ZANTAZ Header. \r\nHit Enter to save the current value.\r\n" + cboHeader.Text);
        }

        private void cboHeaderVal_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboHeaderVal, "ZANTAZ Value. \r\nHit Enter to save the current value.\r\n" + cboHeaderVal.Text);
        }

        private void cboMailFrom_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboMailFrom, "Hit Enter to save the current value.\r\n" + cboMailFrom.Text);
        }

        private void cboRcptTo_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(cboRcptTo, "Hit Enter to save the current value.\r\n" + cboRcptTo.Text);
        }

        private void txtInputFile_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtInputFile, "EML file that stream into socket.\r\n" + txtInputFile.Text);
        }

        private void txtMailFolder_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtMailFolder, "Folder contains EML files.\r\n" + txtMailFolder.Text);
        }

        private void txtStartTime_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtStartTime, txtStartTime.Text);
        }

        private void txtEndTime_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtEndTime, txtEndTime.Text);
        }

        private void txtDuration_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtDuration, txtDuration.Text);
        }

        private void txtNumMail_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtNumMail, txtNumMail.Text);
        }

        private void txtMailPerSec_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtMailPerSec, txtMailPerSec.Text);
        }

        private void txtMailsSize_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtMailsSize, txtMailsSize.Text);
        }

        private void txtAveSize_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtAveSize, txtAveSize.Text);
        }

        private void txtSubject_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtSubject, txtSubject.Text);
        }

        private void txtFolder_MouseEnter(object sender, EventArgs e)
        {
            ttpBatchMail.SetToolTip(txtFolder, "Folder contains attachments.\r\n" + txtFolder.Text);
        }
        #endregion
     

    }
}
