using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GalacticImperialism.Components;

namespace GalacticImperialism
{
    public class Tech
    {

        // Static tech variables
        public string name;
        public string description;
        public TechEnum type;
        public int cost;
        public float modifier;

        // Dynamic tech variables
        public int numResearch;
        public bool complete;
        public bool researching;

        public Tech(string n, string d, int c, TechEnum t, float m)
        {
            name = n;
            description = d;
            cost = c;
            type = t;
            modifier = m;

            numResearch = 0;
            complete = false;
            researching = false;
        }

        public void Update(int science)
        {
            numResearch += science;

            if (cost <= numResearch)
            {
                complete = true;
            }
        }

        public bool isDone()
        {
            return complete;
        }

        public void setResearching(bool r)
        {
            researching = r;
        }

        public String ToString()
        {
            return name + " " + description + " " + type + " " + cost + " " + modifier;
        }
    }
}