using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Refractored.Fab;
using System;
using AccesoDatosNet;

namespace RadioFXC
{

    public class ReceivalsFragment : Android.Support.V4.App.ListFragment, IScrollDirectorListener, AbsListView.IOnScrollListener
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.fragment_listview_fab, container, false);

            var list = root.FindViewById<ListView>(Android.Resource.Id.List);
            var adapter = new ListReceivalsAdapter(Activity);
            list.Adapter = adapter;
            list.ChoiceMode = ChoiceMode.Single;
            var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.AttachToListView(list, this, this);
            fab.Click += Fab_Click;
                

            return root;
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            //Toast.MakeText(Activity, "FAB Clicked!", ToastLength.Short).Show();
            var ft = this.FragmentManager.BeginTransaction();
            //ft.Replace(Android.Resource.Id.Content, new FragmentEnterLoad());
            ft.Commit();
        }

        public void OnScrollDown()
        {
           // Console.WriteLine("ListViewFragment: OnScrollDown");
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

        public override void OnListItemClick(ListView lValue, View vValue, int position, long idValue)
        {
            Loads.LoadNumber = lValue.Adapter.GetItem(position).ToString().Split('|')[0];
            IFormatProvider culture = new System.Globalization.CultureInfo("es-ES", true);
            Loads.LoadDate = DateTime.Parse(lValue.Adapter.GetItem(position).ToString().Split('|')[1], culture, System.Globalization.DateTimeStyles.AssumeLocal);
            var intent = new Intent(this.Activity, typeof(LoadsMain));
            StartActivityForResult(intent, 2);
            //var ft = this.FragmentManager.BeginTransaction();
            //string _unit = lValue.Adapter.GetItem(position).ToString().Split('|')[1];
            //string _repair = lValue.Adapter.GetItem(position).ToString().Split('|')[0];
            //ft.Replace(Android.Resource.Id.Content, new FragmentRepairManagement(_unit,_repair,0));
            //ft.Commit();
        }
    }

    public class ListReceivalsAdapter : BaseAdapter
    {
        private Context context;
        private DynamicRS _RS = new DynamicRS("Select Entrada,Fecha=convert(varchar(10),fecha,103) from cab_recepcion where dbo.checkFlag(flags,'INI')=1 order by xfec", Values.gDatosLOG);

        public ListReceivalsAdapter(Context context)
        {
            this.context = context;
            _RS.Open();
        }

        public override int Count
        {
            get { return _RS.RecordCount; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            _RS.Move(position);
            return _RS["Entrada"] + "|" + _RS["Fecha"];
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
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "Receival Number: " + GetItem(position).ToString().Split('|')[0];
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "Date: " + GetItem(position).ToString().Split('|')[1];
            return convertView;
        }
    }
}
