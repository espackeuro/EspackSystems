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
using AccesoDatosNet;
using CommonTools;
namespace RadioLogisticaDeliveries
{
    public class orderFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        private EditText orderNumberET;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var _root = inflater.Inflate(Resource.Layout.enterOrderFt, container, false);
            var _buttonOk = _root.FindViewById<Button>(Resource.Id.orderOkButton);
            _buttonOk.Click += _buttonOk_Click;
            orderNumberET = _root.FindViewById<EditText>(Resource.Id.orderNumber);
            return _root;
        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            if (orderNumberET.Text == "" || !orderNumberET.Text.IsNumeric())
            {
                Toast.MakeText(Activity, "Please enter one valid Order Number",ToastLength.Long).Show();
                orderNumberET.Text = "";
                return;
            }

            var _sp = new SPXML(Values.gDatos, "pAddCabReadingSession");
            //_sp.AddParameterValue("Block", " ");
            //_sp.AddParameterValue("Service", " ");
            _sp.AddParameterValue("User", Values.gDatos.User);
            _sp.AddParameterValue("orderNumber", orderNumberET.Text);
            try
            {
                _sp.Execute();
            } catch (Exception ex)
            {
                Toast.MakeText(Activity, ex.Message, ToastLength.Long).Show();
                orderNumberET.Text = "";
                return;
            }
            ((MainScreen)Activity).hf.Session.Text = _sp.LastMsg.Substring(3);
            Values.gBlock = _sp.ReturnValues()["@Block"].ToString();
            getDataFromServer(orderNumberET.Text.ToInt(), _sp.ReturnValues()["@Service"].ToString());
        }


        //method to get all the data from sql server
        private void getDataFromServer(int orderNumber, string service)
        {
            //data from labels for checkng
            using (var _rs = new XMLRS(string.Format("select partnumber,qty,cajas,rack,Modulo from etiquetas where Numero_orden={0} and Tipo='PEQ'",orderNumber), Values.gDatos))
            {
                _rs.Open();
                _rs.Rows.ForEach(r => SQLiteDatabase.db.Insert(new Labels { Partnumber = r["partnumber"].ToString(), qty = r["qty"].ToInt(), boxes = r["cajas"].ToInt(), rack = r["rack"].ToString(), mod = r["Modulo"].ToString() }));
                
            }
            //data from referencias table
            using (var _rs = new XMLRS(string.Format("select partnumber from referencias where servicio='{0}'",service), Values.gDatos))
            {
                _rs.Open();
                _rs.Rows.ForEach(r => SQLiteDatabase.db.Insert(new Referencias { partnumber = r["partnumber"].ToString() }));
            }
        }

    }
}