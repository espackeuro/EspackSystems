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
        public bool Silent { get; set; }
    }
    public static class sScanner
    {
        private static bool silent;
        public static bool IsBusy { get; set; }
        public static ScanReceiver Receiver { get; set; }
        public static Activity ElContext { get; set; }
        private static IntentFilter Filter { get; set; }
        private static View RootView;

        public delegate bool AfterReceiveProcedure(string data);
        public static AfterReceiveProcedure ARP { get; set; }
        public static void RegisterScannerActivity(this Activity context, View rootView = null ,bool RegularEvents = false, AfterReceiveProcedure Procedure = null, bool Silent = false)
        {
            UnregisterScannerActivity();
            //register the new activity
            ElContext = context;
            ElContext.RegisterReceiver(Receiver,Filter);
            RootView = rootView;
            if (RegularEvents)
            {
                AfterReceive += Scanner_AfterReceive;
                BeforeReceive += Scanner_BeforeReceive;
            }
            ARP = Procedure;
            silent = Silent;
        }

        public static void UnregisterScannerActivity()
        {
            try
            {
                ElContext?.UnregisterReceiver(Receiver);
            }
            catch { }
            AfterReceive?.GetInvocationList().ToList().ForEach(x => AfterReceive -= (EventHandler<ReceiveEventArgs>)x);
            BeforeReceive?.GetInvocationList().ToList().ForEach(x => BeforeReceive -= (EventHandler)x);
            ElContext = null;
            ARP = null;
        }

        private static void Scanner_BeforeReceive(object sender, EventArgs e)
        {
            ((Activity)sender).RunOnUiThread(() => RootView.EditTextList().ForEach(et => et.Enabled = false));
        }

        private static void Scanner_AfterReceive(object sender, ReceiveEventArgs e)
        {
            var l = RootView.EditTextList();
            //var c = root.FocusedEditText();
            var _keyIndex = l.FindIndex(a => a.IsFocused == true);
            if (_keyIndex != -1)
            {
                ElContext.RunOnUiThread(() =>
                {
                    l[_keyIndex].Text = e.ReceivedData;
                    l[_keyIndex].SetSelection(l[_keyIndex].Text.Length);
                    if (!e.Silent) cSounds.Correct(ElContext);
                    if (_keyIndex < l.Count - 1) //move the focus to the next edittext view
                        l[_keyIndex + 1].RequestFocus();
                    else
                    {
                        var _res = ARP?.Invoke(e.ReceivedData);
                        if (_res == false)
                        {
                            l.ForEach(et => et.Text = "");
                            l[0].RequestFocus();
                        }
                    }
                    //c.DispatchKeyEvent(new KeyEvent(0, 0, KeyEventActions.Down, KeyEvent.KeyCodeFromString("KEYCODE_ENTER"), 0));
                });
            }
            else
            {
                if (!e.Silent) cSounds.Error(ElContext);
            }
            ((Activity)sender).RunOnUiThread(() => RootView.EditTextList().ForEach(et => et.Enabled = true));
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
                    try
                    {
                        AfterReceive?.Invoke(context, new ReceiveEventArgs() { ReceivedData = _scan, Silent = silent });
                    } catch (Exception ex)
                    {
                        if (!silent) cSounds.Error(context);
                        Toast.MakeText(context, string.Format("Error in postprocess: {0}", ex.Message), ToastLength.Long).Show();
                        _scan = "";
                        IsBusy = false;
                        return;
                    }
                    if (_scan == "")
                    {
                        if (!silent) cSounds.Error(context);
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