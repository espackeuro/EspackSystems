using AccesoDatosNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommonTools;
namespace FTP
{
    public class cFTP: IDisposable
    {
        private FtpWebRequest request { get; set; }
        private cServer server { get; set; }
        private string startingPath;

        public cFTP(cServer pServer,string pStartingPath)
        {
            server = pServer;
            startingPath = pStartingPath;
        }

        public List<DirectoryItem>GetDirectoryList(string path="")
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server.IP.ToString() + startingPath + path);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Proxy = null;
            request.Credentials = new NetworkCredential(server.User, server.Password);
            request.UsePassive = false;
            request.UseBinary = true;
            request.KeepAlive = false;
            string _all = "";
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    _all = reader.ReadToEnd();
                }
            }
            var DirectoryList = Regex.Matches(_all, "(.*?)\\r\\n");
            var result= DirectoryList.Cast<Match>().Select(match => new DirectoryItem(){BaseUri=request.RequestUri, Name=match.Value.Substring(55).Replace("\r\n",""), IsDirectory=(match.Value.Substring(0,1)=="d") }).ToList();
            return result;    

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~cFTPRequest() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
