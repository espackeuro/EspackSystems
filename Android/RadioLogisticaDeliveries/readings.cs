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
using CommonTools;
using System.Collections;
using System.Threading.Tasks;

namespace RadioLogisticaDeliveries
{

    public class dataReadingList : IEnumerable
    {
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
            cData _data;
            // CHECKING
            if (reading.IsNumeric() && reading.Length == 9) //checking
            {
                _data = new dataChecking() { Data = reading, Serial = reading };
                _dataList.Add(_data);
                position++;
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
                }
            }
            else
            //CLOSE CODE
            if (reading == Values.gCloseCode)
            {
                _data = new dataCloseSession() { Data = reading };
                _dataList.Add(_data);
                position++;
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
            }
            else
            //NEW RACK CODE
            {
                Values.CurrentRack = reading;
            }

            await Current().PushInfo();
        }
        public int position { get; set; } = -1;
        public cData Current()
        {
            return _dataList.ElementAt(position);
        }


    }

    public enum dataStatus { READ, DATABASE, TRANSMITTED }
    public class cData
    {
        public string Data { get; set; }
        public dataStatus Status { get; set; }
        public virtual infoData Info { get; }
        public async Task PushInfo() {
            await Values.iFt.pushInfo(Info);
        }
        public string Error { get; set; }
        public virtual void ToDB() { }
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
}