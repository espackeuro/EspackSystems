using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using System;
using AccesoDatosNet;
using System.Runtime;
using System.Linq.Expressions;
using Android.App;
using Scanner;
using System.Collections.Generic;
using System.Linq;

namespace RadioFXC
{
    //[BroadcastReceiver(Enabled = true)]
    //[IntentFilter(new[] { "com.espack.radiofxc.SCAN" },
    //    Categories = new[] { Intent.CategoryDefault })]


    class LoadsFragmentRepairs2Loads : Android.Support.V4.App.ListFragment, IScrollDirectorListener, AbsListView.IOnScrollListener
    {
        protected ListView list;
        protected TextView LoadLabel;
        //ProgressBar progress;
        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.fragmentAddRepais2Loads, container, false);
            list = root.FindViewById<Android.Widget.ListView>(Android.Resource.Id.List);
            //var progress = root.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            //progress.Visibility = ViewStates.Invisible;

            var adapter = new ListRepairs2LoadAdapter(Activity);
            list.Adapter = adapter;
            adapter.list = list;
            list.ChoiceMode = ChoiceMode.Multiple;

            //var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
            LoadLabel = root.FindViewById<TextView>(Resource.Id.lblLoadNumber);
            LoadLabel.Text = Loads.Label;
            adapter.LoadLabel = LoadLabel;
            var button = root.FindViewById<Button>(Resource.Id.btnAdd2Load);
            button.Click += delegate
            {
                //indicator in the middle of the screen
                //progress.Visibility = ViewStates.Visible;
                try
                {
                    button.Enabled = false;
                    var checkedItemsPositions = list.CheckedItemPositions;
                    var _cadena = "|";
                    for (var i = 0; i < checkedItemsPositions.Size(); i++)
                    {
                        if (checkedItemsPositions.ValueAt(i) == true)
                        {
                            //Toast.MakeText(Activity, "Selected " + list.GetItemAtPosition(checkedItemsPositions.KeyAt(i)).ToString().Split('|')[0], ToastLength.Short).Show();
                            _cadena += list.GetItemAtPosition(checkedItemsPositions.KeyAt(i)).ToString().Split('|')[0] + '|';
                        }
                    }
                    //var _SP = new SP(Values.gDatos, "pAddCadenaRepais2Loads");
                    //_SP.AddParameterValue("LoadNumber", Loads.LoadNumber);
                    //_SP.AddParameterValue("cadena", _cadena);
                    //_SP.Execute();
                    if (Values.gDatos.State == System.Data.ConnectionState.Open)
                    {
                        Values.gDatos.Close();
                    }
                    Values.gDatos.DataBase = "PROCESOS";
                    var _SP = new SP(Values.gDatos, "pLaunchProcess_RepairsAdd2Load");
                    _SP.AddParameterValue("@DB", "REPAIRS");
                    _SP.AddParameterValue("@ProcedureName", "pAddCadenaRepais2Loads");
                    _SP.AddParameterValue("@Parameters", "@LoadNumber='" + Loads.LoadNumber + "',@cadena='" + _cadena + "'");
                    //_SP.AddParameterValue("@TableDB", "REPAIRS");
                    //_SP.AddParameterValue("@TableName", "Repairs");
                    //_SP.AddParameterValue("@TablePK", "");
                    _SP.Execute();
                    if (_SP.LastMsg.Substring(0, 2) != "OK")
                    {
                        throw (new Exception(_SP.LastMsg));
                    }
                    if (Values.gDatos.State == System.Data.ConnectionState.Open)
                    {
                        Values.gDatos.Close();
                    }
                    Values.gDatos.DataBase = "REPAIRS";
                    //progress.Visibility = ViewStates.Invisible;
                    Activity.RunOnUiThread(() => Toast.MakeText(Activity, "Process OK!!", ToastLength.Long).Show());
                    list.CheckedItemPositions.Clear();
                    adapter.NotifyDataSetChanged();
                    list.InvalidateViews();
                    button.Enabled = true;
                }
                catch (Exception ex)
                {
                    Activity.RunOnUiThread(() => Toast.MakeText(Activity, "ERROR: " + ex.Message, ToastLength.Long).Show());
                    button.Enabled = true;
                    //progress.Visibility = ViewStates.Invisible;
                }
            };
            //var filter = new IntentFilter("com.espack.radiofxc.SCAN");
            //filter.AddCategory(Intent.CategoryDefault);
            //var receiver = new ScanReceiver();
            //receiver.list = list;
            //Activity.RegisterReceiver(receiver, filter);
            list.SetSelection(0);
            //list.ItemSelected += List_ItemSelected;

