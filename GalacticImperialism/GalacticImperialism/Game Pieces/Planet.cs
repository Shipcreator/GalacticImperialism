using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GalacticImperialism
{
    [Serializable] class Planet
    {
        //Planet Attributes
        public int size;
        public string planetName;
        public Vector2 position;
        public int[] resourceNumbers = new int[6];
        public int texID; //Texture for the Planet
        public List<Ship> planetShips = new List<Ship>();
        public PlanetManagementMenu managementMenuObject;       //Pop-up menu that allows the player to manage the building and resources of a planet.
        public List<BuildingSlot> buildingSlotsList;
        public BuildingQueue buildingQueue;
        public int productionPerTurn;
        public ShipsQueue shipsQueue;
        public int sciencePerTurn;
        public int goldPerTurn;

        //public UnitProduction unitProduction;


        //Create Base Planets
        public Planet(int s, int t, string n, Vector2 p, int[] r)
        {
            size = s;
            texID = t;
            planetName = n;
            position = p;
            resourceNumbers = r;

            productionPerTurn = 1000;
            managementMenuObject = new PlanetManagementMenu(this);
            buildingSlotsList = new List<BuildingSlot>();
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingQueue = new BuildingQueue();

            //unitProduction = new UnitProduction();
            shipsQueue = new ShipsQueue();

            sciencePerTurn = 10;
            goldPerTurn = 10;
        }

        //Create Starting Planet
        public Planet(int t, string n, Vector2 p)
        {
            size = 2;
            texID = t;
            planetName = n;
            position = p;
            resourceNumbers[0] += 2;
            resourceNumbers[3] += 2;

            productionPerTurn = 2000;
            managementMenuObject = new PlanetManagementMenu(this);
            buildingSlotsList = new List<BuildingSlot>();
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.ResearchFacility));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.MilitaryBase));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Factory));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingSlotsList.Add(new BuildingSlot(BuildingSlot.BuildingType.Empty));
            buildingQueue = new BuildingQueue();

            //unitProduction = new UnitProduction();
            shipsQueue = new ShipsQueue();

            sciencePerTurn = 30;
            goldPerTurn = 30;
        }
    }
}
