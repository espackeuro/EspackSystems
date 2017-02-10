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
       
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainLayout);
            // Create your application here

            string _mainScreenMode = Intent.GetStringExtra("MainScreenMode");


            Values.hFt = new headerFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.headerFragment, Values.hFt);
            //ft.Commit();

            if (_mainScreenMode=="NEW")
            {
                var oFt = new orderFragment();
                ft.Replace(Resource.Id.dataInputFragment, oFt);
            } else
            {
                var edFt = new EnterDataFragment();
                ft.Replace(Resource.Id.dataInputFragment, edFt);
                Values.elIntent = new Intent(this, typeof(DataTransferManager));
                StartService(Values.elIntent);
                DataTransferManager.Active = true;
            }



            Values.iFt = new infoFragment(8);
            ft.Replace(Resource.Id.InfoFragment, Values.iFt);

            Values.dFt = new infoFragment(5);
            ft.Replace(Resource.Id.DebugFragment, Values.dFt);

            Values.sFt = new statusFragment();
            ft.Replace(Resource.Id.StatusFragment, Values.sFt);
            ft.Commit();

            //Values.dtm = new DataTransferManager();
            //start the transmission service


            EspackCommServer.Server.PropertyChanged += ConnectionServer_PropertyChanged; ;
        }

        private async void ConnectionServer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName=="Status")
            {
                switch (EspackCommServer.Server.Status)
                {
                    case CommStatus.OFFLINE:
                        await Values.sFt.commProgressStatus(ProgressStatusEnum.CONNECTED);
                        break;
                    case CommStatus.CONNECTED:
                        await Values.sFt.commProgressStatus(ProgressStatusEnum.TRANSMITTING);
                        break;
                    case CommStatus.ERROR:
                        await Values.sFt.commProgressStatus(ProgressStatusEnum.DISCONNECTED);
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
        public override void OnBackPressed()
        {
        }
    }
}