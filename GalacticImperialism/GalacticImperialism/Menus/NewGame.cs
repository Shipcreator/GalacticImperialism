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

        TextBox numPlanets;
        Vector2 npVector; // Num Planets Vector

        Slider startGold;
        Vector2 goldVector; // Num Planets Vector

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
            numPlanets = new TextBox(new Rectangle(50, 100, 300, 50), 1, 3, Color.White, Color.Black, Color.Black, Color.Blue, graphics, font);
            numPlanets.text = "100";
            numPlanets.acceptsLetters = false;

            goldVector = new Vector2(50, 200);
            startGold = new Slider(new Rectangle(50, 250, 300, 25), new Vector2(10, 30), cm.Load<Texture2D>("Slider Textures/500x20SelectionBarTexture"), cm.Load<Texture2D>("Slider Textures/PillSelectionCursor"));
            startGold.setPercent(0.1f);

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
            numPlanets.Update(ms, oldMS, kbs, oldKBS);
            startGold.Update(ms, oldMS);

            oldKBS = kbs;
            oldMS = ms;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playGame.Draw(spriteBatch);
            numPlanets.Draw(spriteBatch);
            startGold.Draw(spriteBatch);

            spriteBatch.DrawString(font, (int) (startGold.percentage * 10000) + " ", goldVector, Color.White);
            spriteBatch.DrawString(font, "Number of planets (80-125)", npVector, Color.White);
        }

        public int getPlanets()
        {
            return int.Parse(numPlanets.text);
        }

        public Button getButton()
        {
            return playGame;
        }
    }
}
