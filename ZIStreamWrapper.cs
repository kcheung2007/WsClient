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
    /// <summary>
    /// Wrapper object for unmanaged IStream interface.
    /// Default constructor : Take in a stream of data for manipulate
    /// </summary>
    public class ZIStreamWrapper : IStream
    {
        public ZIStreamWrapper(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "Can't wrap null stream.");
            this.stream = stream;
        }//end of constructor

        ~ZIStreamWrapper()
        {
            Debug.WriteLine("ZIStreamWrapper.cs - Destructor : ZIStreamWrapper");
            if (stream != null)
            {
                Debug.WriteLine("\t releasing the stream");
                stream.Close();
                stream.Dispose();
            }
        }

        Stream stream;

        public void Clone(out System.Runtime.InteropServices.ComTypes.IStream ppstm)
        {
            ppstm = null;
        }

        public void Commit(int grfCommitFlags) 
        { 
        }

        public void CopyTo(System.Runtime.InteropServices.ComTypes.IStream pstm, long cb, System.IntPtr pcbRead, System.IntPtr pcbWritten) 
        { 
        }

        public void LockRegion(long libOffset, long cb, int dwLockType) 
        { 
        }

        /// <summary>
        /// Read a managed stream of data into unmanaged memory location. 
        /// </summary>
        /// <param name="pv"></param>
        /// <param name="cb"></param>
        /// <param name="pcbRead"></param>
        public void Read(byte[] pv, int cb, System.IntPtr pcbRead)
        {
            Marshal.WriteInt64(pcbRead, (Int64)stream.Read(pv, 0, cb));
        }

        public void Revert() 
        { 
        }

        public void Seek(long dlibMove, int dwOrigin, System.IntPtr plibNewPosition)
        {
            // Don't use
            // Marshal.WriteInt64(plibNewPosition, stream.Seek(dlibMove, (SeekOrigin)dwOrigin));
        }

        public void SetSize(long libNewSize) 
        { 
        }

        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
        {
            pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG();;
            pstatstg.cbSize = stream.Length; // MUST DO for returning the stream.
        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType) 
        { 
        }

        /// <summary>
        /// Write an unmanaged stream of data to managed memory location.
        /// </summary>
        /// <param name="pv"></param>
        /// <param name="cb"></param>
        /// <param name="pcbWritten"></param>
        public void Write(byte[] pv, int cb, System.IntPtr pcbWritten) 
        {
            stream.Write(pv, 0, cb);
        }
    }//end of class - ZIStreamWrapper
}
