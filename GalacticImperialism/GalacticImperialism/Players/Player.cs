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
    [Serializable] class Player
    {
        int gold, science, hydrogen, oxygen, nitrogen, iron, tungsten, uranium; //All Item Values That every Player stores
        public List<Planet> ownedPlanets = new List<Planet>(); //Owned Planets
        public List<Ship> ships = new List<Ship>();
        public List<Ship> shipsAvailableForConstruction;

        protected Board board; // Holds Current Board

        public Color empireColor;

        public Flag empireFlag;
        public int[] resourcesPerTurn = new int[6];
        public int goldPerTurn = 0;
        public int sciencePerTurn = 0;

        //Creates Base Player
        public Player(int startingGold, Board b, Vector3 playerEmpireColor, Flag playerEmpireFlag)
        {
            gold = startingGold;
            science = hydrogen = oxygen = nitrogen = iron = tungsten = uranium = 0;
            board = b;
            empireColor = new Color((int)playerEmpireColor.X, (int)playerEmpireColor.Y, (int)playerEmpireColor.Z);
            empireFlag = playerEmpireFlag;

            shipsAvailableForConstruction = new List<Ship>();
            shipsAvailableForConstruction.Add(new Ship(1, 1, 5, 1200, "Corvette"));
            shipsAvailableForConstruction.Add(new Ship(2, 2, 3, 2000, "Destroyer"));
            shipsAvailableForConstruction.Add(new Ship(3, 4, 3, 3600, "Cruiser"));
        }


        //Gets all Nearby Planets
        public List<Planet> NearbyPlanets(Planet p)
        {
            List<Planet> planetList = new List<Planet>();
            for (int i = 0; i < board.planets.Count; i++)
            {
                //Distance Formula
                float x1 = p.position.X;
                float x2 = board.planets[i].position.X;
                float y1 = p.position.Y;
                float y2 = board.planets[i].position.Y;

                double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

                if (distance <= 200)
                    planetList.Add(board.planets[i]);
            }
            return planetList;
        }

        //Precondition-Planet is valid and Ship has points left
        //Ship will have moved to new planet
        public void moveShip(Ship s, Planet arrival, Planet destination)
        {
            arrival.planetShips.Add(s);
            destination.planetShips.Remove(s);

            if (!ownedPlanets.Contains(destination))
                ownedPlanets.Add(destination);

            s.currentmove--;
        }

        //Checks if you own the planet or it is neutral
        public bool isValidPlanet(Planet selectedplanet)
        {
            List<Planet> planets = new List<Planet>();

            foreach (Planet planet in board.planets)
            {
                planets.Add(planet);
            }

            foreach (Player player in board.players)
            {
                foreach (Planet p in player.ownedPlanets)
                {
                    planets.Remove(p);
                }
            }

            if (ownedPlanets.Contains(selectedplanet) || planets.Contains(selectedplanet))
                return true;
            else
                return false;
        }


        //Checks For Planet Neutrality
        public bool isNeutral(Planet selectedplanet)
        {
            List<Planet> planets = new List<Planet>();

            foreach (Planet planet in board.planets)
            {
                planets.Add(planet);
            }

            foreach (Player player in board.players)
            {
                foreach (Planet p in player.ownedPlanets)
                {
                    planets.Remove(p);
                }
            }

            if (planets.Contains(selectedplanet))
                return true;
            else
                return false;
        }



        //Ship Movement points
        public bool CanShipMove(Ship s)
        {
            if (s.currentmove > 0)
                return true;
            else
                return false;
        }

        //Preforms Attacking Think Axis and Allies
        public bool Attack(List<Ship> attack, List<Ship> defense, Planet p, Planet surrender)
        {
            Random die = new Random();
            Player defender = null;
            int ahits = 0;
            int dhits = 0;

            //Gets Defender
            foreach (Player player in board.players)
            {
                if (player.ownedPlanets.Contains(p))
                    defender = player;
            }

            if (!defender.Equals(null))
            {
                //Gets Number Of Hits
                foreach (Ship aship in attack)
                {
                    int roll = die.Next(1, 21);
                    if (roll <= aship.getAttack())
                    {
                        ahits++;
                    }
                }

                foreach (Ship dship in defense)
                {
                    int roll = die.Next(1, 21);
                    if (roll <= dship.getDefence())
                    {
                        dhits++;
                    }
                }

                //Removes Ships
                for (int i = 1; i <= ahits; i++)
                {
                    if (defense.Count > 0)
                    {
                        int index = die.Next(0, defense.Count);
                        Ship temp = defense[index];
                        defense.Remove(temp);
                        defender.ships.Remove(temp);
                        p.planetShips.Remove(temp);
                    }
                }
                for (int i = 1; i <= dhits; i++)
                {
                    if (attack.Count > 0)
                    {
                        int index = die.Next(0, attack.Count);
                        Ship temp = attack[index];
                        attack.Remove(temp);
                        this.ships.Remove(temp);
                    }
                }

                //Takeover Planet
                if (defense.Count <= 0 && attack.Count > 0)
                {
                    defender.ownedPlanets.Remove(p);
                    this.ownedPlanets.Add(p);
                    foreach (Ship ship in attack)
                    {
                        p.planetShips.Add(ship);
                    }
                    return true;
                }
            }
            return false;
        }


        //Adds Ownership of a Planet
        public void AddPlanet(Planet p)
        {
            ownedPlanets.Add(p);
            addResourcesPerTurn(p.resourceNumbers);
        }
        //Removes Ownership of a Planet
        public void RemovePlanet(Planet p)
        {
            ownedPlanets.Remove(p);
        }
        public void AddShip(Ship s)
        {
            ships.Add(s);
        }
        public void RemoveShip(Ship s)
        {
            ships.Remove(s);
        }

        public void Update(GameTime gt) //General Update
        {

        }

        public void OnTurn() //General On Turn Start 
        {
            addResources(resourcesPerTurn);
            gold += goldPerTurn;
            science += sciencePerTurn;
            foreach (Ship s in ships)
            {
                s.currentmove = s.getMoves();
            }
        }

        public void EndTurn() //Called On Turn End
        {
            PlayerUI.closeMenus();
            PlayerUI.shipsSelected = new List<Ship>();
            /*for (int i = 0; i < ownedPlanets.Count;i++)
            {
                ownedPlanets[i].unitProduction.productionAdd();
            }*/
            board.NextTurn();
        }

        //Getters And Setters Below
        public int getGold()
        {
            return gold;
        }
        public int getScience()
        {
            return science;
        }
        public int[] getResources()
        {
            return new int[] {hydrogen, oxygen, nitrogen, iron, tungsten, uranium}; // Order of Resources Get and Set
        }

        public void setGold(int g)
        {
            gold = g;
        }
        public void setScience(int s)
        {
            science = s;
        }
        public void addResources(int[] r)
        {
            hydrogen += r[0]; oxygen += r[1]; nitrogen += r[2]; iron += r[3]; tungsten += r[4]; uranium += r[5]; //Sets All Resource Back
        }
        public void subResources(int[] r)
        {
            hydrogen -= r[0]; oxygen -= r[1]; nitrogen -= r[2]; iron -= r[3]; tungsten -= r[4]; uranium -= r[5]; //Sets All Resource Back
        }

        //For ResourcesPerTurn
        public void addResourcesPerTurn(int[] r)
        {
            for (int i = 0; i < 6; i++)
            {
                resourcesPerTurn[i] += r[i];
            }
        }
        public void subResourcesPerTurn(int[] r)
        {
            for (int i = 0; i < 6; i++)
            {
                resourcesPerTurn[i] -= r[i];
            }
        }

    }
}
