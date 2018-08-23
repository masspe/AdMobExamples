#region File Description
//-----------------------------------------------------------------------------
// MessageBoxScreen.cs
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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

#endregion

namespace RenderEngine.Screens
{
    /// <summary>
    /// A popup message box screen, used to display "are you sure?"
    /// confirmation messages.
    /// </summary>
   public class MessageBoxScreen : GameScreen
    {
        #region Fields

        string message;
        Texture2D gradientTexture,ok,cancel;
       
        Vector2 cancpos, okpos;
        Vector2 viewportSize;
        Vector2 textSize;
        Vector2 textPosition;
        Rectangle backgroundRectangle;
        Rectangle textrect;
        // The background includes a border somewhat larger than the text itself.
        const int hPad = 32;
        const int vPad = 16;

        #endregion

        #region Events

        public event EventHandler<PlayerIndexEventArgs> Accepted;
        public event EventHandler<PlayerIndexEventArgs> Cancelled;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor automatically includes the standard "A=ok, B=cancel"
        /// usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message)
            : this(message, true)
        { }


        /// <summary>
        /// Constructor lets the caller specify whether to include the standard
        /// "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message, bool includeUsageText)
        {
            
            if (includeUsageText)
                this.message = message ;
            else
                this.message = message;

            
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public static void ShowMessage(ScreenManager Manager, String msg)
        {
            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(msg);            
            Manager.AddScreen(confirmExitMessageBox, null, null);
        }

        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent(LoadingScreen loadscreen)
        {
            ContentManager content = ScreenManager.Game.Content;

            gradientTexture = content.Load<Texture2D>("Screens/gradient");
            ok = content.Load<Texture2D>("Screens/OK");
            cancel = content.Load<Texture2D>("Screens/cancel");
        }


        #endregion

        #region Handle Input

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            SpriteFont font = ScreenManager.Font;
            // Center the message text in the viewport.
            viewportSize = new Vector2(ScreenManager.InternalWidth, ScreenManager.InternalHeight);
             textSize = font.MeasureString(message);
             textPosition = (viewportSize - textSize) / 2;

          
            backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);
            textrect = new Rectangle(
                (int)((viewportSize.X - gradientTexture.Width) / 2),
                (int)((viewportSize.Y - gradientTexture.Height) / 2),
                gradientTexture.Width,
                gradientTexture.Height);
            if (gradientTexture.Width < textSize.X + hPad)
            {
                textrect.Width = (int)(textSize.X + hPad);
                textrect.X -= (int)(textSize.X + hPad - gradientTexture.Width) / 2;
            }
            okpos = new Vector2(textrect.X+30,
                                    textrect.Y + gradientTexture.Height - ok.Height);

            cancpos = new Vector2(textrect.X + 30 + gradientTexture.Width / 2,
                                    textrect.Y + gradientTexture.Height - cancel.Height);

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex = new PlayerIndex();

            try
            {
                
                if (input.mouseprevState.LeftButton == ButtonState.Pressed && input.mouseState.LeftButton == ButtonState.Released)
                {
                    Vector2 trasform = new Vector2((float)ScreenManager.InternalWidth / (float)ScreenManager.GraphicsDevice.PresentationParameters.BackBufferWidth,
                        (float)ScreenManager.InternalHeight / (float)ScreenManager.GraphicsDevice.PresentationParameters.BackBufferHeight);
                    Vector2 orgpos = new Vector2(input.mouseState.X, input.mouseState.Y);
                    Vector2 pos = orgpos * trasform;
                    Vector2 position = new Vector2(0f, 175f);

                    // update each menu entry's location in turn

                    if (pos.X >= okpos.X &&
                          pos.Y >= okpos.Y &&
                         pos.X <= okpos.X + ok.Width &&
                         pos.Y <= okpos.Y + ok.Height)

                    {
                        if (Accepted != null)
                            Accepted(this, new PlayerIndexEventArgs(playerIndex));

                        ExitScreen();

                    }
                    if (pos.X >= cancpos.X &&
                             pos.Y >= cancpos.Y &&
                            pos.X <= cancpos.X + cancel.Width &&
                            pos.Y <= cancpos.Y + cancel.Height)

                    {

                        ExitScreen();

                    }



                }
                

                
                foreach (var touch in input.touchState)
                {
                    if (touch.State == TouchLocationState.Moved && input.touchprevState.Count == 0 )
                    {
                        Vector2 trasform = new Vector2((float)ScreenManager.InternalWidth / (float)ScreenManager.GraphicsDevice.PresentationParameters.BackBufferWidth,
                            (float)ScreenManager.InternalHeight / (float)ScreenManager.GraphicsDevice.PresentationParameters.BackBufferHeight);
                        Vector2 orgpos = touch.Position; ;
                        Vector2 pos = orgpos * trasform;
                        Vector2 position = new Vector2(0f, 175f);

                        if (pos.X >= okpos.X &&
                               pos.Y >= okpos.Y &&
                              pos.X <= okpos.X + ok.Width &&
                              pos.Y <= okpos.Y + ok.Height)

                        {
                            if (Accepted != null)
                                Accepted(this, new PlayerIndexEventArgs(playerIndex));

                            ExitScreen();

                        }
                        if (pos.X >= cancpos.X &&
                              pos.Y >= cancpos.Y &&
                             pos.X <= cancpos.X + cancel.Width &&
                             pos.Y <= cancpos.Y + cancel.Height)

                        {
                           
                            ExitScreen();

                        }
                    }
                }


                // We pass in our ControllingPlayer, which may either be null (to
                // accept input from any player) or a specific index. If we pass a null
                // controlling player, the InputState helper returns to us which player
                // actually provided the input. We pass that through to our Accepted and
                // Cancelled events, so they can tell which player triggered them.
                if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
                {
                    // Raise the accepted event, then exit the message box.
                    if (Accepted != null)
                        Accepted(this, new PlayerIndexEventArgs(playerIndex));

                    ExitScreen();
                }
                else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
                {
                    // Raise the cancelled event, then exit the message box.
                    if (Cancelled != null)
                        Cancelled(this, new PlayerIndexEventArgs(playerIndex));

                    ExitScreen();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MessageBox:" + ex.Message + ex.StackTrace);
            }
        }


        #endregion

        #region Draw


        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

          
            // Fade the popup alpha during transitions.
            Color color = new Color(220,255,220) * TransitionAlpha;

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(gradientTexture, textrect, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, message, textPosition, Color.Black);

            if (Accepted != null)
                spriteBatch.Draw(ok, okpos, color);
            spriteBatch.Draw(cancel, cancpos, color);

            spriteBatch.End();
        }


        #endregion
    }
}
