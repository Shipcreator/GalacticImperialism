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

        protected Board board; // Holds Current Board

        public Color empireColor;

        public Flag empireFlag;

        //Creates Base Player
        public Player(int startingGold, Board b, Vector3 playerEmpireColor, Flag playerEmpireFlag)
        {
            gold = startingGold;
            science = hydrogen = oxygen = nitrogen = iron = tungsten = uranium = 0;
            board = b;
            empireColor = new Color((int)playerEmpireColor.X, (int)playerEmpireColor.Y, (int)playerEmpireColor.Z);
            empireFlag = playerEmpireFlag;
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
            List<Planet> planets = board.planets;

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

        //Ship Movement points
        public bool CanShipMove(Ship s)
        {
            if (s.currentmove > 0)
                return true;
            else
                return false;
        }


        public void Attack()
        {

        }


        //Adds Ownership of a Planet
        public void AddPlanet(Planet p)
        {
            ownedPlanets.Add(p);
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
        public void AddArmy(Army a)
        {
            armies.Add(a);
        }
        public void RemoveArmy(Army a)
        {
            armies.Remove(a);
        }

        public void Update(GameTime gt) //General Update
        {

        }

        public void OnTurn() //General On Turn Start 
        {
            foreach (Ship s in ships)
            {
                s.currentmove = s.getMoves();
            }
        }

        public void EndTurn() //Called On Turn End
        {
            PlayerUI.closeMenus();
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
            r[0] += hydrogen; r[1] += oxygen; r[2] += nitrogen; r[3] += iron; r[4] += tungsten; r[5] += uranium; //Sets All Resource Back
        }
        public void subResources(int[] r)
        {
            r[0] -= hydrogen; r[1] -= oxygen; r[2] -= nitrogen; r[3] -= iron; r[4] -= tungsten; r[5] -= uranium; //Sets All Resource Back
        }
    }
}
