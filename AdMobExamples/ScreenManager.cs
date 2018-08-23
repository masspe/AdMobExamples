#region File Description
//-----------------------------------------------------------------------------
// ScreenManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Android.Gms.Ads;
using Android.Widget;
using Android.Accounts;
using AdModExamples;



// using Microsoft.Xna.Framework.Net;



#endregion

namespace RenderEngine.Screens
{
    /// <summary>
    /// The screen manager is a component which manages one or more GameScreen
    /// instances. It maintains a stack of screens, calls their Update and Draw
    /// methods at the appropriate times, and automatically routes input to the
    /// topmost active screen.
    /// </summary>
    /// 

    public class ScreenManager : DrawableGameComponent
    {
        #region Fields

        
        public List<GameScreen> screens = new List<GameScreen>();
        public List<GameScreen> screensToUpdate = new List<GameScreen>();
     
        public SoundEffectInstance Iclick,Ivalidate, Iback;
        public  InputState input = new InputState();

        public SpriteBatch spriteBatch;
        public SpriteFont font;
        public Texture2D blankTexture, rectTex;
        public string UserName = "";
        private string DeviceID;
        public string LocalUser = "";
        public string RemoteUser = "";
        public int onlinePriority = 0;
        public string ExitMessage = "Are you sure\n you want to exit?\n\n\n";
        bool isInitialized;
        bool traceEnabled;
        public Texture2D blueBar,callout;
        public Texture2D blankButton;


        public ContentManager content;
     
        public int Level = 0;

        public int points = 0;
    
        private GameScreen screen;
        private bool otherScreenHasFocus;
        private bool coveredByOtherScreen;
         
      
        public GraphicsDeviceManager graphics;
        public RenderTarget2D background;
        public int InternalWidth = 720;
        public int InternalHeight = 1280;
        public Boolean GamePlay = false;
     
       
        public OnScreenKeyboard keyBoard;
        public string AdsID = "ca-app-pub-9033799094858050/4668237472";
#if ANDROID
        public AdView adView;
        public LinearLayout linearLayout;       
        public InterstitialAd FinalAd;
        public Android.Content.Context context;
        internal Activity1 activity;
#endif



        #endregion

