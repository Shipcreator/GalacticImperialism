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
using System.IO;

namespace GalacticImperialism
{
    class MusicPlayer
    {
        enum NewSongDeterminationMode
        {
            Shuffle
        }
        NewSongDeterminationMode mode;

        string path;

        GraphicsDevice GraphicsDevice;

        List<CustomSong> songList;

        ContentManager Content;

        int indexOfSongPlaying;
        int indexOfPreviousSong;
        List<int> songsQueued;

        Random rnd;

        int random;
        bool canContinueWithRandom;

        public MusicPlayer(string textFilePath, ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            path = textFilePath;
            this.Content = Content;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            mode = NewSongDeterminationMode.Shuffle;
            songList = new List<CustomSong>();
            songsQueued = new List<int>();
            indexOfSongPlaying = 0;
            indexOfPreviousSong = 0;
            random = 0;
            canContinueWithRandom = true;
            rnd = new Random();
            ReadInSongs();
        }

        private void ReadInSongs()
        {
            try
            {
                using(StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string songName = line.Substring(0, line.IndexOf('|') - 1);
                        string songCredits = line.Substring(line.IndexOf('|') + 2, line.IndexOf('|', line.IndexOf('|') + 1) - 1 - (line.IndexOf('|') + 2));
                        Song song = Content.Load<Song>(line.Substring(line.IndexOf('"') + 1, line.IndexOf('"', line.IndexOf('"') + 1) - (line.IndexOf('"') + 1)));
                        songList.Add(new CustomSong(songName, songCredits, song));
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error reading in songs: ");
                Console.WriteLine(e.Message);
            }
        }

        private void FillUpSongQueue()
        {
            if(mode == NewSongDeterminationMode.Shuffle)
            {
                for(int x = 0; x < songList.Count; x++)
                {
                    for(int y = 0; y > -1; y++)
                    {
                        random = rnd.Next(0, songList.Count);
                        canContinueWithRandom = true;
                        if(x == 0)
                        {
                            if (random != indexOfPreviousSong)
                            {
                                songsQueued.Add(random);
                                //Console.WriteLine(random);
                                break;
                            }
                        }
                        else
                        {
                            for(int z = 0; z < songsQueued.Count; z++)
                            {
                                if (random == songsQueued[z])
                                    canContinueWithRandom = false;
                            }
                            if (canContinueWithRandom == false)
                                continue;
                            songsQueued.Add(random);
                            //Console.WriteLine(random);
                            break;
                        }
                    }
                }
            }
        }

        public void Update(float masterVolume, float musicVolume)
        {
            MediaPlayer.Volume = masterVolume * musicVolume;
            if(MediaPlayer.State == MediaState.Stopped)
            {
                indexOfPreviousSong = indexOfSongPlaying;
                if(mode == NewSongDeterminationMode.Shuffle)
                {
                    if(songsQueued.Count < 1)
                    {
                        FillUpSongQueue();
                    }
                    MediaPlayer.Play(songList[songsQueued[0]].song);
                    songsQueued.Remove(songsQueued[0]);
                }
                /*Console.WriteLine();
                for(int x = 0; x < songsQueued.Count; x++)
                {
                    Console.WriteLine(songsQueued[x]);
                }*/
            }
        }
    }
}
