using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Text.RegularExpressions;
using CommonAndroidTools;

namespace Scanner
{
    public class ReceiveEventArgs : EventArgs
    {
        public string ReceivedData { get; set; }
    }
    public static class sScanner
    {
        public static bool IsBusy { get; set; }
        public static ScanReceiver Receiver { get; set; }
        public static Activity Context { get; set; }
        private static IntentFilter Filter { get; set; }
        public static void RegisterScannerActivity(this Activity context)
        {
            //remove events defined in previous activities
            AfterReceive?.GetInvocationList().ToList().ForEach(x => AfterReceive -= (EventHandler<ReceiveEventArgs>)x);
            BeforeReceive?.GetInvocationList().ToList().ForEach(x => BeforeReceive -= (EventHandler)x);
            //unregister the previous activity
            if (Context!= null)
            {
                Context.UnregisterReceiver(Receiver);
            }
            //register the new activity
            Context = context;
            Context.RegisterReceiver(Receiver,Filter);
        }

        static sScanner()
        {
            Filter = new IntentFilter("com.espack.SCAN");
            Receiver = new ScanReceiver();
            Filter.AddCategory(Intent.CategoryDefault);
            IsBusy = false;
        }
        //public static string ReceivedData { get; set; }
        public static event EventHandler<ReceiveEventArgs> AfterReceive;
        public static event EventHandler BeforeReceive;
        public class ScanReceiver : BroadcastReceiver
        {
            //public string ReceivedData { get; set; }
            //public event EventHandler AfterReceive;
            //public event EventHandler BeforeReceive;
            public override void OnReceive(Context context, Intent intent)
            {

                try
                {
                    if (IsBusy)
                        return;
                    IsBusy = true;
                    BeforeReceive?.Invoke(context, new EventArgs());
                    
                    string _scanAll = cDataWedge.HandleDecodeData(intent);
                    string _pattern = @"(.*)\|(Scanner|MSR)\|(.*)\|(\d+)";
                    var _matches = Regex.Match(_scanAll, _pattern);
                    string _scan = _matches.Groups[1].ToString();
                    AfterReceive?.Invoke(context, new ReceiveEventArgs() { ReceivedData = _scan });
                    if (_scan == "")
                    {
                        cSounds.Error(context);
                        Toast.MakeText(context, "Please enter valid data", ToastLength.Long).Show();
                        IsBusy = false;
                        return;
                    }

                    IsBusy = false;

                }
                catch //(Exception ex)
                {
                    IsBusy = false;
                }
            }
        }
       
    }
}