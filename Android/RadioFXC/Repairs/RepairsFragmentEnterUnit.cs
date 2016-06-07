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
using Android.Support.V4.App;
namespace RadioFXC
{
    public class FragmentEnterUnit : Android.Support.V4.App.Fragment
    {
        //public override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    // Create your fragment here
        //}
        private EditText cUnitNumber;
        private EditText cRepairCode;
        private Button cButtonEnter;
        private TextView cMsgText;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.EnterUnit, container, false);
            cUnitNumber = root.FindViewById<EditText>(Resource.Id.txtUnitNumber);
            cRepairCode = root.FindViewById<EditText>(Resource.Id.txtRepairCode);
            cButtonEnter = root.FindViewById<Button>(Resource.Id.btnEnter);
            cMsgText = root.FindViewById<TextView>(Resource.Id.msgText);
            cButtonEnter.Click += CButtonEnter_Click;
            return root;
            // Create your application here
        }
        private void CButtonEnter_Click(object sender, EventArgs e)
        {
            SP pAddRepairs = new SP(Values.gDatos, "pAddRepairs");
            pAddRepairs.AddParameterValue("UnitNumber", cUnitNumber.Text);
            pAddRepairs.AddParameterValue("RepairCode", cRepairCode.Text);
            pAddRepairs.AddParameterValue("Service", Values.gService);
            pAddRepairs.Execute();
            //Intent intent;
            if (pAddRepairs.LastMsg == "OK")
            {
                UnitRepair.cUnitNumber = cUnitNumber.Text;
                UnitRepair.cRepairCode = pAddRepairs.Parameters["@RepairCode"].Value.ToString();
                var intent = new Intent(this.Activity, typeof(RepairsMain));
                StartActivityForResult(intent, 2);
                var ft = this.FragmentManager.BeginTransaction();
                //intent = new Intent(this, typeof(RepairManagement));
                //intent.PutExtra("UnitNumber", cUnitNumber.Text);
                //intent.PutExtra("RepairCode", cRepairCode.Text);
                //intent.PutExtra("PicturesCount", "0");
                //StartActivityForResult(intent, 2);
            }
            else if (pAddRepairs.LastMsg.Substring(0, 3) == "OK|")
            {


                var builder = new Android.Support.V7.App.AlertDialog.Builder(Activity);
                builder.SetTitle("Warning");
                builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                builder.SetMessage("Repair code already exist, what do you want to do?");
                builder.SetNeutralButton("Enter new one", delegate
                {
                    cUnitNumber.Text = "";
                    cRepairCode.Text = "";
                    cUnitNumber.RequestFocus();
                });
                builder.SetPositiveButton("Enter this code.",delegate
                {
                    UnitRepair.cUnitNumber = cUnitNumber.Text;
                    UnitRepair.cRepairCode = cRepairCode.Text;
                    var intent = new Intent(this.Activity, typeof(RepairsMain));
                    StartActivityForResult(intent, 2);
                    var ft = this.FragmentManager.BeginTransaction();
                    //intent = new Intent(this, typeof(RepairManagement));
                    //intent.PutExtra("UnitNumber", cUnitNumber.Text);
                    //intent.PutExtra("RepairCode", cRepairCode.Text);
                    //intent.PutExtra("PicturesCount", pAddRepairs.LastMsg.Split('|')[1]);
                    //StartActivityForResult(intent, 2);
                    //cUnitNumber.Text = "";
                    //cRepairCode.Text = "";
                    //cUnitNumber.RequestFocus();
                });
                builder.Create().Show();
                return;
            }
            else
            {
                cMsgText.Text = pAddRepairs.LastMsg;

            }
            //
            cUnitNumber.Text = "";
            cRepairCode.Text = "";
            cUnitNumber.RequestFocus();
        }
    }
}