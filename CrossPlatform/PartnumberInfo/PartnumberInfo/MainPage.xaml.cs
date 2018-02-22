using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data;
using System.Data.SqlClient;
using static PartnumberInfo.Values;
using Android.Content;

namespace PartnumberInfo
{
    public static class Values
    {
        public static SqlConnection gDatos { get; set; }
        public static string Version { get; set; }
        public static Context MainContext { get; set; }

    }
    // http://docs.xamarin.com/android/advanced_topics/linking#falseflag
    static class ForceI18NInclusion
    {
        static bool falseflag = false;
        static ForceI18NInclusion()
        {
            if (falseflag)
            {
                var ignore = new I18N.West.CP1252();
            }
        }
    }
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            var a = I18N.West.CP1252.GetEncoding("utf-32");
            lblVersion.Text = Values.Version;

            
#if DEBUG
            txtUser.Text = "restelles";
            txtPwd.Text = "G8npi3rc";
            txtPartnumber.Text = "DS73F61295DN35B8";
#endif
            btnSearch.Clicked += BtnSearch_Clicked;
            txtPartnumber.Focused += TxtPartnumber_Focused;
		}

        private void TxtPartnumber_Focused(object sender, FocusEventArgs e)
        {
            txtPartnumber.Text = "";
        }

        private async void BtnSearch_Clicked(object sender, EventArgs e)
        {
            try
            {
                await GetDatabaseData();
                DisplayedData.IsVisible = true;
                ErrorLayout.IsVisible = false;
            } catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                DisplayedData.IsVisible = false;
                ErrorLayout.IsVisible = true;
            }
            if (gDatos.State == ConnectionState.Open)
            {
                gDatos.Close();
            }
        }
    }
}
