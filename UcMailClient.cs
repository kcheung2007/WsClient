using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

namespace WsClient
{
    public partial class UcMailClient : UserControl
    {
        private int _w = 1; // default width 
        private int _h = 1; // default height
        private int _delay = 0;
        private int _loop = 1;
        private int _sentCount = 0; // number of mail sent out
        private int _failCount = 0; // number of fail count
        private bool _ckGuid = false; // default don't include guid
        private bool _ckMultiAttach = false; // for UI mailing
        private bool _ckAttach = false; // for the file mailing
        private string _addrFile = "";
        private string _attachFolder = ""; // for file
        private string _attachFile = ""; //for UI
        private string _password = ""; // Outlook Profile password
        private string _profile = ""; // Outlook profile
        private string _to = "";
        private string _cc = "";
        private string _bcc = "";
        private string _txtSubject = ""; // default subject - variable reuse for UI update
        private string _richBox = ""; // default richard text box - variable reuse for UI update

        
        private Thread mailThread;
        private WsClient.CommObj commObj = new CommObj();
        private WsClient.AttachObj attachObj = null;

        private delegate void DelegateUpdate_txtSubject(string szSubject);
        private DelegateUpdate_txtSubject m_delegateShowSubject;

        private delegate void DelegateUpdate_richBox(string szMsg);
        private DelegateUpdate_richBox m_delegateDisplayMsg;


        public int DefaultWidth
        {
            get { return (_w); }
            set { _w = value; }
        }

        public int DefaultHeight
        {
            get { return (_h); }
            set { _h = value; }
        }

        public UcMailClient()
        {
            InitializeComponent();
            m_delegateShowSubject = new DelegateUpdate_txtSubject(Update_txtSubject);
            m_delegateDisplayMsg = new DelegateUpdate_richBox(Update_richBox);
        }

        private void Update_txtSubject(string szSubject)
        {
            txtSubject.Text = szSubject;
        }//end of Update_txtSubject

        private void Update_richBox(string szMsg)
        {
            richBox.Text = szMsg;
        }//end of Update_richBox

        // "UpdateStatusEventHandler" same as delegate above
        public event EventHandler<StatusEventArgs> statusChanged;
        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged(this, eArgs);
        }//end of virtual OnUpdateStatusBar

        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcMailClient.cs - SaveComboBoxItem");
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\InitMailClient.xml";
                if (!File.Exists(cboPath))
                {
                    using (StreamWriter sw = File.CreateText(cboPath))
                    {
                    }//end of using - for auto close etc... yes... empty
                }

                // Save the combox
                tw = new XmlTextWriter(Application.StartupPath + "\\InitMailClient.xml", System.Text.Encoding.UTF8);

                Debug.WriteLine("\t ComboBox Item file" + Application.StartupPath + "\\InitMailClient.xml");

                tw.WriteStartDocument();
                tw.WriteStartElement("InitData");

                //The order is important - match with InitMailSender.xml and switch case in load combo box
                WriteComboBoxEntries(cboProfile, "cboProfile", cboProfile.Text, tw); //nodeList.Item(0)
                WriteComboBoxEntries(cboTo, "cboTo", cboTo.Text, tw); //nodeList.Item(1)
                WriteComboBoxEntries(cboCC, "cboCC", cboCC.Text, tw); //nodeList.Item(2)
                WriteComboBoxEntries(cboBCC, "cboBCC", cboBCC.Text, tw); //nodeList.Item(3)

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
            Debug.WriteLine("UcMailClient.cs - WriteComboBoxEntries");
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

