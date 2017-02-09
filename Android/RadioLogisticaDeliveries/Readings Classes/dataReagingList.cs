
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
        public async Task Add(string reading)
        {
            if (Context == null)
            {
                throw new Exception("Context not set");
            }
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
                    return;
                }
                _data = new dataChecking() { Context = Context, Rack = Values.CurrentRack, Data = reading, Serial = reading };
                if (await _data.doCheckings())
                {
                    _dataList.Add(_data);
                    position++;
                    //Values.sFt.CheckQtyReceived++;
                }
                await _data.PushInfo();
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
                    await _data.PushInfo();
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
                    Toast.MakeText(Context, "Cancelled!", ToastLength.Short).Show();
                    return;
                }
                _data = new dataCloseSession() { Context = Context, Data = reading };
                if (await _data.doCheckings())
                {
                    _dataList.Add(_data);
                    //after close code we insert all reading from previous rack
                    await Values.dFt.SetMessage("Waiting for the pending data to be transmitted");
                    _dataList.Where(r => r.Status == dataStatus.READ || r.Status == dataStatus.WARNING).ToList().ForEach(async r => await r.ToDB());
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
                    //remove the database and recreate it.
                    //((Activity)Context).RunOnUiThread(() =>
                    //{
                    //    DataTransferManager.Active = false;
                    //    Values.SQLidb.DropDatabase();
                    //    try
                    //    {
                    //        Context.DeleteDatabase("DELIVERIES");
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.Write(ex.Message);
                    //    }
                    //    Values.SQLidb = new SQLiteDatabase("DELIVERIES");
                    //    Values.CreateDatabase();
                    //});
                    await Values.EmptyDatabase();
                        //clear info, debug and status fragments
                    Values.iFt.Clear();
                    Values.dFt.Clear();
                    Values.sFt.Clear();
                    Values.hFt.Clear();
                    //change to enter order fragment
                    ((MainScreen)Context).changeEnterDataToOrderFragments();
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
                    return;
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
                    await Values.dFt.SetMessage(string.Format("Transmitting Rack {0}",Values.CurrentRack));
                    _dataList.Where(r => r.Status == dataStatus.READ || r.Status == dataStatus.WARNING).ToList().ForEach(async r => await r.ToDB());
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