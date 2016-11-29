

using System;
using System.Threading.Tasks;

namespace RadioLogisticaDeliveries
{

    public class dataCloseSession : cData
    {
        public override infoData Info
        {
            get
            {
                return new infoData()
                {
                    c0 = "Close",
                    c1 = "",
                    c2 = "",
                    c3 = ""
                };
            }
        }

        public override async Task<bool> doCheckings()
        {
            _status = dataStatus.READ;
            return true;
        }

        public override async Task<bool> ToDB()
        {
            try
            {
                await SQLidb.db.InsertAsync(new ScannedData() { Action = "CLOSE", Session = Values.gSession, Transmitted = false });
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                _status = dataStatus.ERROR;
                return false;
            }
            _status = dataStatus.DATABASE;
            return true;
        }
    }
}