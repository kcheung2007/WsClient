using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace WsClient
{
    /// <summary>
    /// Custom exception class for SMTPSender
    /// </summary>
    [Serializable()]
    public class SMTPException:System.Exception
    {
        private string _msg = "";

        public SMTPException()
        {
            _msg = "Generic SMTP Exception";
        }

        public SMTPException( string str ):base(str)
        {
            _msg = str;
        }

        public SMTPException(string message, Exception innerException):base (message, innerException)      
        {
         // Add any type-specific logic for inner exceptions.      
        }

        protected SMTPException(SerializationInfo info, StreamingContext context) : base(info, context)      
        {
         // Implement type-specific serialization constructor logic.
        }


        /// <summary>
        /// Exception Message
        /// </summary>
        public string SmtpMessage
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
            }
        }// end of construction
    }// end of class - SMTPException

	/// <summary>
	/// Class SMTPSender inherites from System.net.Socket.TcpClient that provides all the basic
	/// functionality to do TCP/IP programming.
	/// </summary>
	public class SMTPSender:System.Net.Sockets.TcpClient
	{
        private string _strRcptTo  = "";
        private string _strFrom    = "";
        private string _strTo      = "";
        private string _strCC      = "";
        private string _strBCC     = "";
        private string _strSubject = "";
        private string _strBodyTxt = "";
        private string _strSentDay = ""; 
        private string _strServer  = ""; // SMTP server name or ip
        private string _strPortNum = "";
        private string _strContent = "text/plain; charset=\"utf-8\"";
        private string _encoding   = "UTF-8";

        private const int BYTE_SIZE = 8192;

		public SMTPSender()
		{
			
		}// end of constructor

        public SMTPSender(string encoding)
        {
            _encoding = encoding;
        }// end of constructor

        /// <summary>
        /// RFC 821 - MAIL FROM:
        /// </summary>
        public string mailFrom
        {
            get
            {
                return _strFrom;
            }
            set
            {
                _strFrom = value;
            }
        }//end of property - mailFrom

        /// <summary>
        /// RFC 821 - RCPT TO:
        /// May contain more than one mail address with semicolon or comma. 
        /// </summary>
        public string RcptTo
        {
            get
            {
                return _strRcptTo;
            }
            set
            {
                _strRcptTo = value;
            }
        }// end of property


        /// <summary>
        /// RFC 822 - TO:
        /// May contain more than one mail address with semicolon or comma. 
        /// </summary>
        public string mailTo
        {
            get
            {
                return _strTo;
            }
            set
            {
                _strTo = value;
            }
        }// end of property

        /// <summary>
        /// RFC 822 - CC
        /// </summary>
        public string mailCC
        {
            get
            {
                return _strCC;
            }
            set
            {
                _strCC = value;
            }
        }// end of property

        /// <summary>
        /// RFC 822 - BCC
        /// </summary>
        public string mailBCC
        {
            get
            {
                return _strBCC;
            }
            set
            {
                _strBCC = value;
            }
        }//end of property

        /// <summary>
        /// RFC 822 - Mail Send date
        /// </summary>
        public string mailSentDate
        {
            get
            {
                return _strSentDay;
            }

            set
            {
                _strSentDay = value;
            }
        }//end of property

        /// <summary>
        /// RFC 822 - subject line - take whatever from user.
        /// </summary>
        public string mailSubject
        {                        
            get
            {
                return _strSubject;
            }

            set
            {
                _strSubject = value;
            }
        }//end of property

        /// <summary>
        /// RFC 822 - mail body
        /// </summary>
        public string mailBody
        {
            get
            {
                return _strBodyTxt;
            }

            set
            {
                _strBodyTxt = value;
            }
        }//end of property

        /// <summary>
        /// SMTP Server name or IP
        /// </summary>
        public string smtpServer
        {
            get
            {
                return _strServer;
            }
            set
            {
                _strServer = value;
            }
        }//end of property

        /// <summary>
        /// SMTP Server Port number
        /// </summary>
        public string smtpPortNum
        {
            get
            {
                return _strPortNum;
            }
            set
            {
                // TO DO : validate input - numeric characters only - regular expression
                _strPortNum = value;
            }
        }//end of property

        /// <summary>
        /// RFC 822 - Set Content type - for unparsable content
        /// </summary>
        public string mailContentType
        {

            get
            {
                return _strContent;
            }

            set
            {
                _strContent = value;
            }
        }//end of property - mailContentType

        /// <summary>
        /// Does all the work: Initiates SMTP communication, send the mail.
        /// This method uses three methods: 
        /// Connect() - inherited from the TcpClient class for establishing a TCP connection between client and TCP server.
        /// WriteToSocket() - write data to socket in ASCII format.
        /// ReadFromSocket() - read socket stream using GetStream method in TcpClient class
        /// Mail building based on property value... (similar to MS API)
        /// </summary>
        /// <returns>bool: true OK, false: fail</returns>
        public bool SmtpSend()
        {
            Trace.WriteLine("SMTPSender.cs - SmtpSend()" );
            bool rv = false; // assume everything is BAD due to throwing exception class
            string strMsg;
            string strReply; // reply from smtp server

            try
            {
                Debug.WriteLine("\tSmtpSend() - Connect to smtp server");
                Connect( _strServer, int.Parse( _strPortNum, CultureInfo.InvariantCulture ) ); // inherit from System.Net.Sockets.TcpClient
                strReply = ReadFromSocket();
                if( strReply.Substring(0,3) != "220" )
                    throw new SMTPException( strReply ); // will catch in the caller (eg. main)

                Debug.WriteLine("\tSmtpSend() - Test the connection HELO");
                strMsg = "HELO world\r\n"; // test connection
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.Substring(0, 3) != "250" )
                    throw new SMTPException( strReply );

                Debug.WriteLine("\tSmtpSend() - write mail from into socket");
                strMsg = "MAIL FROM:<" + _strFrom + ">\r\n"; // set up 821 header
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.Substring(0,3) != "250" )
                    throw new SMTPException( strReply );

                Debug.WriteLine("\tSmtpSend() - write rcpt to into socket");
                strMsg = "RCPT TO:<" + _strRcptTo + ">\r\n";
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.Substring(0, 3) != "250" )
                    throw new SMTPException( strReply );

