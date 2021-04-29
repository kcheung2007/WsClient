using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Windows.Forms;
using WatiN.Core;

namespace WsClient
{
    public partial class UcWebSearch : UserControl    
    {
        private string m_xmlFile = "WebSearch.xml";
        private string m_logFile = "SearchLog.txt";
        private string mySearchLogTitle = "xYz 0f SeaRcH loG file with Imp0ssible Name"; // uniqe name for killing windows
        private int m_Pass;
        private int m_Fail;
        private int m_Total;        
        private string m_searchField;

        private IE ie;
        private Thread m_thdWebSearch;
        private CommObj commObj = new CommObj();

        private delegate void DelegateJobDoneNotification(int count); // all thread done - indicate Thread index
        private DelegateJobDoneNotification m_delegateJobDoneNotification;

        public UcWebSearch()
        {
            InitializeComponent();
            m_delegateJobDoneNotification = new DelegateJobDoneNotification( JobDoneHandler );
        }

        public void JobDoneHandler(int count)
        {
            Debug.WriteLine( "UcWebSearch.cs - +++++++ JobDoneHandler ++++++++" );

            string thdName = m_thdWebSearch == null ? "" : m_thdWebSearch.Name;
            string msg = "Thread " + thdName + "Done\r\n"
                + "Total Web Search Pass: " + m_Pass;
            OutputMessage( msg );
            //commObj.LogToFile( m_logFile, msg );
        }//end of JobDoneHandler

        /// <summary>
        /// Return search field for searching based on the radio button selection
        /// Default search field is subject. Other options are Boday and Attachment.
        /// </summary>
        /// <returns>string: Search Field</returns>
        private string getSearchField()
        {
            string str = "D_SUBJECT"; //default search
            if(rdoAttachment.Checked)
                str = "D_=ATTACHMENTNAME=";
            else
                if(rdoBody.Checked)
                    str = "D_=BODY=";

            return (str);
        }//end of getSearchField

        /// <summary>
        /// Test the DS Web UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // Open a new Internet Explorer window and goto the google website.
                IE ie = new IE( cboURL.Text );

                // Find the search text field and type Watin in it.
                ie.TextField( Find.ByName( "loginName" ) ).TypeText( cboLoginID.Text );
                ie.TextField( Find.ByName( "password" ) ).TypeText( cboPassword.Text );

                // Click the Log on search button.
                ie.Button( Find.ByValue( "  Log on  " ) ).Click();

                if(ie.ContainsText( cboLoginID.Text ))
                {
                    txtDisplay.Text += "Login sucessfully";
                }
                else
                {
                    txtDisplay.Text += "Login FAIL +_+";
                    return; // exist
                }

                txtDisplay.Refresh();
                ie.Link( Find.ByText( "New Search " ) ).Click();

                // ie.TextField( Find.ByName( "Filter" ) ).TypeText( "testdomain1.ceo.repository" );
                ie.TextField( Find.ByName( "Filter" ) ).TypeText( cboRepository.Text );
                ie.Button( Find.ByValue( "Search" ) ).Click();

                if(ie.ContainsText( cboRepository.Text ))
                {
                    Debug.WriteLine( "Find " + cboRepository.Text + " - do it..." );
                    txtDisplay.Text += "\r\nFind " + cboRepository.Text + " - do it...";
                    ie.Link( Find.ByText( cboRepository.Text ) ).Click();
                }
                else // not find
                {
                    Debug.WriteLine( "Cannot find the repository... " + cboRepository.Text + " Stop here..." );
                    txtDisplay.Text += "\r\nCannot find the repository... " + cboRepository.Text + " Stop here...";
                    return;
                }

                txtDisplay.Refresh();
                Debug.WriteLine( "Set date range: " );
                Debug.WriteLine( "From: " + dtpFrom.Value.Month + "-" + dtpFrom.Value.Day + "-" + dtpFrom.Value.Year );
                txtDisplay.Text += "\r\nSet date range: ";
                txtDisplay.Text += "\r\nFrom: " + dtpFrom.Value.Month + "-" + dtpFrom.Value.Day + "-" + dtpFrom.Value.Year;


