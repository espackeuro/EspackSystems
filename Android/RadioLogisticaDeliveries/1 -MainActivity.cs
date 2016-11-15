using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AccesoDatosNet;
using LogonScreen;

namespace RadioLogisticaDeliveries
{
    
    static class Values
    {
        public static cAccesoDatosXML gDatos = new cAccesoDatosXML();
        public static string gBlock;
        public static int gOrderNumber;
        public static string gService;
        public static string gCloseCode = "000";
        public static dataReadingList gDRL = new dataReadingList();
        public static string CurrentRack= "";
        public static headerFragment hFt { get; set; }
        public static infoFragment iFt { get; set; }
    }

    [Activity(Label = "RadioLogisticaDeliveries", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var intent = new Intent(this, typeof(LogonScreenClass));
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
                    SQLiteDatabase.CreateDatabase("DELIVERIES");
                    SQLiteDatabase.db.CreateTable<Readings>();
                    SQLiteDatabase.db.CreateTable<Labels>();
                    SQLiteDatabase.db.CreateTable<Referencias>();
                    SQLiteDatabase.db.CreateTable<RacksBlocks>();
                    SQLiteDatabase.db.CreateTable<PartnumbersRacks>();
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


