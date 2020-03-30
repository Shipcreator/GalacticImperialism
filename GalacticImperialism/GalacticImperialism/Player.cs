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
    class Player
    {
        int gold, science, hydrogen, oxygen, nitrogen, iron, tungsten, uranium; //All Item Values That every Player stores

        //Creates Base Player
        public Player(int startingGold)
        {
            gold = startingGold;
            science = hydrogen = oxygen = nitrogen = iron = tungsten = uranium = 0;
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
