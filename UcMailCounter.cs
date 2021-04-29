using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Outlook = Microsoft.Office.Interop.Outlook;
using ICSharpCode.SharpZipLib.Zip;

namespace WsClient
{
    public partial class UcMailCounter : UserControl
    {
        private string[] fileNames;
        private string _password = "";
        private string _olProfile = "";
        private int _totalMail = 0;
        private double _totalSize = 0.0;

        private Thread countSpecialThread;
        private Outlook._Application oApp;
        private Outlook._NameSpace oNameSpace;
        private CommObj commObj = new CommObj();

        private delegate void DelegateUpdate_txtTotalSize(double size);
        private DelegateUpdate_txtTotalSize m_delegateShowTotalSize;

        private delegate void DelegateUpdate_txtTotalMail(int numMail);
        private DelegateUpdate_txtTotalMail m_delegateShowTotalMail;

        private delegate void DelegateUpdate_dtgResult( DataTable dataTable);
        private DelegateUpdate_dtgResult m_delegateShowDataGrid;

        private delegate void DelegateUpdate_contextMenu();
        private DelegateUpdate_contextMenu m_delegateShowContextMenu;

        private delegate void DelegateUpdate_uiControls( bool falg );
        private DelegateUpdate_uiControls m_delegateUiControls;


        //public event UpdateStatusEventHandler statusChanged;
        public event EventHandler<StatusEventArgs> statusChanged;

        public UcMailCounter()
        {
            InitializeComponent();

            m_delegateShowTotalSize = new DelegateUpdate_txtTotalSize( Update_txtTotalSize );
            m_delegateShowTotalMail = new DelegateUpdate_txtTotalMail( Update_txtTotalMail );
            m_delegateShowDataGrid = new DelegateUpdate_dtgResult( Update_dtgResult );
            m_delegateShowContextMenu = new DelegateUpdate_contextMenu( Update_contextMenu );
            m_delegateUiControls = new DelegateUpdate_uiControls( Update_uiControls );
        }

        // "StatusEventArgs" - argument in EventArgs class
        protected virtual void OnUpdateStatusBar(StatusEventArgs eArgs)
        {
            statusChanged( this, eArgs );
        }//end of virtual

        private void Update_uiControls(bool flag)
        {
            EnableControls( flag );
        }//end of Update_enableControls

        private void Update_txtTotalSize(double size)
        {
            try
            {
                double l = _totalSize / 1048576.00;
                string szTmp = String.Format( "{0:F2}MB", l );
                Debug.WriteLine( size );
                txtTotalSize.Text = szTmp.ToString();                
                txtTotalSize.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "Update Total Size", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }
        }//end of Update_txtTotalSize

        private void Update_contextMenu()
        {
            dgContextMenuStrip.Items.Clear();
            dgContextMenuStrip.Items.Add( "Export Entire Table", null, new System.EventHandler( this.MnuExportTable ) );
            dgContextMenuStrip.Items.Add( "Export Selected Items", null, new System.EventHandler( this.MnuExportItems ) );
        }//end of Update_contextMenu

        private void Update_txtTotalMail(int numMail)
        {
            Debug.WriteLine( numMail );
            txtTotalMail.Text = numMail.ToString(CultureInfo.InvariantCulture);
            txtTotalMail.Refresh();
        }//end of Update_txtSubject

        private void Update_dtgResult(DataTable A)
        {
            dtgResult.DataSource = A;
            SizeColumns( dtgResult ); // in btnCountIt

        }//end of Update_dtgResult

        private void SetNsfLabel()
        {
            lnkFile.Text = "NSF Files";
        }

        private void SetPstLabel()
        {
            lnkFile.Text = "PST Files"; //
        }

        private void rdoPstCount_Click(object sender, EventArgs e)
        {
            SetPstLabel();
        }

        private void rdoNsfCount_Click(object sender, EventArgs e)
        {
            SetNsfLabel();
        }

