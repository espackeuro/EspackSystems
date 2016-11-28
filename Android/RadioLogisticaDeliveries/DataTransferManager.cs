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

namespace RadioLogisticaDeliveries
{
    public class DataTransferManager
    {
        public NetworkStatusMonitor monitor { get; set; }
        public Context context { get; set; }
        public bool Transmitting { get; private set; } = false;
        public DataTransferManager(Context _context)
        {
            context = _context;
            monitor = new NetworkStatusMonitor(context);
            monitor.NetworkStatusChanged += Monitor_NetworkStatusChanged;
            monitor.Start();
        }

        private void Monitor_NetworkStatusChanged(object sender, EventArgs e)
        {
            Values.sFt.text1.Text = monitor.State.ToString();
        }

        public async Task Transfer()
        {
            if (Transmitting)
                return;
            Transmitting = true;

            while (true)
            {
                var query = await SQLidb.db.Table<ScannedData>().Where(r => r.Transmitted == false).ToListAsync();
                if (query.Count == 0)
                    break;
                query.ForEach(async r =>
                {
                    if (monitor.State == NetworkState.ConnectedData || monitor.State == NetworkState.ConnectedWifi)
                    {

                        Values.gDatos.DataBase = "PROCESOS";
                        using (SPXML _sp = new SPXML(Values.gDatos, "pLaunchProcess_ReadingSessionControl"))
                        {
                            _sp.AddParameterValue("@DB", "LOGISTICA");
                            _sp.AddParameterValue("@ProcedureName", "pReadingSessionControl_TEST");
                            _sp.AddParameterValue("@Parameters", r.ProcedureParameters());
                            _sp.AddParameterValue("@TableDB", "");
                            _sp.AddParameterValue("@TableName", "");
                            _sp.AddParameterValue("@TablePK", "");
                            try
                            {
                                _sp.Execute();
                                if (_sp.LastMsg.Substring(0, 2) != "OK")
                                {
                                    Transmitting = false;
                                    return;
                                }
                                else
                                {
                                    r.Transmitted = true;
                                    await SQLidb.db.UpdateAsync(r);
                                }
                            }
                            catch
                            {
                                Transmitting = false;
                                return;
                            }
                        }
                    }
                    else
                    {
                        Transmitting = false;
                        return;
                    }
                    await Task.Delay(5000);
                });
            }
            Transmitting = false;
        }
    }
}