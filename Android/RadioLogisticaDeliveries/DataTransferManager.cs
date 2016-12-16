using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using NetworkDetection;
using System.Threading.Tasks;
using AccesoDatosNet;
using System.Threading;

namespace RadioLogisticaDeliveries
{
    [Service]
    public class DataTransferManager:Service
    {
        public NetworkStatusMonitor monitor { get; set; }
        //public Context Context { get; set; }
        public bool Transmitting { get; private set; } = false;
        //public DataTransferManager(Context _context)
        //{
        //    context = _context;
        //    monitor = new NetworkStatusMonitor(context);
        //    monitor.NetworkStatusChanged += Monitor_NetworkStatusChanged;
        //    monitor.Start();
        //}

        private async void Monitor_NetworkStatusChanged(object sender, EventArgs e)
        {
            if (monitor.State != NetworkState.ConnectedData && monitor.State != NetworkState.ConnectedWifi)
            {
                await Values.sFt.socksProgressStatus(ProgressStatusEnum.DISCONNECTED);
            } else
            {
                await Values.sFt.socksProgressStatus(ProgressStatusEnum.CONNECTED);
            }
        }

        public async Task DoWork()
        {
            while (true)
            {
                //SpinWait.SpinUntil(() => Values.SQLidb.pendingData && (monitor.State == NetworkState.ConnectedData || monitor.State == NetworkState.ConnectedWifi));
                Thread.Sleep(5000);
                await Transfer();
            }
        }

        public async Task Transfer()
        {
            if (Transmitting)
                return;
            Transmitting = true;
            while (true)
            {
                var query = await Values.SQLidb.db.Table<ScannedData>().Where(r => r.Transmitted == false).ToListAsync();
                if (query.Count == 0)
                    break;
                query.ForEach(async r =>
                {
                    Thread.Sleep(500);
                    if (monitor.State == NetworkState.ConnectedData || monitor.State == NetworkState.ConnectedWifi)
                    {
                        //Values.sFt.socksProgress.Visibility = ViewStates.Visible;
                        Values.gDatos.DataBase = "PROCESOS";
                        using (SPXML _sp = new SPXML(Values.gDatos, "pLaunchProcess_ReadingSessionControl"))
                        {
                            //SPXML _sp = new SPXML(Values.gDatos, "pLaunchProcess_ReadingSessionControl");
                            _sp.AddParameterValue("@DB", "LOGISTICA");
                            _sp.AddParameterValue("@ProcedureName", "pReadingSessionControl_TEST");
                            _sp.AddParameterValue("@Parameters", r.ProcedureParameters());
                            _sp.AddParameterValue("@TableDB", "");
                            _sp.AddParameterValue("@TableName", "");
                            _sp.AddParameterValue("@TablePK", "");
                            try
                            {
                                await _sp.Execute();
                                if (_sp.LastMsg.Substring(0, 2) != "OK")
                                {
                                    Transmitting = false;
                                    return;
                                }
                                else
                                {
                                    r.Transmitted = true;
                                    await Values.SQLidb.db.UpdateAsync(r);
                                    switch (r.Action)
                                    {
                                        case "CHECK":
                                            Values.sFt.CheckQtyTransmitted++;
                                            break;
                                        case "ADD":
                                            Values.sFt.ReadQtyTransmitted++;
                                            break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Transmitting = false;
                                await Values.dFt.pushInfo(ex.Message);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Transmitting = false;
                        return;
                    }
                    
                });
            }
            Transmitting = false;
            return;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            monitor = new NetworkStatusMonitor(this);
            monitor.NetworkStatusChanged += Monitor_NetworkStatusChanged;
            monitor.Start();
            new Task(async () => await DoWork()).Start();
            return StartCommandResult.Sticky;
        }
    }
}