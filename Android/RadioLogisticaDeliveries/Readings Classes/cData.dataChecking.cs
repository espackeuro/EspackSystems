using System.Linq;
using System.Threading.Tasks;
using System;

namespace RadioLogisticaDeliveries
{
    public class dataChecking : cData
    {
        public string Serial { get; set; }
        public override infoData Info
        {
            get
            {
                return new infoData()
                {
                    c0 = "Serial:",
                    c1 = Serial,
                    c2 = "",
                    c3 = ""
                };
            }
        }
        public override async Task<bool> doCheckings()
        {
            //check if Rack is set
            if (Values.CurrentRack == "")
            {
                _errorMessage = "Rack not set, read rack first.";
                Status = dataStatus.ERROR;
                return false;
            }
            //check if already checked
            var query = await SQLidb.db.Table<SerialTracking>().Where(r => r.Serial == Data).ToListAsync();
            if (query.Count() != 0)
            {
                _errorMessage = "Serial already checked.";
                Status = dataStatus.ERROR;
                return false;
            }
            query.Clear();
            query = null;

            //check if present and get serial data
            var query2 = await SQLidb.db.Table<Labels>().Where(r => r.Serial == Data).ToListAsync();
            if (query2.Count() == 0)
            {
                _errorMessage = string.Format("Wrong serial {0}.", Serial);
                Status = dataStatus.ERROR;
                query2.Clear();
                query2 = null;
                return false;
            }
            var _LabelRack = query2[0].rack;
            if (_LabelRack != Values.CurrentRack)
            {
                _errorMessage = String.Format("Rack mismatch\nSerial {0} should go to {1}", Serial, _LabelRack);
                Status = dataStatus.ERROR;
                return false;
            }
            Status = dataStatus.READ;
            return true;
        }

        public override async Task<bool> ToDB()
        {
            try
            {
                await SQLidb.db.InsertAsync(new ScannedData() { Action = "CHECK", Rack = Values.CurrentRack, Serial = Serial, Service = Values.gService });
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                Status = dataStatus.ERROR;
                return false;
            }
            Status = dataStatus.DATABASE;
            try
            {
                await SQLidb.db.InsertAsync(new SerialTracking() { Serial = Data });
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                Status = dataStatus.ERROR;
                return false;
            }
            return true;
        }
    }
}