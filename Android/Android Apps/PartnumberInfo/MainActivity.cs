using Android.App;
using Android.Widget;
using Android.OS;

namespace PartnumberInfo
{
    [Activity(Label = "PartnumberInfo", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var hFt = new HeaderFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.headerFragment, hFt);
            ft.Commit();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

