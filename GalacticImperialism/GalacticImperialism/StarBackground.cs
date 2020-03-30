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
    class StarBackground
    {
        int numOfStars;
        int widthAndHeightOfStars;
        public int speedOfStars;
        public int twinklingStarChance;        //0 (least likely) - 100 (most likely)

        bool currentRandomIsTwinkling;

        Texture2D whiteCircleTexture;

        GraphicsDevice GraphicsDevice;

        List<Color> starColorsList;

        List<Star> starsList;

        Random rnd;

        public StarBackground(int numberOfStars, int starsWidthAndHeight, int starSpeed, int chanceOfTwinklingStar, Texture2D whiteCircle, List<Color> starColors, GraphicsDevice GraphicsDevice)
        {
            numOfStars = numberOfStars;
            widthAndHeightOfStars = starsWidthAndHeight;
            speedOfStars = starSpeed;
            twinklingStarChance = chanceOfTwinklingStar;
            whiteCircleTexture = whiteCircle;
            starColorsList = starColors;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            rnd = new Random();

            starsList = new List<Star>();

            currentRandomIsTwinkling = false;
            for(int x = 0; x < numOfStars; x++)
            {
                if (rnd.Next(1, 101) <= twinklingStarChance)
                    currentRandomIsTwinkling = true;
                else
                    currentRandomIsTwinkling = false;
                starsList.Add(new Star(speedOfStars, rnd.Next(0, 101), currentRandomIsTwinkling, new Rectangle(rnd.Next(0, (GraphicsDevice.Viewport.Width - widthAndHeightOfStars) + 1), rnd.Next(0, (GraphicsDevice.Viewport.Height - widthAndHeightOfStars) + 1), widthAndHeightOfStars, widthAndHeightOfStars), whiteCircleTexture, starColorsList[rnd.Next(0, starColorsList.Count)], GraphicsDevice));
            }
        }

        public void Update()
        {
            for(int x = starsList.Count - 1; x > -1; x--)
            {
                starsList[x].Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = starsList.Count - 1; x > -1; x--)
            {
                starsList[x].Draw(spriteBatch);
            }
        }
    }
}