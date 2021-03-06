﻿using System;
//using Android.App;
//using Android.Content;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.OS;
using System.Data;
using System.Data.SqlClient;

using LoginActivity;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using Java.Util.Zip;
using I18N.Common;
using I18N.West;
using Android.Support.V7.App;
using ZXing.Mobile;

namespace logon
{
    public static class Values
    {
        public static SqlConnection gDatos;
        public static string User { get; set; }
        public static string Pwd { get; set; }
        public static string gService;
        public static string Version;
        public static HeaderFragment hF { get; set; }
        public static DataInputFragment diF { get; set; }
        public static DataOutputFragment doF { get; set; }
        public static StatusFragment sF { get; set; }
    }

    [Activity(Label = "Espack Logon", MainLauncher = true, Icon = "@drawable/keyiconWhite")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            var a = CP1252.GetEncoding("utf-32");
            base.OnCreate(bundle);
            //for the image barcode scanner
            MobileBarcodeScanner.Initialize(Application);
            //version control
            Context context = this.ApplicationContext;
            Values.Version = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            ApplicationInfo ai = PackageManager.GetApplicationInfo(context.PackageName, 0);
            ZipFile zf = new ZipFile(ai.SourceDir);
            ZipEntry ze = zf.GetEntry("classes.dex");
            long time = ze.Time;
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(Math.Round(time / 1000D)).ToLocalTime();
            zf.Close();
            Values.Version = string.Format("{0}.{1}", Values.Version, dtDateTime.ToString("yyyyMMdd.Hmmss"));
            var intent = new Intent(this, typeof(LoginActivityClass));
            intent.SetAction(Intent.ActionView);
            intent.AddCategory(Intent.CategoryLauncher);
            intent.PutExtra("Version", Values.Version);
            intent.PutExtra("PackageName", "Partnumber Info");
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
                    Values.User = LoginDetails.User;
                    Values.Pwd = LoginDetails.Password;


                    //var RS = new DynamicRS("Select Datos=CMP_Varchar from datosEmpresa where codigo='FTP_DATA'",Values.gDatos);
                    //RS.Open();
                    //Values.gFTPServer = RS["Datos"].ToString().Split('|')[1]; //"10.201.10.1";
                    //Values.gFTPDir = RS["Datos"].ToString().Split('|')[2]; ;//"/FTP/";
                    //Values.gFTPUser = RS["Datos"].ToString().Split('|')[3];//"logon";
                    //Values.gFTPPassword = RS["Datos"].ToString().Split('|')[4]; ;//"*logon*";
                    var intent = new Intent(this, typeof(MainScreen));
                    StartActivityForResult(intent, 1);
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
        /*
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            var hFt = new HeaderFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.headerFragment, hFt);
            ft.Commit();
        }
        */
    }
}

