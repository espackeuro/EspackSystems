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
using cAccesoDatosAndroid;
using Android.Support.V4.App;
namespace RadioFXC
{
    public class FragmentEnterLoad : Android.Support.V4.App.Fragment
    {
        private EditText cNotesLoads;
        private Button cButtonEnter;
        private TextView cMsgText;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.EnterLoad, container, false);
            cNotesLoads = root.FindViewById<EditText>(Resource.Id.txtNotesLoads);
            cButtonEnter = root.FindViewById<Button>(Resource.Id.btnEnter);
            cMsgText = root.FindViewById<TextView>(Resource.Id.msgText);
            cButtonEnter.Click += CButtonEnter_Click;
            return root;
            // Create your application here
        }
        private void CButtonEnter_Click(object sender, EventArgs e)
        {
            SP pAddLoads = new SP(Values.gDatos, "pAddLoads");
            pAddLoads.AddParameterValue("Notes", cNotesLoads.Text);
            pAddLoads.AddParameterValue("Service", Values.gService);
            pAddLoads.Execute();
            if (pAddLoads.LastMsg.Substring(0, 2) == "OK")
            {
                Loads.LoadNumber = pAddLoads.LastMsg.Substring(3);
                //IFormatProvider culture = new System.Globalization.CultureInfo("es-ES", true);
                //Loads.LoadDate = DateTime.Parse(lValue.Adapter.GetItem(position).ToString().Split('|')[1], culture, System.Globalization.DateTimeStyles.AssumeLocal);
                var intent = new Intent(this.Activity, typeof(LoadsMain));
                StartActivityForResult(intent, 2);
            }
            else
            {
                cMsgText.Text = pAddLoads.LastMsg;
            }
            cNotesLoads.Text = "";
            cNotesLoads.RequestFocus();
        }
    }
}