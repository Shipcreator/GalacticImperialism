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
    [Serializable] class Computer : Player
    {
        Random random = new Random();

        int[] tempResources;
        int tempRandomNumber;
        int tempRandomNumber2;

        public Computer(int startingGold, Board b, Vector3 empireColor, Flag empireFlag) : base(startingGold, b, empireColor, empireFlag)
        {
            tempResources = new int[6];
            for(int x = 0; x < tempResources.Length; x++)
            {
                tempResources[x] = 0;
            }
            tempRandomNumber = 0;
            tempRandomNumber2 = 0;
        }

        public void OnTurn()
        {
            base.OnTurn();
            CheckTech();
            AddMovesHydrogenBuff();

            for (int i = 0; i <= 5; i++)
                MoveShips();

            NewUnits();
            Buildings();
            EndTurn();
        }

        private void CheckTech()
        {
            if(techTreeObject.techResearching == TechTree.Tech.None)
            {
                if(random.Next(0, 2) == 0)
                {
                    techTreeObject.techResearching = TechTree.Tech.Attack;
                }
                else
                {
                    if (random.Next(0, 2) == 0)
                    {
                        techTreeObject.techResearching = TechTree.Tech.Defense;
                    }
                    else
                    {
                        techTreeObject.techResearching = TechTree.Tech.Movement;
                    }
                }
            }
        }

        private void AddMovesHydrogenBuff()
        {
            if (getResources()[0] >= 10)
            {
                tempRandomNumber = random.Next(0, ownedPlanets.Count);
                if (ownedPlanets[tempRandomNumber].planetShips.Count > 0)
                {
                    tempRandomNumber2 = random.Next(0, ownedPlanets[tempRandomNumber].planetShips.Count);
                    ownedPlanets[tempRandomNumber].planetShips[tempRandomNumber2].currentmove += ownedPlanets[tempRandomNumber].planetShips[tempRandomNumber2].getMoves();
                    for (int y = 0; y < tempResources.Length; y++)
                    {
                        tempResources[y] = 0;
                    }
                    tempResources[0] = 10;
                    subResources(tempResources);
                }
            }
        }

        private void MoveShips()
        {
            for (int k = 0; k < ownedPlanets.Count; k++)
            {
                Planet p = ownedPlanets[k];

                int numOfShips = random.Next(0, p.planetShips.Count + 1);
                List<Ship> selectedShips = new List<Ship>();
                for (int i = 0; i < numOfShips; i++)
                {
                    selectedShips.Add(p.planetShips[i]);
                }

                List<Planet> planets = NearbyPlanets(p);
                int planetSel = random.Next(0, planets.Count);

                if (isValidPlanet(planets[planetSel]))
                {
                    foreach (Ship ship in selectedShips)
                    {
                        if (CanShipMove(ship))
                        {
                            moveShip(ship, planets[planetSel], p);

                            if (isNeutral(planets[planetSel]))
                                AddPlanet(planets[planetSel]);
                        }
                    }
                }
                else
                {
                    List<Ship> attacking = new List<Ship>();

                    foreach (Ship ship in selectedShips)
                    {
                        if (CanShipMove(ship))
                        {
                            ship.currentmove--;
                            p.planetShips.Remove(ship);
                            attacking.Add(ship);
                        }
                    }

                    if (Attack(attacking, planets[planetSel].planetShips, planets[planetSel], p))
                    {
                    }

                }
            }
        }

        private void NewUnits()
        {
            foreach(Planet p in ownedPlanets)
            {

                if (random.Next(0, 2) == 0)
                {
                    if (p.shipsQueue.queuedShips.Count == 0)
                    {
                        int index = random.Next(0,shipsAvailableForConstruction.Count);
                        if(shipsAvailableForConstruction[index].getName().Equals("Corvette") || shipsAvailableForConstruction[index].getName().Equals("Destroyer") || shipsAvailableForConstruction[index].getName().Equals("Cruiser"))
                        {
                            if (shipsAvailableForConstruction[index].getName().Equals("Corvette") && getGold() >= 100 && getResources()[3] >= 5)
                            {
                                setGold(getGold() - 100);
                                for(int x = 0; x < tempResources.Length; x++)
                                {
                                    tempResources[x] = 0;
                                }
                                tempResources[3] = 5;
                                subResources(tempResources);
                                p.shipsQueue.queuedShips.Add(shipsAvailableForConstruction[index]);
                            }
                            if (shipsAvailableForConstruction[index].getName().Equals("Destroyer") && getGold() >= 200 && getResources()[3] >= 10 && getResources()[5] >= 10)
                            {
                                setGold(getGold() - 200);
                                for (int x = 0; x < tempResources.Length; x++)
                                {
                                    tempResources[x] = 0;
                                }
                                tempResources[3] = 10;
                                tempResources[5] = 10;
                                subResources(tempResources);
                                p.shipsQueue.queuedShips.Add(shipsAvailableForConstruction[index]);
                            }
                            if (shipsAvailableForConstruction[index].getName().Equals("Cruiser") && getGold() >= 300 && getResources()[3] >= 15 && getResources()[5] >= 15)
                            {
                                setGold(getGold() - 300);
                                for (int x = 0; x < tempResources.Length; x++)
                                {
                                    tempResources[x] = 0;
                                }
                                tempResources[3] = 15;
                                tempResources[5] = 15;
                                subResources(tempResources);
                                p.shipsQueue.queuedShips.Add(shipsAvailableForConstruction[index]);
                            }
                        }
                        else
                        {
                            p.shipsQueue.queuedShips.Add(shipsAvailableForConstruction[index]);
                        }
                    }
                }
            }
        }

        private void Buildings()
        {
            foreach (Planet p in ownedPlanets)
            {
                int i = 0;

                if (random.Next(0, 3) == 0)
                {

                    foreach (BuildingSlot slot in p.buildingSlotsList)
                    {
                        foreach (BuildingQueued building in p.buildingQueue.queuedBuildings)
                        {
                            if (building.buildingSlotIndex == i)
                                continue;
                        }

                        if (slot.typeOfBuilding == BuildingSlot.BuildingType.Empty)
                        {
                            string[] types = { "ResearchFacility", "MilitaryBase", "Factory" };

                            int index = random.Next(0, 3);

                            if (types[index].Equals("Factory") && getResources()[1] >= 5)
                            {
                                p.buildingQueue.AddBuildingToQueue(types[index], i);
                                for (int x = 0; x < tempResources.Length; x++)
                                {
                                    tempResources[x] = 0;
                                }
                                tempResources[1] = 5;
                                subResources(tempResources);
                            }
                            if(types[index].Equals("MilitaryBase") && getResources()[4] >= 5)
                            {
                                p.buildingQueue.AddBuildingToQueue(types[index], i);
                                for (int x = 0; x < tempResources.Length; x++)
                                {
                                    tempResources[x] = 0;
                                }
                                tempResources[4] = 5;
                                subResources(tempResources);
                            }
                            if (types[index].Equals("ResearchFacility") && getResources()[2] >= 5)
                            {
                                p.buildingQueue.AddBuildingToQueue(types[index], i);
                                for (int x = 0; x < tempResources.Length; x++)
                                {
                                    tempResources[x] = 0;
                                }
                                tempResources[2] = 5;
                                subResources(tempResources);
                            }
                            break;
                        }

                        i++;
                    }
                }
            }
        }

    }
}
