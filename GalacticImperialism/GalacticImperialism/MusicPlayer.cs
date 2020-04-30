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
            Shuffle,
            Random
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
        int newSongScrollSpeed;

        bool canContinueWithRandom;
        public bool newSongScroll;

        string newSongScrollText;

        Vector2 textSize;
        Vector2 textPosition;

        SpriteFont newSongScrollFont;

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
            indexOfSongPlaying = -1;
            indexOfPreviousSong = -1;
            random = 0;
            newSongScrollSpeed = 6;
            canContinueWithRandom = true;
            newSongScroll = true;
            newSongScrollText = "";
            textSize = new Vector2(0, 0);
            textPosition = new Vector2(0, 0);
            rnd = new Random();
            newSongScrollFont = Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point");
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
                    newSongScrollText = songList[songsQueued[0]].songName + " - " + songList[songsQueued[0]].songCredits;
                    songsQueued.Remove(songsQueued[0]);
                }
                if(mode == NewSongDeterminationMode.Random)
                {
                    for(int x = 0; x > -1; x++)
                    {
                        random = rnd.Next(0, songList.Count);
                        if(random != indexOfPreviousSong)
                        {
                            MediaPlayer.Play(songList[random].song);
                            newSongScrollText = songList[random].songName + " - " + songList[random].songCredits;
                            break;
                        }
                    }
                }
                textSize = newSongScrollFont.MeasureString(newSongScrollText);
                textPosition.X = GraphicsDevice.Viewport.Width;
                textPosition.Y = 0;
            }
            if (textPosition.X > textSize.X * -1)
                textPosition.X -= newSongScrollSpeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (newSongScroll == true)
                spriteBatch.DrawString(newSongScrollFont, newSongScrollText, textPosition, Color.White);
        }
    }
}
