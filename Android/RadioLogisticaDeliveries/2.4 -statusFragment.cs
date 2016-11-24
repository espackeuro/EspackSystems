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
        public ProgressBar socksProgress { get; set; }
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
            return _root;
        }
    }
}