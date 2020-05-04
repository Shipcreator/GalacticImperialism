﻿using System;
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
    [Serializable]
    class Human : Player
    {
        Planet selectedPlanet; //Used For Clicking On Planets

        public Human(int startingGold, Board b, Vector3 empireColor, Flag empireFlag) : base(startingGold, b, empireColor, empireFlag)
        {

        }

        public void Update(GameTime gt, MouseState mb, KeyboardState kb, MouseState oldms, KeyboardState oldkb)
        {
            base.Update(gt);
            
            //Detects Click on board only
            if (Game1.menuSelected == Game1.Menus.Game && PlayerUI.planetManagementMenuOpen == false)
            {
                if (mb.LeftButton == ButtonState.Pressed && oldms.LeftButton == ButtonState.Released) // On Left Mouse Click
                    MouseClick(new Vector2(mb.X, mb.Y));

            }
        }

        public void MouseClick(Vector2 position)
        {
            //Selects Planet
            for (int i = 0; i < ownedPlanets.Count; i++)
            {
                Planet temp = ownedPlanets[i];
                Rectangle planetRect = new Rectangle((int)temp.position.X, (int)temp.position.Y, temp.size * 25, temp.size * 25);
                Rectangle mouse = new Rectangle((int)position.X, (int)position.Y + 15, 1, 1);
                if (mouse.Intersects(planetRect))
                {
                    selectedPlanet = ownedPlanets[i];
                    PlayerUI.drawPlanetMenu(selectedPlanet, NearbyPlanets(selectedPlanet));
                    break;
                }
            }
        }
    }
}
