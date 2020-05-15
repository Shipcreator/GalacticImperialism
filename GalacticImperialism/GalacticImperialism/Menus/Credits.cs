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
    class Credits
    {
        public Credits()
        {

        }

        public void Initialize()
        {

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.WinFont, "Game By:", new Vector2(600, 100), Color.White);
            spriteBatch.DrawString(Game1.WinFont, "Ciaran Vaille", new Vector2(600, 350), Color.White);
            spriteBatch.DrawString(Game1.WinFont, "Dylan Rogers", new Vector2(600, 500), Color.White);
            spriteBatch.DrawString(Game1.WinFont, "Christian White", new Vector2(600, 650), Color.White);
            spriteBatch.DrawString(Game1.WinFont, "Xander Pham-Rojas", new Vector2(600, 800), Color.White);
            spriteBatch.DrawString(Game1.WinFont, "Press Escape To Return To Settings", new Vector2(100,975), Color.White);
        }
    }
}
