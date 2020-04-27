using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GalacticImperialism
{
    class Settings
    {
        public List<Button> buttonList;

        int numberOfButtons;

        string titleText;

        Texture2D buttonSelectedTexture;
        Texture2D buttonUnselectedTexture;

        SpriteFont fontOfButtons;
        SpriteFont fontOfTitle;

        GraphicsDevice GraphicsDevice;

        SoundEffect buttonSelectedSoundEffect;

        float masterVolume;
        float soundEffectsVolume;

        public Settings(Texture2D selectedButtonTexture, Texture2D unselectedButtonTexture, SpriteFont buttonFont, SpriteFont titleFont, GraphicsDevice GraphicsDevice, SoundEffect buttonSelectedSoundEffect)
        {
            buttonSelectedTexture = selectedButtonTexture;
            buttonUnselectedTexture = unselectedButtonTexture;
            fontOfButtons = buttonFont;
            fontOfTitle = titleFont;
            this.GraphicsDevice = GraphicsDevice;
            this.buttonSelectedSoundEffect = buttonSelectedSoundEffect;
            Initialize();
        }

        public void Initialize()
        {
            buttonList = new List<Button>();
            numberOfButtons = 2;
            titleText = "Settings";
            for(int x = 0; x < numberOfButtons; x++)
            {
                buttonList.Add(new Button(new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((1894 / 4) / 2), 350 + ((693 / 4) * x), (1894 / 4), (693 / 4)), buttonUnselectedTexture, buttonSelectedTexture, "", fontOfButtons, Color.White, buttonSelectedSoundEffect, null));
            }
            buttonList[0].buttonText = "Audio Settings";
            buttonList[1].buttonText = "Video Settings";
            masterVolume = 1.0f;
            soundEffectsVolume = 1.0f;
        }

        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse, MouseState oldMouse, float masterVolume, float soundEffectsVolume)
        {
            this.masterVolume = masterVolume;
            this.soundEffectsVolume = soundEffectsVolume;
            for (int x = 0; x < buttonList.Count; x++)
            {
                buttonList[x].Update(mouse, oldMouse);
                if (buttonList[x].isSelected && buttonList[x].wasSelected == false)
                    buttonList[x].playSelectedSoundEffect(this.masterVolume * this.soundEffectsVolume);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < buttonList.Count; x++)
            {
                buttonList[x].Draw(spriteBatch);
            }
            Vector2 textSize = fontOfTitle.MeasureString(titleText);
            spriteBatch.DrawString(fontOfTitle, titleText, new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), (GraphicsDevice.Viewport.Height / 6) - (textSize.Y / 2)), Color.White);
            textSize = fontOfButtons.MeasureString("Press Escape To Return To Main Menu");
            spriteBatch.DrawString(fontOfButtons, "Press Escape To Return To Main Menu", new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), GraphicsDevice.Viewport.Height - textSize.Y), Color.White);
        }
    }
}
