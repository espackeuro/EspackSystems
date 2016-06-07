//using System;
//using Android.App;
//using Android.Content;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.OS;
using AccesoDatosNet;
using LogonScreen;

using Android.App;
using Android.Content;
using Android.OS;
using DataWedge;

namespace RadioFXC
{
    static class Values
    {
        public static cAccesoDatosNet gDatos = new cAccesoDatosNet();
        public static cAccesoDatosNet gDatosLOG = new cAccesoDatosNet();
        public static string gFTPServer;
        public static string gFTPUser;
        public static string gFTPDir;
        public static string gFTPPassword;
        public static string gService;
    }

    [Activity(Label = "*Radio REPAIRS", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : Activity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var intent = new Intent(this, typeof(LogonScreenClass));
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok && requestCode==0)
            {
                string Result = data.GetStringExtra("Result");
                if (Result == "OK")
                {
                    Values.gDatos.DataBase = "REPAIRS";
                    Values.gDatos.Server = "main.db.logon";
                    Values.gDatos.User = LogonDetails.user;
                    Values.gDatos.Password = LogonDetails.password;


                    var RS = new DynamicRS("Select Datos=CMP_Varchar from datosEmpresa where codigo='FTP_DATA'",Values.gDatos);
                    RS.Open();
                    Values.gFTPServer = RS["Datos"].ToString().Split('|')[1]; //"10.201.10.1";
                    Values.gFTPDir = RS["Datos"].ToString().Split('|')[2]; ;//"/FTP/";
                    Values.gFTPUser = RS["Datos"].ToString().Split('|')[3];//"logon";
                    Values.gFTPPassword = RS["Datos"].ToString().Split('|')[4]; ;//"*logon*";

                    // gDatos for LOGISTICA
                    Values.gDatosLOG.DataBase = "LOGTEST";
                    Values.gDatosLOG.Server = "main.db.logon";
                    Values.gDatosLOG.User = LogonDetails.user;
                    Values.gDatosLOG.Password = LogonDetails.password;

                    var intent = new Intent(this, typeof(ServiceSelection));
                    StartActivityForResult(intent, 1);
                } else
                {
                    Finish();
                }
            } else
            {
                Finish();
            }
        }
    }
}

