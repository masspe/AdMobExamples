package md599d50d8b737a9e822f0880b9e646cd14;


public class adlistener
	extends com.google.android.gms.ads.AdListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAdLoaded:()V:GetOnAdLoadedHandler\n" +
			"n_onAdClosed:()V:GetOnAdClosedHandler\n" +
			"n_onAdOpened:()V:GetOnAdOpenedHandler\n" +
			"";
		mono.android.Runtime.register ("AdModExamples.adlistener, AdModExamples", adlistener.class, __md_methods);
	}


	public adlistener ()
	{
		super ();
		if (getClass () == adlistener.class)
			mono.android.TypeManager.Activate ("AdModExamples.adlistener, AdModExamples", "", this, new java.lang.Object[] {  });
	}


	public void onAdLoaded ()
	{
		n_onAdLoaded ();
	}

	private native void n_onAdLoaded ();


	public void onAdClosed ()
	{
		n_onAdClosed ();
	}

	private native void n_onAdClosed ();


	public void onAdOpened ()
	{
		n_onAdOpened ();
	}

	private native void n_onAdOpened ();

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