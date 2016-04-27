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
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ActionBar = Android.Support.V7.App.ActionBar;

namespace RadioFXC
{
    [Activity(Label = "Radio REPAIRS - Repairs")]
    public class RepairsMain : AppCompatActivity, ActionBar.ITabListener
    {
        private string cUnitNumber;
        public string cRepairCode { get; set; }

        //public MainRepairs(string pUnitNumber, string pRepairCode)
        //{
        //    cUnitNumber = pUnitNumber;
        //    cRepairCode = pRepairCode;
        //}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.Title = "Repairs - Service " + Values.gService;
            if (SupportActionBar == null)
                return;
            var actionBar = SupportActionBar;
            actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;

            var tab1 = actionBar.NewTab();
            tab1.SetTabListener(this);
            tab1.SetText("Pictures");
            actionBar.AddTab(tab1);

            var tab2 = actionBar.NewTab();
            tab2.SetTabListener(this);
            tab2.SetText("Parts");
            actionBar.AddTab(tab2);
        }
        public void OnTabReselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {

        }

        public void OnTabSelected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            switch (tab.Text)
            {
                case "Pictures":
                    ft.Replace(Android.Resource.Id.Content, new FragmentPicturesManagement());
                    break;
                case "Parts":
                    ft.Replace(Android.Resource.Id.Content, new FragmentPartsManagement());
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

            switch (item.ItemId)
            {
                case Resource.Id.mnurepair:
                    //Intent intent = new Intent(this, typeof(RepairSelection));
                    //StartActivity(intent);
                    //break;
                case Resource.Id.mnuloads:
                    {
                        break;
                    }
                case Resource.Id.mnuclose:
                    {
                        break;
                    }
                default:
                    break;
            }

            return true;
        }
    }
}