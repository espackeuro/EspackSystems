using System.Linq;
using System.Threading.Tasks;
using System;


namespace RadioLogisticaDeliveries
{
    public class dataReading : cData
    {
        public string Rack { get; set; }
        public string Partnumber { get; set; }
        public string LabelRack { get; set; }
        public string LabelService { get; set; }
        public int Qty { get; set; }

        public override infoData Info
        {
            get
            {
                return new infoData()
                {
                    c0 = Partnumber,
                    c1 = LabelRack,
                    c2 = LabelService,
                    c3 = Qty.ToString()
                };
            }
        }

        public async override Task<bool> doCheckings()
        {
            //check if Rack is set
            if (Values.CurrentRack == "")
            {
                _errorMessage = "Rack not set, read rack first.";
                Status = dataStatus.ERROR;
                return false;
            }
            //check if present and get serial data
            var query1 = await SQLidb.db.Table<PartnumbersRacks>().Where(r => r.Partnumber == Partnumber).ToListAsync();
            if (query1.Count == 0)
            {
                _errorMessage = string.Format("Wrong partnumber {0}.", Partnumber);
                Status = dataStatus.ERROR;
                query1.Clear();
                query1 = null;
                return false;
            }
            int _min = query1[0].MinBoxes;
            int _max = query1[0].MaxBoxes;
            //find the one corresponding to labelrack
            var _partRack = query1.FirstOrDefault(r => r.Rack==LabelRack);
            if (_partRack == null)
            {
                _errorMessage = string.Format("Partnumber {0} is not assigned to this Rack.", Partnumber);
                Status = dataStatus.ERROR;
                query1.Clear();
                query1 = null;
                return false;
            }
            //if labelrack is not currentrack, warning and copy the new destination
            if (LabelRack!= Values.CurrentRack)
            {
                _warningMessage = string.Format("Label Rack is {0}.\n Copying to {1}.", LabelRack, Values.CurrentRack);
                Status = dataStatus.WARNING;
                //insert the new rack for the partnumber
                await SQLidb.db.InsertAsync(new PartnumbersRacks()
                {
                    Rack = Values.CurrentRack,
                    Partnumber = Partnumber,
                    MinBoxes = _min,
                    MaxBoxes = _max
                });
            }
            //check block from RacksBlocks for current rack
            var _readRack= await SQLidb.db.FindAsync<RacksBlocks>(r => r.Rack == Values.CurrentRack);
            if (_readRack==null)
            {
                _errorMessage = string.Format("Wrong Rack {0}.", Values.CurrentRack);
                Status = dataStatus.ERROR;
                return false;
            }
            var _block = _readRack.Block;
            
            //if block is different we move it
            if (_block!=Values.gBlock)
            {
                _warningMessage = string.Format("Rack block is {0}.\n Moving to {1}.", _block, Values.gBlock);
                Status = dataStatus.WARNING;
                //update the block
                _readRack.Block = Values.gBlock;
                await SQLidb.db.UpdateAsync(_readRack);

            }
            //check block from RacksBlocks for current labelrack
            var _labelRack = await SQLidb.db.FindAsync<RacksBlocks>(r => r.Rack == LabelRack);
            if (_labelRack == null)
            {
                _errorMessage = string.Format("Wrong Rack {0}.", LabelRack);
                Status = dataStatus.ERROR;
                return false;
            }
            //check block match
            if (_labelRack.Block!=_block)
            {
                _warningMessage = string.Format("Label rack block {0} and current block do not match", _labelRack.Block);
                Status = dataStatus.WARNING;
            }
            //qty checks 
            if (Qty>_max)
            {
                _warningMessage= string.Format("QTY overflow, max QTY is {0}.", _max);
                Status = dataStatus.WARNING;
                Qty = _max;
            }
            return true;

        }
        public override async Task<bool> ToDB()
        {
            try
            {
                await SQLidb.db.InsertAsync(new ScannedData() { Action = "ADD", Service = LabelService, Session = Values.gSession, Rack = Rack, Partnumber = Partnumber, Qty = Qty, LabelRack = LabelRack, Transmitted=false });
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
