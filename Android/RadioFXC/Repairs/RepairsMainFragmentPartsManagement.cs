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
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using cAccesoDatosAndroid;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace RadioFXC
{
    public class FragmentPartsManagement : Android.Support.V4.App.Fragment
    {
        private string cUnitNumber;
        public string cRepairCode { get; set; }
        private int cCount;
        private string cSQLList;
        private EditText cDataBox;
        private ListView cList;
        private ListPartsAdapter cListAdapter;
        public FragmentPartsManagement() 
            : base()
        {
            cUnitNumber = UnitRepair.cUnitNumber;
            cRepairCode = UnitRepair.cRepairCode;
            cCount = 0;
            cSQLList = "Select Reference,Descripcion,Qty,Active from vPartsRepairs where RepairCode='" + cRepairCode + "' and UnitNumber='" + cUnitNumber + "' order by xfec desc";


        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.fragment_RepairParts, container, false);
            root.FindViewById<TextView>(Resource.Id.lblUnitNumber).Text = cUnitNumber;
            root.FindViewById<TextView>(Resource.Id.lblRepairNumber).Text = cRepairCode;
            cDataBox = root.FindViewById<EditText>(Resource.Id.txtDataBox);
            cList = root.FindViewById<ListView>(Android.Resource.Id.List);
            cListAdapter = new ListPartsAdapter(Activity, cSQLList);
            cList.Adapter = cListAdapter;
            cList.ChoiceMode = ChoiceMode.None;
            cDataBox.KeyPress += CDataBox_KeyPress;
            cDataBox.RequestFocus();
            cList.ItemLongClick += cList_ItemLongClick;
            cList.SetSelection(0);
            return root;
        }

        private void CDataBox_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.Event.Action == KeyEventActions.Down && (e.KeyCode == Keycode.Enter || e.KeyCode== Keycode.Tab))
            {
                var _SP = new SP(Values.gDatos, "pAddPartsRepairs");
                try
                {
                    _SP.AddParameterValue("RepairCode", cRepairCode);
                    _SP.AddParameterValue("UnitNumber", cUnitNumber);
                    _SP.AddParameterValue("Reading", cDataBox.Text);
                    _SP.Execute();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(Activity, "Error: "+ex.Message, ToastLength.Long).Show();
                    cDataBox.Text = "";
                    cDataBox.RequestFocus();
                    e.Handled = true;
                    return;
                }
                if (_SP.LastMsg!="OK")
                {
                    Toast.MakeText(Activity, "Error: " + _SP.LastMsg, ToastLength.Long).Show();
                    cDataBox.Text = "";
                    cDataBox.RequestFocus();
                    e.Handled = true;
                    return;
                }
                cListAdapter.NotifyDataSetChanged();
                cList.InvalidateViews();
                cDataBox.Text = "";
                cDataBox.RequestFocus();
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }
        private void cList_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var builder = new Android.Support.V7.App.AlertDialog.Builder(Activity);

            var position = e.Position;
            var _partnumber = cList.Adapter.GetItem(position).ToString().Split('|')[3];

            builder.SetTitle("Warning");
            builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            builder.SetMessage("Do you want to remove reference " + _partnumber + " from current repair?");
            builder.SetNegativeButton("No", delegate
            {
            });
            builder.SetPositiveButton("Yes", delegate
            {

                var _SP = new SP(Values.gDatos, "pDelPartsRepairs");
                _SP.AddParameterValue("RepairCode", cRepairCode);
                _SP.AddParameterValue("UnitNumber", cUnitNumber);
                _SP.AddParameterValue("Partnumber", _partnumber);
                _SP.Execute();
                if (_SP.LastMsg != "OK")
                {
                    throw (new Exception(_SP.LastMsg));
                }

                Activity.RunOnUiThread(() => Toast.MakeText(Activity, "Reference " + _partnumber + " removed.", ToastLength.Long).Show());
                ((ListPartsAdapter)cList.Adapter).NotifyDataSetChanged();
                //list.InvalidateViews();

            });
            builder.Create().Show();
        }
    }

    public class ListPartsAdapter : BaseAdapter
    {
        private Context context;
        private DynamicRS _RS= new DynamicRS(); //= new DynamicRS("Select RepairCode,UnitNumber from Repairs where dbo.checkFlag(flags,'INI')=1 order by xfec", Values.gDatos);
        private Color cBackColor;
        private string cSQL;
        public ListPartsAdapter(Context context, string SQL)
        {
            this.context = context;
            cSQL = SQL;
            _RS.Open(SQL,Values.gDatos);
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
            get { return _RS.RecordCount; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            _RS.Move(position);
            return _RS["Reference"]+"("+_RS["Descripcion"] + ")|" + _RS["Qty"].ToString()+"|"+ _RS["Active"].ToString()+'|'+_RS["Reference"].ToString().ToUpper();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView != null)
                convertView = null;
            convertView = LayoutInflater.From(context).Inflate(Android.Resource.Layout.SimpleListItemActivated2, parent, false);
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "Reference: " + GetItem(position).ToString().Split('|')[0];
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "Qty: " + GetItem(position).ToString().Split('|')[1];
            if (GetItem(position).ToString().Split('|')[2] == "True")
            {
                convertView.SetBackgroundColor(Color.LightBlue);
            } 
            return convertView;
        }
    }
}