using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DataWedge;
using AccesoDatosNet;

namespace RadioLogisticaDeliveries
{

    [Activity(Label = "Radio LOGISTICA deliveries", WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainScreen : AppCompatActivity
    {
        protected EditText txtReading;
        protected TextView txtPartnumber;
        protected TextView txtQty;
        protected TextView txtLabelRack;
        protected TextView txtCM;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.Title = "Block " + Values.gBlock;
            SetContentView(Resource.Layout.Deliveries);
            txtReading = FindViewById<EditText>(Resource.Id.txtReading);
            txtPartnumber = FindViewById<TextView>(Resource.Id.txtPartnumber);
            txtQty = FindViewById<TextView>(Resource.Id.txtQty);
            txtLabelRack = FindViewById<TextView>(Resource.Id.txtLabelRack);
            var btnCloseSession = FindViewById<Button>(Resource.Id.btnCloseSession);

            txtReading.KeyPress += TxtReading_KeyPress;

        }

        private void TxtReading_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.KeyCode== Keycode.Enter || e.KeyCode== Keycode.Tab)
            {
                Toast.MakeText(this, txtReading.Text, ToastLength.Long).Show();
            }
        }

        private void TxtReading_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

}