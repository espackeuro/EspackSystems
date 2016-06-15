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
        private FragmentPicturesManagement _fpim;
        private FragmentPartsManagement _fpam;
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

        //to cancel back button in Android
        public override void OnBackPressed()
        {
            if (_fpim != null)
            {
                if (_fpim.cImageFileList.pendingItems.Count != 0)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "There are pictures pending to transmit, wait till transmission is done.", ToastLength.Long).Show());
                    return;
                }
            }
            base.OnBackPressed();
        }

        public void OnTabSelected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            switch (tab.Text)
            {
                case "Pictures":
                    if (_fpim == null)
                        _fpim = new FragmentPicturesManagement();
                    ft.Replace(Android.Resource.Id.Content, _fpim);
                    break;
                case "Parts":
                    if (_fpam == null)
                        _fpam = new FragmentPartsManagement();
                    ft.Replace(Android.Resource.Id.Content, _fpam);
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
            if (_fpim != null)
            {
                if (_fpim.cImageFileList.pendingItems.Count!=0)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "There are pictures pending to transmit, wait till transmission is done.", ToastLength.Long).Show());
                    return false;
                }
            }
            base.OnOptionsItemSelected(item);
            Intent intent;
            switch (item.ItemId)
            {
                case Resource.Id.mnumain:
                    intent = new Intent(this, typeof(MainScreen));
                    StartActivity(intent);
                    break;
                case Resource.Id.mnurepair:
                    intent = new Intent(this, typeof(RepairsMain));
                    StartActivity(intent);
                    break;
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
}