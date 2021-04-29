using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using System.Text;

namespace WsClient
{
    [CLSCompliantAttribute( false )]
    public class ZDllWrapper
    {
        /// <summary>
        /// Check is the server alive
        /// </summary>
        /// <param name="serverURL">INPUT:string : WS URL</param>
        /// <param name="domain">INPUT:string : DS domain name</param>
        /// <returns>uint : 1 - OK; 0 - fail</returns>
        [DllImport("ZLib\\dsnadll.dll", EntryPoint = "isAlive")]
        public static extern uint isAlive(string serverURL, string domain);

        /// <summary>
        /// Save the file stream to DS
        /// </summary>
        /// <param name="fStream">INPUT:Stream : File stream</param>
        /// <param name="serverURL">INPUT:string : WS URL</param>
        /// <param name="domain">INPUT:string : DS domain name</param>
        /// <param name="sender">INPUT:string : mail from - sender email address</param>
        /// <param name="rcpt">INPUT:string : rcpt to - destination of the DS account</param>
        /// <param name="createTime">INPUT:string : archive creation time</param>
        /// <param name="strZDK">OUTPUT:string : return ZDK</param>
        /// <returns>uint : return code from gSOAP - unknown</returns>
        [DllImport("ZLib\\dsnadll.dll", EntryPoint = "savetods")]
        public static extern uint savetods(IStream fStream, string serverURL, string domain, string sender, string rcpt, string createTime, StringBuilder bufZDK);

        /// <summary>
        /// Restore a file from DS by passing the zdk and domain name
        /// </summary>
        /// <param name="fStream">OUTPUT:Stream : File Steam</param>
        /// <param name="strZDK">INPUT:string : ZDK</param>
        /// <param name="serverURL">INPUT:string : DS WS URL</param>
        /// <param name="domain">INPUT:string : DS domain</param>
        /// <returns>uint : return code from gSOAP</returns>
        [DllImport("ZLib\\dsnadll.dll", EntryPoint = "restorefromds")]
        public static extern uint restorefromds(IStream fStream, string strZDK, string serverURL, string domain);

        /// <summary>
        /// Update the meta data for particular file in DS
        /// </summary>
        /// <param name="zdk">INPUT : string</param>
        /// <param name="serverURL">INPUT : string</param>
        /// <param name="domain">INPUT : string</param>
        /// <param name="metadata">INPUT : string</param>
        /// <param name="rc">OUTPUT:bool - from g-soap???</param>
        /// <returns></returns>
        [DllImport("ZLib\\dsnadll.dll", EntryPoint = "metaupdate")]
        public static extern uint metaupdate(string zdk, string serverURL, string domain, string metadata, ref bool rc);
        
        public ZDllWrapper()
        {
            //MessageBox.Show("Constructor - ZDllWrapper");
        }

        ~ZDllWrapper()
        {
            //MessageBox.Show( "Destructor - ZDllWrapper" );
        }

        public static string getgSOAPErrorCode( uint rc )
        {
            string str = "";
            switch( rc )
            {
                case 1 :
                    str = "SOAP_CLI_FAULT";
                    break;
                case 2 :
                    str = "SOAP_SVR_FAULT";
                    break;
                case 3 :
                    str = "SOAP_TAG_MISMATCH";
                    break;
                case 4 :
                    str = "SOAP_TYPE";
                    break;
                case 5 :
                    str = "SOAP_SYNTAX_ERROR";
                    break;                    
                case 6 :
                    str = "SOAP_NO_TAG";                    
                    break;
                case 7:
                    str = "SOAP_IOB";                    
                    break;
                case 8 :
                    str = "SOAP_MUST_UNDERSTAND";                    
                    break;
                case 9 :
                    str = "SOAP_NAMESPACE";
                    break;
                case 10:
                    str = "SOAP_USER_ERROR";
                    break;
                case 11:
                    str = "SOAP_FATAL_ERROR";
                    break;
                case 12:
                    str = "SOAP_FAULT";
                    break;
                case 13:
                    str = "SOAP_NO_METHOD";
                    break;
                case 14:
                    str = "SOAP_GET_METHOD";
                    break;
                case 15:
                    str = "SOAP_EOM";
                    break;
                case 16:
                    str = "SOAP_NULL";
                    break;
                case 17:
                    str = "SOAP_DUPLICATE_ID";
                    break;
                case 18:
                    str = "SOAP_MISSING_ID";
                    break;
                case 19:
                    str = "SOAP_HREF";
                    break;
                case 20:
                    str = "SOAP_UDP_ERROR";
                    break;
                case 21:
                    str = "SOAP_TCP_ERROR";
                    break;
                case 22:
                    str = "SOAP_HTTP_ERROR";
                    break;
                case 23:
                    str = "SOAP_SSL_ERROR";
                    break;
                case 24:
                    str = "SOAP_ZLIB_ERROR";
                    break;
                case 25:
                    str = "SOAP_DIME_ERROR";
                    break;
                case 26:
                    str = "SOAP_DIME_HREF";
                    break;
                case 27:
                    str = "SOAP_DIME_MISMATCH";
                    break;
                case 28:
                    str = "SOAP_DIME_END";
                    break;
                case 29:
                    str = "SOAP_MIME_ERROR";
                    break;
                case 30:
                    str = "SOAP_MIME_HREF";
                    break;
                case 31:
                    str = "SOAP_MIME_END";
                    break;
                case 32:
                    str = "SOAP_VERSION_MISMATCH";
                    break;
                case 33:
                    str = "SOAP_PLUGIN_ERROR";
                    break;
                case 34:
                    str = "SOAP_DATA_ENCODING_UNKNOWN";
                    break;
                case 35:
                    str = "SOAP_REQUIRED";
                    break;
                case 36:
                    str = "SOAP_PROHIBITED";
                    break;
                case 37:
                    str = "SOAP_OCCURS";
                    break;
                case 38:
                    str = "SOAP_LENGTH";
                    break;
            }//end of switch
            return ("G-SOAP Error : " + str);
        }
    }//end of class - ZDllWrapper
}
