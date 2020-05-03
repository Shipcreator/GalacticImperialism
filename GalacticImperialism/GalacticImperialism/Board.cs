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
using GalacticImperialism.Networking;
using Lidgren.Network;
using System.IO;

namespace GalacticImperialism
{
    //Board Class, Incompasses all Non-UI Objects
    [Serializable] class Board
    {
        //Stores All Planets On Map
        public List<Planet> planets;
        //Random Object
        Random rand;
        //Sets Radius For Game Movement
        int moveRadius;
        //Holds All Resources
        string[] resourceList = new string[] { "iron", "uranium", "tungsten", "hydrogen", "nitrogen", "oxygen" };
        //List Of Players
        public List<Player> players;
        // Number of bots
        public int numBots;

        //Data base of flags that links them to a player ID
        public FlagDataBase flagDataBaseObject;
        //Current Turn
        public int turn;

        //List of all planet names, named loaded in from a text file.
        List<string> planetNames;
        //List of all indexes of the planetNames list that has already been applied to a planet.
        List<int> planetNamesUsedIndexes;
        int randomPlanetNumber;
        bool breakLoop;

        //Creates Board, Assigns Texture Objects
        public Board(int r)
        {
            moveRadius = r;
        }

        //Creates A New Board(Assume numOfBots < numOfPlayers)
        public void NewBoard(int numPlanets, int seed, int numOfPlayers, int numOfBots, int gold)
        {
            planetNames = new List<string>();
            planetNamesUsedIndexes = new List<int>();
            randomPlanetNumber = 0;
            breakLoop = true;
            ReadInPlanetNames(@"Content/Planets/Planet Names.txt");

            turn = 0;
            players = new List<Player>(); //Resets Players
            numBots = numOfBots;

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

            flagDataBaseObject = new FlagDataBase();
        }

        private void ReadInPlanetNames(string path)
        {
            try
            {
                using(StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        planetNames.Add(line);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("The planet names file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }

        //Creates Basic Planet
        private void CreatePlanet()
        {
            //Planet Attributes
            Vector2 pos; //Position In Space
            int size = rand.Next(0, 3) + 1; //Size Increments (1,2 or 3)

            do //Loop Assigns Position
            {
                pos = new Vector2(rand.Next(1860), rand.Next(75,1000));
            } while (CheckPos(pos) == false); //Checks Distance

            if (planetNamesUsedIndexes.Count > 0)
            {
                for (int x = 0; x > -1; x++)
                {
                    breakLoop = true;
                    randomPlanetNumber = rand.Next(0, planetNames.Count);
                    for (int y = 0; y < planetNamesUsedIndexes.Count; y++)
                    {
                        if (planetNamesUsedIndexes[y] == randomPlanetNumber)
                            breakLoop = false;
                    }
                    if (breakLoop)
                        break;
                }
            }
            else
                randomPlanetNumber = rand.Next(0, planetNames.Count);
            planetNamesUsedIndexes.Add(randomPlanetNumber);
            Planet temp = new Planet(size, rand.Next(0, 19), planetNames[randomPlanetNumber], pos, AssignResources()); //Creates and Adds Planet to List
            planets.Add(temp);
        }

        //Creates Starting Planet
        private void CreateStartingPlanet(int p)
        {
            if (p >= 2)
            {
                //Create Planet One
                if (planetNamesUsedIndexes.Count > 0)
                {
                    for (int x = 0; x > -1; x++)
                    {
                        breakLoop = true;
                        randomPlanetNumber = rand.Next(0, planetNames.Count);
                        for (int y = 0; y < planetNamesUsedIndexes.Count; y++)
                        {
                            if (planetNamesUsedIndexes[y] == randomPlanetNumber)
                                breakLoop = false;
                        }
                        if (breakLoop)
                            break;
                    }
                }
                else
                    randomPlanetNumber = rand.Next(0, planetNames.Count);
                planetNamesUsedIndexes.Add(randomPlanetNumber);
                Planet temp = new Planet(rand.Next(0,19), planetNames[randomPlanetNumber], new Vector2(10, 75));
                planets.Add(temp);
                players[0].AddPlanet(temp);

                //Create Planet Two
                if (planetNamesUsedIndexes.Count > 0)
                {
                    for (int x = 0; x > -1; x++)
                    {
                        breakLoop = true;
                        randomPlanetNumber = rand.Next(0, planetNames.Count);
                        for (int y = 0; y < planetNamesUsedIndexes.Count; y++)
                        {
                            if (planetNamesUsedIndexes[y] == randomPlanetNumber)
                                breakLoop = false;
                        }
                        if (breakLoop)
                            break;
                    }
                }
                else
                    randomPlanetNumber = rand.Next(0, planetNames.Count);
                planetNamesUsedIndexes.Add(randomPlanetNumber);
                temp = new Planet(rand.Next(0, 19), planetNames[randomPlanetNumber], new Vector2(1860, 1020));
                planets.Add(temp);
                players[1].AddPlanet(temp);
            }
            if (p >= 3)
            {
                //Create Planet Three
                if (planetNamesUsedIndexes.Count > 0)
                {
                    for (int x = 0; x > -1; x++)
                    {
                        breakLoop = true;
                        randomPlanetNumber = rand.Next(0, planetNames.Count);
                        for (int y = 0; y < planetNamesUsedIndexes.Count; y++)
                        {
                            if (planetNamesUsedIndexes[y] == randomPlanetNumber)
                                breakLoop = false;
                        }
                        if (breakLoop)
                            break;
                    }
                }
                else
                    randomPlanetNumber = rand.Next(0, planetNames.Count);
                planetNamesUsedIndexes.Add(randomPlanetNumber);
                Planet temp = new Planet(rand.Next(0, 19), planetNames[randomPlanetNumber], new Vector2(1860, 75));
                planets.Add(temp);
                players[2].AddPlanet(temp);
            }
            if (p == 4)
            {
                //Create Planet Four
                if (planetNamesUsedIndexes.Count > 0)
                {
                    for (int x = 0; x > -1; x++)
                    {
                        breakLoop = true;
                        randomPlanetNumber = rand.Next(0, planetNames.Count);
                        for (int y = 0; y < planetNamesUsedIndexes.Count; y++)
                        {
                            if (planetNamesUsedIndexes[y] == randomPlanetNumber)
                                breakLoop = false;
                        }
                        if (breakLoop)
                            break;
                    }
                }
                else
                    randomPlanetNumber = rand.Next(0, planetNames.Count);
                planetNamesUsedIndexes.Add(randomPlanetNumber);
                Planet temp = new Planet(rand.Next(0, 19), planetNames[randomPlanetNumber], new Vector2(10, 1020));
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
        public void Update(GameTime gt, MouseState mb, KeyboardState kb, MouseState oldms, KeyboardState oldkb)
        {
            if (players[turn] is Human && turn == Game1.playerID)
            {
                Human temp = (Human)players[turn];
                temp.Update(gt, mb, kb, oldms, oldkb);
            }
            else if (players[turn] is Computer)
            {
                Computer temp = (Computer)players[turn];
                temp.Update(gt);
            }
        }

        //NextTurn Handling
        public void NextTurn()
        {

            //Goes to Next Player
            if (turn == players.Count - 1)
                turn = 0;
            else
                turn++;

            //Runs OnTurn Function
            if (players[turn] is Human)
            {
                Human temp = (Human)players[turn];
                temp.OnTurn();
            }
            else if (players[turn] is Computer)
            {
                Computer temp = (Computer)players[turn];
                temp.OnTurn();
            }

            //Send the Board Class
            ConnectionHandler connection = Game1.connection;

            if (connection.getCon().ConnectionsCount > 0)
            {
                NetOutgoingMessage msg = connection.getCon().CreateMessage();
                msg.Write(connection.SerializeData(this));

                foreach (NetConnection con in connection.getCon().Connections)
                {
                    connection.getCon().SendMessage(msg, con, NetDeliveryMethod.ReliableOrdered);
                }
            }
        }

        //Board Draw Method
        public void Draw(SpriteBatch sb)
        {

            //Draws Planets
            foreach (Planet p in planets)
            {
                Rectangle tempRect = new Rectangle((int)p.position.X, (int)p.position.Y, p.size * 25, p.size * 25);
                sb.Draw(Game1.planetTex[p.texID], tempRect, Color.White);
            }
        }
    }
}
