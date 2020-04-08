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
    //Board Class, Incompasses all Non-UI Objects
    class Board
    {
        //Stores All Planets On Map
        List<Planet> planets;
        //Random Object
        Random rand;
        //Sets Radius For Game Movement
        int moveRadius;
        //Holds All Resources
        string[] resourceList = new string[] { "iron", "uranium", "tungsten", "hydrogen", "nitrogen", "oxygen" };
        //PlanetTextures
        List<Texture2D> planetTexs;
        //List Of Players
        List<Player> players;
        //White Circle
        Texture2D circle;
        public Rectangle selection;
        //Current Turn
        int turn;

        //Creates Board, Assigns Texture Objects
        public Board(int r, List<Texture2D> t, Texture2D c)
        {
            planetTexs = t;
            moveRadius = r;
            circle = c;
        }


        //Creates A New Board(Assume numOfBots < numOfPlayers)
        public void NewBoard(int numPlanets, int seed, int numOfPlayers, int numOfBots, int gold)
        {
            turn = 0;
            players = new List<Player>(); //Resets Players

            for (int i = 0; i < numOfBots; i++)
                players.Add(new Computer(gold, this));
            for (int i = 0; i < numOfPlayers - numOfBots; i++)
                players.Add(new Human(gold, this));

            rand = new Random(seed); //Resets Random with New Seed
            do
            {
                planets = new List<Planet>(); //Resets Planets Object
                CreateStartingPlanet(numOfPlayers);
                for (int i = 0; i < numPlanets - numOfPlayers; i++) //Creates Planets
                    CreatePlanet();
            } while (CheckRoutes() == false);

        }

        //Creates Basic Planet
        private void CreatePlanet()
        {
            //Planet Attributes
            Vector2 pos; //Position In Space
            int size = rand.Next(0, 3) + 1; //Size Increments (1,2 or 3)

            do //Loop Assigns Position
            {
                pos = new Vector2(rand.Next(1860), rand.Next(150,1000));
            } while (CheckPos(pos) == false); //Checks Distance

            Planet temp = new Planet(size, planetTexs[rand.Next(0, 19)], pos, AssignResources()); //Creates and Adds Planet to List
            planets.Add(temp);
        }

        //Creates Starting Planet
        private void CreateStartingPlanet(int p)
        {
            if (p >= 2)
            {
                //Create Planet One
                Planet temp = new Planet(planetTexs[rand.Next(0,19)], new Vector2(10, 150));
                planets.Add(temp);
                players[0].AddPlanet(temp);

                //Create Planet Two
                temp = new Planet(planetTexs[rand.Next(0, 19)], new Vector2(1860, 1020));
                planets.Add(temp);
                players[1].AddPlanet(temp);
            }
            if (p >= 3)
            {
                //Create Planet Three
                Planet temp = new Planet(planetTexs[rand.Next(0, 19)], new Vector2(1860, 150));
                planets.Add(temp);
                players[2].AddPlanet(temp);
            }
            if (p == 4)
            {
                //Create Planet Four
                Planet temp = new Planet(planetTexs[rand.Next(0, 19)], new Vector2(10, 1020));
                planets.Add(temp);
                players[3].AddPlanet(temp);
            }
        }

        //Checks Position of Vector Against other Planets
        private bool CheckPos(Vector2 p)
        {
            for (int i = 0; i < planets.Count; i++)
            {
                //Distance Formula
                float x1 = p.X;
                float x2 = planets[i].position.X;
                float y1 = p.Y;
                float y2 = planets[i].position.Y;

                double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

                //To Close
                if (distance < 100)
                    return false;
            }
            //Looks Good
            return true;
        }

        //Checks that all planets are connected(Ignore this unless you really want to see how it works)
        private bool CheckRoutes()
        {
            List<Planet> connected = new List<Planet>();
            connected.Add(planets[0]);

            Planet temp = planets[0];

            do
            {
                bool hasPath = false;
                for (int k = 0; k < planets.Count; k++)
                {
                    //Distance Formula
                    float x1 = planets[k].position.X;
                    float x2 = temp.position.X;
                    float y1 = planets[k].position.Y;
                    float y2 = temp.position.Y;

                    double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

                    if (distance <= moveRadius && connected.IndexOf(planets[k]) == -1)
                    {
                        temp = planets[k];
                        connected.Add(planets[k]);
                        hasPath = true;
                        break;
                    }
                }

                if (hasPath == false)
                {
                    if (temp.Equals(planets[0]))
                    {
                        return false;
                    }
                    temp = connected[connected.IndexOf(temp) - 1];
                }

            } while (connected.Count != planets.Count);
            return true;
        }

        //Gets List of Resources for A planet
        private List<string> AssignResources()
        {
            int numOfResources = rand.Next(1, 5); //How many resources on a planet 1-4
            List<string> resources = new List<string>();

            do
            {
                int index = rand.Next(0, 6); // Index Of ResourceList Array

                if (resources.IndexOf(resourceList[index]) == -1) //Checks to make sure the resource is not already in use.
                {
                    resources.Add(resourceList[index]); //Adds resource to array
                }

            } while (resources.Count != numOfResources); //Keeps running until there are enough resources.
            return resources; //Returns List
        }

        //Handles Game Updates
        public void Update(GameTime gt)
        {
            if (players[turn] is Human)
            {
                Human temp = (Human)players[turn];
                temp.Update(gt);
            }
            if (players[turn] is Computer)
            {
                Computer temp = (Computer)players[turn];
                temp.Update(gt);
            }
        }

        //NextTurn Handling
        public void NextTurn()
        {
            //Reset Functions
            players[turn].isTurn = false;
            selection = new Rectangle(0,0,0,0);

            //Goes to Next Player
            if (turn == players.Count - 1)
                turn = 0;
            else
                turn++;

            players[turn].isTurn = true;

            //Runs OnTurn Function
            if (players[turn] is Human)
            {
                Human temp = (Human)players[turn];
                temp.OnTurn();
            }
            if (players[turn] is Computer)
            {
                Computer temp = (Computer)players[turn];
                temp.OnTurn();
            }

            //Send the Board Class
        }

        //Board Draw Method
        public void Draw(SpriteBatch sb)
        {
            //Draw Selection
            sb.Draw(circle, selection, Color.White);

            //Draws Planets
            foreach (Planet p in planets)
            {
                Rectangle tempRect = new Rectangle((int)p.position.X, (int)p.position.Y, p.size * 25, p.size * 25);
                sb.Draw(p.tex, tempRect, Color.White);
            }
        }
    }
}
