using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GalacticImperialism.UI.Components;

namespace GalacticImperialism
{
    class TechTreeMenu
    {
        Texture2D unselectedButtonTexture;
        Texture2D selectedButtonTexture;
        Texture2D whiteTexture;

        SpriteFont Castellar20;

        List<Button> techButtons;

        Rectangle selectedRectangle;

        public TechTree techTreeObject;

        Vector2 textSize;

        public TechTreeMenu(ContentManager Content, Texture2D white)
        {
            whiteTexture = white;
            LoadContent(Content);
            Initialize();
        }

        public void Initialize()
        {
            techButtons = new List<Button>();
            techButtons.Add(new Button(new Rectangle((1920 / 4) - (400 / 2), ((1080 / 3) * 2) - (200 / 2), 400, 200), unselectedButtonTexture, selectedButtonTexture, "Attack 1\nCost: 300", Castellar20, Color.White, null, null));
            techButtons.Add(new Button(new Rectangle(((1920 / 4) * 2) - (400 / 2), ((1080 / 3) * 2) - (200 / 2), 400, 200), unselectedButtonTexture, selectedButtonTexture, "Defense 1\nCost: 300", Castellar20, Color.White, null, null));
            techButtons.Add(new Button(new Rectangle(((1920 / 4) * 3) - (400 / 2), ((1080 / 3) * 2) - (200 / 2), 400, 200), unselectedButtonTexture, selectedButtonTexture, "Movement 1\nCose: 300", Castellar20, Color.White, null, null));
            selectedRectangle = new Rectangle((1920 / 2) - (400 / 2), (1080 / 3) - (200 / 2), 400, 200);

            textSize = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager Content)
        {
            unselectedButtonTexture = Content.Load<Texture2D>("Button Textures/UnselectedButtonTexture1");
            selectedButtonTexture = Content.Load<Texture2D>("Button Textures/SelectedButtonTexture1");
            Castellar20 = Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point");
        }

        public void Update(MouseState mouse, MouseState oldMouse, TechTree tree)
        {
            techTreeObject = tree;
            for(int x = 0; x < techButtons.Count; x++)
            {
                techButtons[x].Update(mouse, oldMouse);
            }
            if (techButtons[0].isClicked)
            {
                techTreeObject.techResearching = TechTree.Tech.Attack;
            }
            if (techButtons[1].isClicked)
            {
                techTreeObject.techResearching = TechTree.Tech.Defense;
            }
            if (techButtons[2].isClicked)
            {
                techTreeObject.techResearching = TechTree.Tech.Movement;
            }
            techButtons[0].buttonText = "Attack " + (techTreeObject.attackTechsResearched + 1) + "\nCost: " + ((techTreeObject.attackTechsResearched + 1) * 300);
            techButtons[1].buttonText = "Defense " + (techTreeObject.defenseTechsResearched + 1) + "\nCost: " + ((techTreeObject.defenseTechsResearched + 1) * 300);
            techButtons[2].buttonText = "Movement " + (techTreeObject.movementTechsResearched + 1) + "\nCost: " + ((techTreeObject.movementTechsResearched + 1) * 300);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < techButtons.Count; x++)
            {
                techButtons[x].Draw(spriteBatch);
            }
            spriteBatch.Draw(whiteTexture, selectedRectangle, Color.Navy);
            if(techTreeObject.techResearching == TechTree.Tech.Attack)
            {
                textSize = Castellar20.MeasureString("Attack " + (techTreeObject.attackTechsResearched + 1) + "\nProgress: " + techTreeObject.science + " / " + ((techTreeObject.attackTechsResearched + 1) * 300));
                spriteBatch.DrawString(Castellar20, "Attack " + (techTreeObject.attackTechsResearched + 1) + "\nProgress: " + techTreeObject.science + " / " + ((techTreeObject.attackTechsResearched + 1) * 300), new Vector2(selectedRectangle.Center.X - (textSize.X / 2), selectedRectangle.Center.Y - (textSize.Y / 2)), Color.White);
            }
            if(techTreeObject.techResearching == TechTree.Tech.Defense)
            {
                textSize = Castellar20.MeasureString("Defense " + (techTreeObject.defenseTechsResearched + 1) + "\nProgress: " + techTreeObject.science + " / " + ((techTreeObject.defenseTechsResearched + 1) * 300));
                spriteBatch.DrawString(Castellar20, "Defense " + (techTreeObject.defenseTechsResearched + 1) + "\nProgress: " + techTreeObject.science + " / " + ((techTreeObject.defenseTechsResearched + 1) * 300), new Vector2(selectedRectangle.Center.X - (textSize.X / 2), selectedRectangle.Center.Y - (textSize.Y / 2)), Color.White);
            }
            if(techTreeObject.techResearching == TechTree.Tech.Movement)
            {
                textSize = Castellar20.MeasureString("Movement " + (techTreeObject.movementTechsResearched + 1) + "\nProgress: " + techTreeObject.science + " / " + ((techTreeObject.movementTechsResearched + 1) * 300));
                spriteBatch.DrawString(Castellar20, "Movement " + (techTreeObject.movementTechsResearched + 1) + "\nProgress: " + techTreeObject.science + " / " + ((techTreeObject.movementTechsResearched + 1) * 300), new Vector2(selectedRectangle.Center.X - (textSize.X / 2), selectedRectangle.Center.Y - (textSize.Y / 2)), Color.White);
            }
            if(techTreeObject.techResearching == TechTree.Tech.None)
            {
                textSize = Castellar20.MeasureString("No Tech Selected");
                spriteBatch.DrawString(Castellar20, "No Tech Selected", new Vector2(selectedRectangle.Center.X - (textSize.X / 2), selectedRectangle.Center.Y - (textSize.Y / 2)), Color.White);
            }
            spriteBatch.DrawString(Castellar20, "Press Escape To Return To Board", new Vector2((1920 / 2) - (Castellar20.MeasureString("Press Escape To Return To Board").X / 2), 1080 - Castellar20.MeasureString("Press Escape To Return To Board").Y), Color.White);
        }
    }
}
