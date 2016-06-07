using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using AccesoDatosNet;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace RadioFXC
{
    [Activity(Label = "Radio REPAIRS - Service Selection")]
    public class ServiceSelection : AppCompatActivity
    {
        private ListView _list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleList);
            _list = FindViewById<ListView>(Resource.Id.list);
            _list.Adapter = new ListServicesAdapter(this);
            _list.ItemClick += _list_ItemClick;
            // Create your application here
        }

        private void _list_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Values.gService = _list.Adapter.GetItem(e.Position).ToString().Split('|')[0];
            var intent = new Intent(this, typeof(MainScreen));
            StartActivityForResult(intent, 2);
        }

    }


    public class ListServicesAdapter:BaseAdapter
    {
        private Context context;
        private DynamicRS _RS = new DynamicRS("Select Codigo,Nombre from LOGISTICA..Servicios where dbo.checkFlag(flags,'REPAIRS')=1 order by Codigo", Values.gDatos);

        public ListServicesAdapter(Context context)
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
            return _RS["Codigo"] + "|" + _RS["Nombre"];
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
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "SERVICE: " + GetItem(position).ToString().Split('|')[0];
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text =  GetItem(position).ToString().Split('|')[1];
            return convertView;
        }
    }
}