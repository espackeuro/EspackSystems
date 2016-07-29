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
using System.Net;
using Android.Telephony;
using Socks;
using System.Xml.Linq;
using Encryption;

namespace LogonScreen
{
    public static class LogonDetails
    {
        public static string user { get; set; }
        public static string password { get; set; }
        

        public static String getDeviceID(Context p_context)
        {
            String m_deviceID = null;
            TelephonyManager m_telephonyManager = null;
            m_telephonyManager = (TelephonyManager)p_context.GetSystemService(Context.TelephonyService);
            m_deviceID = m_telephonyManager.DeviceId;

            if (m_deviceID == null || "00000000000000".Equals(m_deviceID))
            {
                m_deviceID = "AAAAAAA";
            }

            return m_deviceID;
        }
        public static string Serial
        {
            get
            {
                return Android.OS.Build.Serial;
            }
                
        }
        public static string getDeviceInfo(string p_seperator)
        {
            string m_data = "";
            Java.Lang.StringBuilder m_builder = new Java.Lang.StringBuilder();
            m_builder.Append("RELEASE " + Android.OS.Build.VERSION.Release + p_seperator);
            m_builder.Append("DEVICE " + Android.OS.Build.Device + p_seperator);
            m_builder.Append("MODEL " + Android.OS.Build.Model + p_seperator);
            m_builder.Append("PRODUCT " + Android.OS.Build.Product + p_seperator);
            m_builder.Append("BRAND " + Android.OS.Build.Brand + p_seperator);
            m_builder.Append("DISPLAY " + Android.OS.Build.Display + p_seperator);
            // TODO : Android.OS.Build.CPU_ABI is deprecated
            m_builder.Append("CPU_ABI " + Android.OS.Build.CpuAbi + p_seperator);
            // TODO : Android.OS.Build.CPU_ABI2 is deprecated
            m_builder.Append("CPU_ABI2 " + Android.OS.Build.CpuAbi2 + p_seperator);
            m_builder.Append("UNKNOWN " + Android.OS.Build.Unknown + p_seperator);
            m_builder.Append("HARDWARE " + Android.OS.Build.Hardware + p_seperator);
            m_builder.Append("ID " + Android.OS.Build.Id + p_seperator);
            m_builder.Append("MANUFACTURER " + Android.OS.Build.Manufacturer + p_seperator);
            m_builder.Append("SERIAL " + Android.OS.Build.Serial + p_seperator);
            m_builder.Append("USER " + Android.OS.Build.User + p_seperator);
            m_builder.Append("HOST " + Android.OS.Build.Host + p_seperator);
            m_data = m_builder.ToString();
            return m_data;
        }
}

    [Activity(Label = "Logon Screen")]
    public class LogonScreenClass : AppCompatActivity
    {
        private EditText cUser;
        private EditText cPassword;
        private TextView cMsgText;
        private Button cLoginButton;
        private cAccesoDatosNet gDatos = new cAccesoDatosNet();
        private cSocks gSocks;
        //private string typeofCaller;
        private bool Socks;
        private cSocks SocksServer;
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
            //typeofCaller = Intent.GetStringExtra("typeof") ;

#if DEBUG
            cUser.Text = "restelles";
            cPassword.Text = "1312";
#endif

            Socks = Intent.GetBooleanExtra("Socks", false);
            if (Socks)
            {
                XDocument _msgOut;
                string _result = "";
                gSocks = new cSocks("socks.espackeuro.com");
                try
                {
                    var _msg = gSocks.SyncEncConversation(gSocks.BuildSPXML("SISTEMAS", "pGetExternalIP", string.Format("@Serial='{0}'", LogonDetails.Serial)));
                    _msgOut = XDocument.Parse(_msg);
                    _result = _msgOut.Element("Result").Value;
                } catch (Exception ex)
                {
                    cMsgText.Text = "ERROR: " + ex.Message;
                    return;
                }
                gDatos.Server = _result.Substring(3);
                gDatos.DataBase = "SISTEMAS";
                gDatos.User = "SA";
                gDatos.Password = "5380";
                //SocksServer = Dns.GetHostEntry("socks.espackeuro.com").AddressList[0];
            }
            else
            {
                gDatos.DataBase = "SISTEMAS";
                gDatos.Server = "10.200.10.138";
                gDatos.User = "SA";
                gDatos.Password = "5380";
            }


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
            if (cUser.Text == "" || cPassword.Text == "")
            {
                cMsgText.Text = "Please input correct User and Password";
            }
            else
            {
                bool error = false;
                if (Socks)
                {
                    XDocument _msgOut;
                    try
                    {
                        var _msg = gSocks.SyncEncConversation(gSocks.BuildSPXML(gDatos.DataBase, "pLogonUser", "@Origin='RADIO LOGISTICA (VAL)'", cUser.Text, StringCipher.Encrypt(cPassword.Text)));
                        _msgOut = XDocument.Parse(_msg);
                    }
                    catch (Exception ex)
                    {
                        cMsgText.Text = "ERROR: " + ex.Message;
                        cUser.Text = "";
                        cPassword.Text = "";
                        cUser.RequestFocus();
                        return;
                    }
                    if (_msgOut.Element("Result").Value.Substring(0, 2) != "OK")
                    {
                        cMsgText.Text = "ERROR: " + _msgOut.Element("Result").Value;
                        cUser.Text = "";
                        cPassword.Text = "";
                        cUser.RequestFocus();
                        return;

                    }
                    LogonDetails.user = _msgOut.Root.Element("User").Value;
                    LogonDetails.password = StringCipher.Decrypt(_msgOut.Root.Element("Password").Value);
                }
                else
                {
                    try
                    {
                        RunOnUiThread(() => { gDatos.Connect(); });
                    }
                    catch (Exception ex)
                    {
                        var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                        builder.SetTitle("ERROR");
                        builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                        builder.SetMessage("Error: " + ex.Message);
                        builder.SetNeutralButton("OK", delegate
                        {
                            Intent inte = new Intent();
                            inte.PutExtra("Result", "ERROR");
                            SetResult(Result.Ok, inte);
                            Finish();
                        });
                        RunOnUiThread(() => { builder.Create().Show(); });
                        error = true;
                    }
                    if (!error)
                    {
                        StaticRS _RS = new StaticRS("select date=getdate()", gDatos);
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
                        catch (Exception ex)
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
                            return;
                        }
                        LogonDetails.user = LogonSP.ReturnValues()["@User"].ToString();
                        LogonDetails.password = LogonSP.ReturnValues()["@Password"].ToString();
                    }

                    //Toast.MakeText(this, "Logon OK!", ToastLength.Short).Show();


                }

                Intent intent = new Intent();
                intent.PutExtra("Result", "OK");
                SetResult(Result.Ok, intent);
                Finish();
            }
        }



    }



}
