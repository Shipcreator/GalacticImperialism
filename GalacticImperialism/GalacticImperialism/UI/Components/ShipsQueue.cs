using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [Serializable] class ShipsQueue
    {
        public List<Ship> queuedShips;
        public List<Ship> finishedShips;

        int constructionAmount;

        public ShipsQueue()
        {
            Initialize();
        }

        public void Initialize()
        {
            queuedShips = new List<Ship>();
            finishedShips = new List<Ship>();
            constructionAmount = 0;
        }

        public void EndTurn(int amountOfConstruction)
        {
            constructionAmount = amountOfConstruction;
            try
            {
                for(int x = 0; x < queuedShips.Count; x++)
                {
                    if(queuedShips[x].getConCost() <= constructionAmount)
                    {
                        constructionAmount = constructionAmount - queuedShips[x].getConCost();
                        finishedShips.Add(queuedShips[x]);
                        queuedShips.RemoveAt(x);
                        x--;
                    }
                    else
                    {
                        queuedShips[x].constructionCost -= constructionAmount;
                        constructionAmount = 0;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
