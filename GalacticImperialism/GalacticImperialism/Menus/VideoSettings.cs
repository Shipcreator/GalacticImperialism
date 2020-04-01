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
    class VideoSettings
    {
        string titleText;

        SpriteFont fontOfButtons;
        SpriteFont fontOfTitle;

        GraphicsDevice GraphicsDevice;

        public bool fullScreenOn;
        public bool toggleFullScreen;

        List<Button> buttonsList;

        Texture2D unselectedButtonTexture;
        Texture2D selectedButtonTexture;
        Texture2D unselectedSaveSettingsButtonTexture;
        Texture2D selectedSaveSettingsButtonTexture;

        Button saveSettingsButton;

        int readFileLineNumber;

        public VideoSettings(Texture2D unselectedSaveSettingsButtonTexture, Texture2D selectedSaveSettingsButtonTexture, Texture2D unselectedButtonTexture, Texture2D selectedButtonTexture, SpriteFont buttonFont, SpriteFont titleFont, GraphicsDevice GraphicsDevice)
        {
            this.unselectedSaveSettingsButtonTexture = unselectedSaveSettingsButtonTexture;
            this.selectedSaveSettingsButtonTexture = selectedSaveSettingsButtonTexture;
            this.unselectedButtonTexture = unselectedButtonTexture;
            this.selectedButtonTexture = selectedButtonTexture;
            fontOfButtons = buttonFont;
            fontOfTitle = titleFont;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            titleText = "Video Settings";
            buttonsList = new List<Button>();
            buttonsList.Add(new Button(new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((1894 / 4) / 2), 350, (1894 / 4), (693 / 4)), unselectedButtonTexture, selectedButtonTexture, "Fullscreen: ", fontOfButtons, Color.White, null, null));
            saveSettingsButton = new Button(new Rectangle(100, GraphicsDevice.Viewport.Height - 150, 100, 100), unselectedSaveSettingsButtonTexture, selectedSaveSettingsButtonTexture, "", fontOfButtons, Color.White, null, null);
            toggleFullScreen = false;
            fullScreenOn = false;
            readFileLineNumber = 0;
            ReadSettings(@"Content/Saved Settings/Video Settings.txt");
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
                        if(readFileLineNumber == 0)
                        {
                            if (line.Contains("false"))
                                toggleFullScreen = false;
                            if (line.Contains("true"))
                            {
                                toggleFullScreen = true;
                                fullScreenOn = !fullScreenOn;
                            }
                        }
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
            myFileOut.WriteLine("Fullscreen = ");
            if (fullScreenOn)
                myFileOut.Write("true");
            else
                myFileOut.Write("false");
            myFileOut.Close();
        }

        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse, MouseState oldMouse)
        {
            toggleFullScreen = false;

            for(int x = 0; x < buttonsList.Count; x++)
            {
                buttonsList[x].Update(mouse, oldMouse);
            }

            if (buttonsList[0].isClicked)
            {
                toggleFullScreen = true;
                fullScreenOn = !fullScreenOn;
            }

            if (fullScreenOn)
                buttonsList[0].buttonText = "Fullscreen: On";
            else
                buttonsList[0].buttonText = "Fullscreen: Off";

            saveSettingsButton.Update(mouse, oldMouse);
            if (saveSettingsButton.isClicked)
                WriteSettings(@"Content/Saved Settings/Video Settings.txt");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < buttonsList.Count; x++)
            {
                buttonsList[x].Draw(spriteBatch);
            }
            Vector2 textSize = fontOfTitle.MeasureString(titleText);
            spriteBatch.DrawString(fontOfTitle, titleText, new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), (GraphicsDevice.Viewport.Height / 6) - (textSize.Y / 2)), Color.White);
            textSize = fontOfButtons.MeasureString("Press Escape To Return To Settings");
            spriteBatch.DrawString(fontOfButtons, "Press Escape To Return To Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), GraphicsDevice.Viewport.Height - textSize.Y), Color.White);
            saveSettingsButton.Draw(spriteBatch);
            textSize = fontOfButtons.MeasureString("Save");
            spriteBatch.DrawString(fontOfButtons, "Save", new Vector2(150 - (textSize.X / 2), GraphicsDevice.Viewport.Height - textSize.Y), Color.White);
        }
    }
}
