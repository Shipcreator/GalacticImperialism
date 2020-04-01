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
    class Star
    {
        int speedOfStar;
        int opacityInt;

        float opacityFloat;

        bool isTwinklingStar;
        bool opacityGoingDown;
        bool firstUpdate;

        Rectangle starRect;

        Texture2D whiteCircleTexture;

        Color colorOfStar;

        GraphicsDevice GraphicsDevice;

        public Star(int starSpeed, int startingStarOpacity, bool twinklingStar, Rectangle starRectangle, Texture2D whiteCircle, Color starColor, GraphicsDevice GraphicsDevice)
        {
            speedOfStar = starSpeed;
            opacityInt = startingStarOpacity;
            isTwinklingStar = twinklingStar;
            starRect = starRectangle;
            whiteCircleTexture = whiteCircle;
            colorOfStar = starColor;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            opacityFloat = 1.0f;
            if (opacityInt == 0)
                opacityGoingDown = false;
            else
                opacityGoingDown = true;
            firstUpdate = true;
        }

        public void Update()
        {
            starRect.Y += speedOfStar;

            if(starRect.Y > GraphicsDevice.Viewport.Height)
                starRect.Y -= GraphicsDevice.Viewport.Height;

            if ((opacityInt >= 100 || opacityInt <= 0) && firstUpdate == false)
                opacityGoingDown = !opacityGoingDown;

            if (opacityGoingDown)
                opacityInt--;
            else
                opacityInt++;

            opacityFloat = opacityInt / 100.0f;
            firstUpdate = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isTwinklingStar)
                spriteBatch.Draw(whiteCircleTexture, starRect, colorOfStar * opacityFloat);
            else
                spriteBatch.Draw(whiteCircleTexture, starRect, colorOfStar);
        }
    }
}