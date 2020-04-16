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
using GalacticImperialism.Networking;

namespace GalacticImperialism
{
    class Multiplayer
    {
        SpriteFont fontOfTitle;
        SpriteFont fontOfText;

        GraphicsDevice GraphicsDevice;

        public Multiplayer(SpriteFont titleFont, SpriteFont textFont, GraphicsDevice GraphicsDevice)
        {
            fontOfTitle = titleFont;
            fontOfText = textFont;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {

        }

        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse, MouseState oldMouse)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = fontOfTitle.MeasureString("Multiplayer");
            spriteBatch.DrawString(fontOfTitle, "Multiplayer", new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), (GraphicsDevice.Viewport.Height / 6) - (textSize.Y / 2)), Color.White);
            textSize = fontOfText.MeasureString("Press Escape To Return To Main Menu");
            spriteBatch.DrawString(fontOfText, "Press Escape To Return To Main Menu", new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), GraphicsDevice.Viewport.Height - textSize.Y), Color.White);
        }
    }
}
