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

namespace GalacticImperialism
{
    [Serializable] class FlagDataBase
    {
        int numberOfFlagsAdded;

        List<string> matchingList;

        List<Flag> empireFlags;

        public FlagDataBase()
        {
            numberOfFlagsAdded = 0;
            matchingList = new List<string>();
            empireFlags = new List<Flag>();
        }

        public void AddFlag(int playerID, Flag empireFlag)
        {
            empireFlags.Add(empireFlag);
            matchingList.Add(playerID + " " + numberOfFlagsAdded);
            numberOfFlagsAdded++;
        }

        public Flag GetFlag(int playerID)
        {
            for(int x = 0; x < matchingList.Count; x++)
            {
                if (playerID == Convert.ToInt32(matchingList[x].Substring(0, matchingList[x].IndexOf(' '))))
                    return empireFlags[Convert.ToInt32(matchingList[x].Substring(matchingList[x].IndexOf(' ') + 1, matchingList[x].Length - (matchingList[x].IndexOf(' ') + 1)))];
            }
            return empireFlags[0];
        }
    }
}
