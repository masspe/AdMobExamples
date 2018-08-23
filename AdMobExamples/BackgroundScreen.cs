#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
#endregion

namespace RenderEngine.Screens
{
    /// <summary>
    /// The background screen sits behind all the other menu screens.
    /// It draws a background image that remains fixed in place regardless
    /// of whatever transitions the screens on top of it may be doing.
    /// </summary>
    public class BackgroundScreen : GameScreen
    {
        #region Fields

        ContentManager content;
      
      
        Viewport viewport;
        Vector2 V1, V2,V3;
        Rectangle fullscreen;
        float elapsed, circle;
        SpriteBatch spriteBatch;
        private Texture2D backgroundTexture;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void LoadContent(LoadingScreen loadscreen)
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            backgroundTexture = content.Load<Texture2D>("Screens/bg");
               spriteBatch = ScreenManager.SpriteBatch;
          
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
          
       
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            // The time since Update was called last.
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds/20;
            // TODO: Add your game logic here.
            

        }


        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
       
          
            fullscreen = new Rectangle(0, 0, ScreenManager.InternalWidth, ScreenManager.InternalHeight);
            V1 = new Vector2(fullscreen.Width / 2, fullscreen.Height / 2);
           
            
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture,      
             fullscreen,Color.White);
      
          
            spriteBatch.End();
             
        }


        #endregion
    }
}