                ie.SelectList( Find.ByName( "amo" ) ).SelectByValue( dtpFrom.Value.Month.ToString() );
                ie.SelectList( Find.ByName( "ady" ) ).SelectByValue( dtpFrom.Value.Day.ToString() );
                ie.SelectList( Find.ByName( "ayr" ) ).SelectByValue( dtpFrom.Value.Year.ToString() );

                Debug.WriteLine( "To:   " + dtpTo.Value.Month + "-" + dtpTo.Value.Day + "-" + dtpTo.Value.Year );
                txtDisplay.Text += "\r\nTo:   " + dtpTo.Value.Month + "-" + dtpTo.Value.Day + "-" + dtpTo.Value.Year;

                ie.SelectList( Find.ByName( "bmo" ) ).SelectByValue( dtpTo.Value.Month.ToString() );
                ie.SelectList( Find.ByName( "bdy" ) ).SelectByValue( dtpTo.Value.Day.ToString() );
                ie.SelectList( Find.ByName( "byr" ) ).SelectByValue( dtpTo.Value.Year.ToString() );

                ie.SelectList( Find.ByName( "docLang" ) ).SelectByValue( SetLangCode( cboLanguage.Text ) );

                txtDisplay.Refresh();
                if(rdoAttachment.Checked)
                    ie.TextField( Find.ByName( "D_=ATTACHMENTNAME=" ) ).TypeText( cboSearchKey.Text );
                else
                    if(rdoBody.Checked)
                        ie.TextField( Find.ByName( "D_=BODY=" ) ).TypeText( cboSearchKey.Text );
                    else
                        ie.TextField( Find.ByName( "D_SUBJECT" ) ).TypeText( cboSearchKey.Text ); // default

                //ie.TextField( Find.ByName( "D_SUBJECT" ) ).TypeText( "乾燥肌" );
                //ie.TextField( Find.ByName( "D_=BODY=" ) ).TypeText( "できま" );

                ie.Button( Find.ByValue( "  Search  " ) ).Click();
                Debug.WriteLine( "Searching now.... wait...." );
                txtDisplay.Text += "\r\nSearching now.... wait....";
                txtDisplay.Refresh();

                while(!ie.ContainsText( "Query results complete" ))
                {
                    Thread.Sleep( 50 );
                    if(ie.ContainsText( "No messages found" ))
                        break;

                    if(ie.ContainsText( "error" ))
                        break;

                    if(ie.ContainsText( "Query in progress" ))
                    {
                        ie.Image( Find.ByAlt( "next 20 messages" ) ).Click();
                    }
                }//end of while

                if(ie.ContainsText( "Query results complete" ))
                {
                    //Regex r = new Regex( "\bNumMessages\b.*\bdocuments\b" );
                    Regex r = new Regex( ".*documents" );
                    string tmp = r.ToString();
                    string str = ie.FindText( r );
                    Debug.WriteLine( "String == " + str );
                    Debug.WriteLine( "DONE ^_^" );
                    txtDisplay.Text += "\r\nString == " + str + "\r\nDONE ^_^";
                }
                else
                    if(ie.ContainsText( "No messages found" ))
                    {
                        Debug.WriteLine( "No message found" );
                        txtDisplay.Text += "\r\nNo message found";
                        ie.Link( Find.ByText( "here" ) ).Click();
                    }

                txtDisplay.Text += "\r\nExit";
                txtDisplay.Refresh();

