using Android.App;
using Android.Widget;
using Android.OS;
using System.Data;
using System.Data.SqlClient;
using Android.Support.V7.App;
using Android.Support.Design.Widget;

namespace LoginScreen
{
    public static class LogonDetails
    {
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string ConnectionServer { get; set; }
        public static string FullName { get; set; }
    }

    [Activity(Label = "LoginScreen", MainLauncher = true)]
    public class LoginScreen : Activity
    {
        private TextInputEditText cUser;
        private TextInputEditText cPassword;
        private TextView cMsgText;
        private TextView cPackageInfoText;
        private Button cLoginButton;
        //private SqlConnection gDatos;
        private string typeofCaller;
        private string version;
        private string packageName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginScreen);
            //Form Elements
            cLoginButton = FindViewById<Button>(Resource.Id.btnLogin);
            cUser = FindViewById<TextInputEditText>(Resource.Id.User);
            //cUser.Text = "REJ";
            cPassword = FindViewById<TextInputEditText>(Resource.Id.Password);
            //cPassword.Text = "5380";
            cMsgText = FindViewById<TextView>(Resource.Id.msgText);
            cPackageInfoText = FindViewById<TextView>(Resource.Id.msgPkgInfo);
            //Button event
            cLoginButton.Click += CLoginButton_Click;
            typeofCaller = Intent.GetStringExtra("ConnectionType") ?? "Net";
            version = Intent.GetStringExtra("Version");
            packageName = Intent.GetStringExtra("PackageName");
            LogonDetails.ConnectionServer = "net.espackeuro.com";
            cPackageInfoText.Text = string.Format("{0} Version {1}", packageName, version);
#if DEBUG
            cUser.Text = "restelles";
            cPassword.Text = "1312";
#endif
        }
        public override void OnBackPressed()
        {

        }

    }
}

