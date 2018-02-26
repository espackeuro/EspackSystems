package md5466558fdf6f1b5370bec1504d79b5c8c;


public class RepairsMain
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		android.support.v7.app.ActionBar.TabListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"n_onCreateOptionsMenu:(Landroid/view/Menu;)Z:GetOnCreateOptionsMenu_Landroid_view_Menu_Handler\n" +
			"n_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\n" +
			"n_onTabReselected:(Landroid/support/v7/app/ActionBar$Tab;Landroid/support/v4/app/FragmentTransaction;)V:GetOnTabReselected_Landroid_support_v7_app_ActionBar_Tab_Landroid_support_v4_app_FragmentTransaction_Handler:Android.Support.V7.App.ActionBar/ITabListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"n_onTabSelected:(Landroid/support/v7/app/ActionBar$Tab;Landroid/support/v4/app/FragmentTransaction;)V:GetOnTabSelected_Landroid_support_v7_app_ActionBar_Tab_Landroid_support_v4_app_FragmentTransaction_Handler:Android.Support.V7.App.ActionBar/ITabListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"n_onTabUnselected:(Landroid/support/v7/app/ActionBar$Tab;Landroid/support/v4/app/FragmentTransaction;)V:GetOnTabUnselected_Landroid_support_v7_app_ActionBar_Tab_Landroid_support_v4_app_FragmentTransaction_Handler:Android.Support.V7.App.ActionBar/ITabListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"";
		mono.android.Runtime.register ("RadioFXC.RepairsMain, RadioFXC, Version=1.0.6631.25754, Culture=neutral, PublicKeyToken=null", RepairsMain.class, __md_methods);
	}


	public RepairsMain ()
	{
		super ();
		if (getClass () == RepairsMain.class)
			mono.android.TypeManager.Activate ("RadioFXC.RepairsMain, RadioFXC, Version=1.0.6631.25754, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();


	public boolean onCreateOptionsMenu (android.view.Menu p0)
	{
		return n_onCreateOptionsMenu (p0);
	}

	private native boolean n_onCreateOptionsMenu (android.view.Menu p0);


	public boolean onOptionsItemSelected (android.view.MenuItem p0)
	{
		return n_onOptionsItemSelected (p0);
	}

	private native boolean n_onOptionsItemSelected (android.view.MenuItem p0);


	public void onTabReselected (android.support.v7.app.ActionBar.Tab p0, android.support.v4.app.FragmentTransaction p1)
	{
		n_onTabReselected (p0, p1);
	}

	private native void n_onTabReselected (android.support.v7.app.ActionBar.Tab p0, android.support.v4.app.FragmentTransaction p1);


	public void onTabSelected (android.support.v7.app.ActionBar.Tab p0, android.support.v4.app.FragmentTransaction p1)
	{
		n_onTabSelected (p0, p1);
	}

	private native void n_onTabSelected (android.support.v7.app.ActionBar.Tab p0, android.support.v4.app.FragmentTransaction p1);


	public void onTabUnselected (android.support.v7.app.ActionBar.Tab p0, android.support.v4.app.FragmentTransaction p1)
	{
		n_onTabUnselected (p0, p1);
	}

	private native void n_onTabUnselected (android.support.v7.app.ActionBar.Tab p0, android.support.v4.app.FragmentTransaction p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
