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

namespace logon
{
    public class DataOutputFragment : Fragment
    {
        public TextView txtSupplier { get; set; }
        public TextView txtFase4 { get; set; }
        public TextView txtDescription { get; set; }
        public TextView txtPack { get; set; }
        public TextView txtQtyPack { get; set; }
        public TextView txtDock { get; set; }
        public TextView txtLoc1 { get; set; }
        public TextView txtLoc2 { get; set; }
        public TextView txtSPP { get; set; }
        public TextView txtSPA { get; set; }
        public TextView txtSTD { get; set; }
        public TextView txtSPS { get; set; }
        public TextView txtSPC { get; set; }
        public TextView txtSPE { get; set; }
        public TextView txtSPX { get; set; }
        public TextView txtSQC { get; set; }
        public TextView txtSEV { get; set; }
        public TextView txtLastDeliveryDate { get; set; }
        public TextView txtLastDeliveryQty { get; set; }
        public TextView txtMinGVDBA { get; set; }
        public TextView txtMinDate { get; set; }
        public TextView txtMinQty { get; set; }
        public TextView txtFlags { get; set; }
        public TextView txtBreakDate { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var _root = inflater.Inflate(Resource.Layout.DataOutputFragment, container, false);
            txtSupplier = _root.FindViewById<TextView>(Resource.Id.txtSupplier);
            txtFase4 = _root.FindViewById<TextView>(Resource.Id.txtFase4);
            txtDescription= _root.FindViewById<TextView>(Resource.Id.txtDescription);
            txtPack= _root.FindViewById<TextView>(Resource.Id.txtPack);
            txtQtyPack= _root.FindViewById<TextView>(Resource.Id.txtQtyPack);
            txtDock= _root.FindViewById<TextView>(Resource.Id.txtDock);
            txtLoc1= _root.FindViewById<TextView>(Resource.Id.txtLOC1);
            txtLoc2= _root.FindViewById<TextView>(Resource.Id.txtLOC2);
            txtSPP = _root.FindViewById<TextView>(Resource.Id.txtSPP);
            txtSPA = _root.FindViewById<TextView>(Resource.Id.txtSPA);
            txtSTD = _root.FindViewById<TextView>(Resource.Id.txtSTD);
            txtSPS = _root.FindViewById<TextView>(Resource.Id.txtSPS);
            txtSPC = _root.FindViewById<TextView>(Resource.Id.txtSPC);
            txtSPE = _root.FindViewById<TextView>(Resource.Id.txtSPE);
            txtSPX = _root.FindViewById<TextView>(Resource.Id.txtSPX);
            txtSQC = _root.FindViewById<TextView>(Resource.Id.txtSQC);
            txtSEV = _root.FindViewById<TextView>(Resource.Id.txtSEV);
            txtLastDeliveryDate = _root.FindViewById<TextView>(Resource.Id.txtLastDeliveryDate);
            txtLastDeliveryQty = _root.FindViewById<TextView>(Resource.Id.txtLastDeliveryQty);
            txtMinGVDBA = _root.FindViewById<TextView>(Resource.Id.txtMinGVDBA);
            txtMinDate = _root.FindViewById<TextView>(Resource.Id.txtMinDate);
            txtMinQty = _root.FindViewById<TextView>(Resource.Id.txtMinQTY);
            txtFlags = _root.FindViewById<TextView>(Resource.Id.txtFlags);
            txtBreakDate = _root.FindViewById<TextView>(Resource.Id.txtBreakDate);
            return _root;
        }
    }
}
  