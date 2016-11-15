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
using CommonTools;

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
        private EditText elData { get; set; }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            MainScreen = (MainScreen)Activity;
            var _root = inflater.Inflate(Resource.Layout.enterDataFt, container, false);
            elData = _root.FindViewById<EditText>(Resource.Id.data);
            elData.KeyPress += ElData_KeyPress;
            return _root;
        }

        private void ElData_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.Event.Action == KeyEventActions.Down && (e.KeyCode == Keycode.Enter || e.KeyCode == Keycode.Tab))
            {
                //discriminator
                if (elData.Text == "" )
                {
                    Toast.MakeText(Activity, "Please enter valid data", ToastLength.Long).Show();
                    return;
                }
                Values.gDRL.Add(elData.Text);

            }
            else
            {
                e.Handled = false;
            }
        }

        public async override void OnResume()
        {
            base.OnResume();
            await Values.iFt.Clear();
            test();

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
        public void test()
        {
            var _query = SQLiteDatabase.db.Query<RacksBlocksParts>("Select * from RacksBlocks inner join PartnumbersRacks on RacksBlocks.Rack=PartnumbersRacks.Rack limit 6;");
            _query.ForEach(r => Values.iFt.pushInfo(r.Block, r.Rack, r.Partnumber, string.Format("{0}/{1}", r.MinBoxes, r.MaxBoxes)));
        }
    }



}