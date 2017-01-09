using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace RadioLogisticaDeliveries
{
    public class headerFragment : Fragment
    {
        public TextView t1 { get; set; }
        public TextView t2 { get; set; }
        public TextView t3 { get; set; }
        public TextView t4 { get; set; }
        public TextView t5 { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var _root = inflater.Inflate(Resource.Layout.headerFt, container, false);
            t1 = _root.FindViewById<TextView>(Resource.Id.t1);
            t1.Text = string.Format("User: {0}", Values.gDatos.User);
            t2 = _root.FindViewById<TextView>(Resource.Id.t2);
            t2.Text = string.Format("Session: {0}", "");
            t3 = _root.FindViewById<TextView>(Resource.Id.t3);
            t4 = _root.FindViewById<TextView>(Resource.Id.t4);
            t5 = _root.FindViewById<TextView>(Resource.Id.t5);
            return _root;
        }
        public void Clear()
        {
            t1.Text = string.Format("User: {0}", Values.gDatos.User);
            t2.Text = string.Format("Session: {0}", "");
            t3.Text = "";
            t4.Text = "";
            t5.Text = "";
        }
    }
}