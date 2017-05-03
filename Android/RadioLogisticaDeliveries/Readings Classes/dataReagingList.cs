
using System.Collections.Generic;
using System.Linq;
using CommonTools;
using System.Collections;
using System.Threading.Tasks;
using CommonAndroidTools;
using Android.Content;
using System;
using System.Threading;
using Android.App;
using Android.Widget;

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

        public bool Processing { get; set; } = false;

        public async Task Add(string reading)
        {
            if (Context == null)
            {
                throw new Exception("Context not set");
            }
            Processing = true;
            cData _data;
            //QRCODE
            if (reading.Length>2 && reading.Substring(0, 2) == "@@" && reading.Substring(reading.Length - 2, 2) == "##") //QRCODE
            {
                var _readingFields = reading.Split('|');
                if (Values.WorkMode == WorkModes.READING)
                {
                    //"QRCODE|R GRAZ|07/02/2017|724008707|VCE15|303639641|KLT3215|U00045|L538|1000|W700530S300|STRAP 3-80X4.6 PLA||"
                    reading = string.Format("%{0}%{1}%{2}",_readingFields[11],_readingFields[4],_readingFields[12]);
                } else
                {
                    reading = _readingFields[5];
                }
            }



            // CHECKING
            if ((reading.IsNumeric() && reading.Length == 9) || (reading.Substring(0,1)=="S" && reading.Substring(1,9).IsNumeric())) //checking
            {
                if (reading.Substring(0, 1) == "S" && reading.Substring(1, 9).IsNumeric())
                    reading = reading.Substring(1, 9);
                if (Values.WorkMode == WorkModes.READING)
                {
                    cSounds.Error(Context);
                    await AlertDialogHelper.ShowAsync(Context, "ERROR", "Current mode is READING, cannot process this data.", "OK", "");
                    Processing = false;
                    return;
                }
                //check if serial is in list
                if (_dataList.OfType<dataChecking>().FirstOrDefault(r => r.Serial== reading) != null)
                    {
                    cSounds.Error(Context);
                    Values.iFt.SetMessage(string.Format("Error: Serial {0} already checked.", reading ));
                    Processing = false;
                    return;
                }
                _data = new dataChecking() { Context = Context, Rack = Values.CurrentRack, Data = reading, Serial = reading };
                if (await _data.doCheckings())
                {
                    _dataList.Add(_data);
                    position++;
                    //Values.sFt.CheckQtyReceived++;
                }
                _data.PushInfo();
                Processing = false;
                return;
            }
            else
            // READING
            if (reading.Substring(0, 1) == "%") //reading
            {
                if (Values.WorkMode== WorkModes.CHECKING)
                {
                    cSounds.Error(Context);
                    await AlertDialogHelper.ShowAsync(Context, "ERROR", "Current mode is CHECKING, cannot process this data.", "OK", "");
                    Processing = false;
                    return;
                }
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
                        Current().UpdateCurrent();
                        Processing = false;
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
                    //get previous reading in the rack with the same data
                    var itemToRemove = _dataList.SingleOrDefault(r => r is dataReading && ((dataReading)r).Partnumber == _pn && ((dataReading)r).LabelRack == _lr && ((dataReading)r).LabelService == _ls && (((dataReading)r).Status == dataStatus.READ || ((dataReading)r).Status == dataStatus.WARNING));
                    //var _query = from r in _dataList
                    //             where r is dataReading && ((dataReading)r).Partnumber == _pn && ((dataReading)r).LabelRack == _lr && ((dataReading)r).LabelService == _ls && (((dataReading)r).Status == dataStatus.READ || ((dataReading)r).Status == dataStatus.WARNING)
                    //             select r;
                    //foreach (var d in _query)
                    //_dataList.Remove(d);
                    if (itemToRemove != null)
                    {
                        _dataList.Remove(itemToRemove);
                        position--;
                    }

                    //_dataList.OfType<dataReading>().Select(r) => r.Partnumber=_pn && r.)
                    _data = new dataReading()
                    {
                        Context = Context,
                        Rack = Values.CurrentRack,
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
                        //Values.sFt.ReadQtyReceived++;
                    }
                    _data.PushInfo();
                    Processing = false;
                    return;
                }
            }
            else
            //CLOSE CODE
            if (reading == Values.gCloseCode)
            {
                //set alert for executing the task
                bool dialogResult = await AlertDialogHelper.ShowAsync(Context, "Confirm Close Session", "This will close current session. Are you sure?", "Close Session", "Cancel");

                if (!dialogResult)
                {
                    ((Activity)Context).RunOnUiThread(() =>
                    {
                        Toast.MakeText(Context, "Cancelled!", ToastLength.Short).Show();
                    });
                    Processing = false;
                    return;
                }
                _data = new dataCloseSession() { Context = Context, Data = reading };
                if (await _data.doCheckings())
                {
                    _dataList.Add(_data);
                    //after close code we insert all reading from previous rack
                    Values.dFt.SetMessage("Waiting for the pending data to be transmitted");
                    foreach (var r in _dataList.Where(r => r.Status == dataStatus.READ || r.Status == dataStatus.WARNING))
                    {
                        await r.ToDB();
                    }
                    Values.sFt.UpdateInfo();
                    //await Values.iFt.pushInfo("Waiting for the pending data to be transmitted");
                    while (true)
                    {
                        //SpinWait.SpinUntil(() => Values.SQLidb.pendingData && (monitor.State == NetworkState.ConnectedData || monitor.State == NetworkState.ConnectedWifi));
                        await Task.Delay(1000);
                        if (Values.SQLidb.db.Table<ScannedData>().Where(r => r.Transmitted == false).CountAsync().Result==0)//(Values.sFt.ReadQtyReceived == Values.sFt.ReadQtyTransmitted && Values.sFt.CheckQtyReceived == Values.sFt.CheckQtyTransmitted)
                        {
                            break;
                        }
                    }

                    await Values.EmptyDatabase();
                    //clear info, debug and status fragments
                    Values.iFt.Clear();
                    Values.dFt.Clear();
                    Values.sFt.Clear();
                    Values.hFt.Clear();
                    Values.gDatos.User = "";
                    Values.gDatos.Password = "";
                    Values.gSession = "";
                    Values.gService = "";
                    Values.gOrderNumber = 0;
                    Values.gBlock = "";
                    _dataList.Clear();
                    position = -1;
                    Values.SetCurrentRack("");
                    //change to enter order fragment

                    var intent = new Intent(Context, typeof(MainActivity));
                    ((MainScreen)Context).StartActivityForResult(intent, 1);
                    //Context.Dispose();
                    Processing = false;
                    return;
                }
            }
            else
            //NEW READING QTY
            if (reading.IsNumeric())
            {
                if (position > -1 && Current() is dataReading)
                {
                    dataReading _r = (dataReading)Current();
                    _r.Qty = reading.ToInt();
                    await _r.doCheckings();
                } else
                {
                    cSounds.Error(Context);
                    Processing = false;
                    return;
                }
                cSounds.Scan(Context);
                Current().UpdateCurrent();
                Processing = false;
                return;
            }
            else
            //NEW RACK CODE
            {
                if (reading==Values.CurrentRack)
                {
                    ((Activity)Context).RunOnUiThread(() =>
                    {
                        Toast.MakeText(Context, "Scanned Rack is the current one.", ToastLength.Short).Show();
                        cSounds.Error(Context);
                    });
                    Processing = false;
                    return;
                }
                _data = new dataChangeRack() { Context = Context, Data = reading };
                if (await _data.doCheckings())
                {
                    //after new rack code we insert all reading from previous rack
                    if (_dataList.Count != 0)
                    {
                        Values.dFt.SetMessage(string.Format("Transmitting Rack {0}", Values.CurrentRack));
                        foreach (var r in _dataList.Where(r => r.Status == dataStatus.READ || r.Status == dataStatus.WARNING))
                        {
                            await r.ToDB();
                        }
                    }
                    _dataList.Add(_data);
                    Values.sFt.UpdateInfo();
                    position++;
                    //try
                    //{
                    //    await Values.dtm.Transfer();
                    //}
                    //catch (Exception ex)
                    //{
                    //    await Values.dFt.pushInfo(ex.Message);
                    //}
                }
                _data.PushInfo();
                Processing = false;
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