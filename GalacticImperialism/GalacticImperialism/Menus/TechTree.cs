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
using GalacticImperialism.Networking;
using Lidgren.Network;
using GalacticImperialism.Components;

namespace GalacticImperialism
{
    [Serializable] class TechTree
    {
        public enum Tech
        {
            None,
            Attack,
            Defense,
            Movement
        }
        public Tech techResearching;

        public int attackTechsResearched;
        public int defenseTechsResearched;
        public int movementTechsResearched;

        public int science;

        public List<string> newTechsResearched;

        public TechTree()
        {
            Initialize();
        }

        public void Initialize()
        {
            attackTechsResearched = 0;
            defenseTechsResearched = 0;
            movementTechsResearched = 0;

            science = 0;

            techResearching = Tech.None;

            newTechsResearched = new List<string>();
        }

        public void EndTurn(int amountOfScience)
        {
            science += amountOfScience;
            if(techResearching == Tech.Attack)
            {
                if(science >= (attackTechsResearched + 1) * 300)
                {
                    science -= (attackTechsResearched + 1) * 300;
                    attackTechsResearched++;
                    newTechsResearched.Add("Attack");
                    techResearching = Tech.None;
                }
            }
            if(techResearching == Tech.Defense)
            {
                if(science >= (defenseTechsResearched + 1) * 300)
                {
                    science -= (defenseTechsResearched + 1) * 300;
                    defenseTechsResearched++;
                    newTechsResearched.Add("Defense");
                    techResearching = Tech.None;
                }
            }
            if(techResearching == Tech.Movement)
            {
                if(science >= (movementTechsResearched + 1) * 300)
                {
                    science -= (movementTechsResearched + 1) * 300;
                    movementTechsResearched++;
                    newTechsResearched.Add("Movement");
                    techResearching = Tech.None;
                }
            }
        }
    }
}
