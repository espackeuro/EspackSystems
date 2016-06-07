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
using DataWedge;

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
            var progress = root.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progress.Visibility = ViewStates.Invisible;

            var adapter = new ListRepairs2LoadAdapter(Activity);
            list.Adapter = adapter;
            adapter.list = list;
            list.ChoiceMode = ChoiceMode.Multiple;
            var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
            LoadLabel = root.FindViewById<TextView>(Resource.Id.lblLoadNumber);
            LoadLabel.Text = Loads.Label;
            adapter.LoadLabel = LoadLabel;
            fab.AttachToListView(list, this, this);
            fab.Focusable = false;
            fab.FocusableInTouchMode = false;
            fab.Clickable = true;
            fab.Click += delegate
            {
                //indicator in the middle of the screen
                progress.Visibility = ViewStates.Visible;
                try
                {
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
                    _SP.AddParameterValue("@Parameters", "@LoadNumber='"+Loads.LoadNumber+"',@cadena='" +_cadena+ "'");
                    //_SP.AddParameterValue("@TableDB", "REPAIRS");
                    //_SP.AddParameterValue("@TableName", "Repairs");
                    //_SP.AddParameterValue("@TablePK", "");
                    _SP.Execute();
                    if (_SP.LastMsg.Substring(0,2) != "OK")
                    {
                        throw (new Exception(_SP.LastMsg));
                    }
                    if (Values.gDatos.State == System.Data.ConnectionState.Open)
                    {
                        Values.gDatos.Close();
                    }
                    Values.gDatos.DataBase = "REPAIRS";
                    progress.Visibility = ViewStates.Invisible;
                    Activity.RunOnUiThread(() => Toast.MakeText(Activity, "Process OK!!", ToastLength.Long).Show());
                    list.CheckedItemPositions.Clear();
                    adapter.NotifyDataSetChanged();
                    list.InvalidateViews();
                }
                catch (Exception ex)
                {
                    Activity.RunOnUiThread (()=> Toast.MakeText(Activity, "ERROR: " + ex.Message, ToastLength.Long).Show());
                    progress.Visibility = ViewStates.Invisible;
                }
            };
            var filter = new IntentFilter("com.espack.radiofxc.SCAN");
            filter.AddCategory(Intent.CategoryDefault);
            var receiver = new ScanReceiver();
            receiver.list = list;
            Activity.RegisterReceiver(receiver, filter);
            //list.ItemSelected += List_ItemSelected;
            return root;
        }

        private void List_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public class ScanReceiver : BroadcastReceiver
        {
            public ListView list { get; set; }

            public override void OnReceive(Context context, Intent intent)
            {
                //Toast.MakeText(Application.Context, cDataWedge.HandleDecodeData(intent).Split('|')[0], ToastLength.Long).Show();
                for (var i=0; i<list.Count-1;i++)
                {
                    if (list.GetItemAtPosition(i).ToString().Split('|')[1] == cDataWedge.HandleDecodeData(intent).Split('|')[0])
                    {
                        list.SetItemChecked(i,true);
                        break;
                    }
                }
            }
        }

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

    

    public class ListRepairs2LoadAdapter : BaseAdapter
    {
        private Context context;
        private DynamicRS _RS = new DynamicRS("Select RepairCode,UnitNumber from Repairs where dbo.checkFlag(flags,'INI')=1 and service='"+Values.gService+"' order by UnitNumber", Values.gDatos);
        public ListView list { get; set; }
        public TextView LoadLabel { get; set; }

        public ListRepairs2LoadAdapter(Context context)
        {
            this.context = context;
            _RS.Open();
        }

        public override void NotifyDataSetChanged()
        {
            base.NotifyDataSetChanged();
            _RS = null;
            _RS = new DynamicRS();
            _RS.Open("Select RepairCode,UnitNumber from Repairs where dbo.checkFlag(flags,'INI')=1 and service='" + Values.gService + "' order by xfec", Values.gDatos);
        }

        public override int Count
        {
            get { return _RS.RecordCount; }
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

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(context).Inflate(Android.Resource.Layout.SimpleListItemActivated2, parent, false);
                //convertView.Click += ConvertView_Click;
            }
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "UNIT: " + GetItem(position).ToString().Split('|')[1];
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "REPAIR CODE: " + GetItem(position).ToString().Split('|')[0];
            
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