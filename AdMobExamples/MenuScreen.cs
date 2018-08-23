#region File Description
//-----------------------------------------------------------------------------
// MenuScreen.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Input.Touch;
#endregion

namespace RenderEngine.Screens
{
   
    public class SelectionData
    {
        public int id = -1;
        public Vector2 pos = Vector2.Zero;

        public SelectionData(int id,Vector2 pos)
        {
            this.id = id;
            this.pos = pos;

        }
    }
    /// <summary>
    /// Base class for screens that contain a menu of options. The user can
    /// move up and down to select an entry, or cancel to back out of the screen.
    /// </summary>
    public abstract class MenuScreen : GameScreen
    {
        #region Fields

       public List<MenuEntry> menuEntries = new List<MenuEntry>();
       public List<Rectangle> selEntries = new List<Rectangle>();
        public bool[] selCheck = new bool[10];
       public EventHandler<SelectionData>[] selSelection = new EventHandler<SelectionData>[10];
       public int selectedEntry = 0;
       public string menuTitle;
       public Texture2D GraphicsTexture;
       
        private float callouttime = 2000;
        private Random rnd = new Random(199);
        TouchCollection touchPrevState;
        public float VStart=75f;
        public bool drag = false;
        #endregion

        #region Properties

    

        /// <summary>
        /// Gets the list of menu entries, so derived classes can add
        /// or change the menu contents.
        /// </summary>
        protected IList<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }


        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public MenuScreen(string menuTitle, Texture2D GraphicsTexture)
        {
            this.menuTitle = menuTitle;
            this.GraphicsTexture = GraphicsTexture;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
           
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public override void HandleInput(InputState input)
        {

          

        try
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    if (IsPopup )
                            ExitScreen();
                }
                if (input.mouseprevState.LeftButton==ButtonState.Pressed && (input.mouseState.LeftButton == ButtonState.Released || drag))
            {
                Vector2 trasform = new Vector2((float)ScreenManager.InternalWidth / (float)ScreenManager.GraphicsDevice.Viewport.Width,
                (float)ScreenManager.InternalHeight / (float)ScreenManager.GraphicsDevice.Viewport.Height);
                Vector2 orgpos = new Vector2(input.mouseState.X, input.mouseState.Y);
                Vector2 pos = orgpos*trasform;
                Vector2 position = new Vector2(0f, 175f);

                    // Custom Selection
                    for (int i = 0; i < selEntries.Count; i++)
                    {
                        selCheck[i] = false;
                        if (selEntries[i].Contains(pos))
                        {
                            if (!drag)
                             ScreenManager.Iclick.Play();
                            if (selSelection[i]!=null)
                            {
                                selSelection[i](this, new SelectionData(i,pos));
                            }
                            selCheck[i] = true;
                          
                        }
                        
                    }
                    


                    // update each menu entry's location in turn
                    for (int i = 0; i < menuEntries.Count; i++)
                     {
                    Vector2 Position = menuEntries[i].Position;
                  //  Console.WriteLine(pos+":" + menuEntries[i].Position + ":" + menuEntries[i].GetWidth(this) + ":" + menuEntries[i].GetHeight(this));
                   
                    if (pos.X>= Position.X &&
                          pos.Y >= Position.Y &&
                         Position.X + menuEntries[i].GetWidth(this) >= pos.X &&
                         Position.Y + menuEntries[i].GetHeight(this) >= pos.Y)

                    {

                        selectedEntry = i;
                        ScreenManager.Iclick.Play();
                            if (menuEntries[i]!=null)
                                 menuEntries[i].OnSelectEntry((PlayerIndex)0);
                        break;
                    }
                }

            }
    

            
            foreach (var touch in input.touchState)
            {
           if ((touch.State == TouchLocationState.Moved &&  (touchPrevState.Count == 0 || drag)) || touch.State == TouchLocationState.Pressed)
                {
                        Vector2 trasform = new Vector2((float)ScreenManager.InternalWidth / (float)ScreenManager.GraphicsDevice.Viewport.Width,
                         (float)ScreenManager.InternalHeight / (float)ScreenManager.GraphicsDevice.Viewport.Height); Vector2 orgpos = touch.Position;;
                Vector2 pos = orgpos*trasform;
                Vector2 position = new Vector2(0f, 175f);

                        // Custom Selection
                        for (int i = 0; i < selEntries.Count; i++)
                        {
                            selCheck[i] = false;
                            if (selEntries[i].Contains(pos))
                            {
                                if(!drag)
                                ScreenManager.Iclick.Play();
                                if (selSelection[i] != null)
                                {
                                    selSelection[i](this, new SelectionData(i, pos));
                                }
                                selCheck[i] = true;
                                
                            }

                        }


                        // update each menu entry's location in turn
                        for (int i = 0; i < menuEntries.Count; i++)
                {
                    Vector2 Position = menuEntries[i].Position;
                   //  Console.WriteLine(pos+":" + menuEntries[i].Position + ":" + menuEntries[i].GetWidth(this) + ":" + menuEntries[i].GetHeight(this));
                  

                        if (pos.X>= Position.X &&
                          pos.Y >= Position.Y &&
                         Position.X + menuEntries[i].GetWidth(this) >= pos.X &&
                         Position.Y + menuEntries[i].GetHeight(this) >= pos.Y)

                    {

                        selectedEntry = i;
                                
                        ScreenManager.Iclick.Play();
                        menuEntries[i].OnSelectEntry((PlayerIndex)0);
                        break;
                    }
                }

            }
            }

             touchPrevState = input.touchState;



            // Move to the previous menu entry?
                if (input.IsMenuUp(ControllingPlayer))
            {
                selectedEntry--;
                ScreenManager.Iclick.Play();
                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (input.IsMenuDown(ControllingPlayer))
            {
                selectedEntry++;
                ScreenManager.Iclick.Play();
                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            // Accept or cancel the menu? We pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                ScreenManager.Ivalidate.Play();
                OnSelectEntry(selectedEntry, playerIndex);
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                ScreenManager.Iback.Play();
                OnCancel(playerIndex);
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine("MenuScreen->HandleInput:" + ex.Message + ex.StackTrace);
            }

        }


        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            if (menuEntries.Count>0)
            menuEntries[entryIndex].OnSelectEntry(playerIndex);
        }


        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        

