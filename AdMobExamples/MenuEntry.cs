#region File Description
//-----------------------------------------------------------------------------
// MenuEntry.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace RenderEngine.Screens
{
    /// <summary>
    /// Helper class represents a single entry in a MenuScreen. By default this
    /// just draws the entry text string, but it can be customized to display menu
    /// entries in different ways. This also provides an event that will be raised
    /// when the menu entry is selected.
    /// </summary>
   public class MenuEntry
    {
        #region Fields

        /// <summary>
        /// The text rendered for this entry.
        /// </summary>
        string text, callouttext;
        bool callout;
        public string CustomValue1;
        public string CustomValue2;
        public string CustomValue3;
        public Texture2D GraphicText;
        /// <summary>
        /// 
        /// Tracks a fading selection effect on the entry.
        /// </summary>
        /// <remarks>
        /// The entries transition out of the selection effect when they are deselected.
        /// </remarks>
        float selectionFade;

        /// <summary>
        /// The position at which the entry is drawn. This is set by the MenuScreen
        /// each frame in Update.
        /// </summary>
        Vector2 position;

        #endregion

        #region Properties


        /// <summary>
        /// Gets or sets the text of this menu entry.
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string CallOutText
        {
            get { return callouttext; }
            set { callouttext = value; }
        }

        public bool CallOut
        {
            get { return callout; }
            set { callout = value; }
        }


        /// <summary>
        /// Gets or sets the position at which to draw this menu entry.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        
        #endregion

        #region Events


        /// <summary>
        /// Event raised when the menu entry is selected.
        /// </summary>
        public event EventHandler<PlayerIndexEventArgs> Selected;


        /// <summary>
        /// Method for raising the Selected event.
        /// </summary>
        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            if (Selected != null)
                Selected(this, new PlayerIndexEventArgs(playerIndex));
        }


        #endregion

        #region Initialization


        public MenuEntry(string text): this (text,null) {}

        
        /// <summary>
        /// Constructs a new menu entry with the specified text.
        /// </summary>
        public MenuEntry(string text,Texture2D graphicsText)
        {
            this.text = text;
            this.GraphicText = graphicsText;
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the menu entry.
        /// </summary>
        public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // there is no such thing as a selected item on Windows Phone, so we always
            // force isSelected to be false

            isSelected = false;


          
            // When the menu selection changes, entries gradually fade between
            // their selected and deselected appearance, rather than instantly
            // popping to the new state.
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
        }

              /// <summary>
        /// Draws the menu entry. This can be overridden to customize the appearance.
        /// </summary>
        public virtual void Draw(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // there is no such thing as a selected item on Windows Phone, so we always
            // force isSelected to be false
            string DrawText=AsciiR(text);

            float scale = 1;
           

            // Draw text, centered on the middle of each line.
            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

           
            
            // button
           // spriteBatch.Draw(CreateRectangle(spriteBatch.GraphicsDevice, 200, 50, Color.Red), position, Color.Transparent);
            if (GraphicText != null)
            {
                if (isSelected)
                    spriteBatch.Draw(GraphicText, position , Color.LightGray);
                else
                    spriteBatch.Draw(GraphicText, position , Color.White);
            }
            else
            {
                Vector2 S = font.MeasureString(DrawText);
                Vector2 Shadow = new Vector2(3, 3);
                Rectangle rect = new Rectangle((int)position.X, (int)position.Y, screenManager.blankButton.Width, screenManager.blankButton.Height);
               if (S.X > screenManager.blankButton.Width)
                { 
                        rect.Width =(int)( S.X + 20);
                  //  rect.X = (int)(screenManager.InternalWidth - rect.Width) / 2;
                }
                Vector2 textPos = new Vector2((rect.Width - S.X) / 2, (rect.Height) / 2) + position;

                if (isSelected)
                {
                    spriteBatch.Draw(screenManager.blankButton, rect , Color.LightGray);
                             
               
                }
                else
                {
                   
                    spriteBatch.Draw(screenManager.blankButton, rect, Color.White);
                }
             
                spriteBatch.DrawString(font,DrawText , textPos, Color.Orange, 0,
                                       origin, scale, SpriteEffects.None, 0);


               /* if (DrawText.IndexOf("Music Volume") > -1)
                {
                    Rectangle outRect = new Rectangle((int)(575 + scale*100), (int)(220 + move.Y),
                        (int)(150 * screenManager.Volume), 20);

                    spriteBatch.Draw(screenManager.blueBar, outRect, Color.White);
                }*/
            } // else

            
        }

        public void CallOutDraw(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            

            if (CallOut && CallOutText!=null)
            {
                ScreenManager screenManager = screen.ScreenManager;
                SpriteBatch spriteBatch = screenManager.SpriteBatch;
                SpriteFont font = screenManager.Font;

                Vector2 origin = new Vector2(0, font.LineSpacing / 2);

                Vector2 S = font.MeasureString(CallOutText);
                Vector2 cpos = position- new Vector2(20+S.X/2,0);
                Vector2 spos = position - new Vector2(25+S.X / 2, 25);
                
                spriteBatch.Draw(screenManager.callout, spos, Color.White);                
                spriteBatch.DrawString(font, CallOutText, cpos, Color.White,0,Vector2.Zero,0.7f, 
                    SpriteEffects.None, 0);
            }

        }


        /// <summary>
        /// Queries how much space this menu entry requires.
        /// </summary>
        public virtual int GetHeight(MenuScreen screen)
        {
            if (GraphicText != null)
                return GraphicText.Height;
            else
                return screen.ScreenManager.blankButton.Height;
            //   return screen.ScreenManager.Font.LineSpacing;
        }

        string AsciiR(string S)
        {
            if (S == null) return "";
            char[] ss = S.ToCharArray();
            string result = "";
            for (int i = 0; i < S.Length; i++)
            {
                if (ss[i] > 31 && ss[i] < 255)
                    result += ss[i];
            }
            return result;
        }

        /// <summary>
        /// Queries how wide the entry is, used for centering on the screen.
        /// </summary>
        public virtual int GetWidth(MenuScreen screen)
        {
            if (GraphicText != null)
                return GraphicText.Width;
            else
            {
                Vector2 S = screen.ScreenManager.Font.MeasureString(Text);
                if (S.X > screen.ScreenManager.blankButton.Width)
                    return (int) S.X;
                else
                    return screen.ScreenManager.blankButton.Width;
            }
               
        }


        #endregion
    }
}
