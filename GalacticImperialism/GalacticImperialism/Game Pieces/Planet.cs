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
        public List<string> resources = new List<string>(); //Holds Resources
        public int texID; //Texture for the Planet
        public List<Ship> planetShips = new List<Ship>();
        public PlanetManagementMenu managementMenuObject;       //Pop-up menu that allows the player to manage the building and resources of a planet.

        //Create Base Planets
        public Planet(int s, int t, string n, Vector2 p, List<string> r)
        {
            size = s;
            texID = t;
            planetName = n;
            position = p;
            resources = r;

            managementMenuObject = new PlanetManagementMenu(this);
        }

        //Create Starting Planet
        public Planet(int t, string n, Vector2 p)
        {
            size = 2;
            texID = t;
            planetName = n;
            position = p;
            resources.Add("iron"); resources.Add("hydrogen");

            managementMenuObject = new PlanetManagementMenu(this);
        }
    }
}
