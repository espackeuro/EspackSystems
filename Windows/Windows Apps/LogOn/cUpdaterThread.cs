using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspackFormControls;
using Renci.SshNet;
using System.IO;
using System.Collections;
using AccesoDatosNet;
namespace LogOn
{
    public enum LogonItemUpdateStatus { PENDING, UPDATING, UPDATED}
    public struct cUpdateListItem
    {
        public cAppBot Parent;
        public string ServerPath;
        public string LocalPath;
        public cUpdaterThread Thread;
        public LogonItemUpdateStatus Status;
    }
    public class cUpdateList : List<cUpdateListItem>
    {

    }

    public class cUpdaterThread
    {
        private SftpClient Client { get; set; }
        private cServer Server { get; set; }
        public static cUpdateListItem stealOne(cUpdaterThread pThread)
        {
            //semaforo
            cUpdateListItem _item;
            try
            {
                _item = Values.UpdateList.OrderBy(x => x.Parent.Code).First(x => x.Status == LogonItemUpdateStatus.PENDING);
                _item.Thread = pThread;
                _item.Status = LogonItemUpdateStatus.UPDATING;
            }
            catch (Exception ex)
            {
                throw ex;
                //fin semaforo
            }
            //fin semaforo
            return _item;
        }
        public async Task Process()
        {
            await Task.Run(() =>
            {
                using (Client = new SftpClient(Values.ShareServerList[Values.COD3].IP.ToString(), Values.ShareServerList[Values.COD3].User, Values.ShareServerList[Values.COD3].Password))
                {
                    while (Values.AppList.PendingApps.Count != 0 || Values.AppList.CheckingApps.Count != 0)
                    {
                        var _item = stealOne(this);
                        //lblMsg.Text = "Connecting the server.";
                        Client.Connect();
                        //lblMsg.Text = "Server Connected!";
                        //Client.ChangeDirectory("/etc/dhcp/");
                        var _path = Path.GetDirectoryName(_item.LocalPath);
                        if (!Directory.Exists(_path))
                            Directory.CreateDirectory(_path);
                        using (var fileStream = new FileStream(_item.LocalPath, FileMode.Create))
                        {
                            Client.BufferSize = 4 * 1024; // bypass Payload error large files
                            Client.DownloadFile(_item.ServerPath, fileStream);
                        }
                        _item.Status = LogonItemUpdateStatus.UPDATED;
                        if (_item.Parent.UpdatedItems.Count == _item.Parent.Items.Count())
                        {
                            _item.Parent.Activate();
                        }
                    }
                }
            });
            return;
        }



    }



}