        private void lnkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtFileName.Text = "";

            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.Multiselect = true;
            if(ofDlg.ShowDialog() == DialogResult.OK)
            {
                fileNames = ofDlg.FileNames;
                foreach(string str in fileNames)
                {
                    txtFileName.Text += ";" + str;
                }//end of foreach

                string tmpStr = txtFileName.Text.ToString();
                if(tmpStr[0] == ';')
                    txtFileName.Text = txtFileName.Text.Remove( 0, 1 );
            }//end of if
        }//end of lnkFile_LinkClicked

        /// <summary>
        /// Only check the profile and password entry. 
        /// </summary>
        /// <returns>OK: true; Fail: false</returns>
        private bool ValidateUserInput()
        {
            bool rv = true;
            //if(txtPassword.Text == "")
            if( String.IsNullOrEmpty(txtPassword.Text) )
            {
                txtPassword.BackColor = Color.Yellow;
                rv = false;
            }
            else
                //if(cboProfile.Text == "")
                if( String.IsNullOrEmpty(cboProfile.Text) )
                {
                    cboProfile.BackColor = Color.Yellow;
                    rv = false;
                }

            return (rv);
        }//end of ValidateUserInput

        private void InitVariable()
        {
            _password = txtPassword.Text;
            _olProfile = cboProfile.Text;
        }//end of InitVariable

        /// <summary>
        /// Reset BOTH UI counter and local variable
        /// </summary>
        private void ResetCounter()
        {
            _totalMail = 0;
            _totalSize = 0;

            txtTotalMail.Text = "";
            txtTotalSize.Text = "";
        }//end of ResetUICounter

        private void RemovePstStore()
        {            
            int numFolder = oNameSpace.Folders.Count;
            for(int i = 0; i < numFolder; i++) 
            {
                string path = oNameSpace.Folders.GetLast().FolderPath;
                Debug.WriteLine( "MAPI Folder = " + path );

                string str = @"\\Personal Folders";
                if(path == str)
                    break;

                Outlook.MAPIFolder olMapiFolder = oNameSpace.Folders.GetLast();
                oNameSpace.RemoveStore( olMapiFolder );
            }//end of for

            numFolder = oNameSpace.Folders.Count;
            for(int i = 0; i < numFolder; i++)
            {
                string path = oNameSpace.Folders.GetFirst().FolderPath;
                Debug.WriteLine( "MAPI Folder = " + path );

                string str = @"\\Personal Folders";
                if(path == str)
                    break;

                Outlook.MAPIFolder olMapiFolder = oNameSpace.Folders.GetFirst();
                oNameSpace.RemoveStore( olMapiFolder );
            }//end of for
        }//end of RemovePstStore

