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
using System.Threading;
using System.Threading.Tasks;
using Android.Views.InputMethods;

namespace RadioLogisticaDeliveries
{
    public class orderFragment : Fragment
    {
        private Button buttonOk;
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
            buttonOk = _root.FindViewById<Button>(Resource.Id.orderOkButton);
            buttonOk.Click += _buttonOk_Click;
            orderNumberET = _root.FindViewById<EditText>(Resource.Id.orderNumber);
#if DEBUG
            orderNumberET.Text = "724006915";
#endif
            return _root;
        }

        private async void _buttonOk_Click(object sender, EventArgs e)
        {
            if (orderNumberET.Text == "" || !orderNumberET.Text.IsNumeric())
            {
                Toast.MakeText(Activity, "Please enter one valid Order Number",ToastLength.Long).Show();
                orderNumberET.Text = "";
                return;
            }
            buttonOk.Enabled = false;
            await ((MainScreen)Activity).iFt.pushInfo("Creating Session");
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
                await ((MainScreen)Activity).iFt.pushInfo(ex.Message);
                orderNumberET.Text = "";
                buttonOk.Enabled = true;
                return;
            }
            await ((MainScreen)Activity).iFt.pushInfo("Done");
            ((MainScreen)Activity).hFt.Session.Text = _sp.LastMsg.Substring(3);
            Values.gBlock = _sp.ReturnValues()["@Block"].ToString();
            Values.gOrderNumber = orderNumberET.Text.ToInt();
            Values.gService= _sp.ReturnValues()["@Service"].ToString();

            //update database data
            await getDataFromServer();
        }


        //method to get all the data from sql server
        private async Task getDataFromServer()
        {
            //Dismiss Keybaord
            InputMethodManager imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(orderNumberET.WindowToken, 0);
            await ((MainScreen)Activity).iFt.pushInfo("Getting Label Data");
            //data from labels for checkng
            using (var _rs = new XMLRS(string.Format("select partnumber,qty,cajas,rack,Modulo from etiquetas where Numero_orden={0} and Tipo='PEQ'", Values.gOrderNumber), Values.gDatos))
            {
                _rs.Open();
                _rs.Rows.ForEach(r => SQLiteDatabase.db.Insert(new Labels { Partnumber = r["partnumber"].ToString(), qty = r["qty"].ToInt(), boxes = r["cajas"].ToInt(), rack = r["rack"].ToString(), mod = r["Modulo"].ToString() }));
                
            }
            await ((MainScreen)Activity).iFt.pushInfo("Done");
            await ((MainScreen)Activity).iFt.pushInfo("Getting References table");
            //data from referencias table
            using (var _rs = new XMLRS(string.Format("select partnumber from referencias where servicio='{0}'", Values.gService), Values.gDatos))
            {
                _rs.Open();
                _rs.Rows.ForEach(r => SQLiteDatabase.db.Insert(new Referencias { partnumber = r["partnumber"].ToString() }));
            }
            await ((MainScreen)Activity).iFt.pushInfo("Done");
            await ((MainScreen)Activity).iFt.pushInfo("Getting RacksBlocks table");
            //data from RacksBlocks table
            using (var _rs = new XMLRS(string.Format("select Block,Rack from RacksBlocks where service='{0}' and dbo.CheckFlag(flags,'OBS')=0", Values.gService), Values.gDatos))
            {
                _rs.Open();
                _rs.Rows.ForEach(r => SQLiteDatabase.db.Insert(new RacksBlocks { Block = r["Block"].ToString(), Rack = r["Rack"].ToString() }));
            }
            await ((MainScreen)Activity).iFt.pushInfo("Done");
            await ((MainScreen)Activity).iFt.pushInfo("Getting PartnumberRacks table");
            //data from RacksBlocks table
            using (var _rs = new XMLRS(string.Format("Select p.Rack,Partnumber,MinBoxes,MaxBoxes,p.flags from PartnumbersRacks p inner join RacksBlocks r on r.Rack=p.Rack where p.service='{0}' and dbo.CheckFlag(r.flags,'OBS')=0", Values.gService), Values.gDatos))
            {
                _rs.Open();
                _rs.Rows.ForEach(r => SQLiteDatabase.db.Insert(new PartnumbersRacks { Rack = r["Rack"].ToString(), Partnumber=r["Partnumber"].ToString(), MinBoxes=r["MinBoxes"].ToInt(), MaxBoxes=r["MaxBoxes"].ToInt() }));
            }
            await ((MainScreen)Activity).iFt.pushInfo("Done loading database data");
            ((MainScreen)Activity).changeOrderToEnterDataFragments();
        }

    }
}