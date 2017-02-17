

using Android.App;
using Android.Content;
using Android.OS;

using Android.Views;


using Android.Support.V7.App;

using ActionBar = Android.Support.V7.App.ActionBar;

namespace RadioFXC
{
    [Activity(Label = "Radio REPAIRS - Loads")]
    public class LoadsMain : AppCompatActivity, ActionBar.ITabListener
    {
        //private string cLoadNumber;
        //public string cRepairCode { get; set; }

        //public MainRepairs(string pUnitNumber, string pRepairCode)
        //{
        //    cUnitNumber = pUnitNumber;
        //    cRepairCode = pRepairCode;
        //}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.Title = "Loads - Service " + Values.gService;
            if (SupportActionBar == null)
                return;
            var actionBar = SupportActionBar;
            actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;

            var tab1 = actionBar.NewTab();
            tab1.SetTabListener(this);
            tab1.SetText("Add Repairs");
            actionBar.AddTab(tab1);

            var tab2 = actionBar.NewTab();
            tab2.SetTabListener(this);
            tab2.SetText("Close Load");
            actionBar.AddTab(tab2);
        }
        public void OnTabReselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {

        }

        public void OnTabSelected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            switch (tab.Text)
            {
                case "Add Repairs":
                    ft.Replace(Android.Resource.Id.Content, new LoadsFragmentRepairs2Loads());
                    break;
                case "Close Load":
                    ft.Replace(Android.Resource.Id.Content, new LoadsFragmentCloseLoads());
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