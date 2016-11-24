

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
            Status = dataStatus.READ;
            return true;
        }

        public override async Task<bool> ToDB()
        {
            try
            {
                await SQLidb.db.InsertAsync(new ScannedData() { Action = "CLOSE", Session = Values.gSession });
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                Status = dataStatus.ERROR;
                return false;
            }
            Status = dataStatus.DATABASE;
            return true;
        }
    }
}