            //scanner
            sScanner.RegisterScannerActivity(Activity);
            sScanner.AfterReceive += SScanner_AfterReceive;
            return root;
        }

        private void SScanner_AfterReceive(object sender, ReceiveEventArgs e)
        {
            var l = ((ListRepairs2LoadAdapter)list.Adapter).ListElements;
            var _index = l.FindIndex(p => p.UnitNumber == e.ReceivedData);
            if (_index != -1)
            {
                Activity.RunOnUiThread(() => list.SetItemChecked(_index, true));
                list.SmoothScrollToPosition(_index);
            }
            //for (var i = 0; i < list.Count - 1; i++)
            //{
            //    if (list.GetItemAtPosition(i).ToString().Split('|')[1] == cDataWedge.HandleDecodeData(intent).Split('|')[0])
            //    {
            //        list.SetItemChecked(i, true);
            //        break;
            //    }
            //}
        }
        public override void OnDestroyView()
        {
            base.OnDestroyView();
            sScanner.UnregisterScannerActivity();
        }
        private void List_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //public class ScanReceiver : BroadcastReceiver
        //{
        //    public ListView list { get; set; }

        //    public override void OnReceive(Context context, Intent intent)
        //    {
        //        //Toast.MakeText(Application.Context, cDataWedge.HandleDecodeData(intent).Split('|')[0], ToastLength.Long).Show();
        //        for (var i=0; i<list.Count-1;i++)
        //        {
        //            if (list.GetItemAtPosition(i).ToString().Split('|')[1] == cDataWedge.HandleDecodeData(intent).Split('|')[0])
        //            {
        //                list.SetItemChecked(i,true);
        //                break;
        //            }
        //        }
        //    }
        //}

        public void OnScrollDown()
        {
            //Console.WriteLine("ListViewFragment: OnScrollDown");
        }

        public void OnScrollUp()
        {
            //Console.WriteLine("ListViewFragment: OnScrollUp");
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            //Console.WriteLine("ListViewFragment: OnScroll");
        }

        public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
        {
            //Console.WriteLine("ListViewFragment: OnScrollChanged");
        }
    }

    public struct Repair 
    {
        public string UnitNumber { get; set; }
        public string RepairCode { get; set; }
        public string Flags { get; set; }
    }

    public class ListRepairs2LoadAdapter : BaseAdapter
    {
        private Context context;
        //private DynamicRS recordset; //= new DynamicRS(query, Values.gDatos);
        public ListView list { get; set; }
        public TextView LoadLabel { get; set; }
        public List<Repair> ListElements { get; private set; }
        private string query = "Select RepairCode,UnitNumber,Flags from Repairs r where (dbo.checkFlag(flags,'INI')=1 or  dbo.checkFlag(flags,'PENDING')=1 or dbo.checkFlag(flags,'ERR')=1) and service='" + Values.gService + "' and exists (select 0 from PartsRepairs where RepairCode=r.RepairCode) order by xfec";
        public ListRepairs2LoadAdapter(Context context)
        {
            this.context = context;
            using (var recordset = new DynamicRS(query, Values.gDatos))
            {
                recordset.Open();
                ListElements = recordset.ToList().Select(r => new Repair() { UnitNumber = r["UnitNumber"].ToString().Trim(), RepairCode = r["RepairCode"].ToString().Trim(), Flags = r["Flags"].ToString().Trim() }).ToList(); //(from r in _RS.ToList() select r["RepairCode"] + "|" + r["UnitNumber"]).ToList<string>();
                recordset.Close();
            }
        }

        public override void NotifyDataSetChanged()
        {
            base.NotifyDataSetChanged();
            using (var recordset = new DynamicRS(query, Values.gDatos))
            {
                recordset.Open();
                ListElements = recordset.ToList().Select(r => new Repair() { UnitNumber = r["UnitNumber"].ToString().Trim(), RepairCode = r["RepairCode"].ToString().Trim(), Flags = r["Flags"].ToString().Trim() }).ToList(); //(from r in _RS.ToList() select r["RepairCode"] + "|" + r["UnitNumber"]).ToList<string>();
                recordset.Close();
            }
        }

        public override int Count
        {
            get { return ListElements.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            //_RS.Move(position);
            return Format(ListElements[position]); //_RS["RepairCode"] + "|" + _RS["UnitNumber"];
        }
        protected string Format(Repair r)
        {
            return string.Format("{0}|{1}", r.RepairCode, r.UnitNumber);
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        { 
            if (convertView == null)
            {
                convertView = LayoutInflater.From(context).Inflate(Android.Resource.Layout.SimpleListItemActivated2, parent, false);
                //convertView.Click += ConvertView_Click;
            }
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "UNIT: " +ListElements[position].UnitNumber;
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "REPAIR CODE: " + ListElements[position].RepairCode;
            if (ListElements[position].Flags.IndexOf("ERR") != -1)
                convertView.SetBackgroundResource(Resource.Drawable.cellErrorDrawable);
            //convertView.SetBackgroundColor(Android.Graphics.Color.Red);
            else if (ListElements[position].Flags.IndexOf("PENDING") != -1)
                convertView.SetBackgroundResource(Resource.Drawable.cellPendingrDrawable);
            else if (list.IsItemChecked(position))
                convertView.SetBackgroundResource(Resource.Drawable.cellNormalDrawable);
            //else
            //    convertView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            return convertView;
        }

        private void ConvertView_Click(object sender, EventArgs e)

        {
            var positions = list.CheckedItemPositions;
            int counter = 0;
            if (positions != null)
            {
                int length = positions.Size();
                for (int i = 0; i < length; i++)
                {
                    if (positions.ValueAt(i) == true)
                    {
                        counter++;
                    }
                }
                LoadLabel.Text = Loads.Label;
            }
            
        }

    }

}