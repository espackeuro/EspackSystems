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
        public headerFragment hFt { get; set; }
        public infoFragment iFt { get; set; }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainLayout);
            // Create your application here

            hFt = new headerFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.headerFragment, hFt);
            //ft.Commit();

            var oFt = new orderFragment();
            ft.Replace(Resource.Id.dataInputFragment, oFt);


            iFt = new infoFragment();
            ft.Replace(Resource.Id.InfoFragment, iFt);
            ft.Commit();
        }

        public void changeOrderToEnterDataFragments()
        {
            using (var ft = FragmentManager.BeginTransaction())
            {
                var edFt = new EnterDataFragment();
                ft.Replace(Resource.Id.dataInputFragment, edFt);
                ft.Commit();
            }
        }

    }
}