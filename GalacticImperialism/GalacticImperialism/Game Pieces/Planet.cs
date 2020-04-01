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
    class Planet
    {
        //Planet Attributes
        public int size;
        public Vector2 position;
        public List<string> resources = new List<string>(); //Holds Resources
        public Texture2D tex; //Texture for the Planet
        public Ship[] fleet; //Holds 3 Ships at Once at a Planet

        //Create Base Planets
        public Planet(int s, Texture2D t, Vector2 p, List<string> r)
        {
            size = s;
            tex = t;
            position = p;
            resources = r;
            fleet = new Ship[3];
        }

        //Create Starting Planet
        public Planet(Texture2D t, Vector2 p)
        {
            size = 2;
            tex = t;
            position = p;
            resources.Add("iron"); resources.Add("hydrogen");
            fleet = new Ship[3];
        }
    }
}
