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
    [Serializable] class PlanetManagementMenu
    {
        public Vector4 menuRectangle;

        int viewportWidth;
        int viewportHeight;
        int menuRectWidth;
        int menuRectHeight;

        Planet assignedPlanet;

        Vector2 textSize;

        public PlanetManagementMenu(Planet planetAssigned)
        {
            assignedPlanet = planetAssigned;
            Initialize();
        }

        public void Initialize()
        {
            viewportWidth = 1920;
            viewportHeight = 1080;
            menuRectWidth = 900;
            menuRectHeight = 900;
            menuRectangle = new Vector4((viewportWidth / 2) - (menuRectWidth / 2), (viewportHeight / 2) - (menuRectHeight / 2), menuRectWidth, menuRectHeight);
            textSize = new Vector2(0, 0);
        }

        public void Update(MouseState mouse, MouseState oldMouse)
        {

        }
    }
}
