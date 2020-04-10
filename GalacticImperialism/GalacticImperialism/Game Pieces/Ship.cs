using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticImperialism
{
    [Serializable] class Ship
    {
        int attack;
        int defend;
        int move;
        string name;

        public Ship(int a, int d, int m, string n) //Sets all Basic Ship Attributes
        {
            attack = a; defend = d; move = m; name = n;
        }
    }
}
