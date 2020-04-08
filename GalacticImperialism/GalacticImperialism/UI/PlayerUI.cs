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
    class PlayerUI
    {
        Texture2D barTexture;
        Texture2D flagTexture;

        Rectangle barRect;
        Rectangle flagRect;

        GraphicsDevice GraphicsDevice;

        public PlayerUI(Texture2D topBarTexture, Texture2D empireFlagTexture, GraphicsDevice GraphicsDevice)
        {
            barTexture = topBarTexture;
            flagTexture = empireFlagTexture;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            barRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, (int)((GraphicsDevice.Viewport.Height / 100.0f) * 12.5f));
            flagRect = new Rectangle(15, 15, (int)(((barRect.Height - 30) / 3.0f) * 5.0f), barRect.Height - 30);
            
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barTexture, barRect, Color.White);
            spriteBatch.Draw(flagTexture, flagRect, Color.White);
        }
    }
}
