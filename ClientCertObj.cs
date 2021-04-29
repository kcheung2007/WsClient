using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Microsoft.Web.Services2.Security.X509;

namespace WsClient
{
    class ClientCertObj
    {
        private static CommObj commObj = new CommObj();

        public ClientCertObj()
        {
        }
    
        /// <summary>
        /// Return the client certificate by subject name from the local machine.
        /// Assumption:
        /// 1. Cert install per user level: Current user -> Personal store
        /// 2. Based on Subject name from the cert in reverse order
        /// 3. Subject Name was located in the app config
        /// </summary>
        /// <param name="szKeyID"></param>
        /// <returns></returns>
        public static X509Certificate GetClientCertBySubjectName(string subjectName)
        {
            X509Certificate cert = null;
            X509CertificateStore store = X509CertificateStore.CurrentUserStore( X509CertificateStore.MyStore );
            bool open = store.OpenRead();

            if(!open)
            {
                Debug.WriteLine( "Fail to open cert store." );
            }

            //Subject Key Identifier from Client cert.
            //byte[] certKeyID = new byte[] { 0x46, 0xc4, 0x8f, 0x21, 0x48, 0x41, 0x5f, 0xf6, 0x46, 0x75, 0xa6, 0x93, 0x6b, 0x92, 0xb4, 0x24, 0xfe, 0xc8, 0x62, 0x13 };
            //X509CertificateCollection certs = store.FindCertificateByKeyIdentifier( certKeyID );
  
            //X509CertificateCollection certs = store.FindCertificateBySubjectName( "C=US,S=California,L=San Francisco,O=Autonomy,OU=SE,CN=SE,E=sam.yan@autonomy.com" );
            X509CertificateCollection certs = store.FindCertificateBySubjectName( subjectName );


            if(0 < certs.Count)
            {
                string msg = "... Cert count more than 1. Using the first one from the collection ...";
                commObj.LogToFile( "WsClient.log", msg );

                cert = certs[0]; // assume there is only one client cert                
            }
            else
                cert = null;
            return (cert);
        }//end of GetClientCertBySubjectName
    }//end of class 
}
