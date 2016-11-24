using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AccesoDatosNet;
using LogonScreen;
using System.Collections.Generic;

namespace RadioLogisticaDeliveries
{
    
    static class Values
    {
        private static string _rack = "";
        public static string _block = "";
        public static cAccesoDatosXML gDatos = new cAccesoDatosXML();
        public static string gBlock
        {
            get
            {
                return _block;
            }
            set
            {
                _block = value;
                Values.hFt.t3.Text = string.Format("Block: {0}", _block);
            }
        }
        

        public static int gOrderNumber;
        public static string gService;
        public static string gCloseCode = "000";
        public static string gSession;
        public static dataReadingList gDRL = new dataReadingList();
        public static string CurrentRack
        {
            get
            {
                return _rack;
            }
            set
            {
                _rack = value;
                hFt.t5.Text = string.Format("Rack: {0}", _rack);
            }
        }
        public static headerFragment hFt { get; set; }
        public static infoFragment iFt { get; set; }
        public static infoFragment dFt { get; set; }
        public static statusFragment sFt { get; set; }
    }

    [Activity(Label = "RadioLogisticaDeliveries", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var intent = new Intent(this, typeof(LogonScreenClass));
            intent.SetAction(Intent.ActionMain);
            intent.AddCategory(Intent.CategoryLauncher);
            intent.PutExtra("ConnectionType", "Socks");
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok && requestCode == 0)
            {
                string Result = data.GetStringExtra("Result");
                if (Result == "OK")
                {
                    Values.gDatos.DataBase = "LOGISTICA";
                    Values.gDatos.Server = "10.200.10.138";
                    Values.gDatos.User = LogonDetails.user;
                    Values.gDatos.Password = LogonDetails.password;

                    //create sqlite database
                    SQLidb.CreateDatabase("DELIVERIES");
                    SQLidb.db.CreateTableAsync<Readings>();
                    SQLidb.db.CreateTableAsync<Labels>();
                    //SQLidb.db.CreateTableAsync<Referencias>();
                    SQLidb.db.CreateTableAsync<RacksBlocks>();
                    SQLidb.db.CreateTableAsync<PartnumbersRacks>();
                    SQLidb.db.CreateTableAsync<SerialTracking>();
                    SQLidb.db.CreateTableAsync<ScannedData>();
                    // to do what to do when readings exist



                    //

                    var intent = new Intent(this, typeof(MainScreen));
                    StartActivityForResult(intent, 1);
                    //SetContentView(Resource.Layout.mainLayout);
                }
                else
                {
                    Finish();
                }
            }
            else
            {
                Finish();
            }
        }
    }
}


