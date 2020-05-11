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
    [Serializable] class BuildingQueue
    {
        public List<BuildingQueued> queuedBuildings;
        public List<BuildingQueued> finishedBuildings;

        public int productionTowardsNextBuilding;

        public BuildingQueue()
        {
            Initialize();
        }

        public void Initialize()
        {
            queuedBuildings = new List<BuildingQueued>();
            finishedBuildings = new List<BuildingQueued>();
            productionTowardsNextBuilding = 0;
        }

        public void AddBuildingToQueue(string type, int buildingSlot)
        {
            if (type.Equals("ResearchFacility"))
            {
                queuedBuildings.Add(new BuildingQueued(BuildingQueued.BuildingType.ResearchFacility, buildingSlot));
            }
            if (type.Equals("MilitaryBase"))
            {
                queuedBuildings.Add(new BuildingQueued(BuildingQueued.BuildingType.MilitaryBase, buildingSlot));
            }
            if (type.Equals("Factory"))
            {
                queuedBuildings.Add(new BuildingQueued(BuildingQueued.BuildingType.Factory, buildingSlot));
            }
        }

        public void EndTurn(int amountOfProductionToAdd)
        {
            productionTowardsNextBuilding += amountOfProductionToAdd;
            try
            {
                for (int x = 0; x < queuedBuildings.Count; x++)
                {
                    if (productionTowardsNextBuilding >= queuedBuildings[x].buildCost)
                    {
                        productionTowardsNextBuilding -= queuedBuildings[x].buildCost;
                        finishedBuildings.Add(queuedBuildings[x]);
                        queuedBuildings.RemoveAt(x);
                        x--;
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
