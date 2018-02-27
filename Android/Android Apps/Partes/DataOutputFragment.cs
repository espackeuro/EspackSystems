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

namespace Partes
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
            return _root;
        }
    }
}