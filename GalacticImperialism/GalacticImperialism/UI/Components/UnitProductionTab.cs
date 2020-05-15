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
using GalacticImperialism.Networking;
using Lidgren.Network;

namespace GalacticImperialism
{
    class UnitProductionTab
    {
        public List<Ship> shipsAvailableForConstruction;

        public Planet selectedPlanet;

        List<Button> buildButtons;

        SpriteFont Castellar15;

        Line lineUnderColumnHeader;
        Line lineUnderAvailableShips;
        Line lineAboveConstructionQueue;

        Texture2D whiteTexture;
        Texture2D unselectedButtonTexture;
        Texture2D selectedButtonTexture;

        public int goldAmount;
        public int ironAmount;
        public int uraniumAmount;

        public UnitProductionTab(ContentManager Content, Texture2D white)
        {
            whiteTexture = white;
            LoadContent(Content);
            Initialize();
        }

        public void Initialize()
        {
            buildButtons = new List<Button>();
            lineUnderColumnHeader = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
            lineUnderAvailableShips = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
            lineAboveConstructionQueue = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
        }

        public void LoadContent(ContentManager Content)
        {
            Castellar15 = Content.Load<SpriteFont>("Sprite Fonts/Castellar15Point");
            unselectedButtonTexture = Content.Load<Texture2D>("Button Textures/UnselectedButtonTexture1");
            selectedButtonTexture = Content.Load<Texture2D>("Button Textures/SelectedButtonTexture1");
        }