        /// <summary>
        /// Dynamic create a table to store pst file name and its message count
        /// </summary>
        /// <returns></returns>
        private DataTable CreatePstCountTable()
        {
            DataTable aTable = new DataTable( "Pst Count Table" );
            DataColumn dtCol;
            DataRow dtRow;


            dtCol = new DataColumn();
            dtCol.DataType = System.Type.GetType( "System.Int32" );
            dtCol.ColumnName = "ID";
            dtCol.AutoIncrement = true;
            dtCol.Caption = "ID";
            dtCol.ReadOnly = true;
            dtCol.Unique = true;

            aTable.Columns.Add( dtCol );


            dtCol = new DataColumn();
            dtCol.DataType = System.Type.GetType( "System.String" );
            dtCol.ColumnName = "File Name";
            dtCol.AutoIncrement = false;
            dtCol.Caption = "PST File Name";
            dtCol.ReadOnly = true;
            dtCol.Unique = true;
            aTable.Columns.Add( dtCol );


            dtCol = new DataColumn();
            dtCol.DataType = System.Type.GetType( "System.Int32" );
            dtCol.ColumnName = "Mail Count";
            dtCol.AutoIncrement = false;
            dtCol.Caption = "Mail Count";
            dtCol.ReadOnly = true;
            dtCol.Unique = false;
            aTable.Columns.Add( dtCol );


            dtCol = new DataColumn();
            dtCol.DataType = System.Type.GetType( "System.Decimal" );
            dtCol.ColumnName = "PST Size (MB)";
            dtCol.AutoIncrement = false;
            dtCol.Caption = "PST Size (MB)";
            dtCol.ReadOnly = true;
            dtCol.Unique = false;
            aTable.Columns.Add( dtCol );


            int len = fileNames.Length;
            int tmpMail = 0;
            double tmpSize = 0;

            int k = 0;
            for(int i = 0; i < len; i++)
            {
                k++;
                if(k == 137)
                {
                    RemovePstStore();
                    oNameSpace.Logoff();
                    oApp.Quit();
                    Process[] localByName = Process.GetProcessesByName( "outlook" );
                    for(int n = 0; n < localByName.Length; n++)
                        localByName[n].Kill();

                    System.Threading.Thread.Sleep( 3000 );
                    k = 0; //reset k
                    oApp = new Outlook.ApplicationClass();
                    oNameSpace = oApp.GetNamespace( "MAPI" );
                    oNameSpace.Logon( _olProfile, _password, false, true );

                }//end of if - remove pst store

                dtRow = aTable.NewRow();
                dtRow["ID"] = i + 1;
                dtRow["File Name"] = getShortFileName( fileNames[i] );

                // Change on 04-25-08
                // count unconverted mail inside the zip file
                //tmpMail = getPstMailCount( fileNames[i] );

                switch( getFileExt( fileNames[i] ) )
                {
                    case ".pst":
                    case ".PST":
                        tmpMail = getPstMailCount( fileNames[i] );
                        break;
                    case ".zip":
                    case ".ZIP":
                        tmpMail = getZipMailCount( fileNames[i] );
                        break;
                    default:
                        tmpMail = 0;
                        break;
                }//end of switch

                _totalMail += tmpMail;
                dtRow["Mail Count"] = tmpMail;

                tmpSize = getFileSize( fileNames[i] );
                _totalSize += tmpSize;
                dtRow["PST Size (MB)"] = (tmpSize / 1048576.00);

                aTable.Rows.Add( dtRow );

                // Update GUI - so user know it is NOT hang
                lock(this)
                {
                    // m_sentMail++; // inc counter
                    BeginInvoke( m_delegateShowTotalSize, new object[] { _totalSize } );
                    BeginInvoke( m_delegateShowTotalMail, new object[] { _totalMail } );
                }
                

                //if(countSpecialThread != null && countSpecialThread.IsAlive)
                //{
                //    Debug.WriteLine( "\t Update Datagrid" );
                //    // IAsyncResult r = BeginInvoke(m_delegateUpdateDataGrid, new object[] {aTable} );
                //}
                //else
                //{
                //    dtgResult.DataSource = aTable;
                //    dtgResult.Refresh();
                //}
            }//end of for

            return (aTable);
        }// end of CreatePstCountTable

        /// <summary>
        /// Get the mail count.. use the Start from GetLast
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private int getPstMailCount(string fileName)
        {
            Debug.WriteLine( "PstCheckerWnd.cd - getPstMailCount" );
            Debug.WriteLine( "\t file name = " + fileName );

            // save for reference - adding the pst into outlook
            // oNameSpace.AddStore( @"D:\\R1_00017.pst" );
            oNameSpace.AddStore( fileName );

            //            int count = oNameSpace.Folders.Count;
            Outlook.MAPIFolder olMapiFolder = oNameSpace.Folders.GetLast();
            Debug.WriteLine( "\t MAPIFolder = " + olMapiFolder.Folders.ToString() );
            int mailCount = olMapiFolder.Items.Count;
            Debug.WriteLine( "\t mail count = " + mailCount.ToString() );

            if(mailCount == 0) // try get start from first folder
            {
                Outlook.MAPIFolder olMapiFirstFolder = oNameSpace.Folders.GetFirst();
                mailCount = olMapiFirstFolder.Items.Count;
            }

            return mailCount;
        }//end of getPstMailCount

