using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using static Partes.Values;

namespace Partes
{
    [Activity(Label = "", Theme = "@style/AppTheme.NoActionBar")]
    public class PartnumberInfo : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PartnumberInfo);

            hF = new HeaderFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.headerFragment, hF);
            diF = new DataInputFragment();
            ft.Replace(Resource.Id.dataInputFragment, diF);
            doF = new DataOutputFragment();
            ft.Replace(Resource.Id.dataOutputFragment, doF);
            sF = new StatusFragment();
            ft.Replace(Resource.Id.StatusFragment, sF);
            ft.Commit();
            // Create your application here
        }
    }
}