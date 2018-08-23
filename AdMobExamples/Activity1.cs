using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Android.Gms.Ads;
using Android.Support.V4;
using Android.Gms.Ads.Reward;
using System;

namespace AdModExamples
{
        [Activity(Label = "AdModExamples"
            , MainLauncher = true
            , Icon = "@drawable/icon"
            , Theme = "@style/Theme.Splash"
            , AlwaysRetainTaskState = true
            , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
            , ScreenOrientation = ScreenOrientation.FullUser
            , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
        public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
        {
            string appID = "ca-app-pub-9033799094858050/5564654580";

            protected override void OnCreate(Bundle bundle)
            {
                base.OnCreate(bundle);
                Forms.Init(this, bundle);

                //Game1 + ADS creation
                var g = createAds();


                g.Run();
            }


            private Game1 createAds()
            {
                Game1 g = null;
                try
                {
                    var frameLayout = new FrameLayout(this);
                    var linearLayout = new LinearLayout(this);

                    linearLayout.Orientation = Orientation.Horizontal;
                    linearLayout.SetGravity(Android.Views.GravityFlags.Right | Android.Views.GravityFlags.Bottom);
                            
              

                    //----------------------------------------------banner add stuff
                    AdView _bannerad = AdWrapper.ConstructStandardBanner(this, AdSize.SmartBanner, appID);
                    var listener = new adlistener();
                    listener.AdLoaded += () => { };
                    _bannerad.AdListener = listener;
                    _bannerad.CustomBuild();


                    //-------------------------------------------------------------


                    g = new Game1(_bannerad, linearLayout, this, this);
                    Android.Views.View t = (Android.Views.View)g.Services.GetService(typeof(Android.Views.View));
                    frameLayout.AddView(t);

                    linearLayout.AddView(_bannerad);
                    frameLayout.AddView(linearLayout);
                    SetContentView(frameLayout);



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Load AD:" + ex.Message + ex.StackTrace);
                    // your error Console.ging goes here
                }
                return g;
            }
        }
}