        /// <summary>
        /// Load the text value into combo boxes. (OK... hardcode)
        /// </summary>
        private void LoadComboBoxes()
        {
            Debug.WriteLine("UcMailClient.cs - LoadComboBoxes");
            try
            {
                cboProfile.Items.Clear();
                cboTo.Items.Clear();
                cboCC.Items.Clear();
                cboBCC.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\InitMailClient.xml";
                if (!File.Exists(cboPath))
                {
                    //File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load(cboPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                int numComboBox = nodeList.Count;
                int x;
                for (int i = 0; i < numComboBox; i++) // Order is important here
                {
                    switch (nodeList.Item(i).Attributes.GetNamedItem("name").InnerText)
                    {
                        case "cboProfile":
                            for (x = 0; x < nodeList.Item(0).ChildNodes.Count; ++x)
                            {
                                cboProfile.Items.Add(nodeList.Item(0).ChildNodes.Item(x).InnerText);
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
                    }//end of switch
                }//end of for

                if (0 < cboProfile.Items.Count)
                    cboProfile.Text = cboProfile.Items[0].ToString();
                if (0 < cboTo.Items.Count)
                    cboTo.Text = cboTo.Items[0].ToString();
                if (0 < cboCC.Items.Count)
                    cboCC.Text = cboCC.Items[0].ToString();
                if (0 < cboBCC.Items.Count)
                    cboBCC.Text = cboBCC.Items[0].ToString();
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
        private void cboProfile_KeyDown(object sender, KeyEventArgs e)
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


        private void cboProfile_KeyPress(object sender, KeyPressEventArgs e)
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

        /// <summary>
        /// Set the initial data from UI. 
        /// </summary>
        private void SetInitData()
        {
            if (rdoFile.Checked)
            {
                _addrFile = txtFile.Text;
                _attachFolder = txtFolder.Text;
                _ckAttach = chkAttach.Checked;
            }
            else
                if (rdoUI.Checked)
                {
                    _password = txtPassword.Text;
                    _profile = cboProfile.Text;
                    _to = cboTo.Text;
                    _cc = cboCC.Text;
                    _bcc = cboBCC.Text;
                    _ckMultiAttach = chkMultiAttach.Checked;
                    _attachFile = txtAttach.Text;
                }

            _txtSubject = txtSubject.Text;
            _richBox = richBox.Text;
            _delay = (int)nudDelay.Value;
            _loop = (int)nudLoop.Value;
            _ckGuid = chkGUID.Checked;

            _sentCount = 0; //reset
            _failCount = 0;
        }//end of void SetInitData

        private void EnableFileControls(bool value)
        {
            lnkFile.Enabled = value;
            txtFile.Enabled = value;
            chkAttach.Enabled = value;
            lnkFolder.Enabled = value;
            txtFolder.Enabled = value;

            rdoFile.Checked = value;

        }//end of EnableFileControls

        private void EnableUIControls(bool value)
        {
            lnkProfile.Enabled = value;
            cboProfile.Enabled = value;
            txtPassword.Enabled = value;
            lnkTo.Enabled = value;
            cboTo.Enabled = value;
            lnkCC.Enabled = value;
            cboCC.Enabled = value;
            lnkBCC.Enabled = value;
            cboBCC.Enabled = value;
            lnkAttach.Enabled = value;
            txtAttach.Enabled = value;
            chkMultiAttach.Enabled = value;

            rdoUI.Checked = value; // checked

        }//end of EnableUIControls

        /// <summary>
        /// For the UI during sending mail, pass false value.
        /// 1) Set the wait hour cursor.
        /// 2) Disable send button.
        /// 3) Enable abort button.
        /// </summary>
        /// <param name="value"></param>
        private void EnableOtherControls(bool value)
        {
            btnSend.Enabled = value;
            btnAbort.Enabled = !value;

            this.Cursor = value ? Cursors.Default : Cursors.WaitCursor;
        }//end of EnableOtherControls

        private void rdoOutlook_Click(object sender, EventArgs e)
        {
            lnkProfile.Text = "Profile";
            cboProfile.Text = cboProfile.Items[0].ToString();
        }//end of rdoOutlook_Click

        private void rdoLotus_Click(object sender, EventArgs e)
        {
            lnkProfile.Text = "Item";
            cboProfile.Text = "memo";
        }//end of rdoLotus_Click

        private void rdoUI_Click(object sender, EventArgs e)
        {
            EnableUIControls(true);
            EnableFileControls(false);
        }

        private void rdoFile_Click(object sender, EventArgs e)
        {
            EnableUIControls(false);
            EnableFileControls(true);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            richBox.Text = "";
            if (rdoOutlook.Checked)
            {
                HandleOutlookCase();
            }
            else
                if (rdoLotus.Checked)
                {
                    HandleLotusCase();
                }
        }//end of btnSend_Click

        /// <summary>
        /// Handle mail sending via Outlook client by using COM
        /// </summary>
        private void HandleOutlookCase()
        {
            SetInitData();
            if (chkAttach.Checked)
            {
                attachObj = new AttachObj(txtFolder.Text);
            }// end of if - attachment check

            mailThread = new Thread(new ThreadStart(this.Thd_SendOutlookMail));
            mailThread.Name = "OutlookMailThread";
            mailThread.Start();

            commObj.LogToFile("Thread.log", "++ OutlookMailThread Start ++");
        }//end of HandleOutlookCase

        /// <summary>
        /// Send mail by using outlook in threading manner
        /// </summary>
        private void Thd_SendOutlookMail()
        {
            Trace.WriteLine("UcMailClient.cs - Thd_SendOutlookMail()");
            if (rdoUI.Checked) // selected - info from UI
            {
                OlUiSendMail();
            }//end of if - select info from UI
            else
                if (rdoFile.Checked)
                {
                    OlFileSendMail();
                }//end of if - select info from file
        }// end of Thd_SendOutlookMail

        /// <summary>
        /// Send mail by using Lotus Notes client COM object in threading manner 
        /// </summary>
        private void Thd_SendLotusNotesMail()
        {
            if (rdoUI.Checked) // selected - info from UI
            {
                LnUiSendMail();
            }//end of if - select info from UI
            else
                if (rdoFile.Checked)
                {
                    LnFileSendMail();
                }//end of if - select info from file
           
        }//end of Thd_SendLotusNotesMail

        /// <summary>
        /// </summary>
        private void OlUiSendMail()
        {
            string strGUID = "";
            string tmpSubject = _txtSubject;
            WsClient.olMailObj olmailObj = new olMailObj();
            for (int j = 0; j < _loop; j++)
            {                
                try
                {
                    if (_ckGuid)
                    {
                        strGUID = System.Guid.NewGuid().ToString();
                        tmpSubject = _txtSubject + j.ToString() + " " + strGUID;
                        //txtSubject.Text = txtSubject.Text + j.ToString() + " " + strGUID;

                        commObj.LogGUID("GUID.LOG", strGUID);
                    }//end of if - GUID			

                    olmailObj.strPassword = _password;
                    olmailObj.strProfile = _profile;
                    olmailObj.strTo = _to;
                    olmailObj.strCC = _cc;
                    olmailObj.strBCC = _bcc;
                    olmailObj.strSubject = tmpSubject;
                    olmailObj.strBody = "Sent Mail: " + j.ToString()
                                 + "\nFrom profile: " + _profile // start from here change
                                 + "\n TO:  " + _to
                                 + "\n CC:  " + _cc
                                 + "\n BCC: " + _bcc
                                 + "\n Body_Subject: " + tmpSubject
                                 + "\n" + DateTime.Now;

                    // validate the input - trim the space before check the length
                    _attachFile.TrimStart(new char[] { ' ' });
                    if (0 < _attachFile.Length)
                    {
                        commObj.LogToFile("Attachment - " + _attachFile);
                        olmailObj.strAttachName = _attachFile;
                    }//end of if - attachment

                    if (olmailObj.dumpToOutbox())
                    {
                        _sentCount++; // may need lock here
                    }
                    else
                    {
                        _failCount++;
                    }

                    IAsyncResult rs = BeginInvoke(m_delegateShowSubject, new object[] { tmpSubject });
                    
                }//end of try
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message.ToString(), "WsClient");
                    commObj.LogGUID("olGUID.LOG", "Exception occur " + strGUID.ToString() + "\n\t" + ex.Message.ToString());
                }//end of catch - exception
            }//end of for

            string msg = "Total mail sent = " + _sentCount + "\r\n"
                       + "Total fail count = " + _failCount + "\r\n"
                       + "Please check the olGUID.LOG for detail";

            IAsyncResult ra = BeginInvoke(m_delegateDisplayMsg, new object[] { msg });

        }//end of OlUiSendMail

        /// <summary>
        /// Send notes client mail based on user input from UI. Send one mail at a time.
        /// Only create one session and then loop through it.
        /// </summary>
        private void LnUiSendMail()
        {
            Debug.WriteLine("UcMailClient.cs - LnUiSendMail");
            Domino.NotesSession domSession = null;
            //int counter = 0;
            try
            {
                domSession = new Domino.NotesSession();

                // only with computer with Domino Server installed
                // domSession.InitializeUsingNotesUserName("atest0", "password0");

                // used on a computer with a Notes client/Domino server 
                // and bases on the session on the current user ID - admin.id
                // domSession.Initialize("");
                domSession.Initialize(txtPassword.Text);
            }//end of try
            catch (Exception exSession)
            {
                string msg = "Fail in creating Notes Session\n" + exSession.Message + "\n"
                    + exSession.GetType().ToString() + "\n";
                MessageBox.Show(msg, "UcMailClient", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Debug.WriteLine(msg + "\n" + exSession.StackTrace);

                Debug.WriteLine("NotesClient.cs -   Session Fail: releaseing session COM Object");
                System.Runtime.InteropServices.Marshal.ReleaseComObject(domSession);
            }//end of catch - notes session exception

            string strGUID = "";
            string strTmpSubj = "";
            try
            {
                Domino.NotesDbDirectory domDBDir = domSession.GetDbDirectory("");
                for (int j = 0; j < _loop; j++)
                {
                    if (chkGUID.Checked)
                    {
                        strGUID = System.Guid.NewGuid().ToString();
                        strTmpSubj = _txtSubject + j.ToString() + " " + strGUID;
                        commObj.LogGUID("lnGUID.LOG", strGUID);
                    }//end of if - GUID

                    _richBox = "\nUser name: " + domSession.UserName
                             + "\nServer name: " + domSession.ServerName; // server name is null                

                    string strTo = _to;
                    string strCC = _cc;
                    string strBCC = _bcc;

                    string[] toArray = strTo.Split(new char[] { ',', ';' });
                    string[] ccArray = strCC.Split(new char[] { ',', ';' });
                    string[] bccArray = strBCC.Split(new char[] { ',', ';' });
                    string inSubject = j.ToString() + " " + strTmpSubj;
                    string bodyText = "";
                    string notesItems = _profile; // actually it is note item, not profile. reuse the combo box

                    Domino.NotesDatabase notesDB;
                    Domino.NotesDocument notesDoc;
                    Domino.NotesItem docForm;
                    Domino.NotesItem docSubject;
                    Domino.NotesItem docCopyTo;  // CC
                    Domino.NotesItem docBlindCC; // BCC
                    Domino.NotesRichTextItem docRTFBody;

                    Object recipients = toArray;
                    Object carboncopy = ccArray;
                    Object blindcopy = bccArray;

                    notesDB = domDBDir.OpenMailDatabase();
                    notesDoc = notesDB.CreateDocument();

                    docForm = notesDoc.ReplaceItemValue("Form", notesItems);
                    docSubject = notesDoc.ReplaceItemValue("Subject", inSubject);
                    docCopyTo = notesDoc.ReplaceItemValue("CopyTo", carboncopy);
                    docBlindCC = notesDoc.ReplaceItemValue("BlindCopyTo", blindcopy);
                    docRTFBody = notesDoc.CreateRichTextItem("Body");

                    bodyText = _richBox
                        + "\n To: " + _to
                        + "\n CC: " + _cc
                        + "\n BCC: " + _bcc
                        + "\n Notes_Subject: " + inSubject
                        + "\n " + DateTime.Now + "\n";

                    Trace.WriteLine("Before docRTFBody.AppendText " + docRTFBody.Values.ToString());
                    docRTFBody.AppendText(bodyText);
                    Trace.WriteLine("docRTFBody.AppendText( bodyText )");

                    string fn = _attachFile;
                    if (fn != "")
                    {
                        bodyText += fn;
                        char[] delim = new char[] { ';' };
                        foreach (string str in fn.Split(delim))
                        {
                            docRTFBody.EmbedObject(Domino.EMBED_TYPE.EMBED_ATTACHMENT, "", str, "");
                        }//end of foreach
                    }//end of if - adding attachment

                    // update UI info:
                    IAsyncResult rs = BeginInvoke(m_delegateShowSubject, new object[] { inSubject });
                    
                    notesDoc.Send(false, ref recipients); // send notes mail
                    _sentCount++;
#if(DEBUG)
                    commObj.LogToFile("Notes Document sent");
#endif
                }//end of for
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + "\n" + ex.StackTrace;
                MessageBox.Show(msg, "Handle UI Info", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                _failCount++;
            }//end of catch - exception
            finally
            {
                Debug.WriteLine("NotesClient.cs - finally - releaseing session COM Object");
                System.Runtime.InteropServices.Marshal.ReleaseComObject(domSession);                
            }//end of finally

            string info = "Total mail sent = " + _sentCount + "\r\n"
                       + "Total fail count = " + _failCount;

            IAsyncResult ra = BeginInvoke(m_delegateDisplayMsg, new object[] { info });
        }//end of LnUiSendMail
        private void OlFileSendMail()
        {
            commObj.LogToFile("UcMailClient.cs +++ Enter HandleFileSendMail()");
            WsClient.olMailObj olmailObj = new olMailObj();

            if (chkAttach.Checked)
                attachObj = new AttachObj(txtFolder.Text);

            //int counter = 0; // mail sent count
            string tmpSubj = _txtSubject; // save the user input subject
            string tmpBody = _richBox;// save the rich Box info

#if(DEBUG)
            commObj.LogToFile("\t Ready get into for loop");
#endif
            int k = 0;
            for (int j = 0; j < _loop; j++)
            {
                string strGUID = "";
                string strLine = "";
                StreamReader sr = null;

                try
                {
                    sr = new StreamReader(_addrFile); // address book - put here for exception catch
#if(DEBUG)
                    commObj.LogToFile("\t Inside for loop - create stream reader");
                    commObj.LogToFile("\t StreamReader = " + sr.ToString());
#endif                    
                    while ((strLine = sr.ReadLine()) != null) // file name from txtFrom field
                    {                        
                        Debug.WriteLine("\t - HandleFileSendMail - inside while loop : " + k.ToString(CultureInfo.CurrentCulture));
                        Debug.WriteLine( "\t - strLine = " + strLine );

                        if (strLine[0] != '#') // skip all comment
                        {
                            tmpBody = "\r\n- Read line : " + strLine;
                            if (chkGUID.Checked)
                            {
                                strGUID = System.Guid.NewGuid().ToString();
                                tmpSubj = _txtSubject + " " + strGUID;
                                //commObj.LogGUID( "GUID.LOG", strGUID );
                            }//end of if - GUID			

                            string[] splitStr = new string[7];
                            splitStr = strLine.Split(new Char[] { ',' });

                            Debug.WriteLine( "spliteStr length = " + splitStr.Length );

                            for(int m = 0; m < splitStr.Length; m++) // trim leading and ending space
                            {
                                splitStr[m] = splitStr[m].Trim( ' ' );
                            }

                            if (splitStr.Length == 5) // must exactly 5 cloumn
                            {
                                Debug.WriteLine( "splitStr[0] = " + splitStr[0] );
                                Debug.WriteLine( "splitStr[1] = " + splitStr[1] );
                                Debug.WriteLine( "splitStr[2] = " + splitStr[2] );
                                Debug.WriteLine( "splitStr[3] = " + splitStr[3] );
                                Debug.WriteLine( "splitStr[4] = " + splitStr[4] );

                                olmailObj.strTo = splitStr[0];
                                olmailObj.strCC = splitStr[1];
                                olmailObj.strBCC = splitStr[2];
                                olmailObj.strProfile = splitStr[3];
                                olmailObj.strPassword = splitStr[4];

                                olmailObj.strSubject = tmpSubj;
                                olmailObj.strBody = tmpBody	// unchange user info
                                    + "\n TO:  " + splitStr[0]
                                    + "\n CC:  " + splitStr[1]
                                    + "\n BCC: " + splitStr[2]
                                    + "\n Body_Subject: " + tmpSubj
                                    + "\n" + DateTime.Now;

                                if (chkAttach.Checked)
                                {
                                    if (attachObj.IdxAttach == attachObj.NumFile)
                                        attachObj.IdxAttach = 0; //reset

                                    Debug.WriteLine(attachObj.IdxAttach, "\t - idxAttach");
                                    Debug.WriteLine(attachObj.AttachFullName, "\t - filename");
                                    olmailObj.strAttachName = attachObj.AttachFullName;

                                    olmailObj.strBody += "\r\nAttach file index = " + attachObj.IdxAttach
                                                       + "\r\nAttach file name = " + attachObj.AttachFullName;

                                    attachObj.IdxAttach++; // point to next file
                                }//end of if		

                                Debug.WriteLine( "Dump to mail box la" );
                                if (olmailObj.dumpToOutbox())
                                    _sentCount++;
                                else
                                    _failCount++;

                                if (chkGUID.Checked)
                                {	// log the guid after dumpToOutbox
                                    commObj.LogGUID("GUID.LOG", strGUID);
                                }//end of if - GUID
                            }//end of if - correct file 
                        }//end of if - skip comment                        
                        Thread.Sleep(_delay); // in millium sec
                        k++; // total attemp of sending mail
                    }//end of while

                    olmailObj.nsLogoff();
                }//end of try
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message.ToString(), "\t Exception");
                    commObj.LogGUID("olGUID.LOG", "Exception occur " + strGUID.ToString() + "\n\t" + ex.Message.ToString() + ex.StackTrace);
                }//end of catch - exception
                finally
                {
                    if (sr != null)
                    {
                        Trace.WriteLine("Finally - close the Stream Reader");
                        sr.Close();
                    }//end of if

                    string msg = "Total mail sent = " + _sentCount + "\r\n"
                               + "Total fail count = " + _failCount + "\r\n"
                               + "Please check the olGUID.LOG for detail";

                    IAsyncResult rs = BeginInvoke(m_delegateDisplayMsg, new object[] { msg });

                }//end of finally - clean up everything
            }//end of for

            commObj.LogToFile("OutlookPage.cs +++ End of HandleFileSendMail() +++");

        }//end of OlFileSendMail

        /// <summary>
        /// Read from a file for all senders, receivers
        /// Text file format: To, CC, BCC, From, password.
        /// Each field separated by comma, mail addresses in each field separated by semi-colon;
        /// Two Loops:
        /// 1) Inner loop: sending mail based on the number of 'TO' users in the text file within a session.
        /// 2) Outer loop: repeat the whole mailing list in the file. Each loop will create a new session.
        /// Therefore, total number of sent mail == inner loop x outer loop.
        /// </summary>
        private void LnFileSendMail()
        {
            Trace.WriteLine("UcMailClient.cs - HandleFileSendMail()");

//            int counter = 0; // mail sent count
            string tmpSubj = _txtSubject; // save the user input subject
            string tmpBody = _richBox;	  // save the rich Box info

            string strTo = "";
            string strCC = "";
            string strBCC = "";

            string[] toArray;
            string[] ccArray;
            string[] bccArray;
            string inSubject = "";
            //string bodyText = "";
            string notesItems = "";

            Domino.NotesDatabase notesDB;
            Domino.NotesDocument notesDoc;
            Domino.NotesItem docForm;
            Domino.NotesItem docSubject;
            Domino.NotesItem docCopyTo;  // CC
            Domino.NotesItem docBlindCC; // BCC
            Domino.NotesRichTextItem docRTFBody;

            Object recipients;
            Object carboncopy;
            Object blindcopy;

            StreamReader sr = null;

            //StreamReader sr = new StreamReader( txtFile.Text ); // address book - put here for exception catch
            if (chkAttach.Checked) // point to attachment folder ONLY
                attachObj = new AttachObj(_attachFolder);

            for (int j = 0; j < _loop; j++)
            {
                string strGUID = "";
                string strLine = "";

                Trace.WriteLine("\t inside for loop: Open Domino Session");
                Domino.NotesSession domSession = new Domino.NotesSession();
                try
                {   // only with computer with Domino Server installed
                    // domSession.InitializeUsingNotesUserName("atest0", "password0");

                    // used on a computer with a Notes client/Domino server 
                    // and bases on the session on the current user ID - admin.id
                    // domSession.Initialize("");

                    // TO DO: Need to modify, should NOT reuse the UI password field.
                    domSession.Initialize(txtPassword.Text);
                }//end of try
                catch (Exception exSession)
                {
                    string msg = "Fail in creating Notes Session\n" + exSession.Message + "\n"
                        + exSession.GetType().ToString() + "\n" + exSession.StackTrace;
                    MessageBox.Show(msg, "WsClient", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    Debug.WriteLine("\t Session Fail in LnFileSendMail: releaseing session COM Object");
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(domSession);
                }//end of catch - notes session exception

                try
                {
                    Domino.NotesDbDirectory domDBDir = domSession.GetDbDirectory("");
                    sr = new StreamReader(_addrFile); // address book - put here for exception catch

                    Debug.WriteLine("\t Inside for loop - create stream reader");
                    Debug.WriteLine("\t StreamReader = " + sr.ToString());

                    while ((strLine = sr.ReadLine()) != null) // file name from txtFrom field
                    {
                        _sentCount++;
                        Debug.WriteLine( "\t - LnFileSendMail - inside while loop : " + _sentCount.ToString( CultureInfo.CurrentCulture ) );
                        if (strLine[0] != '#') // skip all comment - first character
                        {
                            _richBox = "\r\n- Read line : " + strLine;
                            if (chkGUID.Checked)
                            {
                                strGUID = System.Guid.NewGuid().ToString();
                                _txtSubject = tmpSubj + " " + strGUID;
                                // commObj.LogGUID( "GUID.LOG", strGUID );
                            }//end of if - GUID			

                            string[] splitStr = new string[5]; // match with the colum of input text file						
                            splitStr = strLine.Split(new Char[] { ',' }); // each field separated by comma

                            Trace.WriteLine("string[0] = " + splitStr[0].ToString());
                            Trace.WriteLine("string[1] = " + splitStr[1].ToString());

                            for (int k = 0; k < splitStr.Length; k++) // trim leading and ending space
                            {
                                Debug.WriteLine( "k = " + k.ToString() + " splitStr = " + splitStr[k].ToString( CultureInfo.CurrentCulture ) );
                                splitStr[k] = splitStr[k].Trim(' ');
                            }//end of for

                            Debug.WriteLine("after for loop - triming leading and ending space");
                            if (splitStr.Length == 5) // must exactly 5 cloumn
                            {
                                Debug.WriteLine("split string == 5. Inside if");
                                strTo = splitStr[0];
                                strCC = splitStr[1];
                                strBCC = splitStr[2];

                                toArray = strTo.Split(new char[] { ';' });
                                ccArray = strCC.Split(new char[] { ';' });
                                bccArray = strBCC.Split(new char[] { ';' });
                                inSubject = _sentCount.ToString( CultureInfo.CurrentCulture ) + " " + _txtSubject;
                                //bodyText = _richBox;
                                notesItems = "memo"; // hard code

                                recipients = toArray;
                                carboncopy = ccArray;
                                blindcopy = bccArray;

                                Debug.WriteLine("Will open domino DB ");
                                notesDB = domDBDir.OpenMailDatabase();
                                Debug.WriteLine("After open domino DB " + notesDB.ToString());

                                notesDoc = notesDB.CreateDocument();
                                docForm = notesDoc.ReplaceItemValue("Form", notesItems);
                                docSubject = notesDoc.ReplaceItemValue("Subject", inSubject);
                                docCopyTo = notesDoc.ReplaceItemValue("CopyTo", carboncopy);
                                docBlindCC = notesDoc.ReplaceItemValue("BlindCopyTo", blindcopy);
                                docRTFBody = notesDoc.CreateRichTextItem("Body");

                                _richBox = tmpBody
                                    + "\n To: " + _to
                                    + "\n CC: " + _cc
                                    + "\n BCC: " + _bcc
                                    + "\n Notes_Subject: " + _txtSubject
                                    + "\n " + DateTime.Now + "\n";
                                docRTFBody.AppendText(_richBox);

                                if (chkAttach.Checked)
                                {
                                    if (attachObj.IdxAttach == attachObj.NumFile)
                                        attachObj.IdxAttach = 0; //reset

                                    Debug.WriteLine(attachObj.IdxAttach, "\t - idxAttach");
                                    Debug.WriteLine(attachObj.AttachFullName, "\t - filename");
                                    docRTFBody.EmbedObject(Domino.EMBED_TYPE.EMBED_ATTACHMENT, "", attachObj.AttachFullName, "");

                                    _richBox += "\r\nAttach file index = " + attachObj.IdxAttach
                                             + "\r\nAttach file name = " + attachObj.AttachFullName;

                                    attachObj.IdxAttach++; // point to next file
                                }//end of if

                                // update UI info:
                                //txtSubject.Text = inSubject;
                                //richBox.Text = bodyText;

                                Trace.WriteLine("Just before sending mail");
                                notesDoc.Send(false, ref recipients); // send notes mail
                                _sentCount++;
                                Trace.WriteLine("Just after sending mail");

                                _richBox = ""; // reset
                                _txtSubject = tmpSubj;
                                if (chkGUID.Checked)
                                {	// log the guid after dumpToOutbox
                                    commObj.LogGUID("GUID.LOG", strGUID);
                                }//end of if - GUID
                            }//end of if - correct file 
                        }//end of if - skip comment                        
                        Thread.Sleep(_delay); // 2 sec
                    }//end of while
                }//end of try
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message.ToString(), "\t Exception");
                    commObj.LogGUID("lnGUID.LOG", "Exception occur " + strGUID.ToString() + "\r\n" + ex.Message.ToString());
                    _failCount++;
                }//end of catch - exception
                finally
                {
                    if (sr != null)
                    {
                        Trace.WriteLine("Finally - close the Stream Reader");
                        sr.Close();
                    }//end of if

                    string msg = "Total mail sent = " + _sentCount + "\r\n"
                               + "Total fail count = " + _failCount + "\r\n"
                               + "Please check the lnGUID.LOG for detail";

                    IAsyncResult rs = BeginInvoke(m_delegateDisplayMsg, new object[] { msg });
                }//end of finally - clean up everything
            }//end of for

//            richBox.Text = "Total Mail send: " + counter.ToString();

            commObj.LogToFile("WcMailClient.cs +++ End of LnFileSendMail() +++");
        }//end of LnFileSendMail

        /// <summary>
        /// Kill all thread, does not matter outlook or lotus notes
        /// </summary>
        private void KillMailThread()
        {
            Trace.WriteLine("UcMailClient.cs - KillMailThread()");
            try
            {
                commObj.LogToFile("Thread.log", "Kill MailT hread Start");
                mailThread.Abort(); // abort
                mailThread.Join();  // require for ensure the thread kill
            }//end of try 
            catch (ThreadAbortException thdEx)
            {
                Trace.WriteLine(thdEx.Message);
                commObj.LogToFile("Aborting the send guid mail thread : " + thdEx.Message.ToString());
            }//end of catch				
        }//end of KillMailThread

        /// <summary>
        /// Handle mail sending via Lotus Notes Client by using COM
        /// </summary>
        private void HandleLotusCase()
        {
            SetInitData();
            if (chkAttach.Checked)
            {
                attachObj = new AttachObj(txtFolder.Text);
            }// end of if - attachment check

            mailThread = new Thread(new ThreadStart(this.Thd_SendLotusNotesMail));
            mailThread.Name = "LotusNotesMailThread";
            mailThread.Start();

            commObj.LogToFile("Thread.log", "++ LotusNotesMailThread Start ++");            
        }// end of HandleLotusCase

        private void lnkAttach_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] fileNames;

