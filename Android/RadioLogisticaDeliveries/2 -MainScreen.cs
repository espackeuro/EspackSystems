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
using System.Threading.Tasks;
using AccesoDatosNet;
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

            EspackCommServer.Server.PropertyChanged += ConnectionServer_PropertyChanged; ;
        }

        private async void ConnectionServer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName=="Status")
            {
                switch (EspackCommServer.Server.Status)
                {
                    case CommStatus.OFFLINE:
                        await Values.sFt.socksProgressStatus(ProgressStatusEnum.CONNECTED);
                        break;
                    case CommStatus.CONNECTED:
                        await Values.sFt.socksProgressStatus(ProgressStatusEnum.TRANSMITTING);
                        break;
                    case CommStatus.ERROR:
                        await Values.sFt.socksProgressStatus(ProgressStatusEnum.DISCONNECTED);
                        break;
                }
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