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
    [Serializable] class Human : Player
    {
        MouseState oldmb = Mouse.GetState();
        KeyboardState oldkb = Keyboard.GetState();

        Planet selectedPlanet; //Used For Clicking On Planets

        public Human(int startingGold, Board b) : base(startingGold, b)
        {
         
        }

        public void Update(GameTime gt)
        {
            if (isTurn)
            {
                base.Update(gt);
                MouseState mb = Mouse.GetState();
                KeyboardState kb = Keyboard.GetState();

                if (mb.LeftButton == ButtonState.Pressed && oldmb.LeftButton == ButtonState.Released) // On Left Mouse Click
                    MouseClick(new Vector2(mb.X, mb.Y));

                if (kb.IsKeyDown(Keys.Delete) && oldkb.IsKeyUp(Keys.Delete)) //Ends Turn
                {
                    EndTurn();
                }

                oldkb = kb;
                oldmb = mb;
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
                    board.selection = new Rectangle((int)temp.position.X - (int)(temp.size * 2.5), (int)temp.position.Y - (int)(temp.size * 2.5), temp.size* 30, temp.size * 30);
                    break;
                }
            }
        }
    }
}
