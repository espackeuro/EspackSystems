package md5466558fdf6f1b5370bec1504d79b5c8c;


public class ServiceSelection
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("RadioFXC.ServiceSelection, RadioFXC, Version=1.0.6631.25754, Culture=neutral, PublicKeyToken=null", ServiceSelection.class, __md_methods);
	}


	public ServiceSelection ()
	{
		super ();
		if (getClass () == ServiceSelection.class)
			mono.android.TypeManager.Activate ("RadioFXC.ServiceSelection, RadioFXC, Version=1.0.6631.25754, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
