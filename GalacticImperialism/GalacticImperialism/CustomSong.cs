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
    class CustomSong
    {
        public Song song;

        public string songName;
        public string songCredits;

        public CustomSong(string songName, string songCredits, Song song)
        {
            this.songName = songName;
            this.songCredits = songCredits;
            this.song = song;
        }
    }
}
