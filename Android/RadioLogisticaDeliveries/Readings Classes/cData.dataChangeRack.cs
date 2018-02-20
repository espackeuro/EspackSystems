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

namespace RadioLogisticaDeliveries
{
    public class dataChangeRack : cData
    {
        public override infoData Info
        {
            get
            {
                return new infoData()
                {
                    c0 = "New Rack:",
                    c1 = Values.CurrentRack,
                    c2 = "",
                    c3 = ""
                };
            }
        }

        public override async Task<bool> doCheckings()
        {
            var _rack = await Values.SQLidb.db.FindAsync<RacksBlocks>(r => r.Rack == Data);
            if (_rack == null)/*
            var query = await Values.SQLidb.db.Table<RacksBlocks>().Where(r => r.Rack == Data).ToListAsync();
            if (query.Count() == 0)*/
            {
                _errorMessage = string.Format("Wrong Rack {0}.", Data);
                Status = dataStatus.ERROR;
                return false;
            }
            if (_rack.Block != Values.gBlock)
            {
                _errorMessage = string.Format("Wrong Block for the Rack {0}.", _rack.Block);
                Status = dataStatus.ERROR;
                return false;
            }
            Values.SetCurrentRack(Data);
            Status = dataStatus.READ;
            return true;
        }

    }
}