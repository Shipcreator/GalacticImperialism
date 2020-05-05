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
        public Rectangle menuRectangle;

        int viewportWidth;
        int viewportHeight;
        public int menuRectWidth;
        public int menuRectHeight;
        int topBarHeight;

        Planet assignedPlanet;

        Vector2 textSize;
        Vector2 originalDistanceAwayFromMouse;

        bool moving;

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
            topBarHeight = 67;
            menuRectangle = new Rectangle((viewportWidth / 2) - (menuRectWidth / 2), (viewportHeight / 2) - (menuRectHeight / 2), menuRectWidth, menuRectHeight);
            textSize = new Vector2(0, 0);
            originalDistanceAwayFromMouse = new Vector2(0, 0);
            moving = false;
        }

        public void Update(MouseState mouse, MouseState oldMouse)
        {
            if(mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed && moving == false && mouse.X >= menuRectangle.X && mouse.X <= menuRectangle.Right && mouse.Y >= menuRectangle.Y && mouse.Y <= menuRectangle.Bottom)
            {
                moving = true;
                originalDistanceAwayFromMouse.X = mouse.X - menuRectangle.X;
                originalDistanceAwayFromMouse.Y = mouse.Y - menuRectangle.Y;
            }
            if (moving)
            {
                if (mouse.LeftButton != ButtonState.Pressed)
                    moving = false;
                menuRectangle.X = mouse.X - (int)originalDistanceAwayFromMouse.X;
                menuRectangle.Y = mouse.Y - (int)originalDistanceAwayFromMouse.Y;
            }

            if (menuRectangle.X < 0)
                menuRectangle.X = 0;
            if(mouse.X < 0)
                originalDistanceAwayFromMouse.X = 0;
            if (menuRectangle.Right > viewportWidth)
                menuRectangle.X = viewportWidth - menuRectangle.Width;
            if (mouse.X > viewportWidth)
                originalDistanceAwayFromMouse.X = menuRectangle.Width;
            if (menuRectangle.Y <= topBarHeight)
                menuRectangle.Y = topBarHeight + 1;
            if (mouse.Y <= topBarHeight)
                originalDistanceAwayFromMouse.Y = 0;
            if (menuRectangle.Bottom > viewportHeight)
                menuRectangle.Y = viewportHeight - menuRectangle.Height;
            if (mouse.Y > viewportHeight)
                originalDistanceAwayFromMouse.Y = menuRectangle.Height;
        }
    }
}
