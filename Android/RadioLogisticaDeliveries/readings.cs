
using System.Collections.Generic;
using System.Linq;
using CommonTools;
using System.Collections;
using System.Threading.Tasks;
using CommonAndroidTools;
using Android.Content;
using System;

namespace RadioLogisticaDeliveries
{

    public class dataReadingList : IEnumerable
    {
        public Context Context { get; set; } = null;
        private List<cData> _dataList= new List<cData>();

        public IEnumerator<cData> GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Add(cData d)
        {
            _dataList.Add(d);
        }
        public async Task Add(string reading)
        {
            if (Context==null)
            {
                throw new Exception("Context not set");
            }
            cData _data;
            // CHECKING
            if (reading.IsNumeric() && reading.Length == 9) //checking
            {
                _data = new dataChecking() { Context=Context, Data = reading, Serial = reading };
                if (await _data.doCheckings())
                {
                    if (await _data.ToDB())
                    {
                        _dataList.Add(_data);
                        position++;
                    }
                }
                await _data.PushInfo();
                return;
            }
            else
            // READING
            if (reading.Substring(0, 1) == "%") //reading
            {
                var _split = reading.Split('%');
                string _pn = _split[1];
                string _lr = _split[2];
                string _ls = _split.Length == 4 ? _split[3] : "";
                dataReading _r;
                bool newReading= false;
                //SAME READING QTY+1
                if (position > -1 && Current() is dataReading)
                {
                    _r = (dataReading)Current();
                    if (_r.Partnumber == _pn && _r.LabelRack == _lr && _r.LabelService == _ls)
                    {
                        _r.Qty++;
                        cSounds.Scan(Context);
                        await Current().UpdateCurrent();
                        return;
                    }
                    else
                        newReading = true;
                }
                else
                    newReading = true;
                if (newReading)
                {
                    //NEW READING
                    _data = new dataReading()
                    {
                        Data = reading,
                        Partnumber = _split[1],
                        LabelRack = _split[2],
                        LabelService = _split.Length == 4 ? _split[3] : "",
                        Qty=1
                    };
                    _dataList.Add(_data);
                    position++;
                    cSounds.Scan(Context);
                    await Current().PushInfo();
                    return;
                }
            }
            else
            //CLOSE CODE
            if (reading == Values.gCloseCode)
            {
                _data = new dataCloseSession() { Data = reading };
                _dataList.Add(_data);
                position++;
                cSounds.EndOfProcess(Context);
                await Current().PushInfo();
                return;
            }
            else
            //NEW READING QTY
            if (reading.IsNumeric())
            {
                if (position > -1 && Current() is dataReading)
                {
                    dataReading _r = (dataReading)Current();
                    _r.Qty = reading.ToInt();
                }
                cSounds.Scan(Context);
                await Current().UpdateCurrent();
                return;
            }
            else
            //NEW RACK CODE
            {
                _data = new dataChangeRack() { Context=Context, Data = reading };
                if (await _data.doCheckings())
                {
                    _dataList.Add(_data);
                    position++;
                }
                await _data.PushInfo();
                return;
            }

            
        }
        public int position { get; set; } = -1;
        public cData Current()
        {
            return _dataList.ElementAt(position);
        }


    }

    public enum dataStatus { NONE, WARNING, ERROR, READ, DATABASE, TRANSMITTED }
    public class cData
    {
        
        protected string _errorMessage = "";
        protected string _warningMessage = "";
        public dataStatus _status = dataStatus.NONE;
        public string Data { get; set; }
        public dataStatus Status {
            get
            {
                return _status;
            }
            set
            {
                if (Context==null)
                {
                    throw new Exception("Context not set.");
                }
                _status = value;
                switch (_status) {
                    case dataStatus.READ:
                        cSounds.Scan(Context);
                        break;
                    case dataStatus.WARNING:
                        cSounds.Warning(Context);
                        break;
                    case dataStatus.ERROR:
                        cSounds.Error(Context);
                        break;
                }
            }
        }
        public Context Context { get; set; } = null;
        public virtual infoData Info { get; }
        public virtual infoData Error
        {
            get
            {
                return new infoData()
                {
                    c0 = "Error:",
                    c1 = _errorMessage,
                    c2 = "",
                    c3 = ""
                };
            }
        }
        public virtual infoData Warning
        {
            get
            {
                return new infoData()
                {
                    c0 = "Warning:",
                    c1 = _warningMessage,
                    c2 = "",
                    c3 = ""
                };
            }
        }
        public async Task PushInfo() {
            switch (Status)
            {
                case dataStatus.WARNING:
                    await Values.iFt.pushInfo(Warning);
                    break;
                case dataStatus.ERROR:
                    await Values.iFt.pushInfo(Error);
                    return;
            }
            await Values.iFt.pushInfo(Info);
        }

        public async Task UpdateCurrent()
        {
            await Values.iFt.updateMainLine(Info);
        }

        public virtual async Task<bool> doCheckings() { return false; }
        public virtual async Task<bool> ToDB()
        {
            return false;
        }
        public virtual void Transmit() { }

    }

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
            var query = await SQLiteDatabase.db.Table<SerialTracking>().Where(r => r.Serial == Data).ToListAsync();
            if (query.Count() != 0)
            {
                _errorMessage = "Serial already checked.";
                Status = dataStatus.ERROR;
                return false;
            }
            query.Clear();
            query = null;

            //check if present and get serial data
            var query2= await SQLiteDatabase.db.Table<Labels>().Where(r => r.Serial == Data).ToListAsync();
            if (query2.Count() == 0)
            {
                _errorMessage = string.Format("Wrong serial {0}.", Serial);
                Status = dataStatus.ERROR;
                query2.Clear();
                query2 = null;
                return false;
            }
            var _LabelRack = query2[0].rack;
            if (_LabelRack!= Values.CurrentRack)
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
                await SQLiteDatabase.db.InsertAsync(new ScannedData() { Action="CHECK", Rack = Values.CurrentRack, Serial = Serial, Service=Values.gService });
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
                await SQLiteDatabase.db.InsertAsync(new SerialTracking() { Serial = Data }); 
            } catch (Exception ex)
            {
                _errorMessage = ex.Message;
                Status = dataStatus.ERROR;
                return false;
            }
            return true;
        }
    }

    public class dataReading : cData
    {
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
    }
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
    }

    public class dataChangeRack:cData
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
            var query = await SQLiteDatabase.db.Table<RacksBlocks>().Where(r => r.Rack == Data).ToListAsync();
            if (query.Count() == 0)
            {
                
                Status = dataStatus.ERROR;
                return false;
            }
            Values.CurrentRack = Data;
            Status = dataStatus.READ;
            return true;
        }

    }
}