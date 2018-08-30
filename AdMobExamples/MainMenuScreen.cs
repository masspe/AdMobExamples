#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using AdModExamples;
using Android.App;
using Android.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

using System;


#endregion

namespace RenderEngine.Screens
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    public class MainMenuScreen : MenuScreen
    {
        #region Initialization


        public ScreenManager Manager;
        Texture2D logo;
        public float timer = 0;
        
        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen(ScreenManager Manager)
            : base("", null)
        {
            try
            {
                this.Manager = Manager;
                // Create our menu entries.
                MenuEntry MenuEntry1 = new MenuEntry("InterstitialAd");
                MenuEntry MenuEntry2 = new MenuEntry("Disable Banner");
                MenuEntry MenuEntry3 = new MenuEntry("Enable Banner");


                // Hook up menu event handlers.
                MenuEntry1.Selected += MenuMenu1EntrySelected;
                MenuEntry2.Selected += MenuMenu2EntrySelected;
                MenuEntry3.Selected += MenuMenu3EntrySelected;
                // Add entries to the menu.
                MenuEntries.Add(MenuEntry1);
                MenuEntries.Add(MenuEntry2);
                MenuEntries.Add(MenuEntry3);
                logo = Manager.content.Load<Texture2D>("Screens/logo");
                VStart = Manager.InternalHeight / 2; ;
                // callout
                MenuEntry1.CallOutText = "Open\nInterstitial\nADS ";
               

                // ONLINE API


            }
            catch (Exception ex)
            {
                Console.WriteLine("MainMenu Constructor:" + ex.Message + ex.StackTrace);
            }

        }

        private void MenuMenu3EntrySelected(object sender, PlayerIndexEventArgs e)
        {
            try
            { 
#if ANDROID
            Manager.adView.Resume();
            Manager.linearLayout.AddView(ScreenManager.adView);
#endif
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void MenuMenu2EntrySelected(object sender, PlayerIndexEventArgs e)
        {
            try
            {
#if ANDROID

                Manager.adView.Pause();
            Manager.linearLayout.RemoveView(Manager.adView);

#endif
        }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
}

        private void MenuMenu1EntrySelected(object sender, PlayerIndexEventArgs e)
        {
           
            
            try
            {
#if ANDROID
                //-------------------------------------------------InterstitialAd stuff
                Android.Gms.Ads.InterstitialAd FinalAd = AdWrapper.ConstructFullPageAdd(Manager.activity, Manager.AdsID);
                  var intlistener = new adlistener(Manager);
                  intlistener.AdLoaded += () => { if (FinalAd.IsLoaded) FinalAd.Show(); };
                 FinalAd.AdListener = intlistener;
                FinalAd.CustomBuild();
                SetTimer();
                //-------------------------------------------------------------
#endif
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
           
        }




        #endregion

        #region Handle Input


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer>60000)
            {
#if ANDROID
                //-------------------------------------------------InterstitialAd stuff
                Android.Gms.Ads.InterstitialAd FinalAd = AdWrapper.ConstructFullPageAdd(Manager.activity, Manager.AdsID);
                            var intlistener = new adlistener(Manager);
                            intlistener.AdLoaded += () => { if (FinalAd.IsLoaded) FinalAd.Show(); };
                            FinalAd.AdListener = intlistener;
                            FinalAd.CustomBuild();

                SetTimer();
                //-------------------------------------------------------------
#endif
                timer = 0;
            }
            
        }


        private void SetTimer()
        {
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            long millis = (long)ts.TotalMilliseconds;

            Intent intentAlarm = new Intent(Manager.context, typeof(ToastBroadcast));
            AlarmManager alarmManager = (AlarmManager)Manager.activity.GetSystemService(Context.AlarmService);
            int interval = 5000;

            alarmManager.SetRepeating(AlarmType.RtcWakeup, millis, interval, PendingIntent.GetBroadcast(Manager.context, 1, intentAlarm, PendingIntentFlags.UpdateCurrent));
        }


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(ScreenManager.ExitMessage);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex, null);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
#if !IOS
            //    ScreenManager.Game.Exit();
#endif
        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle(Manager.InternalWidth/2, 2, Manager.InternalWidth / 2,logo.Height);

           
            Manager.spriteBatch.Begin();
           
            Manager.spriteBatch.DrawString(Manager.font, Manager.LocalUser, new Vector2(2, 2),Color.DarkGray);
            Manager.spriteBatch.DrawString(Manager.font, "Credits:"+Manager.Credits, new Vector2(2, 40), Color.DarkGray);
            Manager.spriteBatch.Draw(logo, r, Color.White);
            Manager.spriteBatch.End();

            base.Draw(gameTime);
            #endregion
        }
    }
}
