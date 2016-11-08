using System;
using System.Collections.Generic;
using System.Linq;
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

namespace RadioLogisticaDeliveries
{
    [Activity(Label = "Radio Deliveries - Block Selection")]
    public class BlockSelection : AppCompatActivity
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
            Values.gBlock = _list.Adapter.GetItem(e.Position).ToString();
            var intent = new Intent(this, typeof(MainScreen));
            StartActivityForResult(intent, 2);
        }

    }


    public class ListServicesAdapter : BaseAdapter
    {
        private Context context;
        private XMLRS _RS = new XMLRS("select distinct Block=planta from servicios_destinos sd inner join servicios s on s.codigo=sd.Servicio where s.cod3='LIV' and dbo.checkflag(s.flags,'TRUCKDEL')=1 order by planta", Values.gDatos);

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
            return _RS["Block"].ToString(); //+ "|" + _RS["codigo"];
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
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "BLOCK: " + GetItem(position).ToString();
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "";
            return convertView;
        }
    }
}