/************************************
 * Do Not Add CC and BCC to RcptTo
 * 
                //if( _strCC != "" ) // CC exist - show in different repository but only one physical mail
                if( !String.IsNullOrEmpty(_strCC) )
                {
                    Debug.WriteLine("\tSmtpSend() - write CC (rcpt to) into socket");
                    strMsg = "RCPT TO: " + _strCC + "\r\n";
                    WriteToSocket( strMsg );
                    strReply = ReadFromSocket();
                    if( strReply.Substring(0, 3) != "250" )
                        throw new SMTPException( strReply );
                }//end of if - CC exist

                //if( _strBCC != "" ) // BCC exist - show in different repository but only one physical mail
                if(!String.IsNullOrEmpty( _strBCC ))
                {
                    Debug.WriteLine("\tSmtpSend() - write BCC (rcpt to) into socket");
                    strMsg = "RCPT TO: " + _strBCC + "\r\n";
                    WriteToSocket( strMsg );
                    strReply = ReadFromSocket();
                    if( strReply.Substring(0, 3) != "250" )
                        throw new SMTPException( strReply );
                }//end of if - BCC exist
 * 
 * Do Not Add CC and BCC to RcptTo
 * **********************************/

                Debug.WriteLine("\tSmtpSend() - Write DATA into socket - signaling SMTP server");
                strMsg = "DATA\r\n";
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.Substring(0, 3) != "354" )
                    throw new SMTPException( strReply );

                strMsg = "MIME-Version: 1.0\r\n";
                strMsg += "From: " + _strFrom + "\r\n";
                strMsg += "To: "     + _strTo + "\r\n"; // Everything here is identical
                if(!String.IsNullOrEmpty( _strCC ))
                    strMsg += "CC: " + _strCC + "\r\n";

                if(!String.IsNullOrEmpty( _strBCC ))
                    strMsg += "BCC: " + _strBCC + "\r\n"; // BCC is not require.. 

                strMsg += "Date: "   + _strSentDay + "\r\n";
                strMsg += "Subject: " + _strSubject + "\r\n"; // start 822 header set up

                strMsg += "Content-Type: " + _strContent + "\r\n";                    
                strMsg += "Message-ID: <" + System.Guid.NewGuid().ToString() + "@_Test_Message>\r\n"
                          + "\r\n"; // blink line;
 
                strMsg += _strBodyTxt + "\r\n";
                strMsg += ".\r\n"; // period - end of mail

                Debug.WriteLine("\tSmtpSend() - write message (DATA) into socket");
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.Substring(0, 3) != "250" )
                    throw new SMTPException( strReply );
                Debug.WriteLine("\tSmtpSend() - Send now and quit");
                strMsg = "QUIT\r\n"; // Send now...
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.IndexOf("221") == -1 )
                    throw new SMTPException( strReply );

                Close(); // TCP connection - inherited from System.Net.Sockets.TcpClient
                rv = true; // all the way down here is GOOD
            }//end of try
            catch( SocketException ex )
            {
                Trace.WriteLine("\tSocket Exception: " + ex.Message.ToString() );
            }//end of catch - socket exception
            
            return (rv);
        }//end of smtpSend


        /// <summary>
        /// Does all the work: Initiates SMTP communication, send the mail.
        /// This method uses three methods: 
        /// Connect() - inherited from the TcpClient class for establishing a TCP connection between client and TCP server.
        /// WriteToSocket() - write data to socket in ASCII format.
        /// ReadFromSocket() - read socket stream using GetStream method in TcpClient class
        /// Generic streaming a file into socket... Use to send RFC822 file is perfect or eml...
        /// </summary>
        /// <param name="fileName">eml file name for steaming into socket</param>
        /// <param name="bModMid">default is false. True ONLY if need to modify Message ID set as "Kentest"</param>
        /// <returns></returns>
        public bool SmtpSend( string fileName, bool bModMid )
        {
            Trace.WriteLine("SMTPSender.cs - SmtpSend " + fileName );
            bool rv = false; // assume everything is BAD due to throwing exception class
            string strMsg;
            string strReply; // reply from smtp server

            StreamReader sr = null;
            try
            {
                Debug.WriteLine("\tSmtpSend() - Connect to smtp server");
                Connect( _strServer, int.Parse( _strPortNum, CultureInfo.InvariantCulture ) ); // inherit from System.Net.Sockets.TcpClient
                strReply = ReadFromSocket();
                if( strReply.Substring(0,3) != "220" )
                    throw new SMTPException( strReply ); // will catch in the caller (eg. main)

                Debug.WriteLine("\tSmtpSend() - Test the connection HELO");
                strMsg = "HELO world\r\n"; // test connection
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.Substring(0, 3) != "250" )
                    throw new SMTPException( strReply );

                Debug.WriteLine("\tSmtpSend() - write mail from into socket");
                strMsg = "MAIL FROM: " + _strFrom + "\r\n"; // set up 821 header
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.Substring(0,3) != "250" )
                    throw new SMTPException( strReply );

                Debug.WriteLine("\tSmtpSend() - write rcpt to into socket");
                strMsg = "RCPT TO: " + _strRcptTo + "\r\n";
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.Substring(0, 3) != "250" )
                    throw new SMTPException( strReply );

                Debug.WriteLine("\tSmtpSend() - Write DATA into socket - signaling SMTP server");
                strMsg = "DATA\r\n";
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.Substring(0, 3) != "354" )
                    throw new SMTPException( strReply );

                sr = new StreamReader( fileName );

                if(bModMid) // modify the Message-ID
                {
                    while((strMsg = sr.ReadLine()) != null)
                    {
                        Debug.WriteLine( "\tLooping the text file line by line" );

                        // special case for Message-ID
                        if(strMsg.IndexOf( "Message-ID:" ) != -1)
                        {
                            strMsg = "Message-ID: <" + System.Guid.NewGuid().ToString() + "@_Test_Message>";
                        }

                        WriteToSocket( strMsg + "\r\n" );
                        // I think don't need the new line here
                        // WriteToSocket( strMsg );
                    }//end of while
                }//end of if
                else // no need to modify Message-ID - speed up the sending
                {
                    while((strMsg = sr.ReadLine()) != null)
                    {
                        Debug.WriteLine( "\tLooping the text file line by line" );
                        WriteToSocket( strMsg + "\r\n" );
                        // I think don't need the new line here
                        // WriteToSocket( strMsg );
                    }//end of while                   
                }

                strMsg = ".\r\n"; // period - end of mail
                // since no new line in message body add here
