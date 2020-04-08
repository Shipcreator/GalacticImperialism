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
        Texture2D selected;
        Texture2D unselected;
        SpriteFont font;
        MouseState oldMS;
        KeyboardState oldKBS;

        Slider numPlanets;
        Vector2 npVector; // Num Planets Vector

        TextBox startGold;
        Vector2 goldVector; // Gold Vector

        GraphicsDevice graphics;

        public NewGame(SpriteFont f, GraphicsDevice g, ContentManager cm)
        {
            graphics = g;
            selected = cm.Load<Texture2D>("Button Textures/SelectedButtonTexture1");
            unselected = cm.Load<Texture2D>("Button Textures/UnselectedButtonTexture1");
            font = f;
            oldMS = Mouse.GetState();
            oldKBS = Keyboard.GetState();

            npVector = new Vector2(50, 50);
            numPlanets = new Slider(new Rectangle(50, 100, 300, 25), new Vector2(10, 30), cm.Load<Texture2D>("Slider Textures/500x20SelectionBarTexture"), cm.Load<Texture2D>("Slider Textures/PillSelectionCursor"));
            numPlanets.SetPercentage(0.4f);

            goldVector = new Vector2(50, 200);
            startGold = new TextBox(new Rectangle(50, 250, 300, 25), 1, 5, Color.White, Color.Black, Color.Black, Color.Blue, graphics, font);
            startGold.text = "1000";
            startGold.acceptsLetters = false;

            Initialize();
        }

        public void Initialize()
        {
            playGame = new Button(new Rectangle(graphics.Viewport.Width / 2 - 200, graphics.Viewport.Height - 200, 400, 100), unselected, selected, "Play Game", font, Color.White, null, null);
        }

        public void Update()
        {
            MouseState ms = Mouse.GetState();
            KeyboardState kbs = Keyboard.GetState();

            playGame.Update(ms, oldMS);
            numPlanets.Update(ms, oldMS);
            startGold.Update(ms, oldMS, kbs, oldKBS);

            oldKBS = kbs;
            oldMS = ms;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playGame.Draw(spriteBatch);
            numPlanets.Draw(spriteBatch);
            startGold.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Starting Gold : " + startGold.text, goldVector, Color.White);
            spriteBatch.DrawString(font, "Number of planets : " + ((int) (numPlanets.percentage * 50) + 80), npVector, Color.White);
        }

        public int getPlanets()
        {
            return ((int)(numPlanets.percentage * 50) + 80);
        }

        public int getGold()
        {
            return int.Parse(startGold.text);
        }

        public Button getButton()
        {
            return playGame;
        }
    }
}
