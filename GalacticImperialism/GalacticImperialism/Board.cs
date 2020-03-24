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

namespace ProcedualGenTest
{
    //Board Class, Incompasses all Non-UI Objects
    class Board
    {
        //Stores All Planets On Map
        List<Planet> planets;
        //Random Object
        Random rand;
        //Temporary PlanetTexture
        Texture2D planetTex;
        //Sets Radius For Game Movement
        int moveRadius;

        //Creates Board, Assigns Texture Objects
        public Board(Texture2D t, int r)
        {
            planetTex = t;
            moveRadius = r;
        }


        //Creates A New Board
        public void NewBoard(int numPlanets, int seed, int numOfPlayers)
        {
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
            Color color = new Color(rand.Next(256), rand.Next(256), rand.Next(256)); //Random Color(Will Change Later)

            do //Loop Assigns Position
            {
                pos = new Vector2(rand.Next(1825), rand.Next(1000));
            } while (CheckPos(pos) == false); //Checks Distance

            Planet temp = new Planet(size, color, pos); //Creates and Adds Planet to List
            planets.Add(temp);
        }

        //Creates Starting Planet
        private void CreateStartingPlanet(int players)
        {
            if (players >= 2)
            {
                //Create Planet One
                Planet temp = new Planet(Color.Red, new Vector2(10, 10), 1);
                planets.Add(temp);

                //Create Planet Two
                temp = new Planet(Color.Blue, new Vector2(1860, 1020), 2);
                planets.Add(temp);
            }
            if (players >= 3)
            {
                //Create Planet Three
                Planet temp = new Planet(Color.Yellow, new Vector2(1860, 10), 3);
                planets.Add(temp);
            }
            if (players == 4)
            {
                //Create Planet Four
                Planet temp = new Planet(Color.Green, new Vector2(10, 1020), 4);
                planets.Add(temp);
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

        //Checks that all planets are connected
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

        //Board Draw Method
        public void Draw(SpriteBatch sb)
        {
            //Draws Planets
            foreach (Planet p in planets)
            {
                Rectangle tempRect = new Rectangle((int)p.position.X, (int)p.position.Y, p.size * 25, p.size * 25);
                sb.Draw(planetTex, tempRect, p.planetColor);
            }
        }
    }
}
