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

namespace Partes
{
    [Activity(Label = "", Theme = "@style/AppTheme.NoActionBar")]
    public class PartnumberInfo : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PartnumberInfo);
            var hFt = new HeaderFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.headerFragment, hFt);
            var pFt = new PartnumberFragment();
            ft.Replace(Resource.Id.dataInputFragment, pFt);
            ft.Commit();
            // Create your application here
        }
    }
}