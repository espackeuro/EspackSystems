using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
//using AccesoDatosNet;
using static PartnumberInformationScreen.Values;
using static PartnumberInformationScreen.Resource;

namespace PartnumberInformationScreen
{

    public static class Values
    {
        public static string User;
        public static string Pwd;
        public static string DB;
        //public static cAccesoDatosNet gDatos;
        public static headerFragment hFt;
    }

    [Activity(Label = "", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Layout.Main);
            hFt = new headerFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Id.headerFragment, hFt);
            // else
            ft.Commit();

            
        }
    }
}

