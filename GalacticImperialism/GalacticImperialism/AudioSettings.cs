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
    class AudioSettings
    {
        string titleText;

        SpriteFont fontOfButtons;
        SpriteFont fontOfTitle;

        GraphicsDevice GraphicsDevice;

        public AudioSettings(SpriteFont buttonFont, SpriteFont titleFont, GraphicsDevice GraphicsDevice)
        {
            fontOfButtons = buttonFont;
            fontOfTitle = titleFont;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            titleText = "Audio Settings";
        }

        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse, MouseState oldMouse)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = fontOfTitle.MeasureString(titleText);
            spriteBatch.DrawString(fontOfTitle, titleText, new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), (GraphicsDevice.Viewport.Height / 6) - (textSize.Y / 2)), Color.White);
            textSize = fontOfButtons.MeasureString("Press Escape To Return To Settings");
            spriteBatch.DrawString(fontOfButtons, "Press Escape To Return To Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), GraphicsDevice.Viewport.Height - textSize.Y), Color.White);
        }
    }
}
