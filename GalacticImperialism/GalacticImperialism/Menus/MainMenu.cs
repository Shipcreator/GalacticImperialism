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
    class MainMenu
    {
        GraphicsDevice GraphicsDevice;

        SpriteFont fontOfTitle;
        SpriteFont fontOfOptions;

        bool optionSelectedChangeOnFrame;

        public enum OptionSelected
        {
            NewGame,
            Settings,
            Credits
        }

        public OptionSelected optionSelectedObject;

        public MainMenu(SpriteFont titleFont, SpriteFont optionsFont, GraphicsDevice GraphicsDevice)
        {
            fontOfTitle = titleFont;
            fontOfOptions = optionsFont;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            optionSelectedObject = OptionSelected.NewGame;

            optionSelectedChangeOnFrame = false;
        }

        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse)
        {
            optionSelectedChangeOnFrame = false;

            if(optionSelectedObject == OptionSelected.NewGame && optionSelectedChangeOnFrame == false)
            {
                if((kb.IsKeyDown(Keys.Down) && !oldKb.IsKeyDown(Keys.Down)) || (mouse.X >= 893 && mouse.X <= 1030 && mouse.Y >= 703 && mouse.Y <= 724))
                {
                    optionSelectedObject = OptionSelected.Settings;
                    optionSelectedChangeOnFrame = true;
                }
                if ((kb.IsKeyDown(Keys.Up) && !oldKb.IsKeyDown(Keys.Up)) || (mouse.X >= 900 && mouse.X <= 1020 && mouse.Y >= 753 && mouse.Y <= 775))
                {
                    optionSelectedObject = OptionSelected.Credits;
                    optionSelectedChangeOnFrame = true;
                }
            }
            if (optionSelectedObject == OptionSelected.Settings && optionSelectedChangeOnFrame == false)
            {
                if ((kb.IsKeyDown(Keys.Up) && !oldKb.IsKeyDown(Keys.Up)) || (mouse.X >= 877 && mouse.X <= 1042 && mouse.Y >= 651 && mouse.Y <= 674))
                {
                    optionSelectedObject = OptionSelected.NewGame;
                    optionSelectedChangeOnFrame = true;
                }
                if ((kb.IsKeyDown(Keys.Down) && !oldKb.IsKeyDown(Keys.Down)) || (mouse.X >= 900 && mouse.X <= 1020 && mouse.Y >= 753 && mouse.Y <= 775))
                {
                    optionSelectedObject = OptionSelected.Credits;
                    optionSelectedChangeOnFrame = true;
                }
            }
            if (optionSelectedObject == OptionSelected.Credits && optionSelectedChangeOnFrame == false)
            {
                if ((kb.IsKeyDown(Keys.Up) && !oldKb.IsKeyDown(Keys.Up)) || (mouse.X >= 893 && mouse.X <= 1030 && mouse.Y >= 703 && mouse.Y <= 724))
                {
                    optionSelectedObject = OptionSelected.Settings;
                    optionSelectedChangeOnFrame = true;
                }
                if ((kb.IsKeyDown(Keys.Down) && !oldKb.IsKeyDown(Keys.Down)) || (mouse.X >= 877 && mouse.X <= 1042 && mouse.Y >= 651 && mouse.Y <= 674))
                {
                    optionSelectedObject = OptionSelected.NewGame;
                    optionSelectedChangeOnFrame = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 stringSize = fontOfTitle.MeasureString("Galactic");
            spriteBatch.DrawString(fontOfTitle, "Galactic", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 100), Color.White);
            stringSize = fontOfTitle.MeasureString("Imperialism");
            spriteBatch.DrawString(fontOfTitle, "Imperialism", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 200), Color.White);

            if(optionSelectedObject == OptionSelected.NewGame)
            {
                stringSize = fontOfOptions.MeasureString("New Game");
                spriteBatch.DrawString(fontOfOptions, "New Game", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 650), Color.White);
                stringSize = fontOfOptions.MeasureString("Settings");
                spriteBatch.DrawString(fontOfOptions, "Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 700), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Credits");
                spriteBatch.DrawString(fontOfOptions, "Credits", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 750), Color.White * 0.5f);
            }
            if (optionSelectedObject == OptionSelected.Settings)
            {
                stringSize = fontOfOptions.MeasureString("New Game");
                spriteBatch.DrawString(fontOfOptions, "New Game", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 650), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Settings");
                spriteBatch.DrawString(fontOfOptions, "Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 700), Color.White);
                stringSize = fontOfOptions.MeasureString("Credits");
                spriteBatch.DrawString(fontOfOptions, "Credits", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 750), Color.White * 0.5f);
            }
            if(optionSelectedObject == OptionSelected.Credits)
            {
                stringSize = fontOfOptions.MeasureString("New Game");
                spriteBatch.DrawString(fontOfOptions, "New Game", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 650), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Settings");
                spriteBatch.DrawString(fontOfOptions, "Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 700), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Credits");
                spriteBatch.DrawString(fontOfOptions, "Credits", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 750), Color.White);
            }
            stringSize = fontOfOptions.MeasureString("Press Escape To Exit");
            spriteBatch.DrawString(fontOfOptions, "Press Escape To Exit", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), GraphicsDevice.Viewport.Height - stringSize.Y), Color.White);
        }
    }
}
