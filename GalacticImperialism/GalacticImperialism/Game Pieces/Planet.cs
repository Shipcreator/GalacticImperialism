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
        public Vector2 position;
        public List<string> resources = new List<string>(); //Holds Resources
        public int texID; //Texture for the Planet
        public List<Ship> planetShips = new List<Ship>();

        //Create Base Planets
        public Planet(int s, int t, Vector2 p, List<string> r)
        {
            size = s;
            texID = t;
            position = p;
            resources = r;
        }

        //Create Starting Planet
        public Planet(int t, Vector2 p)
        {
            size = 2;
            texID = t;
            position = p;
            resources.Add("iron"); resources.Add("hydrogen");
        }
    }
}