//                strMsg = "\r\n.\r\n"; // period - end of mail
                Debug.WriteLine("\tSmtpSend() - end of mail: write dot into socket");
                WriteToSocket( strMsg );

                strReply = ReadFromSocket();
                if( strReply.Substring(0, 3) != "250" )
                    throw new SMTPException( strReply );
                Debug.WriteLine("\tSmtpSend() - Send now and quit");
                strMsg = "QUIT\r\n"; // Send now...
                WriteToSocket( strMsg );
                strReply = ReadFromSocket();
                if( strReply.IndexOf("221") == -1 )
                    throw new SMTPException( strReply );

                rv = true; // all the way down here is GOOD
                Close(); // TCP connection - inherited from System.Net.Sockets.TcpClient
            }//end of try
            catch( SocketException ex )
            {
				string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Trace.WriteLine("\tSocket Exception: " + ex.Message.ToString() );
				// MessageBox.Show( msg, "Socket Exception" );

            }//end of catch - socket exception
            catch( IOException ioex )
            {
				string msg = ioex.Message + "\n" + ioex.GetType().ToString() + ioex.StackTrace;
                Trace.WriteLine("\tIO Exception: " + ioex.Message.ToString() );
				// MessageBox.Show( msg, "IO Exception" );
            }// end of catch - IO exception
            catch( Exception gEx )
            {
				string msg = gEx.Message + "\n" + gEx.GetType().ToString() + gEx.StackTrace;
                Trace.WriteLine("\tGeneric Exception: " + gEx.Message.ToString() );
				// MessageBox.Show( msg, "Generic Exception" );
            }// end of catch - IO exception

            return (rv);
        }//end of smtpSend

        /// <summary>
        /// Write data to socket in UTF-8 format. dotNet string class is unicode. ie: need to convert to ASCII encoding.
        /// Only ASCII and UTF-8 implement. Other conversion need more work.
        /// </summary>
        public void WriteToSocket( string msg )
        {
            Debug.WriteLine("SMTPSender.cs - WriteToSocket():" + msg.ToString() );
            byte[] writeBuffer = new byte[BYTE_SIZE]; // 8K

            switch(_encoding)
            {
                case "ASCII":
                    System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                    writeBuffer = asciiEncoding.GetBytes( msg );
                    break;
                case "UTF-7":
                    System.Text.UTF7Encoding utf7Encoding = new System.Text.UTF7Encoding();
                    writeBuffer = utf7Encoding.GetBytes( msg );
                    break;
                case "Unicode":
                    System.Text.UnicodeEncoding utf16Encoding = new System.Text.UnicodeEncoding();
                    writeBuffer = utf16Encoding.GetBytes( msg );
                    break;
                case "UTF-32":
                    System.Text.UTF32Encoding utf32Encoding = new System.Text.UTF32Encoding();
                    writeBuffer = utf32Encoding.GetBytes( msg );
                    break;
                default: //UTF-8
                    System.Text.UTF8Encoding utf8Encoding = new System.Text.UTF8Encoding();
                    writeBuffer = utf8Encoding.GetBytes( msg );
                    break;
            }//end of switch

            NetworkStream nwStream = GetStream();
            nwStream.Write( writeBuffer, 0, writeBuffer.Length );

        }//end of WriteToSocket

        /// <summary>
        /// Read data stream from socket and convert UTF-8 back to native dotNet string.
        /// </summary>
        /// <returns></returns>
        public string ReadFromSocket()
        {
            Debug.WriteLine( "SMTPSender.cs - ReadFromSocket()" );
            //System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            System.Text.UTF8Encoding utf8Encoding = new System.Text.UTF8Encoding();
            byte[] serverBuffer = new byte[BYTE_SIZE]; // 8K

            NetworkStream nwStream = GetStream();
            int count = nwStream.Read( serverBuffer, 0, serverBuffer.Length );
            if( count == 0 ) // no more data
                return( "" );
            else
                return (utf8Encoding.GetString( serverBuffer, 0, count ));
        }//end of ReadFromSocket
	}//end of class - SMTPSender
}// end of namespace - QATool
