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
        bool mouseMovedOnFrame;

        Vector2 oldMouseXAndY;

        public enum OptionSelected
        {
            NewGame,
            Multiplayer,
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
            mouseMovedOnFrame = false;

            oldMouseXAndY = new Vector2(0, 0);
        }

        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse)
        {
            optionSelectedChangeOnFrame = false;
            mouseMovedOnFrame = false;
            if (mouse.X != oldMouseXAndY.X || mouse.Y != oldMouseXAndY.Y)
                mouseMovedOnFrame = true;

            if(optionSelectedObject == OptionSelected.NewGame && optionSelectedChangeOnFrame == false)
            {
                if ((kb.IsKeyDown(Keys.Down) && !oldKb.IsKeyDown(Keys.Down)) || (mouse.Y >= 700 && mouse.Y < 750 && mouseMovedOnFrame))
                {
                    optionSelectedObject = OptionSelected.Multiplayer;
                    optionSelectedChangeOnFrame = true;
                }
                if (mouse.Y >= 750 && mouse.Y < 800 && mouseMovedOnFrame)
                {
                    optionSelectedObject = OptionSelected.Settings;
                    optionSelectedChangeOnFrame = true;
                }
                if ((kb.IsKeyDown(Keys.Up) && !oldKb.IsKeyDown(Keys.Up)) || (mouse.Y >= 800 && mouseMovedOnFrame))
                {
                    optionSelectedObject = OptionSelected.Credits;
                    optionSelectedChangeOnFrame = true;
                }
            }
            if(optionSelectedObject == OptionSelected.Multiplayer && optionSelectedChangeOnFrame == false)
            {
                if ((kb.IsKeyDown(Keys.Up) && !oldKb.IsKeyDown(Keys.Up)) || (mouse.Y < 700 && mouseMovedOnFrame))
                {
                    optionSelectedObject = OptionSelected.NewGame;
                    optionSelectedChangeOnFrame = true;
                }
                if ((kb.IsKeyDown(Keys.Down) && !oldKb.IsKeyDown(Keys.Down)) || (mouse.Y >= 750 && mouse.Y < 800 && mouseMovedOnFrame))
                {
                    optionSelectedObject = OptionSelected.Settings;
                    optionSelectedChangeOnFrame = true;
                }
                if (mouse.Y >= 800 && mouseMovedOnFrame)
                {
                    optionSelectedObject = OptionSelected.Credits;
                    optionSelectedChangeOnFrame = true;
                }
            }
            if (optionSelectedObject == OptionSelected.Settings && optionSelectedChangeOnFrame == false)
            {
                if (mouse.Y < 700 && mouseMovedOnFrame)
                {
                    optionSelectedObject = OptionSelected.NewGame;
                    optionSelectedChangeOnFrame = true;
                }
                if ((kb.IsKeyDown(Keys.Up) && !oldKb.IsKeyDown(Keys.Up)) || (mouse.Y >= 700 && mouse.Y < 750 && mouseMovedOnFrame))
                {
                    optionSelectedObject = OptionSelected.Multiplayer;
                    optionSelectedChangeOnFrame = true;
                }
                if ((kb.IsKeyDown(Keys.Down) && !oldKb.IsKeyDown(Keys.Down)) || (mouse.Y >= 800 && mouseMovedOnFrame))
                {
                    optionSelectedObject = OptionSelected.Credits;
                    optionSelectedChangeOnFrame = true;
                }
            }
            if (optionSelectedObject == OptionSelected.Credits && optionSelectedChangeOnFrame == false)
            {
                if ((kb.IsKeyDown(Keys.Up) && !oldKb.IsKeyDown(Keys.Up)) || (mouse.Y >= 750 && mouse.Y < 800 && mouseMovedOnFrame))
                {
                    optionSelectedObject = OptionSelected.Settings;
                    optionSelectedChangeOnFrame = true;
                }
                if ((kb.IsKeyDown(Keys.Down) && !oldKb.IsKeyDown(Keys.Down)) || (mouse.Y < 700 && mouseMovedOnFrame))
                {
                    optionSelectedObject = OptionSelected.NewGame;
                    optionSelectedChangeOnFrame = true;
                }
                if (mouse.Y >= 700 && mouse.Y < 750 && mouseMovedOnFrame)
                {
                    optionSelectedObject = OptionSelected.Multiplayer;
                    optionSelectedChangeOnFrame = true;
                }
            }

            oldMouseXAndY.X = mouse.X;
            oldMouseXAndY.Y = mouse.Y;
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
                stringSize = fontOfOptions.MeasureString("Multiplayer");
                spriteBatch.DrawString(fontOfOptions, "Multiplayer", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 700), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Settings");
                spriteBatch.DrawString(fontOfOptions, "Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 750), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Credits");
                spriteBatch.DrawString(fontOfOptions, "Credits", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 800), Color.White * 0.5f);
            }
            if(optionSelectedObject == OptionSelected.Multiplayer)
            {
                stringSize = fontOfOptions.MeasureString("New Game");
                spriteBatch.DrawString(fontOfOptions, "New Game", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 650), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Multiplayer");
                spriteBatch.DrawString(fontOfOptions, "Multiplayer", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 700), Color.White);
                stringSize = fontOfOptions.MeasureString("Settings");
                spriteBatch.DrawString(fontOfOptions, "Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 750), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Credits");
                spriteBatch.DrawString(fontOfOptions, "Credits", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 800), Color.White * 0.5f);
            }
            if (optionSelectedObject == OptionSelected.Settings)
            {
                stringSize = fontOfOptions.MeasureString("New Game");
                spriteBatch.DrawString(fontOfOptions, "New Game", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 650), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Multiplayer");
                spriteBatch.DrawString(fontOfOptions, "Multiplayer", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 700), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Settings");
                spriteBatch.DrawString(fontOfOptions, "Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 750), Color.White);
                stringSize = fontOfOptions.MeasureString("Credits");
                spriteBatch.DrawString(fontOfOptions, "Credits", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 800), Color.White * 0.5f);
            }
            if(optionSelectedObject == OptionSelected.Credits)
            {
                stringSize = fontOfOptions.MeasureString("New Game");
                spriteBatch.DrawString(fontOfOptions, "New Game", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 650), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Multiplayer");
                spriteBatch.DrawString(fontOfOptions, "Multiplayer", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 700), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Settings");
                spriteBatch.DrawString(fontOfOptions, "Settings", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 750), Color.White * 0.5f);
                stringSize = fontOfOptions.MeasureString("Credits");
                spriteBatch.DrawString(fontOfOptions, "Credits", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), 800), Color.White);
            }
            stringSize = fontOfOptions.MeasureString("Press Escape To Exit");
            spriteBatch.DrawString(fontOfOptions, "Press Escape To Exit", new Vector2((GraphicsDevice.Viewport.Width / 2) - (stringSize.X / 2), GraphicsDevice.Viewport.Height - stringSize.Y), Color.White);
        }
    }
}
