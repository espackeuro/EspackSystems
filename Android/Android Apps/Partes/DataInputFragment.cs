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
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var _root = inflater.Inflate(Resource.Layout.DataInputFragment, container, false);
            txtPartNumber = _root.FindViewById<TextInputEditText>(Resource.Id.txtDataInput);
            //txtPartNumber.KeyPress += TxtPartNumber_KeyPress;
            txtPartNumber.EditorAction += TxtPartNumber_EditorAction;
            var _l = _root.FindViewById<TextInputLayout>(Resource.Id.layDataInput);
            return _root;
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
                Activity.RunOnUiThread(() =>
                {
                    doF.txtSupplier.Text = result.Supplier;
                    doF.txtFase4.Text = result.Fase4;
                    doF.txtDescription.Text = result.Description;
                    doF.txtPack.Text = result.Pack;
                    doF.txtQtyPack.Text = result.QtyPack.ToString();
                    doF.txtDock.Text = result.Dock;
                    doF.txtLoc1.Text = result.Loc1;
                    doF.txtLoc2.Text = result.Loc2;
                });
                sF.txtStatus.Text = "Partnumber found!";
                //Toast.MakeText(Activity, "Value Entered: " + txtPartNumber.Text, ToastLength.Short).Show();
                e.Handled = true;
            }
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