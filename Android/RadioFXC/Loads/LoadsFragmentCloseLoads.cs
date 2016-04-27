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
using cAccesoDatosAndroid;

namespace RadioFXC
{
    class LoadsFragmentCloseLoads : Android.Support.V4.App.Fragment
    {
        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.fragment_LoadsCloseLoad, container, false);
            root.FindViewById<TextView>(Resource.Id.lblLoadNumber).Text = Loads.Label;
            var button = root.FindViewById<TextView>(Resource.Id.btnClose);
            button.Click += (sender, e) =>
            {
                //Toast.MakeText(Activity, "Click! ", ToastLength.Short).Show();
                var _SP = new SP(Values.gDatos, "pCloseLoad");
                _SP.AddParameterValue("Load", Loads.LoadNumber);
                _SP.AddParameterValue("Service", Values.gService);
                try
                {
                    _SP.Execute();
                    if (_SP.LastMsg=="OK")
                    {
                        Activity.RunOnUiThread(() => Toast.MakeText(Activity, "Load closed correctly. ", ToastLength.Long).Show());
                        var intent = new Intent(Activity, typeof(MainScreen));
                        StartActivityForResult(intent, 1);
                    } else
                    {
                        throw (new Exception(_SP.LastMsg));
                    }
                }
                catch (Exception ex)
                {
                    Activity.RunOnUiThread(() => Toast.MakeText(Activity, "ERROR: " + ex.Message, ToastLength.Long).Show());
                }
            };
            return root;
        }
    }
}