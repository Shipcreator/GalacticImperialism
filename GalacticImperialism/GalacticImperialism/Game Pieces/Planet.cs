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

        //Create Base Planets
        public Planet(int s, int t, string n, Vector2 p, int[] r)
        {
            size = s;
            texID = t;
            planetName = n;
            position = p;
            resourceNumbers = r;

            managementMenuObject = new PlanetManagementMenu(this);
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

            managementMenuObject = new PlanetManagementMenu(this);
        }
    }
}
