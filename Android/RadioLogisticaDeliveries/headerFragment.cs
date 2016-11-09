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
        public TextView User { get; set; }
        public TextView Session { get; set; }
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
            User = _root.FindViewById<TextView>(Resource.Id.UserCell);
            User.Text = string.Format("User: {0}", Values.gDatos.User);
            Session = _root.FindViewById<TextView>(Resource.Id.SessionCell);
            Session.Text = string.Format("Session: {0}", "");
            return _root;
        }
    }
}