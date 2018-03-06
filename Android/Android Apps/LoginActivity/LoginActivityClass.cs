using Android.App;
using Android.Widget;
using Android.OS;
using System.Data;
using System.Data.SqlClient;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using System;
using Android.Content;
using CommonAndroidTools;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Android.Views;

namespace LoginActivity
{
    public static class LoginDetails
    {
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string ConnectionServer { get; set; }
        public static string FullName { get; set; }
    }

    [Activity(Label = "", Theme = "@style/AppTheme.NoActionBar")]
    public class LoginActivityClass : AppCompatActivity
    {
        private EditText txtUser;
        private TextInputEditText txtPwd;
        private TextView txtMsg;
        private TextView txtPkgInfo;

        //private SqlConnection gDatos;
        private string oldVersion;
        private string packageName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            //var currentView = LayoutInflater.Inflate(Resource.Layout.LoginScreen, null);

            SetContentView(Resource.Layout.ls);
            //Form Elements
            txtUser = this.FindViewById<EditText>(Resource.Id.txtUser);

            //cUser.Text = "REJ";
            txtPwd = FindViewById<TextInputEditText>(Resource.Id.txtPwd);
            //cPassword.Text = "5380";
            txtMsg = FindViewById<TextView>(Resource.Id.msgText);
            txtPkgInfo = FindViewById<TextView>(Resource.Id.msgPkgInfo);
            //Button event
            //cLoginButton.Click += CLoginButton_Click;
            //typeofCaller = Intent.GetStringExtra("ConnectionType") ?? "Net";
            oldVersion = Intent.GetStringExtra("Version");
            packageName = Intent.GetStringExtra("PackageName");
            LoginDetails.ConnectionServer = "net.espackeuro.com";
            txtUser.EditorAction += TxtUser_EditorAction;
            txtPwd.EditorAction += TxtPwd_EditorAction;
            txtPkgInfo.Text = string.Format("{0} Version {1}", packageName, oldVersion);
#if DEBUG
            txtUser.Text = "restelles";
            txtPwd.Text = "1312";
#endif
        }
        private void TxtUser_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            e.Handled = false;
            if (e.ActionId == Android.Views.InputMethods.ImeAction.Next)
            {
                txtPwd.RequestFocus();
                e.Handled = true;
            }
        }
        private async void TxtPwd_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            e.Handled = false;
            if (e.ActionId == Android.Views.InputMethods.ImeAction.Send)
            {
                e.Handled = true;
                if (txtUser.Text == "" || txtPwd.Text == "")
                {
                    txtMsg.Text = "Please input correct User and Password";
                }
                else
                {
                    string _connectionString = string.Format("Server={0};Initial Catalog={1};User Id={2};Password={3};MultipleActiveResultSets=True;Connection Lifetime=3;Max Pool Size=3", "net.espackeuro.com", "SISTEMAS", "SA", "5380");
                    using (SqlConnection gDatos = new SqlConnection(_connectionString))
                    {
                        try
                        {
                            await gDatos.OpenAsync();
                        }
                        catch (Exception ex)
                        {
                            throw ex;//control errores TBD
                        }
                        using (var sp = new SqlCommand("pLogonUser", gDatos) { CommandType = CommandType.StoredProcedure })
                        {
                            SqlCommandBuilder.DeriveParameters(sp);
                            sp.Parameters["@msg"].Value = "";
                            sp.Parameters["@User"].Value = txtUser.Text;
                            sp.Parameters["@Password"].Value = txtPwd.Text;
                            sp.Parameters["@Origin"].Value = packageName;
                            sp.Parameters["@Version"].Value = "";
                            sp.Parameters["@PackageName"].Value = "";
                            sp.Parameters["@FullName"].Value = "";
                            try
                            {
                                await sp.ExecuteNonQueryAsync();
                            }
                            catch (Exception ex)
                            {
                                txtMsg.Text = ex.Message;
                                txtUser.Text = "";
                                txtPwd.Text = "";
#if DEBUG
                                txtUser.Text = "restelles";
                                txtPwd.Text = "1312";
#endif
                                txtUser.RequestFocus();
                            }
                            if (sp.Parameters["@msg"].Value.ToString() != "OK")
                            {
                                throw new Exception(sp.Parameters["@msg"].Value.ToString());
                            }
                            string _version = sp.Parameters["@Version"].Value.ToString();
                            string _packageName = sp.Parameters["@PackageName"].Value.ToString();
                            LoginDetails.User = sp.Parameters["@User"].Value.ToString();
                            LoginDetails.FullName = sp.Parameters["@FullName"].Value.ToString();
                            LoginDetails.Password = sp.Parameters["@Password"].Value.ToString();
                            var _versionArray = _version.Split('.');
                            _version = string.Format("{0}.{1}", _versionArray[0], _versionArray[1]);
                            var versionArray = oldVersion.Split('.');
                            var version = string.Format("{0}.{1}", versionArray[0], versionArray[1]);
                            if (_version != version)
                            {
                                bool dialogResult = await AlertDialogHelper.ShowAsync(this, "New version found", "Do you want to update your current program?", "Yes", "No");
                                if (dialogResult)
                                {
                                    var pd = ProgressDialog.Show(this, "", "Downloading...", false, false);
                                    pd.SetProgressStyle(ProgressDialogStyle.Spinner);
                                    await UpdatePackageURL(_packageName);
                                    pd.Dismiss();
                                }
                            }
                            Intent intent = new Intent();
                            intent.PutExtra("Result", "OK");
                            SetResult(Result.Ok, intent);
                            Finish();
                        }

                    }

                }
            }
        }

        public override void OnBackPressed()
        {

        }
        private async Task UpdatePackageURL(string packageName)
        {
            using (var c = new WebClient())
            {
                var URL = string.Format("http://portal.espackeuro.com/{0}.apk", packageName);
                var _local = string.Format("{0}/{1}.apk", Android.OS.Environment.ExternalStorageDirectory.Path, packageName);
                await c.DownloadFileTaskAsync(URL, _local);
                if (!File.Exists(_local))
                {
                    var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                    builder.SetTitle("ERROR");
                    builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    builder.SetMessage("Error: the file was not downloaded correctly, the app will not be updated.");
                    builder.SetNeutralButton("OK", delegate
                    {
                        Intent intnt = new Intent();
                        intnt.PutExtra("Result", "OK");
                        SetResult(Result.Ok, intnt);
                        Finish();
                    });
                    RunOnUiThread(() => { builder.Create().Show(); });
                }
                var intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(_local)), "application/vnd.android.package-archive");
                intent.SetFlags(ActivityFlags.NewTask);
                StartActivity(intent);
            }

        }
    }
}

