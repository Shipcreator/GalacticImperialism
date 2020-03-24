using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GalacticImperialism
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState kb;
        KeyboardState oldKb;

        MouseState mouse;
        MouseState oldMouse;

        enum Menus
        {
            MainMenu,
            NewGame,
            Settings,
            Credits
        }

        Menus menuSelected;

        MainMenu mainMenuObject;
        NewGame newGameMenuObject;
        Settings settingsMenuObject;
        Credits creditsMenuObject;

        StarBackground starBackgroundObject;

        List<Color> listOfStarColors;

        Texture2D whiteTexture;

        Rectangle wholeScreenRect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            menuSelected = Menus.MainMenu;

            IsMouseVisible = true;

            kb = Keyboard.GetState();
            oldKb = Keyboard.GetState();

            mouse = Mouse.GetState();
            oldMouse = Mouse.GetState();

            wholeScreenRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });

            listOfStarColors = new List<Color>();
            listOfStarColors.Add(Color.White);
            listOfStarColors.Add(Color.LightSlateGray);

            starBackgroundObject = new StarBackground(1250, 2, 2, 60, Content.Load<Texture2D>("Star Background/WhiteCircle"), listOfStarColors, GraphicsDevice);

            mainMenuObject = new MainMenu(Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), GraphicsDevice);
            newGameMenuObject = new NewGame();
            settingsMenuObject = new Settings();
            creditsMenuObject = new Credits();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState();
            mouse = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if(menuSelected == Menus.MainMenu)
            {
                starBackgroundObject.Update();
                mainMenuObject.Update(kb, oldKb, mouse);
                if((kb.IsKeyDown(Keys.Enter) && !oldKb.IsKeyDown(Keys.Enter)) || (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space)) || (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed))
                {
                    if (mainMenuObject.optionSelectedObject == MainMenu.OptionSelected.NewGame)
                        menuSelected = Menus.NewGame;
                    if (mainMenuObject.optionSelectedObject == MainMenu.OptionSelected.Settings)
                        menuSelected = Menus.Settings;
                    if (mainMenuObject.optionSelectedObject == MainMenu.OptionSelected.Credits)
                        menuSelected = Menus.Credits;
                }
                if (kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape))
                    this.Exit();
            }
            if(menuSelected == Menus.NewGame)
            {
                starBackgroundObject.Update();
                newGameMenuObject.Update();
            }
            if(menuSelected == Menus.Settings)
            {
                starBackgroundObject.Update();
                settingsMenuObject.Update();
            }
            if(menuSelected == Menus.Credits)
            {
                starBackgroundObject.Update();
                creditsMenuObject.Update();
            }

            oldKb = kb;
            oldMouse = mouse;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (menuSelected == Menus.MainMenu || menuSelected == Menus.NewGame || menuSelected == Menus.Settings || menuSelected == Menus.Credits)
            {
                spriteBatch.Draw(whiteTexture, wholeScreenRect, Color.Black);
                starBackgroundObject.Draw(spriteBatch);
            }
            if(menuSelected == Menus.MainMenu)
                mainMenuObject.Draw(spriteBatch);
            if (menuSelected == Menus.NewGame)
                newGameMenuObject.Draw(spriteBatch);
            if (menuSelected == Menus.Settings)
                settingsMenuObject.Draw(spriteBatch);
            if (menuSelected == Menus.Credits)
                creditsMenuObject.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
