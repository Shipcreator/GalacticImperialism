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
using Lidgren.Network;

namespace GalacticImperialism
{
    [Serializable] class Flag
    {
        public Color[] flagColorArray;

        GraphicsDevice GraphicsDevice;

        int flagWidth;
        int flagHeight;

        public Flag(Texture2D flagTexture, GraphicsDevice GraphicsDevice)
        {
            flagWidth = flagTexture.Width;
            flagHeight = flagTexture.Height;
            flagColorArray = new Color[flagWidth * flagHeight];
            flagTexture.GetData<Color>(flagColorArray);
            this.GraphicsDevice = GraphicsDevice;
        }

        public void SetColorArray(Texture2D flagTexture)
        {
            flagWidth = flagTexture.Width;
            flagHeight = flagTexture.Height;
            flagColorArray = new Color[flagWidth * flagHeight];
            flagTexture.GetData<Color>(flagColorArray);
        }

        //The method below causes the system to run out of memory, do not use unless for a one time access!
        public Texture2D GetFlagTexture()
        {
            Texture2D temporaryFlagTexture = new Texture2D(GraphicsDevice, flagWidth, flagHeight);
            temporaryFlagTexture.SetData<Color>(flagColorArray);
            return temporaryFlagTexture;
        }
    }
}
