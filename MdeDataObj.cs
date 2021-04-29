using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Sample MDE from mde file
// 1 +++++++++++++++++++++++++++++
// 2 ./RT_0000000000000344583/L000/L100/L200/05283062b2d6e2ebe20000003048793f9e00000120d37e84bf00000000_SMTPSOAP/smtpsoap.latest
// 3 X-ZANTAZ-RECIPIENTS-S6-USER-GROUP-MAPPING: U3650_G5138,
// 4         ZMissing
// 5 X-MSSUPERDIVISIONCODE: ZMissing
// 6 X-ZANTAZ-SENDER-ID: #143388
// 7 X-ZANTAZ-STAMPING-STATUS: Complete
// 8 X-DEPARTMENTNUMBER: ZMissing
// 9 X-ZANTAZ-IDENTITY-LDIF-NAME: td1_JPMC_Standard.import@
//10 X-EMPLOYEENUMBER: ZMissing
//11 X-ZANTAZ-RECIPIENTS-S6-GROUP-ID: G5138,
//12         ZMissing
//13 X-ZANTAZ-RECIPIENTS-ID: U724716,
//14         O023833
//15 X-MSBLOOMBERGID: ZMissing
//16 X-ZANTAZ-SENDER-S6-GROUP-ID: G1
//17 X-ZANTAZ-REGION-CODE: ZMissing
//18 X-MSDIVISIONCODE: ZMissing
//19 X-ZANTAZ-IDENTITY-LOOKUP-STATUS: Complete
//20 X-ZANTAZ-SENDER-S6-USER-GROUP-MAPPING: U1319_G1
//21 X-MDE-Version: 1.2
//22 X-MDE-Application: smtpsoap
//23 X-MDE-ZDK-Version: 1.2
//24 SUBJECT: TC0
//25 Date: Sat Jan 01 00:00:00 GMT 2000
//26 MESSAGE-ID: <e03cae8d-6893-4eee-bdd7-dd8a44bd523a@_Test_Message>
//27 FROM: reghtest1@jpmorgan.com
//28 TO: chapmangrumbles@sametime
//29 BCC: simondrake@bloomberg.net
//30 X-MDE-Dedup-MethodID: 1
//31 RESENT-MESSAGE-ID: ZMissing
//32 RESENT-FROM: ZMissing
//33 RESENT-TO: ZMissing
//34 RESENT-DATE: ZMissing
//35 RESENT-CC: ZMissing
//36
//37 This


namespace WsClient
{
    class MdeDataObj
    {
        private string _mdeFileName;
        private string _xRcptS6UsrGrpMapping;
        private string _xSentS6UsrGrpMapping;
        private string _xMsSuperDivisionCode;       
        private string _xSenderID;
        private string _xStampingStatus; // Complete
        private string _xDepartmentNum;
        private string _xIdentityLdifName;
        private string _xEmployeeNum;
        private string _xRcptS6GrpID;
        private string _xRcptID;
        private string _xSenderS6GrpID;
        private string _xRegionCode;
        private string _xMsDivisionCode;
        private string _xRetentionStatus; // Complete
        private string _xIdentityLookupStatus; //Complete
        private string _xMsBloombergID;
        private string _xMdeVersion; // 1.2
        private string _xMdeApplication;
        private string _xMdeZdkVersion; // 1.2
        private string _from;
        private string _to;
        private string _cc;
        private string _bcc;

        private CommObj commObj = new CommObj();

        #region properties
        public string MdeFullFileName
        {
            get
            {
                return _mdeFileName;
            }
        }// end of 

        public string RcptS6UsrGrpMapping
        {
            get
            {
                return _xRcptS6UsrGrpMapping;
            }
        }

        public string SentS6UsrGrpMapping
        {
            get
            {
                return _xSentS6UsrGrpMapping;
            }
        }

        public string MsSuperDivisionCode
        {
            get
            {
                return _xMsSuperDivisionCode;
            }
        }// end of MsSuperDivisionCode

        public string SenderID
        {
            get
            {
                return _xSenderID;
            }
        }// end of SenderID

        public string StampingStatus
        {
            get
            {
                return _xStampingStatus;
            }
        }// end of StampingStatus

        public string DepartmentNumber
        {
            get
            {
                return _xDepartmentNum;
            }
        }// end of DepartmentNumber

        public string IdentityLdifName
        {
            get
            {
                return _xIdentityLdifName;
            }
        }// end of IdentityLdifName

        public string EmployeeNumber
        {
            get
            {
                return _xEmployeeNum;
            }
        }// end of EmployeeNumber

        public string RcptS6GroupID
        {
            get 
            { 
                return _xRcptS6GrpID; 
            }
        }

        public string RcptID
        {
            get
            {
                return _xRcptID;
            }
        }
        public string SenderS6GrpID
        {
            get
            {
                return _xSenderS6GrpID;
            }
        }
        public string RegionCode
        {
            get
            {
                return _xRegionCode;
            }
        }// end of RegionCode

        public string MsDivisionCode
        {
            get
            {
                return _xMsDivisionCode;
            }
        }// end of MsDivisionCode

        public string RetentionStatus
        {
            get
            {
                return _xRetentionStatus;
            }
        }// end of RetentionStatus

        public string IdentityLookupStatus
        {
            get
            {
                return _xIdentityLookupStatus;
            }
        }// end of IdentityLookupStatus

        public string MsBloombergID
        {
            get
            {
                return _xMsBloombergID;
            }
        }//end of MsBloombergID

