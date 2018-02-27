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

namespace Partes
{
    public class HeaderFragment : Fragment
    {
        public TextView lblFullName { get; set; }
        public TextView lblVersion { get; set; }
        public Spinner spnDB { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var _root = inflater.Inflate(Resource.Layout.HeaderFragment, container, false);
            lblFullName = _root.FindViewById<TextView>(Resource.Id.lblFullName);
            lblVersion = _root.FindViewById<TextView>(Resource.Id.lblVersion);
            spnDB = _root.FindViewById<Spinner>(Resource.Id.spnDB);
            lblFullName.Text = LogonScreen.LogonDetails.FullName;
            lblVersion.Text = Values.Version;
            return _root;
        }
    }
}