using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AccesoDatosNet;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using DataWedge;

namespace LogonScreen
{
    public static class LogonDetails
    {
        public static string user { get; set; }
        public static string password { get; set; }
    }

    [Activity(Label = "Logon Screen")]
    public class LogonScreenClass : AppCompatActivity
    {
        private EditText cUser;
        private EditText cPassword;
        private TextView cMsgText;
        private Button cLoginButton;
        private cAccesoDatosNet gDatos = new cAccesoDatosNet();
        private string typeofCaller;
        
        //Main method
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //get the layout from Resources
            SetContentView(Resource.Layout.LogonScreenLayoutMaterial);
            //Form Elements
            cLoginButton = FindViewById<Button>(Resource.Id.Login);
            cUser = FindViewById<EditText>(Resource.Id.User);
            //cUser.Text = "REJ";
            cPassword = FindViewById<EditText>(Resource.Id.Password);
            //cPassword.Text = "5380";
            cMsgText= FindViewById<TextView>(Resource.Id.msgText);
            //Button event
            cLoginButton.Click += CLoginButton_Click;
            typeofCaller = Intent.GetStringExtra("typeof") ?? "Data not available";
#if DEBUG
            cUser.Text = "restelles";
            cPassword.Text = "1312";
#endif
            gDatos.DataBase = "SISTEMAS";
            gDatos.Server = "10.200.10.138";
            gDatos.User = "SA";
            gDatos.Password = "5380";
        }

        //to cancel back button in Android
        public override void OnBackPressed()
        {

        }
        public void Alert(string text)
        {
            var _ad = new Android.App.AlertDialog.Builder(this)
                .SetTitle("Alert")
                .SetMessage(text);
        }

        private void CLoginButton_Click(object sender, EventArgs e)
        {
            if (cUser.Text=="" || cPassword.Text=="")
            {
                cMsgText.Text = "Please input correct User and Password";
            } else
            {
                bool error = false;
                try
                {
                    RunOnUiThread(() => { gDatos.Connect(); });
                }
                catch (Exception ex)
                {
                    var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                    builder.SetTitle("ERROR");
                    builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    builder.SetMessage("Error: "+ex.Message);
                    builder.SetNeutralButton("OK", delegate 
                    {
                        Intent intent = new Intent();
                        intent.PutExtra("Result", "ERROR");
                        SetResult(Result.Ok, intent);
                        Finish();
                    });
                    RunOnUiThread(() => { builder.Create().Show(); });
                    error = true;
                }
                if (!error)
                {
                    StaticRS _RS = new StaticRS("select date=getdate()",gDatos);
                    _RS.Open();
                    gDatos.Close();
                    SP LogonSP = new SP(gDatos, "pLogonUser");
                    LogonSP.AddParameterValue("User", cUser.Text);
                    LogonSP.AddParameterValue("Password", cPassword.Text);
                    LogonSP.AddParameterValue("Origin", "RADIO LOGISTICA (VAL)");
                    try
                    {
                        LogonSP.Execute();
                    }
                    catch(Exception ex)
                    {
                        cMsgText.Text = "ERROR: " + ex.Message;
                        cUser.Text = "";
                        cPassword.Text = "";
                        cUser.RequestFocus();
                        return;
                    }
                    if (LogonSP.LastMsg.Substring(0, 2) != "OK")
                    {
                        cMsgText.Text = "ERROR: " + LogonSP.LastMsg;
                        cUser.Text = "";
                        cPassword.Text = "";
                        cUser.RequestFocus();
                        //gDatos.Close();

                    }
                    else
                    {
                        //Toast.MakeText(this, "Logon OK!", ToastLength.Short).Show();
                        LogonDetails.user = LogonSP.ReturnValues()["@User"].ToString();
                        LogonDetails.password = LogonSP.ReturnValues()["@Password"].ToString();
                        Intent intent = new Intent();
                        intent.PutExtra("Result", "OK");
                        SetResult(Result.Ok, intent);
                        Finish();
                    }
                }
            }
        }
    }
}
