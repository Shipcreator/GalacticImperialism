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

        Texture2D selected;
        Texture2D unselected;

        SpriteFont font;

        MouseState oldMS;
        KeyboardState oldKBS;

        Slider numPlanets;
        Vector2 npVector; // Num Planets Vector

        TextBox startGold;
        Vector2 goldVector; // Gold Vector

        Slider numPlayers;
        Vector2 playerVector; // Num players vector

        GraphicsDevice graphics;
        ContentManager cm;

        public NewGame(GraphicsDevice g, ContentManager cm)
        {
            graphics = g;
            selected = cm.Load<Texture2D>("Button Textures/SelectedButtonTexture1");
            unselected = cm.Load<Texture2D>("Button Textures/UnselectedButtonTexture1");
            font = cm.Load<SpriteFont>("Sprite Fonts/Arial20");
            this.cm = cm;
            oldMS = Mouse.GetState();
            oldKBS = Keyboard.GetState();

            Initialize();
        }

        public void Initialize()
        {
            playGame = new Button(new Rectangle(graphics.Viewport.Width / 2 - 500, graphics.Viewport.Height - 200, 400, 100), unselected, selected, "Play Game", font, Color.White, null, null);
            createNetwork = new Button(new Rectangle(graphics.Viewport.Width / 2 + 100, graphics.Viewport.Height - 200, 400, 100), unselected, selected, "Create Network", font, Color.White, null, null);

            npVector = new Vector2(50, 50);
            numPlanets = new Slider(new Rectangle(50, 100, 300, 25), new Vector2(10, 30), cm.Load<Texture2D>("Slider Textures/500x20SelectionBarTexture"), cm.Load<Texture2D>("Slider Textures/PillSelectionCursor"));
            numPlanets.SetPercentage(0.4f);

            goldVector = new Vector2(50, 200);

            startGold = new TextBox(new Rectangle(50, 250, 300, 50), 1, 5, Color.Black, Color.White, Color.White, Color.White, graphics, font);

            startGold.text = "1000";
            startGold.acceptsLetters = false;

            playerVector = new Vector2(50, 350);
            numPlayers = new Slider(new Rectangle(50, 400, 300, 25), new Vector2(10, 30), cm.Load<Texture2D>("Slider Textures/500x20SelectionBarTexture"), cm.Load<Texture2D>("Slider Textures/PillSelectionCursor"));
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

            spriteBatch.DrawString(font, "Starting Gold", goldVector, Color.White);
            spriteBatch.DrawString(font, "Number of planets : " + ((int) (numPlanets.percentage * 50) + 80), npVector, Color.White);
            spriteBatch.DrawString(font, "Number of players : " + ((int)(numPlayers.percentage * 2) + 2), playerVector, Color.White);
        }

        public int getPlanets()
        {
            return ((int)(numPlanets.percentage * 50) + 80);
        }

        public int getGold()
        {
            return int.Parse(startGold.text);
        }

        public int getPlayers()
        {
            return ((int)(numPlayers.percentage * 2) + 2);
        }

        public Button getButton()
        {
            return playGame;
        }
    }
}
