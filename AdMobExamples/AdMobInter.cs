using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using RenderEngine.Screens;

namespace AdModExamples
{
    class adlistener : AdListener
    {
        ScreenManager Manager;
        public adlistener(ScreenManager Manager):base()
        {
            this.Manager = Manager;
        }
        // Declare the delegate (if using non-generic pattern). 
        public delegate void AdLoadedEvent();
        public delegate void AdClosedEvent();
        public delegate void AdOpenedEvent();
        public delegate void AdHideEvent();


        // Declare the event. 
        public event AdLoadedEvent AdLoaded;
        public event AdClosedEvent AdClosed;
        public event AdOpenedEvent AdOpened;
        public event AdHideEvent AdHided;

        public override void OnAdLoaded()
        {
            if (AdLoaded != null) this.AdLoaded();
            base.OnAdLoaded();
        }

        public override void OnAdClosed()
        {
            if (AdClosed != null) this.AdClosed();
            Manager.Credits += 1000;
            base.OnAdClosed();
        }


        public override void OnAdOpened()
        {
            if (AdOpened != null) this.AdOpened();
            base.OnAdOpened();
        }
    }
}