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

namespace PartnumberInfo
{
    public class HeaderFragment : Fragment
    {
        public EditText User { get; set; }
        public EditText Pwd { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var _root = inflater.Inflate(Resource.Layout.HeaderFt, container, false);
            User = _root.FindViewById<EditText>(Resource.Id.txtUser);
            Pwd = _root.FindViewById<EditText>(Resource.Id.txtPwd);
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}