        /// <summary>
        /// Check number of eml file inside a zipped file. Only count the eml.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private int getZipMailCount(string fileName)
        {
            int emlCount = 0;
            byte[] data = new byte[4096];
            using( ZipInputStream s = new ZipInputStream(File.OpenRead(fileName)) )
            {
                ZipEntry theEntry;
                while( (theEntry = s.GetNextEntry()) != null )
                {
                    emlCount++;
                    Debug.WriteLine("Name : " + theEntry.Name);
                    Debug.WriteLine("Date : " + theEntry.DateTime);
                    Debug.WriteLine("Size : (-1, if the size information is in the footer)");
                    Debug.WriteLine("      Uncompressed : " + theEntry.Size);
                    Debug.WriteLine("      Compressed   : " + theEntry.CompressedSize);
				}//end of while
			}//end of using

            return(emlCount);
        }//end of getZipMailCount

        /// <summary>
        /// Generic get file size funtion
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private long getFileSize(string fileName)
        {
            FileInfo fileInfo = new FileInfo( fileName );
            return (fileInfo.Length);
        }

        /// <summary>
        /// Passing the full path file name, then extract the filename + ext.
        /// eg) passing D://abc/def/123.ext --> 123.ext
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns>string: file name + ext</returns>
        public string getShortFileName(string fullFileName)
        {
            FileInfo fileInfo = new FileInfo( fullFileName );
            return (fileInfo.Name);
        }//end of getShortFileName

        public string getFileExt(string fileName)
        {
            FileInfo fileInfo = new FileInfo( fileName );
            return (fileInfo.Extension);
        }//end of getFileExt

        /// <summary>
        /// This function will pass into delegate function.
        /// </summary>
        /// <param name="dataTable"></param>
        private void UpdateDataGrid(DataTable dataTable)
        {
            dtgResult.DataSource = dataTable;
            SizeColumns( dtgResult ); // thd_process
            dtgResult.Refresh();
        }//end of UpdateDataGrid