        #region Properties


        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

     
        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
        }


        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }


        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(Microsoft.Xna.Framework.Game game,GraphicsDeviceManager graphics):base(game)        
        {
            this.graphics = graphics;
            
            // we must set EnabledGestures before we can query for them, but
            // we don't assume the game wants to read them.
            Level = 0;
           
            UserName = "";
        
            keyBoard = new OnScreenKeyboard(this);
            keyBoard.Enabled = false;
           

        }


        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            isInitialized = true;
            keyBoard.Initialize();
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            try
            { 
            // Load content belonging to the screen manager.
            content = Game.Content;

                keyBoard.LoadContent();

            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("Fonts/screenfont");
            
            blankTexture = content.Load<Texture2D>("Screens/blank");          
            rectTex = content.Load<Texture2D>("Screens/box");
            blueBar = content.Load<Texture2D>("Screens/bluebar");
            blankButton= content.Load<Texture2D>("Screens/button");
           
            callout = content.Load<Texture2D>("Screens/callout");
            Iclick = content.Load<SoundEffect>("Sounds/Click01").CreateInstance();
        
            Iclick.IsLooped = false;
            Iclick.Volume = 0.5f;
            Ivalidate = content.Load<SoundEffect>("Sounds/menu-validate").CreateInstance();
     
            Ivalidate.IsLooped = false;
            Ivalidate.Volume = 0.5f;
            Iback = content.Load<SoundEffect>("Sounds/menu-back").CreateInstance();
            Iback.IsLooped = false;
            Iback.Volume = 0.5f;
            // Tell each of the screens to load their content.
            for (int i = 0; i < screens.Count;i++ )
            {
                screens[i].LoadContent(null);
            }


                UserName = getusername();
                DeviceID = getID();

            }
            catch (Exception ex)
            {
                Console.WriteLine("ScreenManager Load:" + ex.Message + ex.StackTrace);
            }

        }

     
      
        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            for (int i = 0; i < screens.Count;i++ )
            {
                screens[i].UnloadContent();
            }
        }
        public string getID()
        {
            string id = Guid.NewGuid().ToString();
#if ANDROID
            id = Android.OS.Build.CpuAbi + Android.OS.Build.Display + Android.OS.Build.Hardware + Android.OS.Build.Serial;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(id);
            return System.Convert.ToBase64String(plainTextBytes);
#endif
            return id;

        }


        public String getusername()
        {
#if ANDROID
            // --------------------------------
            AccountManager manager = AccountManager.Get(context);
            Account[] accounts = manager.GetAccounts();

            for (int l = 0; l < accounts.Length; l++)
            {
                if (accounts[l].Type == "com.google")
                    return accounts[l].Name;
            }

            return Environment.UserName;
#endif
            return Environment.UserName;
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Allows each screen to run Console.ic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {

         
            // Read the keyboard and gamepad.
           input.Update();

            
            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
           screensToUpdate.Clear();

            for (int i = 0; i < screens.Count; i++)
                screensToUpdate.Add(screens[i]);
            
            otherScreenHasFocus = !Game.IsActive;
            coveredByOtherScreen = false;
            
            // Loop as long as there are screens waiting to be updated.
          while (screensToUpdate.Count > 0)
           // for (int i = screens.Count - 1;i>=0 ; i--)
            {
                // Pop the topmost screen off the waiting list.
                //screen = screens[i];  
               screen = screensToUpdate[screensToUpdate.Count - 1];
                
               screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }
            keyBoard.Update(gameTime);
            //  Console.WriteLine(GC.GetTotalMemory(true));    
#if WINDOWS
            // Print debug trace?
            if (traceEnabled)
                TraceScreens();
#endif
        }


        /// <summary> 
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            for (int i = 0; i < screens.Count;i++ )
                screenNames.Add(screens[i].GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }


        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            try
        {
            if (background == null)
            {
                background = new RenderTarget2D(graphics.GraphicsDevice, InternalWidth, InternalHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);


            }

            if (!GamePlay)
            {
                graphics.GraphicsDevice.SetRenderTarget(background);
                graphics.GraphicsDevice.Clear(Color.Black);
            }
            for (int i = 0; i < screens.Count;i++ )
            {
                if (screens[i].ScreenState == ScreenState.Hidden)
                    continue;
                    screens[i].Draw(gameTime);
             
             
            }
                keyBoard.Draw(gameTime);
            if (!GamePlay)
            {
                graphics.GraphicsDevice.SetRenderTarget(null);

                if (background != null)
                {


                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend );
                    spriteBatch.Draw((Texture2D)background,
                         new Rectangle(0, 0,
                         graphics.PreferredBackBufferWidth,
                         graphics.PreferredBackBufferHeight),
                         Color.White);

                    spriteBatch.End();
                }
            }
          }
            catch(Exception ex)
            {
                Console.WriteLine("Screenmanager draw:" + ex.Message + ex.StackTrace);
            }
        }


        #endregion

        #region Public Methods


        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer, LoadingScreen loadscreen)
        {
            if (controllingPlayer == null)
                controllingPlayer = new PlayerIndex();            

            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.LoadContent(loadscreen);
            }

            screens.Add(screen);

            
        }


        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            screen.UnloadContent();
            screensToUpdate.Remove(screen);
            screens.Remove(screen);            
            screen = null;


        }


        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }

        public GameScreen GetScreensByName(String Name)
        {
   
            for (int i=0;i<screens.Count;i++)
            {
                if (screens[i].Name == Name)
                    return screens[i];
            }
            return null;
        }


        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(float alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            try
            {

                spriteBatch.Begin();

                spriteBatch.Draw(blankTexture,
                                 new Rectangle(0, 0, viewport.Width, viewport.Height),
                                 Color.Black * alpha);

                spriteBatch.End();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("FadeBackBufferToBlack " + ex.Message);
            }
        }


     
      

    

     
      
       



     
        #endregion
    }
}
