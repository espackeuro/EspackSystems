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

namespace RadioLogisticaDeliveries
{
    [Activity(Label = "Radio LOGISTICA deliveries", WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainScreen : Activity
    {
        public headerFragment hf { get; set; }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainLayout);
            // Create your application here

            hf = new headerFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.headerFragment, hf);
            //ft.Commit();

            var of = new orderFragment();
            ft.Replace(Resource.Id.dataInputFragment, of);
            ft.Commit();
        }
    }
}