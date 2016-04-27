//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using cAccesoDatosAndroid;

//namespace RadioFXC
//{

//    [Activity(Label = "Enter Unit")]
//    public class EnterUnit : Activity
//    {
//        private EditText cUnitNumber;
//        private EditText cRepairCode;
//        private Button cButtonEnter;
//        private TextView cMsgText;
//        protected override void OnCreate(Bundle bundle)
//        {
//            base.OnCreate(bundle);
//            SetContentView(Resource.Layout.EnterUnit);
//            cUnitNumber = FindViewById<EditText>(Resource.Id.txtUnitNumber);
//            cRepairCode = FindViewById<EditText>(Resource.Id.txtRepairCode);
//            cButtonEnter = FindViewById<Button>(Resource.Id.btnEnter);
//            cMsgText = FindViewById<TextView>(Resource.Id.msgText);
//            cButtonEnter.Click += CButtonEnter_Click;
//            // Create your application here
//        }

//        private void CButtonEnter_Click(object sender, EventArgs e)
//        {
//            SP pAddRepairs = new SP(Values.gDatos, "pAddRepairs");
//            pAddRepairs.AddParameterValue("UnitNumber", cUnitNumber.Text);
//            pAddRepairs.AddParameterValue("RepairCode", cRepairCode.Text);
//            pAddRepairs.Execute();
//            Intent intent;
//            if (pAddRepairs.LastMsg == "OK")
//            {
//                intent = new Intent(this, typeof(RepairManagement));
//                intent.PutExtra("UnitNumber", cUnitNumber.Text);
//                intent.PutExtra("RepairCode", cRepairCode.Text);
//                intent.PutExtra("PicturesCount", "0");
//                StartActivityForResult(intent, 2);
//            }
//            else if (pAddRepairs.LastMsg.Substring(0, 3) == "OK|")
//            {


//                Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
//                AlertDialog alertDialog = builder.Create();
//                alertDialog.SetTitle("Warning");
//                alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
//                alertDialog.SetMessage("Repair code already exist, what do you want to do?");
//                alertDialog.SetButton("Enter new one", (s, ev) =>
//                {
//                    cUnitNumber.Text = "";
//                    cRepairCode.Text = "";
//                    cUnitNumber.RequestFocus();
//                });
//                alertDialog.SetButton2("Enter this code.", (s, ev) =>
//                {
//                    intent = new Intent(this, typeof(RepairManagement));
//                    intent.PutExtra("UnitNumber", cUnitNumber.Text);
//                    intent.PutExtra("RepairCode", cRepairCode.Text);
//                    intent.PutExtra("PicturesCount", pAddRepairs.LastMsg.Split('|')[1]);
//                    StartActivityForResult(intent, 2);
//                    cUnitNumber.Text = "";
//                    cRepairCode.Text = "";
//                    cUnitNumber.RequestFocus();
//                });
//                alertDialog.Show();
//                return;
//            }
//            else
//            {
//                cMsgText.Text = pAddRepairs.LastMsg;

//            }
//            //
//            cUnitNumber.Text = "";
//            cRepairCode.Text = "";
//            cUnitNumber.RequestFocus();
//        }
//    }
//}