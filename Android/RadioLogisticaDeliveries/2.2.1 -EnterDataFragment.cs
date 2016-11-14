using System;
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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            MainScreen= (MainScreen)Activity;
             var _root = inflater.Inflate(Resource.Layout.enterDataFt, container, false);
            return _root;
        }

        public async override void OnResume()
        {
            base.OnResume();
            await MainScreen.iFt.Clear();
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
            _query.ForEach(r => MainScreen.iFt.pushInfo(r.Block, r.Rack, r.Partnumber, string.Format("{0}/{1}", r.MinBoxes, r.MaxBoxes)));
        }
    }
}