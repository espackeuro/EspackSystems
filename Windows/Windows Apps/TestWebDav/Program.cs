using DecaTec.WebDav;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestWebDav
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Run(() => Test());
            t.Wait();
        }

        private static async Task Test()
        {
            var credentials = new NetworkCredential("logon", "*logon*");
            var webDavSession = new WebDavSession(@"https://nextcloud.espackeuro.com/remote.php/dav/files/logon/", credentials);
            try
            {
                var items = await webDavSession.ListAsync(@"Android/APK/");
                foreach (var item in items)
                {
                    using (FileStream fs = File.OpenWrite(string.Format(@"d:\temp\{0}", item.Name)))
                        await webDavSession.DownloadFileAsync(item.Uri, fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private static async Task Test2()
    {
        var credentials = new NetworkCredential("logon", "*logon*");
        var _c = new WebClient();
        _c.UseDefaultCredentials = true;
        _c.Credentials =credentials;
        var _u = new Uri(@"https://nextcloud.espackeuro.com/remote.php/dav/files/logon/Android/APK/com.espack.radiologisticadeliveries.apk");
        await _c.DownloadFileAsync(_u, string.Format(@"d:\temp\com.espack.radiologisticadeliveries.apk_"));

    }

    
}
