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
using Android.Support.V7.App;
using static PartnumberInformationScreen.Resource;
using static PartnumberInformationScreen.Values;

namespace PartnumberInformationScreen
{
    public class headerFragment : Fragment
    {
        private EditText _user;
        private EditText _pwd;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var _root = inflater.Inflate(Layout.headerFragment, container, false);
            _user = _root.FindViewById<EditText>(Resource.Id.txtUser);
            _pwd = _root.FindViewById<EditText>(Resource.Id.txtPwd);

            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}