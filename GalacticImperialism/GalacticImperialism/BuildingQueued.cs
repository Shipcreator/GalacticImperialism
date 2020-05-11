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
    [Serializable] class BuildingQueued
    {
        public enum BuildingType
        {
            ResearchFacility,
            MilitaryBase,
            Factory
        }
        public BuildingType typeOfBuilding;

        public int buildingSlotIndex;
        public int buildCost;

        public BuildingQueued(BuildingType type, int indexOfBuildingSlot)
        {
            typeOfBuilding = type;
            buildingSlotIndex = indexOfBuildingSlot;
            buildCost = 10000;
        }
    }
}
