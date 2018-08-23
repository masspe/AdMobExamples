using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.Threading;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;



namespace RenderEngine.Screens
{
    public class  LoadingScreen : GameScreen  
    {
        #region Fields  

        Thread backgroundThread;
        EventWaitHandle backgroundThreadExit;
        bool loadingIsSlow;  
        
        GameScreen[] screensToLoad;  
        GameTime loadStartTime;

        bool otherScreensAreGone;
        static Texture2D backgroundTexture,loadingTexture,bar;
      
        ContentManager content;
        float alpha = 0,inc=0.01f;
        SpriteFont demiBold;
      
    private int IW, IH;
    public RenderTarget2D background;
        public long lastTime=0;
        ScreenManager Manager;

    #endregion

        #region Initialization  


        /// <summary>  
        /// The constructor is private: loading screens should  
        /// be activated via the static Load method instead.  
        /// </summary>  
        public LoadingScreen(ScreenManager screenManager, bool loadingIsSlow,  
                              GameScreen[] screensToLoad)  
        {
            this.Name = "Loading";
            Manager = screenManager;
            if (content == null)
                content = new ContentManager(screenManager.Game.Services, "Content");

            this.loadingIsSlow = loadingIsSlow;  
            this.screensToLoad = screensToLoad;
            Manager.keyBoard.Enabled = false;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            backgroundTexture = content.Load<Texture2D>("SCManager/loading-screen");
            bar = content.Load<Texture2D>("SCManager/bar");
            loadingTexture = content.Load<Texture2D>("SCManager/loading");
        
            demiBold = content.Load<SpriteFont>("Fonts/font");
        IW = screenManager.InternalWidth;

        IH = screenManager.InternalHeight;

            loadStartTime = new GameTime();
            ThreadPool.QueueUserWorkItem(new WaitCallback(BackgroundWorkerThread),null);
 
        }


        public void BackgroundWorkerThread(Object o)
        {
           
            
                foreach (GameScreen screen in screensToLoad)
                {
                    if (screen != null)
                    {
                   
                    try
                    {
                        if (ControllingPlayer == null)
                            ControllingPlayer = new PlayerIndex();
                        Manager.AddScreen(screen, ControllingPlayer, this);
                        Manager.RemoveScreen(this);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("BackgroundWorkerThread:" + ex.Message + ex.StackTrace);
                    }
                }
                }


               
         
          
        }

       

        /// <summary>  
        /// Activates the loading screen.  
        /// </summary>  
        public static void Load(ScreenManager screenManager, bool loadingIsSlow,  
                                PlayerIndex? controllingPlayer,  
                                params GameScreen[] screensToLoad)  
        {  
            // Tell all the current screens to transition off.  
            foreach (GameScreen screen in screenManager.GetScreens())  
                screen.ExitScreen();  
              
            // Create and activate the loading screen.  
            LoadingScreen loadingScreen = new LoadingScreen(screenManager,  
                                                            loadingIsSlow,  
                                                            screensToLoad);

            screenManager.AddScreen(loadingScreen, controllingPlayer, loadingScreen);

        }
        #endregion

