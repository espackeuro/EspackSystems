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
using AccesoDatosNet;
//using Java.Lang;

namespace RadioFXC
{
    class LoadsFragmentCloseLoads : Android.Support.V4.App.Fragment
    {
        protected ListView list;
        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.fragment_LoadsCloseLoad, container, false);
            root.FindViewById<TextView>(Resource.Id.lblLoadNumber).Text = Loads.Label;
            var button = root.FindViewById<TextView>(Resource.Id.btnClose);
            /* -------- */
            list = root.FindViewById<ListView>(Resource.Id.listRepairs);
            var adapter = new ListLoadsRepairsAdapter(Activity, "Select RepairCode,UnitNumber from Repairs where LoadNumber='" + Loads.LoadNumber + "' order by UnitNumber");
            list.Adapter = adapter;
            adapter.list = list;
            button.Click += (sender, e) =>
            {
                //Toast.MakeText(Activity, "Click! ", ToastLength.Short).Show();
                var _SP = new SP(Values.gDatos, "pCloseLoad");
                _SP.AddParameterValue("Load", Loads.LoadNumber);
                _SP.AddParameterValue("Service", Values.gService);
                try
                {
                    _SP.Execute();
                    if (_SP.LastMsg=="OK")
                    {
                        Activity.RunOnUiThread(() => Toast.MakeText(Activity, "Load closed correctly. ", ToastLength.Long).Show());
                        var intent = new Intent(Activity, typeof(MainScreen));
                        StartActivityForResult(intent, 1);
                    } else
                    {
                        throw (new Exception(_SP.LastMsg));
                    }
                }
                catch (Exception ex)
                {
                    Activity.RunOnUiThread(() => Toast.MakeText(Activity, "ERROR: " + ex.Message, ToastLength.Long).Show());
                }
            };
            list.ItemLongClick += List_ItemLongClick;
            list.SetSelection(0);
            return root;
        }

        private void List_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var builder = new Android.Support.V7.App.AlertDialog.Builder(Activity);

            var position = e.Position;
            //var _unitNumber = list.Adapter.GetItem(position).ToString().Split('|')[1];
            var _repairCode = list.Adapter.GetItem(position).ToString().Split('|')[0];

            builder.SetTitle("Warning");
            builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            builder.SetMessage("Do you want to remove repair "+_repairCode+" from current load?");
            builder.SetNegativeButton("No", delegate
            {
            });
            builder.SetPositiveButton("Yes", delegate
            {

                var _SP = new SP(Values.gDatos, "pDelRepairs2Load");
                _SP.AddParameterValue("RepairCode", _repairCode);
                _SP.AddParameterValue("LoadNumber", Loads.LoadNumber);
                _SP.Execute();
                if (_SP.LastMsg != "OK")
                {
                    throw (new Exception(_SP.LastMsg));
                }

                Activity.RunOnUiThread(() => Toast.MakeText(Activity, "Repair "+ _repairCode + " removed.", ToastLength.Long).Show());
                ((ListLoadsRepairsAdapter)list.Adapter).NotifyDataSetChanged();
                //list.InvalidateViews();

            });
            builder.Create().Show();
        }
    }


    class ListLoadsRepairsAdapter : BaseAdapter
    {
        private Context context;
        private DynamicRS _RS = new DynamicRS(); //= new DynamicRS("Select RepairCode,UnitNumber from Repairs where dbo.checkFlag(flags,'INI')=1 order by xfec", Values.gDatos);
        private string cSQL;
        public ListView list;
        public ListLoadsRepairsAdapter(Context context, string SQL)
        {
            this.context = context;
            cSQL = SQL;
            _RS.Open(SQL, Values.gDatos);
            
        }

        public override void NotifyDataSetChanged()
        {
            base.NotifyDataSetChanged();
            _RS = null;
            _RS = new DynamicRS();
            _RS.Open(cSQL, Values.gDatos);
        }

        public override int Count
        {
            get
            {
                return _RS.RecordCount;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            _RS.Move(position);
            return _RS["RepairCode"] + "|" + _RS["UnitNumber"];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(context).Inflate(Android.Resource.Layout.SimpleListItemActivated2, parent, false);
            }
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "UNIT: " + GetItem(position).ToString().Split('|')[1];
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "REPAIR CODE: " + GetItem(position).ToString().Split('|')[0];

            return convertView;
        }

    }
}