            OpenFileDialog ofDlg = new OpenFileDialog();
            if (chkMultiAttach.Checked)
            {
                ofDlg.Multiselect = true;
                if (ofDlg.ShowDialog() == DialogResult.OK)
                {
                    fileNames = ofDlg.FileNames;
                    foreach (string str in fileNames)
                    {
                        txtAttach.Text += ";" + str;
                    }//end of foreach

                    //check the first char
                    string tmpStr = txtAttach.Text.ToString();
                    if (tmpStr[0] == ';')
                        txtAttach.Text = txtAttach.Text.Remove(0, 1);
                }//end of if		
            }//end of if
            else
            {
                if (ofDlg.ShowDialog() == DialogResult.OK)
                {
                    txtAttach.Text = ofDlg.FileName;
                }//end of if		
            }//end of else		
        }//end of lnkAttach_LinkClicked

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("UcMailClient.cs - btnAbort_Click");
            try
            {
                OnUpdateStatusBar(new StatusEventArgs("Mail Client Panel: Abort!"));

                if (mailThread != null)
                    KillMailThread();

            }//end of try
            catch (Exception ex)
            {
                commObj.LogToFile("UcArchive.cs - btnAbort_Click " + ex.Message);
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "Abort", MessageBoxButtons.OK,  MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }//end of catch
        }//end of btnAbort_Click

        private void lnkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                if (ofDlg.FileName != "")
                {
                    txtFile.Text = ofDlg.FileName;
                }//end of if - open file name
            }// end of if - open file dialog
        }

        private void UcMailClient_Load(object sender, EventArgs e)
        {
            SplashScreen.SetStatus("Loading Main Client");
            LoadComboBoxes(); // cannot do in constructor
        }      
    }
}