                if( chkCloseIE.Checked )
                    ie.Close();
            }//end of try
            catch(Exception ex )
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg );
                OutputMessage( msg );
            }//end of catch
            finally
            {
                this.Cursor = Cursors.Default;
            }            
        }//end of btnTest_Click

        private void btnRun_Click(object sender, EventArgs e)
        {
            if(!ValidateUserInput())
                return;

            txtDisplay.Text = "";
            txtFail.Text = "0";
            txtPass.Text = "0";
            txtTotal.Text = "0";

            m_Fail = m_Pass = m_Total = 0;

            commObj.DeleteFile( commObj.logFullPath + "\\" + m_logFile);
            OutputMessage( "++++++ Start +++++" + DateTime.Now.ToString());

            m_searchField = getSearchField();

            m_thdWebSearch = new Thread( new ThreadStart( this.Thd_WebSearch ) );
            m_thdWebSearch.Name = "Thd_WebSearch";
            m_thdWebSearch.SetApartmentState( ApartmentState.STA );
            m_thdWebSearch.Start();

            commObj.LogToFile( "++ Thd_WebSearch Start ++" );

        }//end of btnRun_Click

        private void Thd_WebSearch()
        {
            try
            {
                // Open a new Internet Explorer window and goto the google website.
                ie = new IE( cboURL.Text );

                // Find the search text field and type Watin in it.
                ie.TextField( Find.ByName( "loginName" ) ).TypeText( cboLoginID.Text );
                ie.TextField( Find.ByName( "password" ) ).TypeText( cboPassword.Text );

                // Click the Log on search button.
                ie.Button( Find.ByValue( "  Log on  " ) ).Click();

                if(ie.ContainsText( cboLoginID.Text ))
                {
                    Debug.WriteLine( "Login sucessfully" );
                    OutputMessage( "Login sucessfully" );                    
                }
                else
                {
                    Debug.WriteLine( "Login FAIL +_+" );
                    OutputMessage( "Login FAIL +_+" );
                    return; // exist
                }

                ie.Link( Find.ByText( "New Search " ) ).Click();

                // ie.TextField( Find.ByName( "Filter" ) ).TypeText( "testdomain1.ceo.repository" );
                ie.TextField( Find.ByName( "Filter" ) ).TypeText( cboRepository.Text );
                ie.Button( Find.ByValue( "Search" ) ).Click();

                if(ie.ContainsText( cboRepository.Text ))
                {
                    Debug.WriteLine( "Find " + cboRepository.Text + " - do it..." );                    
                    OutputMessage( "Find " + cboRepository.Text + " - do it..." );
                    ie.Link( Find.ByText( cboRepository.Text ) ).Click();
                }
                else // not find
                {
                    Debug.WriteLine( "Cannot find the repository... " + cboRepository.Text + " Stop here..." );
                    OutputMessage( "Cannot find the repository... " + cboRepository.Text + " Stop here..." );
                    //txtDisplay.Text += "\r\nCannot find the repository... " + cboRepository.Text + " Stop here...";
                    return;
                }

                Debug.WriteLine( "Set date range: " );
                Debug.WriteLine( "From: " + dtpFrom.Value.Month + "-" + dtpFrom.Value.Day + "-" + dtpFrom.Value.Year );
                OutputMessage( "Set date range: " );
                OutputMessage( "From: " + dtpFrom.Value.Month + "-" + dtpFrom.Value.Day + "-" + dtpFrom.Value.Year );

                ie.SelectList( Find.ByName( "amo" ) ).SelectByValue( dtpFrom.Value.Month.ToString( CultureInfo.InvariantCulture ) );
                ie.SelectList( Find.ByName( "ady" ) ).SelectByValue( dtpFrom.Value.Day.ToString( CultureInfo.InvariantCulture ) );
                ie.SelectList( Find.ByName( "ayr" ) ).SelectByValue( dtpFrom.Value.Year.ToString(CultureInfo.InvariantCulture) );

                Debug.WriteLine( "To:   " + dtpTo.Value.Month + "-" + dtpTo.Value.Day + "-" + dtpTo.Value.Year );
                OutputMessage( "To:   " + dtpTo.Value.Month + "-" + dtpTo.Value.Day + "-" + dtpTo.Value.Year );

                ie.SelectList( Find.ByName( "bmo" ) ).SelectByValue( dtpTo.Value.Month.ToString( CultureInfo.InvariantCulture ) );
                ie.SelectList( Find.ByName( "bdy" ) ).SelectByValue( dtpTo.Value.Day.ToString( CultureInfo.InvariantCulture ) );
                ie.SelectList( Find.ByName( "byr" ) ).SelectByValue( dtpTo.Value.Year.ToString( CultureInfo.InvariantCulture ) );

                //ie.SelectList( Find.ByName( "docLang" ) ).SelectByValue( SetLangCode(cboLanguage.Text) );

                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.                
                using(StreamReader sr = new StreamReader( cboInFile.Text ))
                {
                    string searchStr;                    
                    bool flag = true;
                    // Read and display lines from the file until the end of the file is reached.
                    while((searchStr = sr.ReadLine()) != null)
                    {
                        try
                        {
                            m_Total++;
                            string[] tmp = searchStr.Split( new char[] { ',' } );
                            Debug.WriteLine( "Search Subject: " + searchStr );

                            ie.SelectList( Find.ByName( "docLang" ) ).SelectByValue( tmp[0] );

                            //txtDisplay.Text += "\r\nSearch Subject: " + tmp[1];
                            OutputMessage( "==== Search " + m_searchField + ": " + tmp[1] + " ====");
                            ie.TextField( Find.ByName( m_searchField ) ).TypeText( tmp[1] );

                            ie.Button( Find.ByValue( "  Search  " ) ).Click();
                            Debug.WriteLine( "Searching now.... wait...." );
                            OutputMessage( "Searching now.... wait...." );
                            //txtDisplay.Text += "\r\nSearching now.... wait....";
                            //txtTotal.Refresh();

                            while(!ie.ContainsText( "Query results complete" ))
                            {
                                Thread.Sleep( 50 );                                
                                if(ie.ContainsText( "No messages found" ))
                                    break;
                                if(ie.ContainsText( "error" ))
                                    break;
                                if(ie.ContainsText( "Query in progress" ))
                                {
                                    Thread.Sleep( 1000 ); // give it more time 
                                    if( flag )
                                        ie.Image( Find.ByAlt( "next 20 messages" ) ).Click();
                                    else
                                        ie.Image( Find.ByAlt( "previous 20 messages" ) ).Click();

                                    flag = !flag;
                                }
                                //ie.Refresh();
                            }//end of while

                            if(ie.ContainsText( "Query results complete" ))
                            {
                                m_Pass++;
                                //Regex r = new Regex( "\bNumMessages\b.*\bdocuments\b" );
                                Regex r = new Regex( ".*documents" );
                                //string tmp = r.ToString();
                                string str = ie.FindText( r );
                                Debug.WriteLine( "String == " + str );
                                Debug.WriteLine( "DONE ^_^" );
                                OutputMessage( "Query Result == " + str );
                                //txtDisplay.Text += "\r\nString == " + str;

                                //ie.Image( Find.ByUrl( "http://10.1.41.51/zantaz/DigitalSafe/assets/images/but_modify_search.gif" ) ).Click();
                                ie.Image( Find.ByAlt( "Modify Search" ) ).Click(); // back to search criteria
                            }
                            else
                                if(ie.ContainsText( "No messages found" ))
                                {
                                    m_Fail++;
                                    Debug.WriteLine( "No message found" );
                                    OutputMessage( "No message found" );
                                    //txtDisplay.Text += "No message found";

                                    ie.Link( Find.ByText( "here" ) ).Click(); // back to search criteria
                                }
                                else
                                    if(ie.ContainsText( "error" ))
                                    {
                                        m_Fail++;
                                        Debug.WriteLine( "error" );
                                        OutputMessage( "error" );
                                        //txtDisplay.Text += "No message found";

                                        ie.Back(); // back to search criteria
                                    }

                            ie.TextField( Find.ByName( m_searchField ) ).TypeText( "" ); //clear the field

                            txtTotal.Text = m_Total.ToString( CultureInfo.CurrentCulture);
                            txtPass.Text = m_Pass.ToString( CultureInfo.CurrentCulture );
                            txtFail.Text = m_Fail.ToString( CultureInfo.CurrentCulture );

                            txtFail.Refresh();
                            txtPass.Refresh();
                            txtTotal.Refresh();

                        }//end of try
                        catch(WatiN.Core.Exceptions.WatiNException watinEx)
                        {
                            OutputMessage( watinEx.Message );
                            ie.Back(); // back to search criteria
                            Thread.Sleep( 3000 ); // 3 sec
                        }
                    }//end of while reading from file
                }// end of using

                // Uncomment the following line if you want to close
                // Internet Explorer and the console window immediately.
                if( chkCloseIE.Checked )
                    ie.Close();
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg );
                OutputMessage( msg );
            }
            finally
            {
                if(ie != null && chkCloseIE.Checked)
                    ie.Close();

                lock(this)
                {
                    BeginInvoke( m_delegateJobDoneNotification, new object[] { m_Pass } );
                }
            }//end of finally            
        }//end of Thd_WebSearch

        /// <summary>
        /// Stop the test... don't know what is the side effect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbort_Click(object sender, EventArgs e)
        {
            try
            {
                if(m_thdWebSearch != null && m_thdWebSearch.IsAlive)
                    KillWebSearchThread();

                lock(this)
                {
                    // reset mouse cursor and enable control
                    BeginInvoke( m_delegateJobDoneNotification, new object[] { m_Pass } );
                }
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( "UcWebSearch.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                OutputMessage( "UcWebSearch.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                //MessageBox.Show( ex.Message + "\n" + ex.StackTrace, "Abort Exception" );
            }//end of catch
        }//end of btnAbort_Click

        private void KillWebSearchThread()
        {
            try
            {
                commObj.LogToFile( "Kill KillWebSearchThread Start" );
                m_thdWebSearch.Abort(); // abort
                m_thdWebSearch.Join();  // require for ensure the thread kill

                if(ie != null && chkCloseIE.Checked )
                    ie.Close();
            }//end of try 
            catch(ThreadAbortException thdEx)
            {
                Trace.WriteLine( thdEx.Message );
                commObj.LogToFile( "Aborting the Count thread : " + thdEx.Message.ToString() );
            }//end of catch
        }//end of KillWebSearchThread

        /// <summary>
        /// Log message based on the user selection.
        /// If detail checked, log message to both display area and log file.
        /// If detail not checked, only log to display area
        /// </summary>
        /// <param name="msg"></param>
        private void OutputMessage(string msg)
        {
            txtDisplay.Text += msg + "\r\n";
            //txtDisplay.Refresh();
            txtDisplay.SelectionStart = txtDisplay.Text.Length; // this will move the cursor
            txtDisplay.ScrollToCaret();
            if(chkLogDetail.Checked)
                commObj.WriteLineByLine( commObj.logFullPath + "\\" + m_logFile, msg );
        }//end of OutputMessage

        /// <summary>
        /// Launch the notepad and open the log file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkViewLog_Click(object sender, EventArgs e)
        {
            try
            {
                string logFullPathFileName = commObj.logFullPath + "\\" + m_logFile;
                Debug.WriteLine( logFullPathFileName );
                mySearchLogTitle = commObj.ViewLogFromNotepad( logFullPathFileName, mySearchLogTitle );
            }//emd of try
            catch(Win32Exception win32Ex)
            {
                Trace.WriteLine( win32Ex.Message + "\n" + win32Ex.GetType().ToString() + win32Ex.StackTrace );
            }//end of catch - win32 exception
        }//end of lnkViewLog_Click

        /// <summary>
        /// Set the language code based on GUI input. The langauge list from DS UI
        /// GUI input is the language and this function will convert it to 2 character language code
        /// </summary>
        /// <param name="inStr">Input string</param>
        /// <returns>string: language code</returns>
        private string SetLangCode( string inStr )
        {
            string code = "en"; //default

            switch(inStr)
            {
                case "Afrikaans":
                    code = "af";
                    break;
                case "Amharic":
                    code = "am";
                    break;
                case "Arabic":
                    code = "ar";
                    break;
                case "Armenian":
                    code = "hy";
                    break;
                case "Basque":
                    code = "eu";
                    break;
                case "Belarusian":
                    code = "be";
                    break;
                case "Bulgarian":
                    code = "bg";
                    break;
                case "Burmese":
                    code = "my";
                    break;
                case "Catalan":
                    code = "ca";
                    break;
                case "Chinese":
                    code = "zh";
                    break;
                case "Croatian":
                    code = "hr";
                    break;
                case "Czech":
                    code = "cs";
                    break;
                case "Danish":
                    code = "da";
                    break;
                case "Dutch":
                    code = "nl";
                    break;
                case "English":
                    code = "en";
                    break;
                case "Esperanto":
                    code = "eo";
                    break;
                case "Estonian":
                    code = "et";
                    break;
                case "Finnish":
                    code = "fi";
                    break;
                case "French":
                    code = "fr";
                    break;
                case "Georgian":
                    code = "ka";
                    break;
                case "German":
                    code = "de";
                    break;
                case "Greek":
                    code = "el";
                    break;
                case "Greenlandic":
                    code = "kl";
                    break;
                case "Gujarati":
                    code = "gu";
                    break;
                case "Hebrew":
                    code = "he";
                    break;
                case "Hindi":
                    code = "hi";
                    break;
                case "Hungarian":
                    code = "hu";
                    break;
                case "Icelandic":
                    code = "is";
                    break;
                case "Indonesian":
                    code = "id";
                    break;
                case "Italian":
                    code = "it";
                    break;
                case "Japanese":
                    code = "ja";
                    break;
                case "Kannada":
                    code = "kn";
                    break;                          
                case "Khmer":
                    code = "km";
                    break;
                case "Korean":
                    code = "ko";
                    break;
                case "Lao":
                    code = "lo";
                    break;
                case "Latin":
                    code = "la";
                    break;
                case "Latvian":
                    code = "lv";
                    break;
                case "Lithuanian":
                    code = "lt";
                    break;
                case "Luxembourgish":
                    code = "lb";
                    break;
                case "Malayalam":
                    code = "ml";
                    break;
                case "Maori":
                    code = "mi";
                    break;
                case "Mongolian":
                    code = "mn";
                    break;
                case "Norwegian":
                    code = "no";
                    break;
                case "Oriya":
                    code = "or";
                    break;
                case "Persian":
                    code = "fa";
                    break;
                case "Polish":
                    code = "pl";
                    break;
                case "Portuguese":
                    code = "pt";
                    break;
                case "Romanian":
                    code = "ro";
                    break;
                case "Russian":
                    code = "ru";
                    break;
                case "Scottish Gaelic":
                    code = "gd";
                    break;
                case "Serbian":
                    code = "sr";
                    break;
                case "Slovak":
                    code = "sk";
                    break;
                case "Slovenian":
                    code = "sl";
                    break;
                case "Spanish":
                    code = "es";
                    break;
                case "Swahili":
                    code = "sw";
                    break;
                case "Swedish":
                    code = "sv";
                    break;
                case "Tagalog":
                    code = "tl";
                    break;
                case "Tajik":
                    code = "tg";
                    break;
                case "Tamil":
                    code = "ta";
                    break;
                case "Telugu":
                    code = "te";
                    break;
                case "Thai":
                    code = "th";
                    break;
                case "Tibetan":
                    code = "bo";
                    break;
                case "Turkish":
                    code = "tr";
                    break;
                case "Ukrainian":
                    code = "uk";
                    break;
                case "Vietnamese":
                    code = "vi";
                    break;
                case "Welsh":
                    code = "cy";
                    break;
                case "syr":
                    code = "syr";
                    break;
            }//end of switch
            return (code);
        }//end of SetLangCode

        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine( "UcWebSearch.cs - SaveComboBoxItem" );
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\" + m_xmlFile;
                if(!File.Exists( cboPath ))
                {
                    StreamWriter sw = File.CreateText( cboPath );
                    sw.Close();
                }

                // Save the combox
                tw = new XmlTextWriter( Application.StartupPath + "\\" + m_xmlFile, System.Text.Encoding.UTF8 );

                Debug.WriteLine( "\t ComboBox Item file" + Application.StartupPath + "\\" + m_xmlFile );

                tw.WriteStartDocument();
                tw.WriteStartElement( "InitData" );

                //The order is important
                WriteComboBoxEntries( cboLoginID, "cboLoginID", cboLoginID.Text, tw );
                WriteComboBoxEntries( cboPassword, "cboPassword", cboPassword.Text, tw );
                WriteComboBoxEntries( cboURL, "cboURL", cboURL.Text, tw );
                WriteComboBoxEntries( cboSearchKey, "cboSearchKey", cboSearchKey.Text, tw );
                WriteComboBoxEntries( cboInFile, "cboInFile", cboInFile.Text, tw );
                WriteComboBoxEntries( cboRepository, "cboRepository", cboRepository.Text, tw );
                WriteComboBoxEntries( cboLanguage, "cboLanguage", cboLanguage.Text, tw );

                tw.WriteEndElement();
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg, "SaveComboBoxItem()" );
            }//end of catch
            finally
            {
                tw.Flush();
                tw.Close();
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
            Debug.WriteLine( "UcWebSearch.cs - WriteComboBoxEntries" );
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
            Debug.WriteLine( "UcWebSearch.cs - LoadComboBoxes" );
            try
            {
                cboLoginID.Items.Clear();
                cboPassword.Items.Clear();
                cboURL.Items.Clear();
                cboSearchKey.Items.Clear();
                cboInFile.Items.Clear();
                cboRepository.Items.Clear();
                cboLanguage.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\" + m_xmlFile;
                if(!File.Exists( cboPath ))
                {
                    //File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load( cboPath );
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                int numComboBox = nodeList.Count;
                int x;
                for(int i = 0; i < numComboBox; i++)
                {
                    switch(nodeList.Item( i ).Attributes.GetNamedItem( "name" ).InnerText)
                    {
                        case "cboLoginID":
                            for(x = 0; x < nodeList.Item( 0 ).ChildNodes.Count; ++x)
                            {
                                cboLoginID.Items.Add( nodeList.Item( 0 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboPassword":
                            for(x = 0; x < nodeList.Item( 1 ).ChildNodes.Count; ++x)
                            {
                                cboPassword.Items.Add( nodeList.Item( 1 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboURL":
                            for(x = 0; x < nodeList.Item( 2 ).ChildNodes.Count; ++x)
                            {
                                cboURL.Items.Add( nodeList.Item( 2 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboSearchKey":
                            for(x = 0; x < nodeList.Item( 3 ).ChildNodes.Count; ++x)
                            {
                                cboSearchKey.Items.Add( nodeList.Item( 3 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboInFile":
                            for(x = 0; x < nodeList.Item( 4 ).ChildNodes.Count; ++x)
                            {
                                cboInFile.Items.Add( nodeList.Item( 4 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboRepository":
                            for(x = 0; x < nodeList.Item( 5 ).ChildNodes.Count; ++x)
                            {
                                cboRepository.Items.Add( nodeList.Item( 5 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                        case "cboLanguage":
                            for(x = 0; x < nodeList.Item( 6 ).ChildNodes.Count; ++x)
                            {
                                cboLanguage.Items.Add( nodeList.Item( 6 ).ChildNodes.Item( x ).InnerText );
                            }
                            break;
                    }//end of switch
                }//end of for

                if(0 < cboLoginID.Items.Count)
                    cboLoginID.Text = cboLoginID.Items[0].ToString();
                if(0 < cboPassword.Items.Count)
                    cboPassword.Text = cboPassword.Items[0].ToString();
                if(0 < cboURL.Items.Count)
                    cboURL.Text = cboURL.Items[0].ToString();
                if(0 < cboSearchKey.Items.Count)
                    cboSearchKey.Text = cboSearchKey.Items[0].ToString();
                if(0 < cboInFile.Items.Count)
                    cboInFile.Text = cboInFile.Items[0].ToString();
                if(0 < cboRepository.Items.Count)
                    cboRepository.Text = cboRepository.Items[0].ToString();
                if(0 < cboLanguage.Items.Count)
                    cboLanguage.Text = cboLanguage.Items[0].ToString();

            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg, "Exception" );
                MessageBox.Show( msg, "LoadComboBoxes()", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
        }//end of //end of LoadComboBoxes
        #endregion

        #region Handle Key down and press

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

        private void cboLoginID_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboLoginID_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboPassword_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboURL_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboURL_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboSearchKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboInFile_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboInFile_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboRepository_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboRepository_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }

        private void cboLanguage_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyValue)
            {
                case 13: // enter key down
                    SaveComboBoxItem();
                    break;
            }//end of switch
        }

        private void cboLanguage_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteCombo( sender, e );
        }
        #endregion

        /// <summary>
        /// Set default begining date range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcWebSearch_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            dtpFrom.Value = new DateTime( 2008, 1, 1);
        }//end of UcWebSearch_Load

        /// <summary>
        /// Validate user input
        /// </summary>
        /// <returns>bool: true - everything is OK; false - problem</returns>
        private bool ValidateUserInput()
        {
            bool rv = true; // assume everything is OK
            //if(txtFolder.Text == "")
            if(String.IsNullOrEmpty( cboLoginID.Text ))
            {
                cboLoginID.Focus();
                cboLoginID.BackColor = Color.YellowGreen;
                rv = false;
            }//end of if - check txtFolder
            else
                if(String.IsNullOrEmpty( cboPassword.Text ))
                {
                    cboPassword.Focus();
                    cboPassword.BackColor = Color.YellowGreen;
                    rv = false;
                }//end of if
                else
                    if(String.IsNullOrEmpty( cboRepository.Text ))
                    {
                        cboRepository.Focus();
                        cboRepository.BackColor = Color.YellowGreen;
                        rv = false;
                    }//end of if
                    else
                        if(String.IsNullOrEmpty( cboURL.Text ))
                        {
                            cboURL.Focus();
                            cboURL.BackColor = Color.YellowGreen;
                            rv = false;
                        }//end of if
                        else
                            if(String.IsNullOrEmpty( cboLanguage.Text ))
                            {
                                cboLanguage.Focus();
                                cboLanguage.BackColor = Color.YellowGreen;
                                rv = false;
                            }//end of if
                            else
                                if(String.IsNullOrEmpty( cboInFile.Text ) || !File.Exists( cboInFile.Text ) )
                                {
                                    txtDisplay.Text = "File does not exist";
                                    cboInFile.Focus();
                                    cboInFile.BackColor = Color.YellowGreen;
                                    rv = false;
                                }//end of if

            return (rv);
        }

        private void lnkInFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.ShowReadOnly = true;
            ofDlg.Filter = "Search key files (*.txt)|*.txt|All files (*.*)|*.*";
            if(ofDlg.ShowDialog() == DialogResult.OK)
            {
                cboInFile.Text = ofDlg.FileName;
            }// end of if - open file dialog
            else
            {
                cboInFile.BackColor = Color.YellowGreen;
                cboInFile.Focus();
            }
        }//end of lnkInFile_LinkClicked       
    }
}
