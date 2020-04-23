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
    /// <summary>
    /// //This Class should contain mostly static methods as there only needs to be one per computer, and it is NOT sent over the network.
    /// </summary>
    class PlayerUI
    {
        Texture2D barTexture;
        Texture2D flagTexture;

        Rectangle barRect;
        Rectangle flagRect;

        SpriteFont Arial15;

        //Planet Stats Menu
        static Planet currentPlanet;
        static bool planetMenu;

        GraphicsDevice GraphicsDevice;

        public PlayerUI(Texture2D topBarTexture, Texture2D empireFlagTexture, GraphicsDevice GraphicsDevice, SpriteFont arial15)
        {
            barTexture = topBarTexture;
            flagTexture = empireFlagTexture;
            planetMenu = false;
            Arial15 = arial15;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            barRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, (int)((GraphicsDevice.Viewport.Height / 100.0f) * 6.25f));
            flagRect = new Rectangle((int)((barRect.Width / 1920.0f) * 15), (int)((barRect.Height / 135.0f) * 15), (int)((((int)(barRect.Height - ((barRect.Height / 135.0f) * 30))) / 3.0f) * 5.0f), (int)(barRect.Height - ((barRect.Height / 135.0f) * 30)));          
        }

        public void Update()
        {

        }

        //Called When a Player Clicks on a Planet
        public static void drawPlanetMenu(Planet p)
        {
            currentPlanet = p;
            planetMenu = true;
        }

        public static void closeMenus()
        {
            planetMenu = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barTexture, barRect, Color.White);
            spriteBatch.Draw(flagTexture, flagRect, Color.White);

            if (planetMenu == true)
            {
                spriteBatch.DrawString(Arial15, currentPlanet.planetShips.Count.ToString(), new Vector2(500,500), Color.White);
            }
        }
    }
}
