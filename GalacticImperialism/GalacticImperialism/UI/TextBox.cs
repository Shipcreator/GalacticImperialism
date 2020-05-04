using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GalacticImperialism
{
    class TextBox
    {
        public Rectangle outlineRect;
        public Rectangle boxRect;
        public Rectangle cursorRect;

        public int textBoxOutlineWidth;
        int cursorTimer;
        int backTimer;
        int textBoxMaxCharacters;

        Color textBoxColor;
        Color textBoxOutlineColor;
        Color textBoxTextColor;
        Color textBoxCursorColor;

        Texture2D whiteTexture;

        GraphicsDevice Device;

        SpriteFont textBoxFont;

        public string text;
        string updateText;
        string drawText;

        bool textBoxSelected;
        bool wasTextBoxSelected;
        public bool acceptsNumbers;     //Set this to false if you don't want the text box to accept numbers.
        public bool acceptsLetters;     //Set this to false if you don't want the text box to accept letters.

        public TextBox(Rectangle boxRectangle, int outlineWidth, int maxCharacters, Color boxColor, Color outlineColor, Color textColor, Color cursorColor, GraphicsDevice GraphicsDevice, SpriteFont font)
        {
            outlineRect = boxRectangle;
            textBoxOutlineWidth = outlineWidth;
            textBoxMaxCharacters = maxCharacters;
            textBoxColor = boxColor;
            textBoxOutlineColor = outlineColor;
            textBoxTextColor = textColor;
            textBoxCursorColor = cursorColor;
            Device = GraphicsDevice;
            textBoxFont = font;
            Initialize();
        }

        public void Initialize()
        {
            boxRect = new Rectangle(outlineRect.X + textBoxOutlineWidth, outlineRect.Y + textBoxOutlineWidth, outlineRect.Width - (textBoxOutlineWidth * 2), outlineRect.Height - (textBoxOutlineWidth * 2));
            cursorRect = new Rectangle(boxRect.X, boxRect.Center.Y - ((int)textBoxFont.MeasureString("Y").Y / 2), 1, (int)textBoxFont.MeasureString("Y").Y);
            whiteTexture = new Texture2D(Device, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });
            cursorTimer = 0;
            backTimer = 0;
            text = "";
            updateText = "";
            drawText = "";
            textBoxSelected = false;
            wasTextBoxSelected = false;
            acceptsNumbers = true;
            acceptsLetters = true;
        }

        public void Update(MouseState mouse, MouseState oldMouse, KeyboardState kb, KeyboardState oldKb)
        {
            wasTextBoxSelected = textBoxSelected;
            updateText = "";
            backTimer++;

            if(mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
            {
                if (mouse.X >= boxRect.Left && mouse.X <= boxRect.Right && mouse.Y >= boxRect.Top && mouse.Y <= boxRect.Bottom)
                    textBoxSelected = true;
                else
                    textBoxSelected = false;
            }
            if (textBoxSelected)
            {
                cursorTimer++;
                if (cursorTimer >= 60)
                    cursorTimer = 0;
            }
            if (textBoxSelected && wasTextBoxSelected == false)
                cursorTimer = 0;

            if (textBoxSelected)
            {
                DetectPressedKeys(mouse, oldMouse, kb, oldKb);
                text += updateText;
            }

            if (text.Length > textBoxMaxCharacters)
                text = text.Substring(0, textBoxMaxCharacters);

            cursorRect.Y = outlineRect.Center.Y - (cursorRect.Height / 2);
        }

        private void DetectPressedKeys(MouseState mouse, MouseState oldMouse, KeyboardState kb, KeyboardState oldKb)
        {
            if (acceptsLetters)
            {
                if (kb.IsKeyDown(Keys.A) && !oldKb.IsKeyDown(Keys.A))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "A";
                    else
                        updateText += "a";
                }
                if (kb.IsKeyDown(Keys.B) && !oldKb.IsKeyDown(Keys.B))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "B";
                    else
                        updateText += "b";
                }
                if (kb.IsKeyDown(Keys.C) && !oldKb.IsKeyDown(Keys.C))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "C";
                    else
                        updateText += "c";
                }
                if (kb.IsKeyDown(Keys.D) && !oldKb.IsKeyDown(Keys.D))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "D";
                    else
                        updateText += "d";
                }
                if (kb.IsKeyDown(Keys.E) && !oldKb.IsKeyDown(Keys.E))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "E";
                    else
                        updateText += "e";
                }
                if (kb.IsKeyDown(Keys.F) && !oldKb.IsKeyDown(Keys.F))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "F";
                    else
                        updateText += "f";
                }
                if (kb.IsKeyDown(Keys.G) && !oldKb.IsKeyDown(Keys.G))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "G";
                    else
                        updateText += "g";
                }
                if (kb.IsKeyDown(Keys.H) && !oldKb.IsKeyDown(Keys.H))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "H";
                    else
                        updateText += "h";
                }
                if (kb.IsKeyDown(Keys.I) && !oldKb.IsKeyDown(Keys.I))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "I";
                    else
                        updateText += "i";
                }
                if (kb.IsKeyDown(Keys.J) && !oldKb.IsKeyDown(Keys.J))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "J";
                    else
                        updateText += "j";
                }
                if (kb.IsKeyDown(Keys.K) && !oldKb.IsKeyDown(Keys.K))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "K";
                    else
                        updateText += "k";
                }
                if (kb.IsKeyDown(Keys.L) && !oldKb.IsKeyDown(Keys.L))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "L";
                    else
                        updateText += "l";
                }
                if (kb.IsKeyDown(Keys.M) && !oldKb.IsKeyDown(Keys.M))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "M";
                    else
                        updateText += "m";
                }
                if (kb.IsKeyDown(Keys.N) && !oldKb.IsKeyDown(Keys.N))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "N";
                    else
                        updateText += "n";
                }
                if (kb.IsKeyDown(Keys.O) && !oldKb.IsKeyDown(Keys.O))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "O";
                    else
                        updateText += "o";
                }
                if (kb.IsKeyDown(Keys.P) && !oldKb.IsKeyDown(Keys.P))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "P";
                    else
                        updateText += "p";
                }
                if (kb.IsKeyDown(Keys.Q) && !oldKb.IsKeyDown(Keys.Q))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "Q";
                    else
                        updateText += "q";
                }
                if (kb.IsKeyDown(Keys.R) && !oldKb.IsKeyDown(Keys.R))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "R";
                    else
                        updateText += "r";
                }
                if (kb.IsKeyDown(Keys.S) && !oldKb.IsKeyDown(Keys.S))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "S";
                    else
                        updateText += "s";
                }
                if (kb.IsKeyDown(Keys.T) && !oldKb.IsKeyDown(Keys.T))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "T";
                    else
                        updateText += "t";
                }
                if (kb.IsKeyDown(Keys.U) && !oldKb.IsKeyDown(Keys.U))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "U";
                    else
                        updateText += "u";
                }
                if (kb.IsKeyDown(Keys.V) && !oldKb.IsKeyDown(Keys.V))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "V";
                    else
                        updateText += "v";
                }
                if (kb.IsKeyDown(Keys.W) && !oldKb.IsKeyDown(Keys.W))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "W";
                    else
                        updateText += "w";
                }
                if (kb.IsKeyDown(Keys.X) && !oldKb.IsKeyDown(Keys.X))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "X";
                    else
                        updateText += "x";
                }
                if (kb.IsKeyDown(Keys.Y) && !oldKb.IsKeyDown(Keys.Y))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "Y";
                    else
                        updateText += "y";
                }
                if (kb.IsKeyDown(Keys.Z) && !oldKb.IsKeyDown(Keys.Z))
                {
                    if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
                        updateText += "Z";
                    else
                        updateText += "z";
                }
            }
            if (acceptsNumbers)
            {
                if ((kb.IsKeyDown(Keys.D0) && !oldKb.IsKeyDown(Keys.D0)) || (kb.IsKeyDown(Keys.NumPad0) && !oldKb.IsKeyDown(Keys.NumPad0)))
                {
                    updateText += "0";
                }
                if ((kb.IsKeyDown(Keys.D1) && !oldKb.IsKeyDown(Keys.D1)) || (kb.IsKeyDown(Keys.NumPad1) && !oldKb.IsKeyDown(Keys.NumPad1)))
                {
                    updateText += "1";
                }
                if ((kb.IsKeyDown(Keys.D2) && !oldKb.IsKeyDown(Keys.D2)) || (kb.IsKeyDown(Keys.NumPad2) && !oldKb.IsKeyDown(Keys.NumPad2)))
                {
                    updateText += "2";
                }
                if ((kb.IsKeyDown(Keys.D3) && !oldKb.IsKeyDown(Keys.D3)) || (kb.IsKeyDown(Keys.NumPad3) && !oldKb.IsKeyDown(Keys.NumPad3)))
                {
                    updateText += "3";
                }
                if ((kb.IsKeyDown(Keys.D4) && !oldKb.IsKeyDown(Keys.D4)) || (kb.IsKeyDown(Keys.NumPad4) && !oldKb.IsKeyDown(Keys.NumPad4)))
                {
                    updateText += "4";
                }
                if ((kb.IsKeyDown(Keys.D5) && !oldKb.IsKeyDown(Keys.D5)) || (kb.IsKeyDown(Keys.NumPad5) && !oldKb.IsKeyDown(Keys.NumPad5)))
                {
                    updateText += "5";
                }
                if ((kb.IsKeyDown(Keys.D6) && !oldKb.IsKeyDown(Keys.D6)) || (kb.IsKeyDown(Keys.NumPad6) && !oldKb.IsKeyDown(Keys.NumPad6)))
                {
                    updateText += "6";
                }
                if ((kb.IsKeyDown(Keys.D7) && !oldKb.IsKeyDown(Keys.D7)) || (kb.IsKeyDown(Keys.NumPad7) && !oldKb.IsKeyDown(Keys.NumPad7)))
                {
                    updateText += "7";
                }
                if ((kb.IsKeyDown(Keys.D8) && !oldKb.IsKeyDown(Keys.D8)) || (kb.IsKeyDown(Keys.NumPad8) && !oldKb.IsKeyDown(Keys.NumPad8)))
                {
                    updateText += "8";
                }
                if ((kb.IsKeyDown(Keys.D9) && !oldKb.IsKeyDown(Keys.D9)) || (kb.IsKeyDown(Keys.NumPad9) && !oldKb.IsKeyDown(Keys.NumPad9)))
                {
                    updateText += "9";
                }
            }
            if(kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space))
            {
                updateText += " ";
            }
            if(kb.IsKeyDown(Keys.Back) && backTimer >= 0)
            {
                if (text.Length > 0)
                {
                    text = text.Substring(0, text.Length - 1);
                    backTimer = -8;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(whiteTexture, outlineRect, textBoxOutlineColor);
            spriteBatch.Draw(whiteTexture, boxRect, textBoxColor);
            Vector2 textSize = textBoxFont.MeasureString(text);
            drawText = text;
            if(textSize.X > boxRect.Width)
            {
                for(int x = 0; x > -1; x++)
                {
                    drawText = drawText.Substring(1, drawText.Length - 1);
                    textSize = textBoxFont.MeasureString(drawText);
                    if (textSize.X <= boxRect.Width)
                        break;
                }
            }
            spriteBatch.DrawString(textBoxFont, drawText, new Vector2(boxRect.X, boxRect.Center.Y - (textSize.Y / 2)), textBoxTextColor);
            cursorRect.X = boxRect.X + (int)textSize.X;
            if(textBoxSelected && cursorTimer <= 30)
                spriteBatch.Draw(whiteTexture, cursorRect, textBoxCursorColor);
        }
    }
}