        /// <summary>
        /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Allows the screen the chance to position the menu entries. By default
        /// all menu entries are lined up in a vertical list, centered on the screen.
        /// </summary>
        protected virtual void UpdateMenuEntryLocations(int XDiv)
        {
            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            Vector2 position = new Vector2(0f, VStart);
           
            // update each menu entry's location in turn
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];
                
                
                // each entry is to be centered horizontally
                position.X = ScreenManager.InternalWidth / XDiv - menuEntry.GetWidth(this) / 2;

                if (ScreenState == ScreenState.TransitionOn)
                    position.X -= transitionOffset * 256;
                else
                    position.X += transitionOffset * 512;

                // set the entry's position
                menuEntry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += menuEntry.GetHeight(this);
            }
        }


        /// <summary>
        /// Updates the menu.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Update each nested MenuEntry object.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Update(this, isSelected, gameTime);
            }

            callouttime -= gameTime.ElapsedGameTime.Milliseconds;
            if (callouttime<0)
            {
                for (int i = 0; i < menuEntries.Count; i++)
                {
                    
                    menuEntries[i].CallOut = false;
                }
                if (menuEntries.Count > 0)
                {
                    int s = rnd.Next(0, menuEntries.Count - 1);
                    menuEntries[s].CallOut = true;
                }
                callouttime = 2000;
            }
    }


        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            
            // make sure our entries are in the right place before we draw them
            UpdateMenuEntryLocations(2);

            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

         
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend);

            // Draw each menu entry in turn.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];

                bool isSelected = IsActive && (i == selectedEntry);

                menuEntry.Draw(this, isSelected, gameTime);
            }

            // Draw each menu call out entry in turn.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];

               menuEntry.CallOutDraw(this, false, gameTime);
            }



            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            //Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            Vector2 titlePosition = new Vector2(ScreenManager.InternalWidth / 2, 40);
            Vector2 Shadow = new Vector2(3, 3);
            Vector2 titleOrigin;
            if (GraphicsTexture != null)
                titleOrigin = titlePosition - new Vector2(ScreenManager.InternalWidth / 2, ScreenManager.InternalHeight / 2);
            else
                titleOrigin = font.MeasureString(menuTitle) / 2;
            Color titleColor = Color.White * TransitionAlpha;
            float titleScale = 1.2f;

            titlePosition.Y -= transitionOffset * 100;

            if (GraphicsTexture != null)
                spriteBatch.Draw(GraphicsTexture, titleOrigin, Color.White);
            else
            {
                spriteBatch.DrawString(font, menuTitle, titlePosition-Shadow, Color.Gray, 0,
                                    titleOrigin, titleScale, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, menuTitle, titlePosition + Shadow, Color.Silver, 0,
                                    titleOrigin, titleScale, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0,
                                    titleOrigin, titleScale, SpriteEffects.None, 0);
     
            }
            spriteBatch.End();
            
        



        }


        #endregion
    }
}
