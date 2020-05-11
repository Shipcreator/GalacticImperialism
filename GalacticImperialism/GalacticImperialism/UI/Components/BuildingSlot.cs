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
    [Serializable] class BuildingSlot
    {
        public enum BuildingType
        {
            Empty,
            ResearchFacility,
            MilitaryBase,
            Factory
        }
        public BuildingType typeOfBuilding;

        public Rectangle buildingSlotRectangle;

        public BuildingSlot(BuildingType type)
        {
            typeOfBuilding = type;
            Initialize();
        }

        public void Initialize()
        {
            buildingSlotRectangle = new Rectangle(0, 0, 100, 100);
        }

        public void Update(Vector2 position)
        {
            buildingSlotRectangle.X = (int)position.X;
            buildingSlotRectangle.Y = (int)position.Y;
        }
    }
}
