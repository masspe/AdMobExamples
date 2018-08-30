# AdMobExamples
This is a simple example for banner unit and InterstitialAd

The project target is android 4.4 and use Xamarin.GooglePlayServices.Ads and Xamarin.Forms

After the AdMob registration on https://www.google.com/admob/ create the  app (main menu -> apps -> add app) after the app creation in ad units create 2 units Banner and Interstitial.

in Activity1.cs insert your Banner Ad unit ID, in order to show the bottom banner 

  public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
        {
        // insert your Ad unit ID
            string bannerID = "ca-app-pub-9033799094858050/5564654580";
 
 
 in ScreenManager.cs insert your Interstitial Ad unit ID
 
    public OnScreenKeyboard keyBoard;
    // insert your Ad unit ID
        public string AdsID = "ca-app-pub-9033799094858050/4668237472";
  

The app has 3 button the first show the Interstitial and increase the points the second and the third enable and disable the banner

Every 60 sec the App activate the Interstitial and after 5 sec reactivate the APP
