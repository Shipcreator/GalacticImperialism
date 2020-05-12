using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace GalacticImperialism
{
    [Serializable]public class UnitProduction
    {
        public List<object> productionQueue;
        public int production;

        public static bool Open;
        public static Ship[] shipList = new Ship[] { new Ship(1, 1, 5, 1200, "Corvette"), new Ship(2, 2, 3, 2000, "Destroyer"), new Ship(3, 4, 3, 3600, "Cruiser"), new Ship(5, 4, 2, 4000, "BattleShip"), new Ship(7, 7, 2, 10000, "Capital Ship"), new Ship(1, 1, 5, 500, "Swarm Ship"), new Ship(4, 1, 3, 1200, "Fleet Buster"), new Ship(1, 4, 1, 3, "Shield Ship") };
        public UnitProduction()
        {
            Open = false;
            productionQueue = new List<object>();
            production = 0;
        }

        public void add(object obj)
        {
            productionQueue.Add(obj);
        }

        public void productionAdd()
        {
            production++;
            if (productionQueue.Count > 0)
            {
                if (productionQueue.ElementAt(0) is Ship)//checks if first in queue is a ship or army
                {
                    Ship temp = (Ship)productionQueue.ElementAt(0);
                    if (temp.getConCost() <= production)
                    {
                        //player.AddShip(new Ship(temp.getAttack(), temp.getDefence(), temp.getMoves(), temp.getConCost(), temp.getName()));
                        production -= temp.getConCost();
                        productionQueue.RemoveAt(0);
                        //UnitProductionMenu.Complete_Production_Bar.Width = 0;
                    }
                }
            }
        }

        public void Update()
        {
            if (productionQueue.Count != 0)
            {
                productionAdd();
            }
        }
    }
}
