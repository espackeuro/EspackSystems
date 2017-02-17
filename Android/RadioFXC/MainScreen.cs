using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ActionBar = Android.Support.V7.App.ActionBar;
using Scanner;
using AccesoDatosNet;

namespace RadioFXC
{

    [Activity(Label = "Radio REPAIRS")]
    public class MainScreen : AppCompatActivity, ActionBar.ITabListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.Title = "Service " + Values.gService;
            if (SupportActionBar == null)
                return;
            var actionBar = SupportActionBar;
            actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;

            //var tab0 = actionBar.NewTab();
            //tab0.SetTabListener(this);
            //tab0.SetText("Receivals");
            //actionBar.AddTab(tab0);

            var tab1 = actionBar.NewTab();
            tab1.SetTabListener(this);
            tab1.SetText("Repairs");
            actionBar.AddTab(tab1);

            var tab2 = actionBar.NewTab();
            tab2.SetTabListener(this);
            tab2.SetText("Loads");
            actionBar.AddTab(tab2);
        }
        
        
        public void OnTabReselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {

        }

        public void OnTabSelected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            switch (tab.Text)
            {
                case "Receivals":
                    ft.Replace(Android.Resource.Id.Content, new ReceivalsFragment());
                    break;
                case "Repairs":
                    ft.Replace(Android.Resource.Id.Content, new RepairsFragment());
                    break;
                case "Loads":
                    ft.Replace(Android.Resource.Id.Content, new LoadsFragment());
                    break;
            }
        }

        public void OnTabUnselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);
            MenuInflater inflater = this.MenuInflater;
            inflater.Inflate(Resource.Menu.mainMenu, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);
            Intent intent;
            switch (item.ItemId)
            {
                case Resource.Id.mnumain: break;
                case Resource.Id.mnurepair: break;
                case Resource.Id.mnuloads:
                    intent = new Intent(this, typeof(LoadsMain));
                    StartActivity(intent);
                    break;
                case Resource.Id.mnuclose:
                    System.Environment.Exit(0);
                    break;
                default:
                    break;
            }
            return true;
        }

    }
    public static class UnitRepair
    {
        public static string cUnitNumber { get; set; }
        public static string cRepairCode { get; set; }
    }
    public static class Loads
    {
        public static string LoadNumber { get; set; }
        public static DateTime LoadDate { get; set; }
        public static int RepairNumber
        {
            get
            {
                var lRS = new DynamicRS();
                lRS.Open("Select num=count(*) from Repairs where LoadNumber='" + LoadNumber + "' and service='"+Values.gService+"'", Values.gDatos);
                return Convert.ToInt32(lRS["num"]);
                lRS.Close();
            }
        }
        public static string Label
        {
            get
            {
                return LoadNumber + " (" + RepairNumber.ToString() + ")";
            }
        }
    }
}