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
        GraphicsDevice graphics;

        public NewGame(Texture2D s, Texture2D us, SpriteFont f, GraphicsDevice g)
        {
            graphics = g;
            selected = s;
            unselected = us;
            font = f;
            oldMS = Mouse.GetState();
            Initialize();
        }

        public void Initialize()
        {
            playGame = new Button(new Rectangle(graphics.Viewport.Width / 2 - 200, graphics.Viewport.Height - 200, 400, 100), unselected, selected, "Play Game", font, Color.White, null, null);
        }

        public void Update()
        {
            MouseState ms = Mouse.GetState();

            playGame.Update(ms, oldMS);

            oldMS = ms;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playGame.Draw(spriteBatch);
        }

        public Button getButton()
        {
            return playGame;
        }
    }
}
