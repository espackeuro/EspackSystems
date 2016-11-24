
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
        private List<cData> _dataList = new List<cData>();

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
            if (Context == null)
            {
                throw new Exception("Context not set");
            }
            cData _data;
            // CHECKING
            if (reading.IsNumeric() && reading.Length == 9) //checking
            {
                _data = new dataChecking() { Context = Context, Data = reading, Serial = reading };
                if (await _data.doCheckings())
                {
                    _dataList.Add(_data);
                    position++;
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
                bool newReading = false;
                //SAME READING QTY+1
                if (position > -1 && Current() is dataReading)
                {
                    _r = (dataReading)Current();
                    if (_r.Partnumber == _pn && _r.LabelRack == _lr && _r.LabelService == _ls)
                    {
                        _r.Qty++;
                        await _r.doCheckings();
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
                        Context = Context,
                        Data = reading,
                        Partnumber = _split[1],
                        LabelRack = _split[2],
                        LabelService = _split.Length == 4 ? _split[3] : "",
                        Qty = 1
                    };
                    if (await _data.doCheckings())
                    {
                        _dataList.Add(_data);
                        position++;
                    }
                    await Current().PushInfo();
                    return;
                }
            }
            else
            //CLOSE CODE
            if (reading == Values.gCloseCode)
            {
                _data = new dataCloseSession() { Context = Context, Data = reading };
                if (await _data.doCheckings())
                {
                    _dataList.Add(_data);
                    //after close code we insert all reading from previous rack
                    _dataList.Where(r => r.Status == dataStatus.READ || r.Status == dataStatus.WARNING).ToList().ForEach(async r => await r.ToDB());
                    position++;
                }
                await _data.PushInfo();
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
                _data = new dataChangeRack() { Context = Context, Data = reading };
                if (await _data.doCheckings())
                {
                    //after new rack code we insert all reading from previous rack
                    _dataList.Where(r => r.Status == dataStatus.READ || r.Status == dataStatus.WARNING).ToList().ForEach(async r => await r.ToDB());
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
}