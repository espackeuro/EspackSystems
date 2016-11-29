
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
    public enum dataStatus { NONE, WARNING, ERROR, READ, DATABASE, TRANSMITTED }
    public class cData
    {

        protected string _errorMessage = "";
        protected string _warningMessage = "";
        public dataStatus _status = dataStatus.NONE;
        public string Data { get; set; }
        public dataStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (Context == null)
                {
                    throw new Exception("Context not set.");
                }
                _status = value;
                switch (_status)
                {
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
        public async Task PushInfo()
        {
            switch (Status)
            {
                case dataStatus.WARNING:
                    await Values.iFt.pushInfo(Warning);
                    break;
                case dataStatus.ERROR:
                    await Values.iFt.pushInfo(Error);
                    return;
            }
            await Values.iFt.pushInfo(this.Info);
        }

        public async Task UpdateCurrent()
        {
            switch (Status)
            {
                case dataStatus.WARNING:
                    await Values.iFt.pushInfo(Warning);
                    await Values.iFt.pushInfo(Info);
                    break;
                case dataStatus.ERROR:
                    await Values.iFt.pushInfo(Error);
                    return;
            }
            await Values.iFt.updateMainLine(Info);
        }

        public virtual async Task<bool> doCheckings() { return false; }
        public virtual async Task<bool> ToDB()
        {
            return false;
        }
        public virtual void Transmit() { }

    }
}