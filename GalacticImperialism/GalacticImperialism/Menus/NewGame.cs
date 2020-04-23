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
    public class NewGame
    {

        Button playGame;
        Button createNetwork;
        public Button designFlag;

        Texture2D selected;
        Texture2D unselected;

        SpriteFont font;

        MouseState oldMS;
        KeyboardState oldKBS;

        Slider numPlanets;
        Vector2 npVector; // Num Planets Vector

        TextBox startGold;
        Vector2 goldVector; // Gold Vector

        TextBox seedBox;
        Vector2 seedVector; // Seed Vector

        Slider numPlayers;
        Vector2 playerVector; // Num players vector

        String status;
        Vector2 statusVector;

        Game1 game;
        ContentManager cm;
        public static GraphicsDevice graphics;

        public NewGame(Game1 game)
        {
            graphics = game.GraphicsDevice;
            this.game = game;
            selected = game.Content.Load<Texture2D>("Button Textures/SelectedButtonTexture1");
            unselected = game.Content.Load<Texture2D>("Button Textures/UnselectedButtonTexture1");
            font = game.Content.Load<SpriteFont>("Sprite Fonts/Arial20");
            oldMS = Mouse.GetState();
            oldKBS = Keyboard.GetState();

            Initialize();
        }

        public void Initialize()
        {
            // Creates the play game and create network button.
            playGame = new Button(new Rectangle((graphics.Viewport.Width / 4) - 200, graphics.Viewport.Height - 200, 400, 100), unselected, selected, "Play Game", font, Color.White, null, null);
            createNetwork = new Button(new Rectangle(((graphics.Viewport.Width / 4) * 2) - 200, graphics.Viewport.Height - 200, 400, 100), unselected, selected, "Create Network", font, Color.White, null, null);
            designFlag = new Button(new Rectangle(((graphics.Viewport.Width / 4) * 3) - 200, graphics.Viewport.Height - 200, 400, 100), unselected, selected, "Design Flag", font, Color.White, null, null);

            npVector = new Vector2(50, 50);     // Vector for the "Number Planets" text
            // Creates a slider with a range of 75-125 for the starting planets.
            numPlanets = new Slider(new Rectangle(50, 100, 300, 25), new Vector2(10, 30), game.Content.Load<Texture2D>("Slider Textures/500x20SelectionBarTexture"), game.Content.Load<Texture2D>("Slider Textures/PillSelectionCursor"));
            numPlanets.SetPercentage(0.5f);

            goldVector = new Vector2(50, 200);  // Vector text for the "Starting Gold"
            // Text box for the gold, only accepts numbers and has a max of 99999
            startGold = new TextBox(new Rectangle(50, 250, 300, 50), 1, 5, Color.Black, Color.White, Color.White, Color.White, graphics, font);
            startGold.text = "1000";
            startGold.acceptsLetters = false;

            // Creates the textbox for the seed as well as the vector for the text.
            seedBox = new TextBox(new Rectangle(50, 550, 300, 50),1 , 9, Color.Black, Color.White, Color.White, Color.White, graphics, font);
            seedBox.acceptsLetters = false;
            seedVector = new Vector2(50, 500);

            statusVector = new Vector2(graphics.Viewport.Width - 400, 50);

            // Creates a vector and slider for num players.
            playerVector = new Vector2(50, 350);
            numPlayers = new Slider(new Rectangle(50, 400, 300, 25), new Vector2(10, 30), game.Content.Load<Texture2D>("Slider Textures/500x20SelectionBarTexture"), game.Content.Load<Texture2D>("Slider Textures/PillSelectionCursor"));
        }

        public void Update()
        {
            MouseState ms = Mouse.GetState();
            KeyboardState kbs = Keyboard.GetState();

            playGame.Update(ms, oldMS);
            numPlanets.Update(ms, oldMS);
            startGold.Update(ms, oldMS, kbs, oldKBS);
            numPlayers.Update(ms, oldMS);
            createNetwork.Update(ms, oldMS);
            seedBox.Update(ms, oldMS, kbs, oldKBS);
            designFlag.Update(ms, oldMS);

            oldKBS = kbs;
            oldMS = ms;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playGame.Draw(spriteBatch);
            numPlanets.Draw(spriteBatch);
            startGold.Draw(spriteBatch);
            numPlayers.Draw(spriteBatch);
            createNetwork.Draw(spriteBatch);
            seedBox.Draw(spriteBatch);
            designFlag.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Starting Gold", goldVector, Color.White);
            spriteBatch.DrawString(font, "Number of planets : " + ((int) (numPlanets.percentage * 50) + 75), npVector, Color.White);
            spriteBatch.DrawString(font, "Number of players : " + ((int)(numPlayers.percentage * 2) + 2), playerVector, Color.White);
            spriteBatch.DrawString(font, "Seed", seedVector, Color.White);
            spriteBatch.DrawString(font, Game1.status, statusVector, Color.White);
        }

        public int getPlanets()
        {
            return ((int)(numPlanets.percentage * 50) + 75);
        }

        public int getGold()
        {
            return int.Parse(startGold.text);
        }

        public int getPlayers()
        {
            return ((int)(numPlayers.percentage * 2) + 2);
        }

        public int getSeed()
        {
            if (!(seedBox.text.Equals("")))
            {
                return int.Parse(seedBox.text);
            }
            else
            {
                return 0;
            }
        }

        public Button playButton()
        {
            return playGame;
        }

        public Button networkButton()
        {
            return createNetwork;
        }
    }
}