        public void Update(MouseState mouse, MouseState oldMouse, Planet planetSelected, List<Ship> constructionAvailableShips, int gold, int iron, int uranium)
        {
            shipsAvailableForConstruction = constructionAvailableShips;
            goldAmount = gold;
            ironAmount = iron;
            uraniumAmount = uranium;

            selectedPlanet = planetSelected;
            lineUnderColumnHeader.p1.X = selectedPlanet.managementMenuObject.menuRectangle.X;
            lineUnderColumnHeader.p1.Y = selectedPlanet.managementMenuObject.menuRectangle.Y + 145 + Castellar15.MeasureString("D").Y + 2;
            lineUnderColumnHeader.p2.X = selectedPlanet.managementMenuObject.menuRectangle.Right;
            lineUnderColumnHeader.p2.Y = lineUnderColumnHeader.p1.Y;
            lineUnderColumnHeader.Update();

            buildButtons.Clear();
            for(int x = 0; x < shipsAvailableForConstruction.Count; x++)
            {
                buildButtons.Add(new Button(new Rectangle(selectedPlanet.managementMenuObject.menuRectangle.Right - 100, (int)(lineUnderColumnHeader.p1.Y + lineUnderColumnHeader.thickness + 13 + (x * 50)), 100, 50), unselectedButtonTexture, selectedButtonTexture, "Build", Castellar15, Color.White, null, null));
                buildButtons[x].Update(mouse, oldMouse);
                if (buildButtons[x].isClicked)
                {
                    if(shipsAvailableForConstruction[x].getName().Equals("Corvette") || shipsAvailableForConstruction[x].getName().Equals("Destroyer") || shipsAvailableForConstruction[x].getName().Equals("Cruiser"))
                    {
                        if (shipsAvailableForConstruction[x].getName().Equals("Corvette") && goldAmount >= 100 && ironAmount >= 5)
                        {
                            goldAmount -= 100;
                            ironAmount -= 5;
                            planetSelected.shipsQueue.queuedShips.Add(new Ship(shipsAvailableForConstruction[x].getAttack(), shipsAvailableForConstruction[x].getDefence(), shipsAvailableForConstruction[x].getMoves(), shipsAvailableForConstruction[x].getConCost(), shipsAvailableForConstruction[x].getName()));
                        }
                        if (shipsAvailableForConstruction[x].getName().Equals("Destroyer") && goldAmount >= 200 && ironAmount >= 10 && uraniumAmount >= 10)
                        {
                            goldAmount -= 200;
                            ironAmount -= 10;
                            uraniumAmount -= 10;
                            planetSelected.shipsQueue.queuedShips.Add(new Ship(shipsAvailableForConstruction[x].getAttack(), shipsAvailableForConstruction[x].getDefence(), shipsAvailableForConstruction[x].getMoves(), shipsAvailableForConstruction[x].getConCost(), shipsAvailableForConstruction[x].getName()));
                        }
                        if (shipsAvailableForConstruction[x].getName().Equals("Cruiser") && goldAmount >= 300 && ironAmount >= 15 && uraniumAmount >= 15)
                        {
                            goldAmount -= 300;
                            ironAmount -= 15;
                            uraniumAmount -= 15;
                            planetSelected.shipsQueue.queuedShips.Add(new Ship(shipsAvailableForConstruction[x].getAttack(), shipsAvailableForConstruction[x].getDefence(), shipsAvailableForConstruction[x].getMoves(), shipsAvailableForConstruction[x].getConCost(), shipsAvailableForConstruction[x].getName()));
                        }
                    }
                    else
                    {
                        planetSelected.shipsQueue.queuedShips.Add(new Ship(shipsAvailableForConstruction[x].getAttack(), shipsAvailableForConstruction[x].getDefence(), shipsAvailableForConstruction[x].getMoves(), shipsAvailableForConstruction[x].getConCost(), shipsAvailableForConstruction[x].getName()));
                    }
                }
            }

            lineUnderAvailableShips.p1.X = selectedPlanet.managementMenuObject.menuRectangle.X;
            lineUnderAvailableShips.p1.Y = buildButtons[buildButtons.Count - 1].buttonRect.Bottom + 25;
            lineUnderAvailableShips.p2.X = selectedPlanet.managementMenuObject.menuRectangle.Right;
            lineUnderAvailableShips.p2.Y = lineUnderAvailableShips.p1.Y;
            lineUnderAvailableShips.Update();
            lineAboveConstructionQueue.p1.X = selectedPlanet.managementMenuObject.menuRectangle.X;
            lineAboveConstructionQueue.p1.Y = lineUnderAvailableShips.p1.Y + lineUnderAvailableShips.thickness + 2 + Castellar15.MeasureString("Construction Queue").Y + 2;
            lineAboveConstructionQueue.p2.X = selectedPlanet.managementMenuObject.menuRectangle.Right;
            lineAboveConstructionQueue.p2.Y = lineAboveConstructionQueue.p1.Y;
            lineAboveConstructionQueue.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Castellar15, "Design Name", new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X, selectedPlanet.managementMenuObject.menuRectangle.Y + 145), Color.White);
            spriteBatch.DrawString(Castellar15, "Attack", new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 250, selectedPlanet.managementMenuObject.menuRectangle.Y + 145), Color.White);
            spriteBatch.DrawString(Castellar15, "Defense", new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 383, selectedPlanet.managementMenuObject.menuRectangle.Y + 145), Color.White);
            spriteBatch.DrawString(Castellar15, "Movement", new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 525, selectedPlanet.managementMenuObject.menuRectangle.Y + 145), Color.White);
            spriteBatch.DrawString(Castellar15, "Cost", new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 675, selectedPlanet.managementMenuObject.menuRectangle.Y + 145), Color.White);
            lineUnderColumnHeader.Draw(spriteBatch);
            for(int x = 0; x < shipsAvailableForConstruction.Count; x++)
            {
                spriteBatch.DrawString(Castellar15, shipsAvailableForConstruction[x].getName(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X, lineUnderColumnHeader.p1.Y + lineUnderColumnHeader.thickness + 25 + (x * 50)), Color.White);
                spriteBatch.DrawString(Castellar15, "" + shipsAvailableForConstruction[x].getAttack(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 250, lineUnderColumnHeader.p1.Y + lineUnderColumnHeader.thickness + 25 + (x * 50)), Color.White);
                spriteBatch.DrawString(Castellar15, "" + shipsAvailableForConstruction[x].getDefence(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 383, lineUnderColumnHeader.p1.Y + lineUnderColumnHeader.thickness + 25 + (x * 50)), Color.White);
                spriteBatch.DrawString(Castellar15, "" + shipsAvailableForConstruction[x].getMoves(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 525, lineUnderColumnHeader.p1.Y + lineUnderColumnHeader.thickness + 25 + (x * 50)), Color.White);
                spriteBatch.DrawString(Castellar15, "" + shipsAvailableForConstruction[x].getConCost(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 675, lineUnderColumnHeader.p1.Y + lineUnderColumnHeader.thickness + 25 + (x * 50)), Color.White);
            }
            for(int x = 0; x < buildButtons.Count; x++)
            {
                buildButtons[x].Draw(spriteBatch);
            }
            lineUnderAvailableShips.Draw(spriteBatch);
            spriteBatch.DrawString(Castellar15, "Construction Queue", new Vector2(selectedPlanet.managementMenuObject.menuRectangle.Center.X - (Castellar15.MeasureString("Construction Queue").X / 2), lineUnderAvailableShips.p1.Y + lineUnderAvailableShips.thickness + 2), Color.White);
            lineAboveConstructionQueue.Draw(spriteBatch);
            for(int x = 0; x < selectedPlanet.shipsQueue.queuedShips.Count; x++)
            {
                if(lineAboveConstructionQueue.p1.Y + lineAboveConstructionQueue.thickness + 5 + (x * 25) + Castellar15.MeasureString(selectedPlanet.shipsQueue.queuedShips[x].getName()).Y < selectedPlanet.managementMenuObject.menuRectangle.Bottom - 25)
                {
                    spriteBatch.DrawString(Castellar15, selectedPlanet.shipsQueue.queuedShips[x].getName(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X, lineAboveConstructionQueue.p1.Y + lineAboveConstructionQueue.thickness + 5 + (x * 25)), Color.White);
                    spriteBatch.DrawString(Castellar15, "" + selectedPlanet.shipsQueue.queuedShips[x].getAttack(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 250, lineAboveConstructionQueue.p1.Y + lineAboveConstructionQueue.thickness + 5 + (x * 25)), Color.White);
                    spriteBatch.DrawString(Castellar15, "" + selectedPlanet.shipsQueue.queuedShips[x].getDefence(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 383, lineAboveConstructionQueue.p1.Y + lineAboveConstructionQueue.thickness + 5 + (x * 25)), Color.White);
                    spriteBatch.DrawString(Castellar15, "" + selectedPlanet.shipsQueue.queuedShips[x].getMoves(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 525, lineAboveConstructionQueue.p1.Y + lineAboveConstructionQueue.thickness + 5 + (x * 25)), Color.White);
                    spriteBatch.DrawString(Castellar15, "" + selectedPlanet.shipsQueue.queuedShips[x].getConCost(), new Vector2(selectedPlanet.managementMenuObject.menuRectangle.X + 675, lineAboveConstructionQueue.p1.Y + lineAboveConstructionQueue.thickness + 5 + (x * 25)), Color.White);
                }
            }
        }
    }
}
