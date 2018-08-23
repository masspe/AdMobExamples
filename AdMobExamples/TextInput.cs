using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using System;

namespace RenderEngine.Screens
{
    enum InputStyle
    {
        modal,
        singleline,
    }
    internal class TextInput
    {
        private KeyboardState oldKeyState;
        private Keys oldKey;
        private float timer = 0;
         public OnScreenKeyboard keyboard;
        public string text = "Will you play with me?";
        private Texture2D drawtex;
        public string Title = "Personalize connect msg";
        public Rectangle rect;
        Rectangle backgroundrect, buttondrect;
        Color color;
        public event EventHandler<string> OnClick;
        Color backgroudcolor, fcolor;
        private ButtonState prevState;
        private bool _visible = false;
        private bool cursor = false;
        TouchCollection touchPrevState;
        public InputStyle inputSytle = InputStyle.modal;
        bool upper = false;
        public bool Visible {
            get { 
                return _visible; }
            set {
                keyboard.Enabled = value;
                _visible = value; }
        }

        public TextInput(GraphicsDevice device, OnScreenKeyboard keyboard, Rectangle rect, Color color)
        {
            this.keyboard = keyboard;
            this.rect = rect;
            this.color = color;
            drawtex = new Texture2D(device, 1, 1);

            Color[] data = new Color[1];
            data[0] = new Color(255, 255, 255, 180);
            drawtex.SetData<Color>(data);
            Vector2 size = keyboard.Font.MeasureString(Title);

            backgroundrect = new Rectangle(rect.X + 5, rect.Y + 5 + (int)size.Y, rect.Width - 10, (int)size.Y);
            buttondrect = new Rectangle(rect.X + (rect.Width / 2) - 5,
                rect.Y + 20 + (int)size.Y * 2,
                rect.Width / 2 - 20, (int)size.Y + 20);
           
            backgroudcolor = new Color(255, 255, 255, 180);
            fcolor = new Color(0, 0, 0, 180);
        }

        private void CheckSend(ScreenManager Manager)
        {
            try
            {
                if (inputSytle != InputStyle.modal)
                    return;
                

                    MouseState mouseState = Mouse.GetState();
                if (prevState == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    Vector2 trasform = new Vector2((float)Manager.InternalWidth / (float)Manager.GraphicsDevice.PresentationParameters.BackBufferWidth,
                        (float)Manager.InternalHeight / (float)Manager.GraphicsDevice.PresentationParameters.BackBufferHeight);
                    Vector2 orgpos = new Vector2(mouseState.X, mouseState.Y);
                    Vector2 pos = orgpos * trasform;
                    

                    // update each menu entry's location in turn

                    if (pos.X >= buttondrect.X &&
                          pos.Y >= buttondrect.Y &&
                         pos.X <= buttondrect.X + buttondrect.Width &&
                         pos.Y <= buttondrect.Y + buttondrect.Height)

                    {
                        if (OnClick != null)
                            OnClick(this, text);


                    }
                    
                }
                prevState = mouseState.LeftButton;

                TouchCollection touchState = TouchPanel.GetState();
                foreach (var touch in touchState)
                {
                    if ((touch.State == TouchLocationState.Moved  && touchPrevState.Count ==0) || touch.State == TouchLocationState.Pressed)
                    {
                        Vector2 trasform = new Vector2((float)Manager.InternalWidth / (float)Manager.GraphicsDevice.PresentationParameters.BackBufferWidth,
                            (float)Manager.InternalHeight / (float)Manager.GraphicsDevice.PresentationParameters.BackBufferHeight);
                        Vector2 orgpos = touch.Position; ;
                        Vector2 pos = orgpos * trasform;
                        Vector2 position = new Vector2(0f, 175f);
                        if (pos.X >= buttondrect.X &&
                                                pos.Y >= buttondrect.Y &&
                                               pos.X <= buttondrect.X + buttondrect.Width &&
                                               pos.Y <= buttondrect.Y + buttondrect.Height)

                        {
                            if (OnClick != null)
                                OnClick(this, text);


                        }
                    }
                }

                touchPrevState = touchState;
            }
            catch (Exception ex)
            {
                Console.WriteLine("OnClick:"+ex.Message+ex.StackTrace );
            }
         }
    

        public void GetInput(ScreenManager Manager,GameTime time)
        {
            if (!Visible)
                return;

            timer += time.ElapsedGameTime.Milliseconds;
            if (timer > 500)
            { 
                cursor = !cursor;
                timer = 0;
            }
            KeyboardState keyState = keyboard.GetState();

           
            // Get keyboard input in order to get the ret.
            foreach (Keys key in keyState.GetPressedKeys())
            {
                if (key == Keys.LeftShift)
                {
                    upper = !upper;
                }
                else
                 if (key == Keys.Back)
                    {
                        // Delete.
                        if (text.Length > 0)
                        {
                        text = text.Remove(text.Length - 1, 1);
                        }
                    }
                else if (key == Keys.OemQuestion)
                {
                    if (Manager.keyBoard.Font.MeasureString(text).X < rect.Width - 40)
                    {
                        text += "?";
                    }
                }
                else if (key == Keys.Enter)
                    {
                    // Submit. Update the highscores file and list.
                    if (OnClick != null)
                        OnClick(this,text);
                    }
                     else if (key == Keys.Space)
                        {
                    if ( Manager.keyBoard.Font.MeasureString(text).X < rect.Width - 40)
                    {
                        text += " ";
                    }
                }
                else 
                    {
                        // If there is room left, try and add a character.
                        string k = key.ToString();
                        if (k.Length == 1 && Manager.keyBoard.Font.MeasureString(text).X<rect.Width-20)
                        {
                        if (upper)
                             text += key.ToString();
                        else
                            text += key.ToString().ToLower();
                    }
                    }
                    oldKey = key;
                    
                
              
            }
            oldKeyState = keyState;
            CheckSend(Manager);
        }

        public void Draw(SpriteBatch sprite)
        {
            if (!Visible)
                return;
            try
            {
                string outtext = text;
                if (cursor)
                    outtext += "|";
                switch (inputSytle)
                {
                    case InputStyle.modal:
                sprite.Draw(drawtex, rect, fcolor);
                sprite.Draw(drawtex, backgroundrect, backgroudcolor);
                sprite.Draw(drawtex, buttondrect, fcolor);          
                sprite.DrawString(keyboard.Font, Title, new Vector2(rect.X+5, rect.Y+5), backgroudcolor);
                sprite.DrawString(keyboard.Font, outtext, new Vector2(backgroundrect.X, backgroundrect.Y), color);
                sprite.DrawString(keyboard.Font, "Send", new Vector2(buttondrect.X+100, buttondrect.Y), backgroudcolor);
                        break;
                    case InputStyle.singleline:
                        sprite.Draw(drawtex, rect, backgroudcolor);
                        sprite.DrawString(keyboard.Font, outtext, new Vector2(rect.X, rect.Y), color);
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("TextInput:" + ex.Message + ex.StackTrace);
            }
        }
            
    }
}