        #region Update and Draw  


     
        /// <summary>  
        /// Updates the loading screen.  
        /// </summary>  
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,  
                                                       bool coveredByOtherScreen)  
        {
            try
            {
                base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);


                // If all the previous screens have finished transitioning  
                // off, it is time to actually perform the load.  



             
                alpha += inc;
                if (alpha > 1) inc = -0.01f;
                if (alpha < 0) inc = 0.01f;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loading Update:" + ex.Message + ex.StackTrace);
               
            }
        }  
 
        /// <summary>  
        /// Worker thread draws the loading animation and updates the network  
        /// session while the load is taking place.  
        /// </summary>  

 
 
        /// <summary>  
        /// Works out how long it has been since the last background thread update.  
        /// </summary>  
        GameTime GetGameTime(ref long lastTime)  
        {  
            long currentTime = Stopwatch.GetTimestamp();  
            long elapsedTicks = currentTime - lastTime;  
            lastTime = currentTime;  
 
            TimeSpan elapsedTime = TimeSpan.FromTicks(elapsedTicks *  
                                                      TimeSpan.TicksPerSecond /  
                                                      Stopwatch.Frequency);  
 
            return new GameTime(loadStartTime.TotalGameTime + elapsedTime, elapsedTime);  
        }  
 
 
        /// <summary>  
        /// Calls directly into our Draw method from the background worker thread,  
        /// so as to update the load animation in parallel with the actual loading.  
        /// </summary>  
        void DrawLoadAnimationNOUSE(GameTime gameTime)  
        {  
            if ((ScreenManager.GraphicsDevice == null) || ScreenManager.GraphicsDevice.IsDisposed)  
                return;
            
                        
        if (background == null)
        {
            background = new RenderTarget2D(ScreenManager.GraphicsDevice, IW, IH, false,
                SurfaceFormat.Color, DepthFormat.Depth24);
        }
        try 
            {


                ScreenManager.GraphicsDevice.SetRenderTargets(background);
                
            //    ScreenManager.GraphicsDevice.Clear(Color.White);

                // Draw the loading screen.  
                  InternalDraw(gameTime);

                // If we have a message display component, we want to display  
                // that over the top of the loading screen, too.  
                //if (messageDisplay != null)  
                //{  
                //    messageDisplay.Update(gameTime);  
                //    messageDisplay.Draw(gameTime);  
                //} 

                ScreenManager.GraphicsDevice.SetRenderTargets(null);
                ScreenManager.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Opaque);
                Rectangle rect = new Rectangle(0, 0, 
                ScreenManager.graphics.PreferredBackBufferWidth,
                ScreenManager.graphics.PreferredBackBufferHeight);
                ScreenManager.spriteBatch.Draw(background, rect, Color.White);
                ScreenManager.spriteBatch.End();
                ScreenManager.GraphicsDevice.Present();
            }  
            catch (Exception ex)
            {
                // If anything went wrong (for instance the graphics device was lost  
                // or reset) we don't have any good way to recover while running on a  
                // background thread. Setting the device to null will stop us from  
                // rendering, so the main game can deal with the problem later on.  
                //ScreenManager.GraphicsDevice = null;  
                Console.WriteLine("DrawLoadAnimation:" + ex.Message + ex.StackTrace);
            } 
            
        }

        public override void Draw(GameTime gameTime)
        {

            try
            { //   DrawLoadAnimation(gameTime);
               InternalDraw(gameTime);
                   }  
            catch (Exception ex)
            {
                // If anything went wrong (for instance the graphics device was lost  
                // or reset) we don't have any good way to recover while running on a  
                // background thread. Setting the device to null will stop us from  
                // rendering, so the main game can deal with the problem later on.  
                //ScreenManager.GraphicsDevice = null;  
                Console.WriteLine("Draw Loading:" + ex.Message + ex.StackTrace);
            }
         //   base.Draw(gameTime);
        }
        public void InternalDraw(GameTime gameTime)
        {
            try
            {
                if ((ScreenManager.GraphicsDevice == null) || ScreenManager.GraphicsDevice.IsDisposed)
                    return;


                /// <summary>  
                /// Draws the loading screen.  
                /// </summary>  

                //ScreenManager.GraphicsDevice.Clear(Color.Black);

                // If we are the only active screen, that means all the previous screens  
                // must have finished transitioning off. We check for this in the Draw  
                // method, rather than in Update, because it isn't enough just for the  
                // screens to be gone: in order for the transition to look good we must  
                // have actually drawn a frame without them before we perform the load.  
                if ((ScreenState == ScreenState.Active) &&  
                (ScreenManager.GetScreens().Length == 1))  
            {  
                otherScreensAreGone = true;  
            }  
 
            // The gameplay screen takes a while to load, so we display a loading  
            // message while that is going on, but the menus load very quickly, and  
            // it would look silly if we flashed this up for just a fraction of a  
            // second while returning from the game to the menus. This parameter  
            // tells us how long the loading is going to take, so we know whether  
            // to bother drawing the message.  
            if (loadingIsSlow)  
            {  
                SpriteBatch spriteBatch = ScreenManager.SpriteBatch;                  
                Color color = Color.White * alpha;
                    // Draw the text.  
                    spriteBatch.Begin(SpriteSortMode.Immediate);
                Rectangle rect = new Rectangle(0, 0, ScreenManager.InternalWidth, ScreenManager.InternalHeight);
                spriteBatch.Draw(backgroundTexture, rect, Color.White);
              
                    spriteBatch.Draw(loadingTexture, new Vector2(420, 50), color);
                spriteBatch.End();  
            }
            }
            catch (Exception ex)
            {
                // If anything went wrong (for instance the graphics device was lost  
                // or reset) we don't have any good way to recover while running on a  
                // background thread. Setting the device to null will stop us from  
                // rendering, so the main game can deal with the problem later on.  
                //ScreenManager.GraphicsDevice = null;  
                Console.WriteLine("Internal Draw:" + ex.Message + ex.StackTrace);
            }
        }
        #endregion  
    }
}


