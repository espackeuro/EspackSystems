using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using CommonAndroidTools;
using System.Threading.Tasks;
using DataWedge;
using System.Text.RegularExpressions;

namespace RadioLogisticaDeliveries
{
    public class EnterDataFragment : Fragment
    {
        MainScreen MainScreen;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public static EditText elData { get; private set; }
        //public RadioGroup rg { get; private set; }
        public RadioButton radioChecking { get; private set; }
        public RadioButton radioReading { get; private set; }
        private ScanReceiver receiver;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            MainScreen = (MainScreen)Activity;
            var _root = inflater.Inflate(Resource.Layout.enterDataFt, container, false);
            //edittext to enter data
            elData = _root.FindViewById<EditText>(Resource.Id.data);
            //elData.InputType = Android.Text.InputTypes.ClassNumber;
            //elData.char
            elData.KeyPress += ElData_KeyPress;
            elData.ClearFocus();
            //radioButtons to switch between reading and checking
            //rg= _root.FindViewById<RadioGroup>(Resource.Id.radio)
            radioChecking = _root.FindViewById<RadioButton>(Resource.Id.radioChecking);
            radioReading = _root.FindViewById<RadioButton>(Resource.Id.radioReading);
            radioChecking.Enabled = (Values.gOrderNumber != 0);
            radioReading.Checked = Values.WorkMode==WorkModes.READING;
            radioChecking.Checked = Values.WorkMode == WorkModes.CHECKING;
            radioReading.CheckedChange += RadioReading_CheckedChange;
            //scanner intent
            var filter = new IntentFilter("com.espack.SCAN");
            filter.AddCategory(Intent.CategoryDefault);
            receiver = new ScanReceiver();
            Activity.RegisterReceiver(receiver, filter);


            //end
            return _root;
        }

        private void RadioReading_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Values.WorkMode = e.IsChecked ? WorkModes.READING : WorkModes.CHECKING;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            Activity.UnregisterReceiver(receiver);
        }

        public class ScanReceiver : BroadcastReceiver
        {
            public async override void OnReceive(Context context, Intent intent)
            {

                ((Activity)context).RunOnUiThread(() =>
                {
                    EnterDataFragment.elData.Enabled = false;
                });

                string _scanAll = cDataWedge.HandleDecodeData(intent);
                string _pattern = @"(.*)\|(Scanner|MSR)\|(.*)\|(\d+)";
                var _matches = Regex.Match(_scanAll, _pattern);
                string _scan = _matches.Groups[1].ToString();
                if (_scan == "")
                {
                    cSounds.Error(context);
                    Toast.MakeText(context, "Please enter valid data", ToastLength.Long).Show();
                    return;
                }
                Values.gDRL.Context = context;
                await Values.gDRL.Add(_scan);
                ((Activity)context).RunOnUiThread(() =>
                {
                    EnterDataFragment.elData.Enabled = true;
                    EnterDataFragment.elData.Text = "";
                });
                EnterDataFragment.elData.Tag = "SCAN";
            }
        }


        private async void ElData_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.Event.Action == KeyEventActions.Down && (e.KeyCode == Keycode.Enter || e.KeyCode == Keycode.Tab))
            {
                
                //ignore intent from scanner
                if (elData.Text== "" && elData.Tag.ToString()=="SCAN")
                {
                    elData.Tag = null;
                    e.Handled = true;
                    return;
                }
                //discriminator
                if (elData.Text == "" )
                {
                    Toast.MakeText(Activity, "Please enter valid data", ToastLength.Long).Show();
                    e.Handled = true;
                    return;
                }
                elData.Enabled = false;

                Values.gDRL.Context = Activity;
                await Values.gDRL.Add(elData.Text);
                elData.Text = "";
                elData.ClearFocus();
                elData.Enabled = true;
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            Values.iFt.Clear();
        }
        public class RacksBlocksParts
        {
            public int idreg { get; set; }
            public string Block { get; set; }
            public string Rack { get; set; }
            public string Partnumber { get; set; }
            public int MinBoxes { get; set; }
            public int MaxBoxes { get; set; }

        }
    }



}