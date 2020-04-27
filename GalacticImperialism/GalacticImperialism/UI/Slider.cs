using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GalacticImperialism
{
    class Slider
    {
        Rectangle backgroundRect;
        public Rectangle cursorRect;

        Texture2D backgroundTexture;
        Texture2D cursorTexture;

        bool cursorMoving;

        public float percentage;

        public Slider(Rectangle backgroundRectangle, Vector2 cursorWidthAndHeight, Texture2D backgroundTexture, Texture2D cursorTexture)
        {
            backgroundRect = backgroundRectangle;
            cursorRect = new Rectangle(backgroundRect.Right - (int)cursorWidthAndHeight.X, backgroundRect.Center.Y - ((int)cursorWidthAndHeight.Y / 2), (int)cursorWidthAndHeight.X, (int)cursorWidthAndHeight.Y);
            this.backgroundTexture = backgroundTexture;
            this.cursorTexture = cursorTexture;
            Initialize();
        }

        public void Initialize()
        {
            cursorMoving = false;
            percentage = 1.0f;
        }

        public void SetPercentage(float temporaryPercentage)     //Pass in a number from 0-1.
        {
            cursorRect.X = (int)(((backgroundRect.Right - cursorRect.Width - backgroundRect.X) * temporaryPercentage) + backgroundRect.X);
        }

        public void DeterminePercentage()
        {
            percentage = ((float)cursorRect.X - backgroundRect.X) / (backgroundRect.Right - cursorRect.Width - backgroundRect.X);
        }

        public void Update(MouseState mouse, MouseState oldMouse)
        {
            if (mouse.X >= cursorRect.X && mouse.X <= cursorRect.Right && mouse.Y >= cursorRect.Y && mouse.Y <= cursorRect.Bottom && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed && cursorMoving == false)
                cursorMoving = true;
            if (mouse.LeftButton != ButtonState.Pressed && cursorMoving == true)
                cursorMoving = false;
            if (cursorMoving)
                cursorRect.X = mouse.X - (cursorRect.Width / 2);
            if (cursorRect.X < backgroundRect.X)
                cursorRect.X = backgroundRect.X;
            if (cursorRect.Right > backgroundRect.Right)
                cursorRect.X = backgroundRect.Right - cursorRect.Width;
            DeterminePercentage();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);
            spriteBatch.Draw(cursorTexture, cursorRect, Color.White);
        }
    }
}
