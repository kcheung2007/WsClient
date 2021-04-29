using System;
using System.IO;


namespace WsClient
{
	/// <summary>
	/// Summary description for AttachObj.
	/// </summary>
	public class AttachObj
	{
		private int m_IdxAttach; 
		private int m_NumFile;
		//Don't use - private string m_inFolder = "";

		private DirectoryInfo m_di;
		private FileInfo[] m_lstFiles;

		public AttachObj( string infolder )
		{
            //Don't use - m_inFolder = inFolder;			
			m_di = new DirectoryInfo(infolder); // attachment folder
			m_lstFiles = m_di.GetFiles();
			m_NumFile = m_lstFiles.Length;
		}//end of constructor

		public int IdxAttach
		{
			get
			{
				return m_IdxAttach;
			}
			set
			{
				m_IdxAttach = value;
			}
		}// end of idxAttach

		public int NumFile
		{
			get
			{
				return( m_NumFile );
			}
		}// end of numFile

		public string AttachFullName
		{
			get
			{
				return( m_lstFiles[m_IdxAttach].FullName );
			}
		}//end of attchFullFame

        public FileInfo[] GetListFiles()
        {
            return( m_lstFiles );
            
        }
	}
}
