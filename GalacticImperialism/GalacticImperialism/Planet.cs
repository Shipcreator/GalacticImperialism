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
        public int owner; // 0 = no-one, 1-4 = Players 1-4
        public List<string> resources = new List<string>(); //Holds Resources
        public Texture2D tex; //Texture for the Planet

        //Create Base Planets
        public Planet(int s, Texture2D t, Vector2 p, List<string> r)
        {
            size = s;
            tex = t;
            position = p;
            resources = r;
        }

        //Create Starting Planet
        public Planet(Texture2D t, Vector2 p, int playerIndex)
        {
            size = 2;
            tex = t;
            position = p;
            owner = playerIndex;
            resources.Add("iron"); resources.Add("hydrogen");
        }
    }
}
