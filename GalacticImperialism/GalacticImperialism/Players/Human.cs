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
    [Serializable]
    class Human : Player
    {
        Planet selectedPlanet; //Used For Clicking On Planets

        public Human(int startingGold, Board b) : base(startingGold, b)
        {

        }

        public void Update(GameTime gt, MouseState mb, KeyboardState kb, MouseState oldms, KeyboardState oldkb)
        {
            base.Update(gt);

            if (mb.LeftButton == ButtonState.Pressed && oldms.LeftButton == ButtonState.Released) // On Left Mouse Click
                MouseClick(new Vector2(mb.X, mb.Y));

            //Temporary End Turn Key
            if (kb.IsKeyDown(Keys.Delete) && oldkb.IsKeyUp(Keys.Delete)) //Ends Turn
            {
                PlayerUI.closeMenus();
                EndTurn();
            }
        }

        public void MouseClick(Vector2 position)
        {
            //Selects Planet
            for (int i = 0; i < ownedPlanets.Count; i++)
            {
                Planet temp = ownedPlanets[i];
                Rectangle planetRect = new Rectangle((int)temp.position.X, (int)temp.position.Y, temp.size * 25, temp.size * 25);
                Rectangle mouse = new Rectangle((int)position.X, (int)position.Y, 1, 1);
                if (mouse.Intersects(planetRect))
                {
                    selectedPlanet = ownedPlanets[i];
                    board.selection = new Rectangle((int)temp.position.X - (int)(temp.size * 2.5), (int)temp.position.Y - (int)(temp.size * 2.5), temp.size * 30, temp.size * 30);
                    PlayerUI.drawPlanetMenu(selectedPlanet);
                    break;
                }
            }
        }
    }
}
