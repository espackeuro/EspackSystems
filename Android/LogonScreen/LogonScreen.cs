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
using CommonAndroidTools;

namespace LogonScreen
{
    public static class LogonDetails
    {
        public static string user { get; set; }
        public static string password { get; set; }
        public static string connectionServer { get; set; }
    }

    [Activity(Label = "Logon Screen")]
    public class LogonScreenClass : AppCompatActivity
    {
        private EditText cUser;
        private EditText cPassword;
        private TextView cMsgText;
        private Button cLoginButton;
        private cAccesoDatos gDatos;
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
            typeofCaller = Intent.GetStringExtra("ConnectionType") ?? "Net";
            LogonDetails.connectionServer = "net.espackeuro.com";//typeofCaller == "Net" ? "net.espackeuro.com" : "logon.espackeuro.com";
            switch (typeofCaller)
            {
                case "Net":
                    gDatos = (cAccesoDatosNet)ObjectFactory.createObject("Conn", typeofCaller, serial: cDeviceInfo.Serial);
                    break;
                case "Socks":
                    gDatos = (cAccesoDatosXML)ObjectFactory.createObject("Conn", typeofCaller, serial: cDeviceInfo.Serial);
                    break;

            };
            
#if DEBUG
            cUser.Text = "restelles";
            cPassword.Text = "1312";
#endif
        }

        //to cancel back button in Android
        public override void OnBackPressed()
        {

        }
        public void Alert(string text)
        {
            var _ad = new Android.App.AlertDialog.Builder(this)
                .SetTitle("Alert")
                .SetMessage(text)



                ;
        }

        private async void CLoginButton_Click(object sender, EventArgs e)
        {

            if (cUser.Text=="" || cPassword.Text=="")
            {
                cMsgText.Text = "Please input correct User and Password";
            } else
            {
                this.RunOnUiThread(() =>
                {
                    cLoginButton.Enabled = false;
                    cUser.Enabled = false;
                    cPassword.Enabled = false;
                });
                gDatos.DataBase = "SISTEMAS";
                gDatos.Server = LogonDetails.connectionServer;
                gDatos.User = "SA";
                gDatos.Password = "5380";
                bool error = false;
                
                try
                {
                    await gDatos.Connect();
                    //RunOnUiThread(async () => { });
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
                    RSFrame _RS;
                    _RS = (RSFrame)ObjectFactory.createObject("RS", typeofCaller, "select date=getdate()", gDatos);
                    await _RS.Open();
                    gDatos.Close();
                    SPFrame LogonSP;
                    LogonSP = (SPFrame)ObjectFactory.createObject("SP", typeofCaller, gDatos, "pLogonUser");
                    LogonSP.AddParameterValue("User", cUser.Text);
                    LogonSP.AddParameterValue("Password", cPassword.Text);
                    LogonSP.AddParameterValue("Origin", "RADIO LOGISTICA (VAL)");
                    try
                    {
                        await LogonSP.Execute();
                        if (LogonSP.LastMsg.Substring(0, 2) != "OK")
                            throw new Exception(LogonSP.LastMsg);
                        else
                        {
                            Toast.MakeText(this, "Logon OK!", ToastLength.Short).Show();
                            LogonDetails.user = LogonSP.ReturnValues()["@User"].ToString();
                            LogonDetails.password = LogonSP.ReturnValues()["@Password"].ToString();
                            Intent intent = new Intent();
                            intent.PutExtra("Result", "OK");
                            SetResult(Result.Ok, intent);
                            Finish();
                        }
                    } catch (Exception ex)
                    {
                        cMsgText.Text = ex.Message;
                        cUser.Text = "";
                        cPassword.Text = "";
                        cUser.RequestFocus();
                    }
                }
                this.RunOnUiThread(() =>
                {
                    cLoginButton.Enabled = true;
                    cUser.Enabled = true;
                    cPassword.Enabled = true;
                });
            }
        }
    }
}
