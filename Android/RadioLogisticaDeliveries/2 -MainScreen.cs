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
using Socks;

namespace RadioLogisticaDeliveries
{
    [Activity(Label = "Radio LOGISTICA deliveries", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class MainScreen : Activity
    {
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainLayout);
            // Create your application here

            Values.hFt = new headerFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.headerFragment, Values.hFt);
            //ft.Commit();

            var oFt = new orderFragment();
            ft.Replace(Resource.Id.dataInputFragment, oFt);


            Values.iFt = new infoFragment(12);
            ft.Replace(Resource.Id.InfoFragment, Values.iFt);

            Values.dFt = new infoFragment(5);
            ft.Replace(Resource.Id.DebugFragment, Values.dFt);

            Values.sFt = new statusFragment();
            ft.Replace(Resource.Id.StatusFragment, Values.sFt);
            ft.Commit();

            //Values.dtm = new DataTransferManager();
            //start the transmission service
            this.StartService(new Intent(this, typeof(DataTransferManager)));

            EspackSocksServer.ConnectionServer.StatusChange += ConnectionServer_StatusChange;
        }

        private void ConnectionServer_StatusChange(object sender, StatusChangeEventArgs e)
        {
            switch (e.NewStatus)
            {
                case SocksStatus.OFFLINE:
                    Values.sFt.socksProgressStatus(ProgressStatusEnum.CONNECTED);
                    break;
                case SocksStatus.CONNECTED:
                    Values.sFt.socksProgressStatus(ProgressStatusEnum.TRANSMITTING);
                    break;
                case SocksStatus.ERROR:
                    Values.sFt.socksProgressStatus(ProgressStatusEnum.DISCONNECTED);
                    break;
            }
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

        public void changeEnterDataToOrderFragments()
        {
            using (var ft = FragmentManager.BeginTransaction())
            {
                var oFt = new orderFragment();
                ft.Replace(Resource.Id.dataInputFragment, oFt);
                ft.Commit();
            }
        }

    }
}