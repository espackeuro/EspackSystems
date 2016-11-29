using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace RadioLogisticaDeliveries
{
    public class statusFragment : Fragment
    {
        private int _r, _rt, _cc, _cr, _ct;
        public ProgressBar socksProgress { get; set; }
        public TextView readingsInfo { get; set; }
        public TextView checkingsInfo { get; set; }
        public int ReadQtyReceived
        {
            get
            {
                return _r;
            }
            set
            {
                _r = value;
                updateInfo();
            }
        }
        public int ReadQtyTransmitted
        {
            get
            {
                return _rt;
            }
            set
            {
                _rt = value;
                updateInfo();
            }
        }
        public int CheckQtyTotal
        {
            get
            {
                return _cc;
            }
            set
            {
                _cc = value;
                updateInfo();
            }
        }
        public int CheckQtyReceived
        {
            get
            {
                return _cr;
            }
            set
            {
                _cr = value;
                updateInfo();
            }
        }
        public int CheckQtyTransmitted
        {
            get
            {
                return _ct;
            }
            set
            {
                _ct = value;
                updateInfo();
            }
        }
        private void updateInfo()
        {
            Activity.RunOnUiThread(() =>
            {
                readingsInfo.Text = readingsInfoText;
                checkingsInfo.Text = checkingsInfoText;
            });
        }
        private string readingsInfoText
        {
            get
            {
                return string.Format("{0}/{1}", ReadQtyReceived, ReadQtyTransmitted);
            }
        }
        private string checkingsInfoText
        {
            get
            {
                return string.Format("{0}/{1}/{2}", CheckQtyTotal, CheckQtyReceived, CheckQtyTransmitted);
            }
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public Task ChangeProgressVisibility(bool visible)
        {
            return Task.Run(() => Activity.RunOnUiThread(() =>
             {
                 socksProgress.Visibility = visible ? ViewStates.Visible : ViewStates.Gone;
             }));  
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var _root = inflater.Inflate(Resource.Layout.statusFragment, container, false);
            socksProgress = _root.FindViewById<ProgressBar>(Resource.Id.socksProgress);
            socksProgress.IndeterminateDrawable.SetColorFilter(Color.Red, PorterDuff.Mode.Multiply);
            //socksProgress.ProgressDrawable.SetColorFilter(Color.Red, PorterDuff.Mode.SrcIn);
            socksProgress.Visibility = ViewStates.Gone;
            readingsInfo = _root.FindViewById<TextView>(Resource.Id.readingsInfo);
            checkingsInfo = _root.FindViewById<TextView>(Resource.Id.checkingsInfo);
            return _root;
        }
    }
}