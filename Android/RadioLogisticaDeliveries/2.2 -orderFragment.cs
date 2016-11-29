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
using CommonAndroidTools;

namespace RadioLogisticaDeliveries
{
    public class orderFragment : Fragment
    {
        //private Button buttonOk;
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
            //buttonOk = _root.FindViewById<Button>(Resource.Id.orderOkButton);
            //buttonOk.Click += _buttonOk_Click;
            orderNumberET = _root.FindViewById<EditText>(Resource.Id.orderNumber);
#if DEBUG
            //orderNumberET.Text = "724006915";
#endif
           // orderNumberET.
            //5orderNumberET.EditorAction += OrderNumberET_EditorAction;
            orderNumberET.KeyPress += OrderNumberET_KeyPress;
            return _root;
        }

        private async void OrderNumberET_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.Event.Action == KeyEventActions.Down && (e.KeyCode == Keycode.Enter || e.KeyCode == Keycode.Tab))
            {
                orderNumberET.Enabled = false;
                await ActionGo();
                orderNumberET.Enabled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        //private async void OrderNumberET_EditorAction(object sender, TextView.EditorActionEventArgs e)
        //{
        //    if (e.ActionId == ImeAction.Go)
        //    {
        //        await ActionGo();
        //    }
        //    else
        //    {
        //        e.Handled = false;
        //    }
        //}

        private async Task ActionGo()
        {
            if (orderNumberET.Text == "")
            {

                cSounds.Error(Activity);
                
                Toast.MakeText(Activity, "Please enter one valid Order Number", ToastLength.Long).Show();
                orderNumberET.Text = "";
                return;
            }
            string _orderNumber;
            string _blockCode;
            if (orderNumberET.Text.IsNumeric() && orderNumberET.Text.Length>6)
            {
                _orderNumber = orderNumberET.Text;
                _blockCode = "";
            } else
            {
                _orderNumber = null;
                _blockCode = orderNumberET.Text;
            }
            //buttonOk.Enabled = false;
            await Values.iFt.pushInfo("Creating Session");
            var _sp = new SPXML(Values.gDatos, "pAddCabReadingSession");
            _sp.AddParameterValue("Block", _blockCode);
            //_sp.AddParameterValue("Service", " ");
            _sp.AddParameterValue("User", Values.gDatos.User);
            _sp.AddParameterValue("orderNumber", _orderNumber);
            try
            {
                _sp.Execute();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Activity, ex.Message, ToastLength.Long).Show();
                await Values.iFt.pushInfo(ex.Message);
                orderNumberET.Text = "";
                //buttonOk.Enabled = true;
                return;
            }
            await Values.iFt.pushInfo("Done");
            Values.gSession = _sp.LastMsg.Substring(3);
            Values.hFt.t2.Text = string.Format("Session: {0}", Values.gSession); 
            Values.gBlock = _sp.ReturnValues()["@Block"].ToString();
            
            if (_orderNumber!=null)
            {
                Values.gOrderNumber = orderNumberET.Text.ToInt();
                Values.hFt.t4.Text = string.Format("Order: {0}", Values.gOrderNumber);
            }

            Values.gService = _sp.ReturnValues()["@Service"].ToString();

            //update database data
            await Values.sFt.ChangeProgressVisibility(true);
            await getDataFromServer();
            await Values.sFt.ChangeProgressVisibility(false);
        }

        //method to get all the data from sql server
        private async Task getDataFromServer()
        {
            //Dismiss Keybaord
            InputMethodManager imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(orderNumberET.WindowToken, 0);
            
            if (Values.gOrderNumber!=0)
            {
                await Values.iFt.pushInfo("Getting Label Data");
                //data from labels for checkng
                using (var _rs = new XMLRS(string.Format("select numero,partnumber,qty,cajas,rack,Modulo from etiquetas where Numero_orden={0} and Tipo='PEQ'", Values.gOrderNumber), Values.gDatos))
                {
                    _rs.Open();
                    _rs.Rows.ForEach(async r => await SQLidb.db.InsertAsync(new Labels { Serial = r["numero"].ToString(), Partnumber = r["partnumber"].ToString(), qty = r["qty"].ToInt(), boxes = r["cajas"].ToInt(), rack = r["rack"].ToString(), mod = r["Modulo"].ToString() }));
                    Values.sFt.CheckQtyTotal= _rs.Rows.Count;
                }
                await Values.iFt.pushInfo("Done");
            }

            //await Values.iFt.pushInfo("Getting References table");
            ////data from referencias table
            //using (var _rs = new XMLRS(string.Format("select partnumber from referencias where servicio='{0}'", Values.gService), Values.gDatos))
            //{
            //    _rs.Open();
            //    _rs.Rows.ForEach(async r => await SQLiteDatabase.db.InsertAsync(new Referencias { partnumber = r["partnumber"].ToString() }));
            //}
            //await Values.iFt.pushInfo("Done");

            await Values.iFt.pushInfo("Getting RacksBlocks table");
            //data from RacksBlocks table
            using (var _rs = new XMLRS(string.Format("select Block,Rack from RacksBlocks where service='{0}' and dbo.CheckFlag(flags,'OBS')=0", Values.gService), Values.gDatos))
            {
                _rs.Open();
                _rs.Rows.ForEach(async r => await SQLidb.db.InsertAsync(new RacksBlocks { Block = r["Block"].ToString(), Rack = r["Rack"].ToString() }));
            }
            await Values.iFt.pushInfo("Done");
            await Values.iFt.pushInfo("Getting PartnumberRacks table");
            //data from RacksBlocks table
            using (var _rs = new XMLRS(string.Format("Select p.Rack,Partnumber,MinBoxes,MaxBoxes,p.flags from PartnumbersRacks p inner join RacksBlocks r on r.Rack=p.Rack where p.service='{0}' and dbo.CheckFlag(r.flags,'OBS')=0", Values.gService), Values.gDatos))
            {
                _rs.Open();
                _rs.Rows.ForEach(async r => await SQLidb.db.InsertAsync(new PartnumbersRacks { Rack = r["Rack"].ToString(), Partnumber=r["Partnumber"].ToString(), MinBoxes=r["MinBoxes"].ToInt(), MaxBoxes=r["MaxBoxes"].ToInt() }));
            }
            
            await Values.iFt.pushInfo("Done loading database data");
            ((MainScreen)Activity).changeOrderToEnterDataFragments();
        }

    }
}