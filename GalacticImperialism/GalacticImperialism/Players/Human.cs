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
        List<Planet> nearbyPlanets; // All Planets within radius of selected Planet
        public bool isMenuOpen;

        public Human(int startingGold, Board b) : base(startingGold, b)
        {

        }

        public void Update(GameTime gt, MouseState mb, KeyboardState kb, MouseState oldms, KeyboardState oldkb)
        {
            base.Update(gt);

            if (Game1.menuSelected == Game1.Menus.Game)
            {
                if (mb.LeftButton == ButtonState.Pressed && oldms.LeftButton == ButtonState.Released) // On Left Mouse Click
                    MouseClick(new Vector2(mb.X, mb.Y));

            }

            if (nearbyPlanets != null)
            {

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
                    PlayerUI.drawPlanetMenu(selectedPlanet);
                    nearbyPlanets = NearbyPlanets(selectedPlanet);
                    break;
                }
            }
        }
    }
}
