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

        float masterVolume;
        float musicVolume;
        float soundEffectsVolume;

        Board board;

        enum Menus
        {
            MainMenu,
            NewGame,
            Settings,
            Credits,
            AudioSettings,
            VideoSettings,
            Game
        }

        Menus menuSelected;

        MainMenu mainMenuObject;
        NewGame newGameMenuObject;
        Settings settingsMenuObject;
        Credits creditsMenuObject;
        AudioSettings audioSettingsMenuObject;
        VideoSettings videoSettingsMenuObject;

        StarBackground starBackgroundObject;

        List<Color> listOfStarColors;

        Texture2D whiteTexture;

        Rectangle wholeScreenRect;

        bool menuChangeOnFrame;

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

            menuChangeOnFrame = false;

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

            //Creates Board/Planet Textures
            List<Texture2D> temp = new List<Texture2D>();
            for (int i = 1; i < 20; i++)
                temp.Add(Content.Load<Texture2D>("Planets/" + i));
            board = new Board(200, temp);

            starBackgroundObject = new StarBackground(1250, 2, 2, 60, Content.Load<Texture2D>("Star Background/WhiteCircle"), listOfStarColors, GraphicsDevice);

            mainMenuObject = new MainMenu(Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), GraphicsDevice);
            newGameMenuObject = new NewGame();
            settingsMenuObject = new Settings(Content.Load<Texture2D>("Button Textures/SelectedButtonTexture1"), Content.Load<Texture2D>("Button Textures/UnselectedButtonTexture1"), Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), GraphicsDevice);
            creditsMenuObject = new Credits();
            videoSettingsMenuObject = new VideoSettings(Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), GraphicsDevice);
            audioSettingsMenuObject = new AudioSettings(Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), Content.Load<Texture2D>("Slider Textures/500x20SelectionBarTexture"), Content.Load<Texture2D>("Slider Textures/PillSelectionCursor"), GraphicsDevice);
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
            menuChangeOnFrame = false;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if(menuSelected == Menus.MainMenu)
            {
                starBackgroundObject.Update();
                mainMenuObject.Update(kb, oldKb, mouse);
                if(((kb.IsKeyDown(Keys.Enter) && !oldKb.IsKeyDown(Keys.Enter)) || (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space)) || (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)) && menuChangeOnFrame == false)
                {
                    if (mainMenuObject.optionSelectedObject == MainMenu.OptionSelected.NewGame)
                        menuSelected = Menus.NewGame;
                    if (mainMenuObject.optionSelectedObject == MainMenu.OptionSelected.Settings)
                        menuSelected = Menus.Settings;
                    if (mainMenuObject.optionSelectedObject == MainMenu.OptionSelected.Credits)
                        menuSelected = Menus.Credits;
                    menuChangeOnFrame = true;
                }
                if (kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape) && menuChangeOnFrame == false)
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
                settingsMenuObject.Update(kb, oldKb, mouse, oldMouse);
                if(kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape) && menuChangeOnFrame == false)
                {
                    menuSelected = Menus.MainMenu;
                    menuChangeOnFrame = true;
                }
                if (settingsMenuObject.buttonList[0].isClicked && menuChangeOnFrame == false)
                {
                    menuSelected = Menus.AudioSettings;
                    menuChangeOnFrame = true;
                }
                if(settingsMenuObject.buttonList[1].isClicked && menuChangeOnFrame == false)
                {
                    menuSelected = Menus.VideoSettings;
                    menuChangeOnFrame = true;
                }
            }
            if(menuSelected == Menus.Credits)
            {
                starBackgroundObject.Update();
                creditsMenuObject.Update();
            }
            if(menuSelected == Menus.AudioSettings)
            {
                starBackgroundObject.Update();
                audioSettingsMenuObject.Update(kb, oldKb, mouse, oldMouse);
                if(kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape) && menuChangeOnFrame == false)
                {
                    menuSelected = Menus.Settings;
                    menuChangeOnFrame = true;
                }
            } 
            if(menuSelected == Menus.VideoSettings)
            {
                starBackgroundObject.Update();
                videoSettingsMenuObject.Update(kb, oldKb, mouse, oldMouse);
                if (kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape) && menuChangeOnFrame == false)
                {
                    menuSelected = Menus.Settings;
                    menuChangeOnFrame = true;
                }
            }

            //Dylan's Test Button
            if (kb.IsKeyDown(Keys.Insert) && oldKb.IsKeyUp(Keys.Insert))
            {
                board.NewBoard(100,1, 4, 1, 1000);
                menuSelected = Menus.Game;
            }

            masterVolume = audioSettingsMenuObject.masterVolume;
            musicVolume = audioSettingsMenuObject.musicVolume;
            soundEffectsVolume = audioSettingsMenuObject.soundEffectsVolume;
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
            if (menuSelected == Menus.MainMenu || menuSelected == Menus.NewGame || menuSelected == Menus.Settings || menuSelected == Menus.Credits || menuSelected == Menus.AudioSettings || menuSelected == Menus.VideoSettings || menuSelected == Menus.Game)
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
            if (menuSelected == Menus.AudioSettings)
                audioSettingsMenuObject.Draw(spriteBatch);
            if (menuSelected == Menus.VideoSettings)
                videoSettingsMenuObject.Draw(spriteBatch);
            if (menuSelected == Menus.Game)
                board.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
