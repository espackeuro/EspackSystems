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
    class PartnumberFragment: Fragment
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

        private void TxtPartNumber_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            e.Handled = false;
            if (e.ActionId == ImeAction.Send)
            {
                Toast.MakeText(Activity, "Value Entered: " + txtPartNumber.Text, ToastLength.Short).Show();
                e.Handled = true;
            }
        }

        private void TxtPartNumber_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.Event.Action != KeyEventActions.Down || e.KeyCode != Keycode.Enter)
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;
            DismissKeyboard();
            Toast.MakeText(Activity, "Value Entered: " + txtPartNumber.Text, ToastLength.Short).Show();
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