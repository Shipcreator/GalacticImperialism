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
using GalacticImperialism.Networking;
using Lidgren.Network;

namespace GalacticImperialism
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Texture2D whiteCircle;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState kb;
        KeyboardState oldKb;

        Random rand;

        MouseState mouse;
        MouseState oldMouse;

        float masterVolume;
        float musicVolume;
        float soundEffectsVolume;

        Board board;
        public static int playerID;

        public static ConnectionHandler connection;
        public static String status;

        enum Menus
        {
            MainMenu,
            NewGame,
            Settings,
            Credits,
            AudioSettings,
            VideoSettings,
            Game,
            FlagCreation,
            Multiplayer
        }

        Menus menuSelected;

        MainMenu mainMenuObject;
        NewGame newGameMenuObject;
        Settings settingsMenuObject;
        Credits creditsMenuObject;
        AudioSettings audioSettingsMenuObject;
        VideoSettings videoSettingsMenuObject;
        FlagCreation flagCreationMenuObject;
        Multiplayer multiplayerMenuObject;

        StarBackground starBackgroundObject;

        PlayerUI playerUIObject;

        List<Color> listOfStarColors;

        Texture2D whiteTexture;
        Texture2D[] flagSymbolTextures;

        Rectangle wholeScreenRect;

        bool menuChangeOnFrame;

        UnitProduction unitProduction;
        TechTree techTree;

        public static List<Texture2D> planetTex;

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
            status = "";
            connection = new ConnectionHandler(new Random(), this);
            menuSelected = Menus.MainMenu;
            rand = new Random();
            IsMouseVisible = true;

            kb = Keyboard.GetState();
            oldKb = Keyboard.GetState();

            mouse = Mouse.GetState();
            oldMouse = Mouse.GetState();

            wholeScreenRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            menuChangeOnFrame = false;

            flagSymbolTextures = new Texture2D[6];

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

            whiteCircle = Content.Load<Texture2D>("Star Background/WhiteCircle");

            //Creates Board/Planet Textures
            planetTex = new List<Texture2D>();
            for (int i = 1; i < 20; i++)
                planetTex.Add(Content.Load<Texture2D>("Planets/" + i));

            board = new Board(200);

            starBackgroundObject = new StarBackground(1250, 2, 2, 60, Content.Load<Texture2D>("Star Background/WhiteCircle"), listOfStarColors, GraphicsDevice);

            mainMenuObject = new MainMenu(Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), GraphicsDevice);
            newGameMenuObject = new NewGame(this);
            settingsMenuObject = new Settings(Content.Load<Texture2D>("Button Textures/SelectedButtonTexture1"), Content.Load<Texture2D>("Button Textures/UnselectedButtonTexture1"), Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), GraphicsDevice);
            creditsMenuObject = new Credits();
            videoSettingsMenuObject = new VideoSettings(Content.Load<Texture2D>("Button Textures/UnselectedSaveSettingsButton"), Content.Load<Texture2D>("Button Textures/SelectedSaveSettingsButton"), Content.Load<Texture2D>("Button Textures/UnselectedButtonTexture1"), Content.Load<Texture2D>("Button Textures/SelectedButtonTexture1"), Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), GraphicsDevice);
            if (videoSettingsMenuObject.toggleFullScreen)
                graphics.ToggleFullScreen();
            audioSettingsMenuObject = new AudioSettings(Content.Load<Texture2D>("Button Textures/UnselectedSaveSettingsButton"), Content.Load<Texture2D>("Button Textures/SelectedSaveSettingsButton"), Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), Content.Load<Texture2D>("Slider Textures/500x20SelectionBarTexture"), Content.Load<Texture2D>("Slider Textures/PillSelectionCursor"), GraphicsDevice);
            playerUIObject = new PlayerUI(Content.Load<Texture2D>("Player UI/Bar"), whiteTexture, GraphicsDevice);
            flagSymbolTextures[0] = Content.Load<Texture2D>("Flag/Crown");
            flagSymbolTextures[1] = Content.Load<Texture2D>("Flag/Eagle");
            flagSymbolTextures[2] = Content.Load<Texture2D>("Flag/Cross");
            flagSymbolTextures[3] = Content.Load<Texture2D>("Flag/Fist");
            flagSymbolTextures[4] = Content.Load<Texture2D>("Flag/Lily");
            flagSymbolTextures[5] = Content.Load<Texture2D>("Flag/Star");
            flagCreationMenuObject = new FlagCreation(Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point"), Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"), flagSymbolTextures, Content.Load<Texture2D>("Slider Textures/500x20SelectionBarTexture"), Content.Load<Texture2D>("Slider Textures/PillSelectionCursor"), Content.Load<Texture2D>("Button Textures/UnselectedButtonTexture1"), Content.Load<Texture2D>("Button Textures/SelectedButtonTexture1"), GraphicsDevice);
            multiplayerMenuObject = new Multiplayer(this);

            //Creates TechTree
            techTree = new TechTree(Content.Load<Texture2D>("Tech Textures/Sci-Fi Steel Wall"), Content.Load<Texture2D>("Tech Textures/Tech Backdrop"), Content.Load<Texture2D>("Tech Textures/TechMenuBackdrop"), Content.Load<Texture2D>("Tech Textures/Circuit"), Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point"));

            //UNIT PRODUCTION
            unitProduction = new UnitProduction(Content.Load<Texture2D>("Tech Textures/Sci-Fi Steel Wall"), Content.Load<Texture2D>("Tech Textures/Tech Backdrop"), Content.Load<Texture2D>("Tech Textures/TechMenuBackdrop"), Content.Load<Texture2D>("Tech Textures/Circuit"), Content.Load<SpriteFont>("Sprite Fonts/Arial15"));
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
                    if (mainMenuObject.optionSelectedObject == MainMenu.OptionSelected.Multiplayer)
                        menuSelected = Menus.Multiplayer;
                    if (mainMenuObject.optionSelectedObject == MainMenu.OptionSelected.Settings)
                        menuSelected = Menus.Settings;
                    if (mainMenuObject.optionSelectedObject == MainMenu.OptionSelected.Credits)
                        menuSelected = Menus.Credits;
                    menuChangeOnFrame = true;
                }
                if (kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape) && menuChangeOnFrame == false)
                    this.Exit();
            }
            if (kb.IsKeyDown(Keys.Insert))
                menuSelected = Menus.FlagCreation;
            if (menuSelected == Menus.FlagCreation)
            {
                starBackgroundObject.Update();
                flagCreationMenuObject.Update(kb, oldKb, mouse, oldMouse);
                if (kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape) && menuChangeOnFrame == false)
                {
                    menuSelected = Menus.NewGame;
                    menuChangeOnFrame = true;
                }
            }
            if (menuSelected == Menus.NewGame)
            {
                starBackgroundObject.Update();
                newGameMenuObject.Update();

                if (kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape) && menuChangeOnFrame == false)
                {
                    if (connection.getCon().Status == Lidgren.Network.NetPeerStatus.Running)
                    {
                        connection.getCon().Shutdown("Shutting down!");
                    }

                    menuSelected = Menus.MainMenu;
                    menuChangeOnFrame = true;
                }

                if (newGameMenuObject.networkButton().isClicked && connection.getCon().Status == Lidgren.Network.NetPeerStatus.NotRunning)
                {
                    connection.Start();
                }

                if (newGameMenuObject.playButton().isClicked)
                {
                    int numPlanets = newGameMenuObject.getPlanets();
                    int startingGold = 1000;
                    int seed = rand.Next();

                    if (newGameMenuObject.getGold() >= 100) // Sets starting gold.
                        startingGold = newGameMenuObject.getGold();

                    if (newGameMenuObject.getSeed() != 0) // Sets the seed
                        seed = newGameMenuObject.getSeed();


                    board.NewBoard(numPlanets, seed, newGameMenuObject.getPlayers(), 0, startingGold); /////////////////////////////////////////////////////////////////////////////////////
                    connection.SerializeData(board);

                    if (connection.getCon().ConnectionsCount > 0)
                    {
                        int index = board.numBots;
                        playerID = index;
                        index++;

                        NetOutgoingMessage boardMsg = connection.getCon().CreateMessage();
                        boardMsg.Write(connection.SerializeData(board));

                        NetOutgoingMessage playerMsg = connection.getCon().CreateMessage();
                        playerMsg.Write(connection.SerializeData(index));

                        foreach (NetConnection con in connection.getCon().Connections)
                        {
                            connection.getCon().SendMessage(boardMsg, con, NetDeliveryMethod.ReliableOrdered);
                            connection.getCon().SendMessage(playerMsg, con, NetDeliveryMethod.ReliableOrdered);
                            index++;
                        }
                    }

                    menuSelected = Menus.Game;
                }
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
                if (videoSettingsMenuObject.toggleFullScreen && menuChangeOnFrame == false)
                    graphics.ToggleFullScreen();
                if (kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape) && menuChangeOnFrame == false)
                {
                    menuSelected = Menus.Settings;
                    menuChangeOnFrame = true;
                }
            }
            if(menuSelected == Menus.Multiplayer)
            {
                starBackgroundObject.Update();
                multiplayerMenuObject.Update(kb, oldKb, mouse, oldMouse);

                if (multiplayerMenuObject.getJoin().isClicked)
                {
                    if (connection.getCon().Status == NetPeerStatus.NotRunning)
                    {
                        connection.getCon().Start();
                    }

                    if (!multiplayerMenuObject.getPort().Equals(""))
                    {
                        connection.getCon().Start();
                        connection.FindPeer(multiplayerMenuObject.getPort());
                    }
                }

                if (kb.IsKeyDown(Keys.Escape) && !oldKb.IsKeyDown(Keys.Escape) && menuChangeOnFrame == false)
                {
                    if (connection.getCon().Status == NetPeerStatus.Running)
                    {
                        connection.getCon().Shutdown("Shutting down!");
                    }

                    menuSelected = Menus.MainMenu;
                    menuChangeOnFrame = true;
                }
            }

            switch (connection.getCon().Status)
            {
                case NetPeerStatus.NotRunning:
                    status = "Network Status : Offline";
                    break;
                case NetPeerStatus.Running:
                    status = "Network Status : Online\nPort : " + Game1.connection.getCon().Port + "\nConnections: " + Game1.connection.getCon().ConnectionsCount;
                    Object msg = connection.Listener();
                    if (msg != null)
                    {
                        if (msg is Board)
                        {
                            board = (Board)msg;
                            menuSelected = Menus.Game;
                        } else if (msg is int)
                        {
                            playerID = (int) msg;
                        }
                    }
                    break;
            }

            //Updates Board
            if (menuSelected == Menus.Game)
            {
                board.Update(gameTime, mouse, kb, oldMouse, oldKb);
                playerUIObject.Update();
            }

            //Updates Unit Production
            unitProduction.Update(kb, oldKb, mouse, oldMouse);
            //Update TechTree
            techTree.Update(kb, oldKb, mouse, oldMouse);

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
            if (menuSelected == Menus.MainMenu || menuSelected == Menus.NewGame || menuSelected == Menus.Settings || menuSelected == Menus.Credits || menuSelected == Menus.AudioSettings || menuSelected == Menus.VideoSettings || menuSelected == Menus.Game || menuSelected == Menus.FlagCreation || menuSelected == Menus.Multiplayer)
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
            if (menuSelected == Menus.FlagCreation)
                flagCreationMenuObject.Draw(spriteBatch);
            if (menuSelected == Menus.Multiplayer)
                multiplayerMenuObject.Draw(spriteBatch);
            if (menuSelected == Menus.Game)
            {
                board.Draw(spriteBatch);
                playerUIObject.Draw(spriteBatch);
            }
            techTree.Draw(spriteBatch);
            unitProduction.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
