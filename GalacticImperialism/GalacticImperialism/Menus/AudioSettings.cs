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
    class AudioSettings
    {
        string titleText;

        SpriteFont fontOfButtons;
        SpriteFont fontOfTitle;

        GraphicsDevice GraphicsDevice;

        Slider masterVolumeSlider;
        Slider musicVolumeSlider;
        Slider soundEffectsVolumeSlider;

        Texture2D sliderBackgroundTexture;
        Texture2D sliderCursorTexture;

        public float masterVolume;
        public float musicVolume;
        public float soundEffectsVolume;

        int readFileLineNumber;

        Button saveSettingsButton;
        Button newSongScrollButton;

        public bool newSongScroll;

        Texture2D unselectedSaveSettingsButtonTexture;
        Texture2D selectedSaveSettingsButtonTexture;
        Texture2D unselectedButtonTexture;
        Texture2D selectedButtonTexture;

        SoundEffect buttonSelectedSoundEffect;

        public AudioSettings(Texture2D unselectedSaveSettingsButtonTexture, Texture2D selectedSaveSettingsButtonTexture, Texture2D unselectedButtonTexture, Texture2D selectedButtonTexture, SpriteFont buttonFont, SpriteFont titleFont, Texture2D sliderBackgroundTexture, Texture2D sliderCursorTexture, GraphicsDevice GraphicsDevice, SoundEffect buttonSelectedSoundEffect)
        {
            this.unselectedSaveSettingsButtonTexture = unselectedSaveSettingsButtonTexture;
            this.selectedSaveSettingsButtonTexture = selectedSaveSettingsButtonTexture;
            this.unselectedButtonTexture = unselectedButtonTexture;
            this.selectedButtonTexture = selectedButtonTexture;
            fontOfButtons = buttonFont;
            fontOfTitle = titleFont;
            this.sliderBackgroundTexture = sliderBackgroundTexture;
            this.sliderCursorTexture = sliderCursorTexture;
            this.GraphicsDevice = GraphicsDevice;
            this.buttonSelectedSoundEffect = buttonSelectedSoundEffect;
            Initialize();
        }

        public void Initialize()
        {
            newSongScroll = true;
            masterVolume = 1.0f;
            soundEffectsVolume = 1.0f;
            musicVolume = 1.0f;
            titleText = "Audio Settings";
            masterVolumeSlider = new Slider(new Rectangle(900, 325, 500, 20), new Vector2(15, 30), sliderBackgroundTexture, sliderCursorTexture);
            musicVolumeSlider = new Slider(new Rectangle(900, 425, 500, 20), new Vector2(15, 30), sliderBackgroundTexture, sliderCursorTexture);
            soundEffectsVolumeSlider = new Slider(new Rectangle(900, 525, 500, 20), new Vector2(15, 30), sliderBackgroundTexture, sliderCursorTexture);
            saveSettingsButton = new Button(new Rectangle(100, GraphicsDevice.Viewport.Height - 150, 100, 100), unselectedSaveSettingsButtonTexture, selectedSaveSettingsButtonTexture, "", fontOfButtons, Color.White, buttonSelectedSoundEffect, null);
            newSongScrollButton = new Button(new Rectangle(450, 600, (1894 / 4), (693 / 4)), unselectedButtonTexture, selectedButtonTexture, "New Song Scroll: On", fontOfButtons, Color.White, buttonSelectedSoundEffect, null);
            readFileLineNumber = 0;
            ReadSettings(@"Content/Saved Settings/Audio Settings.txt");
        }

        private void ReadSettings(string path)
        {
            try
            {
                using(StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (readFileLineNumber == 0)
                        {
                            masterVolumeSlider.SetPercentage(Convert.ToInt32(line.Substring(line.IndexOf('=') + 2, line.Length - (line.IndexOf('=') + 2))) / 100.0f);
                            masterVolume = masterVolumeSlider.percentage;
                        }
                        if(readFileLineNumber == 1)
                        {
                            musicVolumeSlider.SetPercentage(Convert.ToInt32(line.Substring(line.IndexOf('=') + 2, line.Length - (line.IndexOf('=') + 2))) / 100.0f);
                            musicVolume = musicVolumeSlider.percentage;
                        }
                        if (readFileLineNumber == 2)
                        {
                            soundEffectsVolumeSlider.SetPercentage(Convert.ToInt32(line.Substring(line.IndexOf('=') + 2, line.Length - (line.IndexOf('=') + 2))) / 100.0f);
                            soundEffectsVolume = soundEffectsVolumeSlider.percentage;
                        }
                        if(readFileLineNumber == 3)
                        {
                            if (line.Contains("true"))
                                newSongScroll = true;
                            if (line.Contains("false"))
                                newSongScroll = false;
                        }
                        readFileLineNumber++;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("This file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }

        private void WriteSettings(string path)
        {
            StreamWriter myFileOut = new StreamWriter(path, false);
            myFileOut.WriteLine("masterVolumeCursorRect.percentage = " + (int)(masterVolumeSlider.percentage * 100));
            myFileOut.WriteLine("musicVolumeCursorRect.X = " + (int)(musicVolumeSlider.percentage * 100));
            myFileOut.WriteLine("soundEffectsVolumeCursorRect.X = " + (int)(soundEffectsVolumeSlider.percentage * 100));
            if (newSongScroll)
                myFileOut.WriteLine("newSongScroll = true");
            else
                myFileOut.WriteLine("newSongScroll = false");
            myFileOut.Close();
        }

        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse, MouseState oldMouse)
        {
            masterVolumeSlider.Update(mouse, oldMouse);
            masterVolume = masterVolumeSlider.percentage;
            musicVolumeSlider.Update(mouse, oldMouse);
            musicVolume = musicVolumeSlider.percentage;
            soundEffectsVolumeSlider.Update(mouse, oldMouse);
            soundEffectsVolume = soundEffectsVolumeSlider.percentage;
            saveSettingsButton.Update(mouse, oldMouse);
            if (saveSettingsButton.isSelected && saveSettingsButton.wasSelected == false)
                saveSettingsButton.playSelectedSoundEffect(masterVolume * soundEffectsVolume);
            if (saveSettingsButton.isClicked)
                WriteSettings(@"Content/Saved Settings/Audio Settings.txt");
            newSongScrollButton.Update(mouse, oldMouse);
            if (newSongScrollButton.isSelected && newSongScrollButton.wasSelected == false)
                newSongScrollButton.playSelectedSoundEffect(masterVolume * soundEffectsVolume);
            if (newSongScrollButton.isClicked)
                newSongScroll = !newSongScroll;
            if (newSongScroll)
                newSongScrollButton.buttonText = "New Song Scroll: On";
            else
                newSongScrollButton.buttonText = "New Song Scroll: Off";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = fontOfTitle.MeasureString(titleText);
            spriteBatch.DrawString(fontOfTitle, titleText, new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), (GraphicsDevice.Viewport.Height / 6) - (textSize.Y / 2)), Color.White);
            textSize = fontOfButtons.MeasureString("Press Escape To Return To Settings");
            spriteBatch.DrawString(fontOfButtons, "Press Escape To Return To Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), GraphicsDevice.Viewport.Height - textSize.Y), Color.White);
            masterVolumeSlider.Draw(spriteBatch);
            spriteBatch.DrawString(fontOfButtons, "Master Volume:", new Vector2(450, 318), Color.White);
            spriteBatch.DrawString(fontOfButtons, (int)(masterVolume * 100) + "%", new Vector2(1450, 318), Color.White);
            musicVolumeSlider.Draw(spriteBatch);
            spriteBatch.DrawString(fontOfButtons, "Music Volume:", new Vector2(450, 418), Color.White);
            spriteBatch.DrawString(fontOfButtons, (int)(musicVolume * 100) + "%", new Vector2(1450, 418), Color.White);
            soundEffectsVolumeSlider.Draw(spriteBatch);
            spriteBatch.DrawString(fontOfButtons, "Sound Effects Volume:", new Vector2(450, 518), Color.White);
            spriteBatch.DrawString(fontOfButtons, (int)(soundEffectsVolume * 100) + "%", new Vector2(1450, 518), Color.White);
            saveSettingsButton.Draw(spriteBatch);
            textSize = fontOfButtons.MeasureString("Save");
            spriteBatch.DrawString(fontOfButtons, "Save", new Vector2(150 - (textSize.X / 2), GraphicsDevice.Viewport.Height - textSize.Y), Color.White);
            newSongScrollButton.Draw(spriteBatch);
        }
    }
}
