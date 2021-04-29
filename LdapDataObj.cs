using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WsClient
{
    class LdapDataObj
    {
        // Hard code for the return ldap data size and define the useful ldap record.
        // Start with "dn" and end with "msccdesc"
        private const int RETURN_REC_SIZE = 100; // retrieved ldap record size
        private const int START_LDAP_REC  = 9;  // starting position of the useful record

        private string _dn = "ZMissing";
        private string _dbDirId = "ZMissing";
        private string _msfwid = "ZMissing";
        private string _employeeNum = "ZMissing";
        private string _departmentNum = "ZMissing";
        private string _cn = "ZMissing";
        private string _uid = "ZMissing";
        private string _mail = "ZMissing";
        private string _mailAlterInt0 = "ZMissing";
        private string _mailAlterInt1 = "ZMissing";
        private string _mailAlterInt2 = "ZMissing";
        private string _mailAlterExt = "ZMissing";
        private string _msBloombergId = "ZMissing";
        private string _dbBloombergId = "ZMissing";
        private string _msSuperDivisionCode = "ZMissing";
        private string _msDivisionCode = "ZMissing";
        private string _c = "ZMissing";
        private string _retentionCode = "ZMissing";
        private string _supervisorUid = "ZMissing";
        private string _s6groupid = "ZMissing";
        private string _s6usergroupmapping = "ZMissing";
        private string _st = "ZMissing";

        private string m_inStr;

        private CommObj commObj = new CommObj();

        #region properties   
        public string supervisorUid
        {
            get
            {
                return _supervisorUid;
            }
        }

        public string s6groupid
        {
            get
            {
                return _s6groupid;
            }
        }
        public string s6usergroupmapping
        {
            get
            {
                return _s6usergroupmapping;
            }
        }
        public string dn
        {
            get
            {
                return _dn;
            }
        }// end of ds

        public string dbdirid
        {
            get
            {
                return _dbDirId;
            }
        }//end of dbdirid

        public string employeeNumber
        {
            get
            {
                return _employeeNum;
            }
        }//end of employeeNumber

        public string departmentNumber
        {
            get
            {
                return _departmentNum;
            }
        }//end of departmentNumber

        public string cn
        {
            get
            {
                return _cn;
            }
        }

        public string uId
        {
            get
            {
                return _uid;
            }
        }

        public string mail
        {
            get
            {
                return _mail;
            }
        }

        public string mailAlterInt0
        {
            get
            {
                return _mailAlterInt0;
            }
        }

        public string mailAlterInt1
        {
            get
            {
                return _mailAlterInt1;
            }
        }

        public string mailAlterInt2
        {
            get
            {
                return _mailAlterInt2;
            }
        }

        public string mailAlterExt
        {
            get
            {
                return _mailAlterExt;
            }
        }

        public string msBloombergId
        {
            get
            {
                return _msBloombergId;
            }
        }

        public string dbBloombergId
        {
            get
            {
                return _dbBloombergId;
            }
        }

        public string msSuperDivisionCode
        {
            get
            {
                return _msSuperDivisionCode;
            }
        }

        public string msDivisionCode
        {
            get
            {
                return _msDivisionCode;
            }
        }

        public string C
        {
            get
            {
                return _c;
            }
        }

        public string retentionCode
        {
            get
            {
                return _retentionCode;
            }
        }

        public string msfwId
        {
            get
            {
                return _msfwid;
            }
        }
        public string ST
        {
            get
            {
                return _st;
            }
        }

        #endregion


        public LdapDataObj()
        {
        }

        public LdapDataObj(string inStr)
        {
            CreateLdapDataObj( inStr );
        }


        /// <summary>
        /// Create the LDAP Data object: Pass the ldapsearch return string and create the data object
        /// </summary>
        /// <param name="inStr"></param>
        public void CreateLdapDataObj( string inStr )
        {
            m_inStr = inStr;
            CreateLdapDataObj();
        }//end of CreateLdapDataObj

        /// <summary>
        /// Use the local member variable...
        /// </summary>
        public void CreateLdapDataObj()
        {
            try
            {
                Debug.WriteLine( m_inStr );
                bool skipMailAltInt = false;
                bool skipMailAltExt = false;
                string[] splitStr = new string[RETURN_REC_SIZE];
                splitStr = m_inStr.Split( new Char[] { '\n' } );

                int i = START_LDAP_REC;
                while(!splitStr[i].Contains( "numEntries" ))
                {
                    string[] subStr = new string[2];
                    subStr = splitStr[i].Split( new Char[] { ':' } );

                    try
                    {
                        switch(subStr[0])
                        {
                            case "dn":
                                _dn = subStr[1].Trim();
                                break;
                            case "dbdirid":
                                _dbDirId = subStr[1].Trim();
                                break;
                            case "msfwid":
                                _msfwid = subStr[1].Trim();
                                break;
                            case "employeeNumber":
                                _employeeNum = subStr[1].Trim();
                                break;
                            case "departmentNumber":
                                _departmentNum = subStr[1].Trim();
                                break;
                            case "cn":
                                _cn = subStr[1].Trim();
                                break;
                            case "uid":
                                _uid = subStr[1].Trim();
                                break;
                            case "mail":
                                _mail = subStr[1].Trim();
                                break;
                            case "mailalternateaddress;int":
                                if(!skipMailAltInt)
                                {
                                    _mailAlterInt0 = subStr[1].Trim();
                                    if( splitStr[i + 1].Contains(":") )
                                        _mailAlterInt1 = splitStr[++i].Split( new Char[] { ':' } )[1].Trim();
                                    if(splitStr[i + 1].Contains( ":" ))
                                        _mailAlterInt2 = splitStr[++i].Split( new Char[] { ':' } )[1].Trim();
                                    skipMailAltInt = true;
                                }
                                break;
                            case "mailalternateaddress;ext":
                                if(!skipMailAltExt)
                                {
                                    _mailAlterExt = subStr[1].Trim();
                                    skipMailAltExt = true;
                                }
                                break;
                            case "msbloombergid":
                                _msBloombergId = subStr[1].Trim();
                                break;
                            case "dbbloombergid":
                                _dbBloombergId = subStr[1].Trim();
                                break;
                            case "mssuperdivisioncode":
                                _msSuperDivisionCode = subStr[1].Trim();
                                break;
                            case "msdivisioncode":
                                _msDivisionCode = subStr[1].Trim();
                                break;
                            case "c":
                                _c = subStr[1].Trim();
                                break;
                            case "st":
                                _st = subStr[1].Trim();
                                break;
                            case "retentioncode":
                                _retentionCode = subStr[1].Trim();
                                break;
                            case "supervisorUid":
                                _supervisorUid = subStr[1].Trim();
                                break;
                            case "s6groupid":
                                if(_s6groupid == "ZMissing")
                                    _s6groupid = subStr[1].Trim();
                                else
                                    _s6groupid = _s6groupid + "," + subStr[1].Trim();
                                break;
                            case "s6usergroupmapping":
                                if(_s6usergroupmapping == "ZMissing")
                                    _s6usergroupmapping = subStr[1].Trim();
                                else
                                    _s6usergroupmapping = _s6usergroupmapping + "," + subStr[1].Trim();
                                break;
                        }//end of switch                            
                    }//end of try
                    catch(IndexOutOfRangeException idxOutRange)
                    {
                        string msg = idxOutRange.Message + ": " + subStr[0] + "\n" + idxOutRange.GetType().ToString() + idxOutRange.StackTrace;
                        Debug.WriteLine( msg );
                        commObj.WriteLineByLine( "mdeData.txt", msg );
                    }//end of catch

                    i++;
                }//end of while
            }//end of try
            catch(Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine( msg );
                commObj.WriteLineByLine( "mdeData.txt", msg );
            }//end of catch

        }//end of CreateLdapDataObj        
    }
}
