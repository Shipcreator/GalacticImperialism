using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticImperialism
{
    [Serializable] class Army
    {
        int attack;
        int defend;
        int move;
        int constructionCost;
        string name;

        public Army(int a, int d, int m, int c, string n) //Sets all Basic Army Attributes
        {
            attack = a; defend = d; move = m; constructionCost = c; name = n;
        }

        public int getAttack()
        {
            return attack;
        }

        public int getDefence()
        {
            return defend;
        }

        public int getMoves()
        {
            return move;
        }

        public int getConCost()
        {
            return constructionCost;
        }

        public String getName()
        {
            return name;
        }

        public String toString()
        {
            String n = name + ":\nAttack:" + attack + "\nDefense: " + defend + "\nMove Speed: " + move + "      Production Cost:" + constructionCost;
            return n;
        }
    }
}