        public string MdeVersion
        {
            get
            {
                return _xMdeVersion;
            }
        }// end of MdeVersion

        public string MdeApplication
        {
            get
            {
                return _xMdeApplication;
            }
        }// end of MdeApplication

        public string MdeZdkVersion
        {
            get
            {
                return _xMdeZdkVersion;
            }
        }// end of MdeZdkVersion

        public string From
        {
            get
            {
                return _from;
            }
        }//end of From

        public string To
        {
            get
            {
                return _to;
            }
        }// end of To

        public string CC
        {
            get
            {
                return _cc;
            }
        }// end of CC

        public string BCC
        {
            get
            {
                return _bcc;
            }
        }// end of BCC

        #endregion


                
        public MdeDataObj()
        {
        }

        public MdeDataObj(string[] strArray)
        {
            CreateMdeDataObj( strArray );
        }


        /// <summary>
        /// Create the MDE Data object: Pass the F
        /// </summary>
        /// <param name="inFile"></param>
        public void CreateMdeDataObj(string[] strArray)
        {            
            _mdeFileName = strArray[1];

            int i = 2;
            while(!strArray[i].Contains( "This" ))
            {
                if(strArray[i].Contains( ": " ))
                {
                    switch(strArray[i].Split( new Char[] { ':' } )[0].Trim())
                    {
                        case "X-ZANTAZ-RECIPIENTS-S6-USER-GROUP-MAPPING":
                            _xRcptS6UsrGrpMapping = strArray[i].Split( new Char[] { ':' } )[1].Trim(); // include comma
                            while(!strArray[i + 1].Contains( ": " ))
                                _xRcptS6UsrGrpMapping += strArray[++i].Trim();
                            break;
                        case "X-MSSUPERDIVISIONCODE":
                            _xMsSuperDivisionCode = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-ZANTAZ-SENDER-ID":
                            _xSenderID = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-ZANTAZ-STAMPING-STATUS":
                            _xStampingStatus = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-DEPARTMENTNUMBER":
                            _xDepartmentNum = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            while(!strArray[i + 1].Contains( ": " ))
                                _xDepartmentNum += strArray[++i].Trim();
                            break;
                        case "X-EMPLOYEENUMBER":
                            _xEmployeeNum = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-ZANTAZ-RECIPIENTS-S6-GROUP-ID":
                            _xRcptS6GrpID = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            while(!strArray[i + 1].Contains( ": " ))
                                _xRcptS6GrpID += strArray[++i].Trim();                            
                            break;
                        case "X-ZANTAZ-RECIPIENTS-ID":
                            _xRcptID = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            while(!strArray[i + 1].Contains( ": " ))
                                _xRcptID += strArray[++i].Trim();
                            break;
                        case "X-MSBLOOMBERGID":
                            _xMsBloombergID = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-ZANTAZ-SENDER-S6-GROUP-ID":
                            _xSenderS6GrpID = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            while(!strArray[i + 1].Contains( ": " ))
                                _xSenderS6GrpID += strArray[++i].Trim();
                            break;
                        case "X-ZANTAZ-REGION-CODE":
                            _xRegionCode = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            while(!strArray[i + 1].Contains( ": " ))
                                _xRegionCode += strArray[++i].Trim();
                            break;
                        case "X-MSDIVISIONCODE":
                            _xMsDivisionCode = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-ZANTAZ-RETENTION-STATUS":
                            _xRetentionStatus = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-ZANTAZ-IDENTITY-LOOKUP-STATUS":
                            _xIdentityLookupStatus = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-ZANTAZ-SENDER-S6-USER-GROUP-MAPPING":
                            _xSentS6UsrGrpMapping = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            while(!strArray[i + 1].Contains( ": " ))
                                _xSentS6UsrGrpMapping += strArray[++i].Trim();
                            break;
                        case "X-MDE-Version":
                            _xMdeVersion = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-MDE-Application":
                            _xMdeApplication = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "X-MDE-ZDK-Version":
                            _xMdeZdkVersion = strArray[i].Split( new Char[] { ':' } )[1].Trim();
                            break;
                        case "FROM":
                            _from = ExtractEmailAddr( strArray[i].Split( new Char[] { ':' } )[1].Trim() );
                            break;
                        case "TO":
                            _to = ExtractEmailAddr( strArray[i].Split( new Char[] { ':' } )[1].Trim() );
                            break;
                        case "CC":
                            _cc = ExtractEmailAddr( strArray[i].Split( new Char[] { ':' } )[1].Trim() );
                            break;
                        case "BCC":
                            _bcc = ExtractEmailAddr( strArray[i].Split( new Char[] { ':' } )[1].Trim() );
                            break;

                    }//end of witch
                }//end of if
                i++;
            }//end of while
        }//end of CreateMdeDataObj

        /// <summary>
        /// Assume 2 cases: 1. within <> or 2. valid abc@abc.com
        /// Extract Email within a <abc@xyz.com>
        /// Return an email address string.
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        private string ExtractEmailAddr(string inStr)
        {
            string str;

            if(inStr.IndexOf( '<' ) != -1) //found
                str = inStr.Substring( inStr.IndexOf( '<' ) + 1, inStr.IndexOf( '>' ) - inStr.IndexOf( '<' ) - 1 );
            else
                str = inStr;
            return (str);
        }

        public void PrintMdeDataRecord(string fileName)
        {
            commObj.LogToFile( fileName, _mdeFileName );
            commObj.LogToFile( fileName, "X-MSSUPERDIVISIONCODE: " + _xMsSuperDivisionCode );
            

        }
    }
}
