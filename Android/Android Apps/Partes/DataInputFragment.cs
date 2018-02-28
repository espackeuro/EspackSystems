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
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Views.InputMethods;

namespace Partes
{
    using static Values;
    public class DataInputFragment: Fragment
    {
        public TextInputEditText txtPartNumber { get; set; }
        public ImageButton btnScan { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var _root = inflater.Inflate(Resource.Layout.DataInputFragment, container, false);
            txtPartNumber = _root.FindViewById<TextInputEditText>(Resource.Id.txtDataInput);
            btnScan = _root.FindViewById<ImageButton>(Resource.Id.btnScan);
            //txtPartNumber.KeyPress += TxtPartNumber_KeyPress;
            txtPartNumber.EditorAction += TxtPartNumber_EditorAction;
            btnScan.Click += BtnScan_Click;
            //var _l = _root.FindViewById<TextInputLayout>(Resource.Id.layDataInput);
            return _root;
        }

        private async void BtnScan_Click(object sender, EventArgs e)
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var scannedText = await scanner.Scan();
            if (scannedText != null)
            {
                txtPartNumber.Text = scannedText.Text.Replace("-","");
                DataRecord result;
                DismissKeyboard();
                try
                {
                    result = await DataBaseAccess.GetData(txtPartNumber.Text, hF.spnDB.SelectedItem.ToString());
                }
                catch (Exception ex)
                {
                    Toast.MakeText(Activity, ex.Message, ToastLength.Long).Show();
                    sF.txtStatus.Text = ex.Message;
                    txtPartNumber.Text = "";
                    return;
                }
                //result = await DataBaseAccess.GetData(txtPartNumber.Text, hF.spnDB.SelectedItem.ToString());
                UpdateValues(result);
                sF.txtStatus.Text = "Partnumber found!";
            }


            //Toast.MakeText(Activity, "Value Entered: " + txtPartNumber.Text, ToastLength.Short).Show();

        }

        private async void TxtPartNumber_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            DataRecord result;
            e.Handled = false;
            if (e.ActionId == ImeAction.Send)
            {
                DismissKeyboard();
                try
                {
                    result = await DataBaseAccess.GetData(txtPartNumber.Text, hF.spnDB.SelectedItem.ToString());
                } catch (Exception ex)
                {
                    Toast.MakeText(Activity,ex.Message, ToastLength.Long).Show();
                    sF.txtStatus.Text = ex.Message;
                    txtPartNumber.Text = "";
                    e.Handled = true;
                    return;
                }
                //result = await DataBaseAccess.GetData(txtPartNumber.Text, hF.spnDB.SelectedItem.ToString());
                UpdateValues(result);
                sF.txtStatus.Text = "Partnumber found!";
                //Toast.MakeText(Activity, "Value Entered: " + txtPartNumber.Text, ToastLength.Short).Show();
                e.Handled = true;
            }
        }

        private void UpdateValues(DataRecord data)
        {
            Activity.RunOnUiThread(() =>
            {
                doF.txtSupplier.Text = data.Supplier;
                doF.txtFase4.Text = data.Fase4;
                doF.txtDescription.Text = data.Description;
                doF.txtPack.Text = data.Pack;
                doF.txtQtyPack.Text = data.QtyPack.ToString();
                doF.txtDock.Text = data.Dock;
                doF.txtLoc1.Text = data.Loc1;
                doF.txtLoc2.Text = data.Loc2;
                doF.txtSPP.Text = data.SPP.ToString();
                doF.txtSPA.Text = data.SPA.ToString();
                doF.txtSTD.Text = data.STD.ToString();
                doF.txtSPS.Text = data.SPS.ToString();
                doF.txtSPC.Text = data.SPC.ToString();
                doF.txtSPE.Text = data.SPE.ToString();
                doF.txtSPX.Text = data.SPX.ToString();
                doF.txtSQC.Text = data.SQC.ToString();
                doF.txtSEV.Text = data.SEV.ToString();
                doF.txtLastDeliveryDate.Text = data.LastDeliveryDate is null ? "--" : data.LastDeliveryDate.ToString();
                doF.txtLastDeliveryQty.Text = data.LastDeliveryQty.ToString();
                doF.txtMinGVDBA.Text = data.MinGVDBA.ToString();
                doF.txtMinDate.Text = data.MinDate.ToString();
                doF.txtMinQty.Text = data.MinQTY.ToString();
                doF.txtFlags.Text = data.Flags;
                doF.txtBreakDate.Text = data.BreakDate is null ? "--" : data.BreakDate.ToString();
            });
        }

        private void DismissKeyboard()
        {
            var view = Activity.CurrentFocus;
            if (view != null)
            {
                var imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
        }
    }
}