        /// <summary>
        /// Auto Size the column of datagrid
        /// </summary>
        /// <param name="grid"></param>
        protected void SizeColumns(DataGrid grid)
        {
            Debug.WriteLine( "UcMailCounter.cs - SizeColumns()" );
            //            Graphics g = CreateGraphics();  
            try
            {
                DataTable dataTable = (DataTable)grid.DataSource;

                bool isContained = grid.TableStyles.Contains( dataTable.TableName );
                if(isContained)
                    return;

                DataGridTableStyle dataGridTableStyle = new DataGridTableStyle();
                dataGridTableStyle.MappingName = dataTable.TableName;
                dataGridTableStyle.AlternatingBackColor = Color.Gainsboro;

                Debug.WriteLine( "\t Table Name = " + dataTable.TableName );

                Graphics g = CreateGraphics();
                foreach(DataColumn dataColumn in dataTable.Columns)
                {
                    int maxSize = 0;                    
                    SizeF size = g.MeasureString( dataColumn.ColumnName, grid.Font );
                    if(maxSize < size.Width)
                        maxSize = (int)size.Width;

                    foreach(DataRow row in dataTable.Rows)
                    {
                        size = g.MeasureString( row[dataColumn.ColumnName].ToString(), grid.Font );

                        if(maxSize < size.Width)
                            maxSize = (int)size.Width;
                    }// end of foreach

                    DataGridColumnStyle dataGridColumnStyle = new DataGridTextBoxColumn();
                    dataGridColumnStyle.MappingName = dataColumn.ColumnName;
                    dataGridColumnStyle.HeaderText = dataColumn.ColumnName;
                    dataGridColumnStyle.Width = maxSize + 5;
                    dataGridTableStyle.GridColumnStyles.Add( dataGridColumnStyle );
                }// end of foreach
                g.Dispose();
                grid.TableStyles.Add( dataGridTableStyle );
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
                MessageBox.Show( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "Size Columns", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
            finally
            {
                Debug.WriteLine( "\t finally end of SizeColumns" );
                //                g.Dispose();
            }//end of finally                      
        }//end of SizeColumns

        /// <summary>
        /// Single Thread for improve UI.. Not really multiple processes
        /// </summary>
        private void Thd_CountPstMail()
        {
            try
            {
                oApp = new Outlook.ApplicationClass();
                oNameSpace = oApp.GetNamespace( "MAPI" );
                oNameSpace.Logon( _olProfile, _password, false, true );

                ////
                // remove all folder - fail if map drive disconnected
                // Assume the default folder name is @"\\Personal Folders", if not change it
                RemovePstStore(); // clean up before do anything

                // Create a DataTable and Bind it to the DataGrid
                DataTable A = CreatePstCountTable();  // btnCountIt
                lock(this)
                {
                    IAsyncResult rd = BeginInvoke( m_delegateShowDataGrid, new object[] { A } );
                }
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message.ToString() + "\n" + ex.GetType().ToString() + ex.StackTrace.ToString();
                commObj.LogToFile( msg );
                MessageBox.Show( "Check log: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of generic Exception
            finally
            {
                try
                {
                    oNameSpace.Logoff(); // both sucessful or fail
                    if(oApp != null)
                    {
                        Debug.WriteLine( "\t Quit Outlook" );
                        commObj.LogToFile( "Quit outlook" );
                        oApp.Quit();
                        Process[] localByName = Process.GetProcessesByName( "outlook" );
                        for(int n = 0; n < localByName.Length; n++)
                            localByName[n].Kill();
                    }
                }//end of try - logoff outlook
                catch(Exception ex2)
                {
                    string msg = ex2.Message.ToString() + "\n" + ex2.GetType().ToString() + ex2.StackTrace.ToString();
                    commObj.LogToFile( msg );
                    MessageBox.Show( "Check log: " + ex2.Message.ToString(), "Outlook Logoff", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
                }//end of catch - mainly for outlook logoff and quit
                
            }//end of finally

            // enable the export table context menu
            // dgContextMenuStrip.Items.Clear();
            // dgContextMenuStrip.Items.Add("Export Entire Table", null, new System.EventHandler( this.MnuExportTable ) );
            // dgContextMenuStrip.Items.Add( "Export Selected Items", null, new System.EventHandler( this.MnuExportItems ) );

            lock(this)
            {
                IAsyncResult rd = BeginInvoke( m_delegateShowContextMenu, new object[] {} );
                BeginInvoke( m_delegateUiControls, new object[] { true } );
            }
        }//end of Thd_CountPstMail

        private void Thd_CountNsfMail()
        {
            // Adding data - size and mail count
            int len = fileNames.Length;

            try
            {
                // Create a DataTable and Bind it to the DataGrid
                DataTable nsfTable = CreateNsfCountTable();

                // Adding data - file names
                DataRow dtRow;
                for(int i = 0; i < len; i++)
                {
                    dtRow = nsfTable.NewRow();
                    dtRow["ID"] = i + 1; // start from 1
                    dtRow["File Name"] = getShortFileName( fileNames[i] );

                    nsfTable.Rows.Add( dtRow );
                }//end of for

                //dtgResult.DataSource = nsfTable;

                processNsfCount( nsfTable );

                // enable the export table context menu
                // dgContextMenuStrip.Items.Clear();
                // dgContextMenuStrip.Items.Add( "Export Entire Table", null, new System.EventHandler( this.MnuExportTable ) );
                lock(this)
                {
                    IAsyncResult rd = BeginInvoke( m_delegateShowContextMenu, new object[] { } );
                    BeginInvoke( m_delegateUiControls, new object[] { true } );
                }
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + "\n" + ex.StackTrace;
                MessageBox.Show( msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch            
        }//end of Thd_CountNsfMail

        /// <summary>
        /// Get the mail info from NSF DB. 
        /// </summary>
        /// <param name="dTable"></param>
        private void processNsfCount(DataTable dTable)
        {
            int len = fileNames.Length;
            int tmpMail = 0;
            double tmpSize = 0;

            Domino.NotesSession domSession = new Domino.NotesSession();

            domSession.Initialize( "" );
            // Used by install client only - Save next line for reference
            // domSession.InitializeUsingNotesUserName("admin", "password0");

            // txtLoginID.Text = domSession.UserName;
            // txtPassword.Text = domSession.ServerName;

            // after initialize session, get the db open
            Domino.NotesDatabase domDB;
            //DataTable myTable = (DataTable)dtgResult.DataSource;
            DataTable myTable = dTable;
            for(int i = 0; i < len; i++)
            {
                domDB = domSession.GetDatabase( "", fileNames[i], false );
                if(domDB == null)
                    continue; // move to next file

                if(domDB.IsOpen)
                {
                    //lblStatus.Text = domDB.Title;
                    tmpMail = domDB.AllDocuments.Count;
                    myTable.Rows[i]["Mail Count"] = tmpMail;
                    _totalMail += tmpMail;
                    tmpSize = domDB.Size;
                    myTable.Rows[i]["NSF Size (MB)"] = (tmpSize / 1048576.00);
                    _totalSize += tmpSize;

                }//end of if
                else
                {
                    //MessageBox.Show( "Error - Fail to Open nsf file " + fileNames[i].ToString() );
                    Debug.WriteLine( "Error - Fail to Open nsf file " + fileNames[i].ToString() );
                    myTable.Rows[i]["Mail Count"] = "Error";
                    myTable.Rows[i]["NSF Size (MB)"] = "Error";
                }//end of else

                lock(this)
                {
                    BeginInvoke( m_delegateShowDataGrid, new object[] { myTable } );
                    BeginInvoke( m_delegateShowTotalSize, new object[] { _totalSize } );
                    BeginInvoke( m_delegateShowTotalMail, new object[] { _totalMail } );
                }
                // dtgResult.ReadOnly = true;
                // dtgResult.Update();

                //txtTotalMail.Text = totalMail.ToString();
                //double l = (double)totalNsfSize / 1048576.00;
                //txtNsfsSize.Text = String.Format( "{0:F2}MB", l );
                //Debug.WriteLine( "Total mail = " + _totalMail + " Total Size = " + _totalSize );
            }//end of for
        }//end of processNsfCount

        private DataTable CreateNsfCountTable()
        {
            DataTable aTable = new DataTable( "NSF Count Table" );
            DataColumn dtCol;

            try
            {
                // Create ID column and add to the DataTable.
                dtCol = new DataColumn();
                dtCol.DataType = System.Type.GetType( "System.Int32" );
                dtCol.ColumnName = "ID";
                dtCol.AutoIncrement = true;
                dtCol.Caption = "ID";
                dtCol.Unique = true;
                // Add the column to the DataColumnCollection.
                aTable.Columns.Add( dtCol );

                // Create PST File Name column and add to the DataTable.
                dtCol = new DataColumn();
                dtCol.DataType = System.Type.GetType( "System.String" );
                dtCol.ColumnName = "File Name";
                dtCol.AutoIncrement = false;
                dtCol.Caption = "NSF File Name";
                //dtCol.ReadOnly = true;
                dtCol.Unique = true;
                aTable.Columns.Add( dtCol );

                // Create Mail count column and add to the DataTable
                dtCol = new DataColumn();
                dtCol.DataType = System.Type.GetType( "System.Int32" );
                dtCol.ColumnName = "Mail Count";
                dtCol.AutoIncrement = false;
                dtCol.Caption = "Mail Count";
                dtCol.Unique = false;
                aTable.Columns.Add( dtCol );

                // Create Mail count column and add to the DataTable
                dtCol = new DataColumn();
                dtCol.DataType = System.Type.GetType( "System.Decimal" );
                dtCol.ColumnName = "NSF Size (MB)";
                dtCol.AutoIncrement = false;
                dtCol.Caption = "NSF Size (MB)";
                dtCol.Unique = false;
                aTable.Columns.Add( dtCol );

            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                MessageBox.Show( msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch - exception

            return (aTable);
        }// end of CreateNsfCountTable


        /// <summary>
        /// Event handler for exporting whole table to CSV file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MnuExportTable(System.Object sender, System.EventArgs e)
        {
            Debug.WriteLine( "UcMailCounter.cs - MnuExportTable" );

            // Create the CSV file to which grid data will be exported.
            string exportFileName = commObj.GetSaveAbsPathFileName( Application.StartupPath.ToString() + "abc.csv" );
            //if(exportFileName == "")
            if( String.IsNullOrEmpty(exportFileName) )
                return;

            try
            {
                DataTable m_dtDataTable = (DataTable)dtgResult.DataSource;

                StreamWriter sw = new StreamWriter( exportFileName );
                // First we will write the headers.
                int colCount = m_dtDataTable.Columns.Count;
                for(int i = 0; i < colCount; i++)
                {
                    sw.Write( m_dtDataTable.Columns[i] );
                    if(i < colCount - 1)
                        sw.Write( "," );
                }//end of for
                sw.Write( sw.NewLine );

                // OK... Now write all the rows
                foreach(DataRow dataRow in m_dtDataTable.Rows)
                {
                    for(int i = 0; i < colCount; i++)
                    {
                        if(!Convert.IsDBNull( dataRow[i] ))
                        {
                            sw.Write( dataRow[i].ToString() );
                        }
                        if(i < colCount - 1)
                            sw.Write( "," );
                    }//end of for
                    sw.Write( sw.NewLine );
                }//end of foreach

                sw.Close();
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }//end of catch
        }//end of MnuExportTable

        /// <summary>
        /// Event handler for exporting whole table to CSV file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MnuExportItems(System.Object sender, System.EventArgs e)
        {
            Debug.WriteLine( "UcMailCounter.cs - MnuExportItems" );

            // Create the CSV file to which grid data will be exported.
            string exportFileName = commObj.GetSaveAbsPathFileName( Application.StartupPath.ToString() );
            try
            {
                DataTable m_dtDataTable = (DataTable)dtgResult.DataSource;

                StreamWriter sw = new StreamWriter( exportFileName );
                // First we will write the headers.
                int colCount = m_dtDataTable.Columns.Count;
                for(int i = 0; i < colCount; i++)
                {
                    sw.Write( m_dtDataTable.Columns[i] );
                    if(i < colCount - 1)
                        sw.Write( "," );
                }//end of for
                sw.Write( sw.NewLine );

                //DataRow xRow = commObj.GetCurrentRow( dtgResult );
                ArrayList al = commObj.GetSelectedRowsArray( dtgResult );
                // OK... Now write the selected Row
                for(int j = 0; j < al.Count; j++)
                {
                    DataRow dataRow = m_dtDataTable.Rows[(int)al[j]];
                    for(int i = 0; i < colCount; i++)
                    {
                        if(!Convert.IsDBNull( dataRow[i] ))
                        {
                            sw.Write( dataRow[i].ToString() );
                        }
                        if(i < colCount - 1)
                            sw.Write( "," );
                    }//end of for
                    sw.Write( sw.NewLine );
                }//end of for                
                sw.Close();
            }//end of try
            catch(Exception ex)
            {
                Debug.WriteLine( ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace );
            }//end of catch
        }//end of MnuExportItems

        /// <summary>
        /// Simply enable or disable control prevent user action
        /// </summary>
        /// <param name="flag"></param>
        private void EnableControls(bool flag)
        {
            btnCountIt.Enabled = flag;
            btnAbort.Enabled = !flag;
            this.Cursor = flag ? Cursors.Default : Cursors.WaitCursor;

            rdoNsfCount.Enabled = flag;
            rdoPstCount.Enabled = flag;

            lnkFile.Enabled = flag;
            txtFileName.Enabled = flag;

            cboProfile.Enabled = flag;
            txtPassword.Enabled = flag;

        }//end of EnableControls

        /// <summary>
        /// Let do it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCountIt_Click(object sender, EventArgs e)
        {
            try
            {
                if(!ValidateUserInput())
                    return;

                InitVariable();
                ResetCounter();

                if(rdoPstCount.Checked)
                {
                    // do the pst count in threading mode
                    countSpecialThread = new Thread( new ThreadStart( this.Thd_CountPstMail ) );
                    countSpecialThread.Name = "Thd_CountPstMail";
                    countSpecialThread.Start();

                    commObj.LogToFile( "Thread.log", "++ PstInFolderThread Start ++" );
                }
                else
                    if(rdoNsfCount.Checked)
                    {
                        // do the nsf count in threading mode
                        countSpecialThread = new Thread( new ThreadStart( this.Thd_CountNsfMail ) );
                        countSpecialThread.Name = "Thd_CountNsfMail";
                        countSpecialThread.Start();

                        commObj.LogToFile( "Thread.log", "++ NsfInFolderThread Start ++" );
                    }

                EnableControls( false );
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + "\n" + ex.StackTrace;
                MessageBox.Show( msg, "Count It", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
                EnableControls( true );
            }
        }//end of btnCountIt_Click

        private void KillCountThread()
        {
            Trace.WriteLine( "UcMailCounter.cs - KillCountThread()" );
            try
            {
                commObj.LogToFile( "Thread.log", "Kill KillCountThread Start" );
                countSpecialThread.Abort(); // abort
                countSpecialThread.Join();  // require for ensure the thread kill
            }//end of try 
            catch(ThreadAbortException thdEx)
            {
                Trace.WriteLine( thdEx.Message );
                commObj.LogToFile( "Aborting the Count thread : " + thdEx.Message.ToString() );
            }//end of catch
            finally
            {
                EnableControls( true );
            }

        }//end of KillCountThread

        /// <summary>
        /// Clean up the added pst store. Map drive path must accessible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCleanMe_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnCleanMe.Enabled = false;
            btnCountIt.Enabled = false;
            btnAbort.Enabled = false;

            try
            {                
                oApp = new Outlook.ApplicationClass();
                oNameSpace = oApp.GetNamespace( "MAPI" );
                oNameSpace.Logon( cboProfile.Text, txtPassword.Text, false, true );

                RemovePstStore();

                //txtTotalMail.Text = "";
                //txtPstsSize.Text = "";
                //txtPstFileName.Text = "";

                DataTable clearDataTable = new DataTable();
                dtgResult.DataSource = clearDataTable;
            }//end of try
            catch(SystemException ex)
            {
                string msg = ex.Message.ToString() + "\n" + ex.GetType().ToString() + ex.StackTrace.ToString();
                commObj.LogToFile( msg );
                MessageBox.Show( "Check log: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly  );
            }//end of generic Exception
            finally
            {
                try
                {
                    oNameSpace.Logoff(); // both sucessful or fail
                    if(oApp != null)
                    {
                        Debug.WriteLine( "\t Quit Outlook" );
                        commObj.LogToFile( "Quit outlook" );
                        oApp.Quit();
                        Process[] localByName = Process.GetProcessesByName( "outlook" );
                        for(int n = 0; n < localByName.Length; n++)
                            localByName[n].Kill();
                    }
                }//end of try
                catch(Exception ex2)
                {
                    string msg = ex2.Message.ToString() + "\n" + ex2.GetType().ToString() + ex2.StackTrace.ToString();
                    commObj.LogToFile( msg );
                    MessageBox.Show( "Check log: " + ex2.Message.ToString(), "Outlook Logoff", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
                }//end of catch - mainly for outlook logoff and quit
            }//end of finally

            this.Cursor = Cursors.Default;
            btnCleanMe.Enabled = true;
            btnCountIt.Enabled = true;
            btnAbort.Enabled = true;
        }//end of btnCleanMe_Click

        private void btnAbort_Click(object sender, EventArgs e)
        {
            try
            {
                btnAbort.Enabled = false;
                btnCountIt.Enabled = true;
                this.Cursor = Cursors.Default;

                if(countSpecialThread != null && countSpecialThread.IsAlive)
                    this.KillCountThread();
            }//end of try
            catch(SystemException ex)
            {
                Debug.WriteLine( "UcMailCounter.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                commObj.LogToFile( "UcMailCounter.cs - btnAbort_Click " + ex.Message + "\n" + ex.StackTrace );
                MessageBox.Show( ex.Message + "\n" + ex.StackTrace, "Abort Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
        }
    }
}
