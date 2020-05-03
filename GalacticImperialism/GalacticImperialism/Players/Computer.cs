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
    [Serializable] class Computer : Player
    {
        public Computer(int startingGold, Board b, Vector3 empireColor) : base(startingGold, b, empireColor)
        {

        }

        public void Update(GameTime gt)
        {
            EndTurn();
        }
    }
}
