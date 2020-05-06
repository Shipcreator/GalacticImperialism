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

namespace GalacticImperialism
{
    /// <summary>
    /// //This Class should contain mostly static methods as there only needs to be one per computer, and it is NOT sent over the network.
    /// </summary>
    class PlayerUI
    {
        enum Tabs
        {
            Buildings = 1,
            Ships = 2
        }
        Tabs tabSelected;

        Texture2D barTexture;
        Texture2D flagTexture;
        Texture2D ironResourceTexture;
        Texture2D uraniumResourceTexture;
        Texture2D tungstenResourceTexture;
        Texture2D hydrogenResourceTexture;
        Texture2D nitrogenResourceTexture;
        Texture2D oxygenResourceTexture;
        Texture2D whiteTexture;
        Texture2D selectedButtonTexture;
        Texture2D unselectedButtonTexture;
        Texture2D temporaryTexture;
        Texture2D shipTexture;

        Board board;

        Rectangle barRect;
        Rectangle flagRect;
        Rectangle ironResourceRect;
        Rectangle uraniumResourceRect;
        Rectangle tungstenResourceRect;
        Rectangle hydrogenResourceRect;
        Rectangle nitrogenResourceRect;
        Rectangle oxygenResourceRect;
        static Rectangle selection;

        SpriteFont Arial15;
        SpriteFont Castellar15;
        SpriteFont Castellar20;

        //Planet Stats Menu
        static Planet currentPlanet;
        static bool planetMenu;
        public static bool planetManagementMenuOpen;
        bool planetMenuLastFrame;
        bool shipNotAlreadySelected;

        //Tech Tree Menu
        static bool techMenu;

        //Lines
        static List<Line> lines = new List<Line>();

        GraphicsDevice GraphicsDevice;

        public int ironAmount;
        public int uraniumAmount;
        public int tungstenAmount;
        public int hydrogenAmount;
        public int nitrogenAmount;
        public int oxygenAmount;

        public int playerID;
        int indexOfPlanetSelected;
        public static List<Ship> shipsSelected;

        public Button endTurnButton;
        public Button techTreeButton;
        Button closePlanetManagementMenuButton;
        Button confirmChangePlanetNameButton;
        List<Button> tabButtons;

        TextBox changePlanetNameTextBox;

        public List<string> PlanetNames;
        public List<Rectangle> PlanetRectangles;
        List<Rectangle> shipIconRects;
        public List<Player> playerList;
        Rectangle temporaryPlanetRect;
        public List<Vector2> positionsDrawnAlready;
        bool skipPlanet;

        Vector2 textSize;

        Line topLine;
        Line lineUnderResources;

        public PlayerUI(Texture2D topBarTexture, Texture2D flagTexture, Texture2D whiteTexture, Texture2D ironTexture, Texture2D uraniumTexture, Texture2D tungstenTexture, Texture2D hydrogenTexture, Texture2D nitrogenTexture, Texture2D oxygenTexture, Texture2D selectedButtonTexture, Texture2D unselectedButtonTexture, GraphicsDevice GraphicsDevice, SpriteFont arial15, SpriteFont castellar20, SpriteFont castellar15, Texture2D textureOfShip)
        {
            barTexture = topBarTexture;
            flagTexture = this.flagTexture;
            this.whiteTexture = whiteTexture;
            ironResourceTexture = ironTexture;
            shipTexture = textureOfShip;
            uraniumResourceTexture = uraniumTexture;
            tungstenResourceTexture = tungstenTexture;
            hydrogenResourceTexture = hydrogenTexture;
            nitrogenResourceTexture = nitrogenTexture;
            oxygenResourceTexture = oxygenTexture;
            this.selectedButtonTexture = selectedButtonTexture;
            this.unselectedButtonTexture = unselectedButtonTexture;
            planetMenu = false;
            techMenu = false;
            Arial15 = arial15;
            Castellar20 = castellar20;
            Castellar15 = castellar15;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            barRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, (int)((GraphicsDevice.Viewport.Height / 100.0f) * 6.25f));
            flagRect = new Rectangle((int)((barRect.Width / 1920.0f) * 15), (int)((barRect.Height / 135.0f) * 15), (int)((((int)(barRect.Height - ((barRect.Height / 135.0f) * 30))) / 3.0f) * 5.0f), (int)(barRect.Height - ((barRect.Height / 135.0f) * 30)));
            ironResourceRect = new Rectangle(125, barRect.Center.Y - (40 / 2), 40, 40);
            uraniumResourceRect = new Rectangle(275, barRect.Center.Y - (40 / 2), 40, 40);
            tungstenResourceRect = new Rectangle(450, barRect.Center.Y - (40 / 2), 40, 40);
            hydrogenResourceRect = new Rectangle(650, barRect.Center.Y - (40 / 2), 40, 40);
            nitrogenResourceRect = new Rectangle(850, barRect.Center.Y - (40 / 2), 40, 40);
            oxygenResourceRect = new Rectangle(1050, barRect.Center.Y - (40 / 2), 40, 40);
            ironAmount = 0;
            uraniumAmount = 0;
            tungstenAmount = 0;
            hydrogenAmount = 0;
            nitrogenAmount = 0;
            oxygenAmount = 0;
            playerID = 0;
            indexOfPlanetSelected = 0;
            shipsSelected = new List<Ship>();
            textSize = new Vector2(0, 0);
            endTurnButton = new Button(new Rectangle(1755, barRect.Center.Y - (55 / 2) + 3, 150, 50), unselectedButtonTexture, selectedButtonTexture, "End Turn", Arial15, Color.White, null, null);
            techTreeButton = new Button(new Rectangle(1605, barRect.Center.Y - (55 / 2) + 3, 150, 50), unselectedButtonTexture, selectedButtonTexture, "Tech Tree", Arial15, Color.White, null, null);
            closePlanetManagementMenuButton = new Button(new Rectangle(0, 0, 100, 100), unselectedButtonTexture, selectedButtonTexture, "X", Castellar20, Color.White, null, null);
            confirmChangePlanetNameButton = new Button(new Rectangle(0, 0, 150, 50), unselectedButtonTexture, selectedButtonTexture, "Confirm", Castellar15, Color.White, null, null);
            tabButtons = new List<Button>();
            tabSelected = Tabs.Buildings;
            tabButtons.Add(new Button(new Rectangle(0, 0, 150, 50), unselectedButtonTexture, selectedButtonTexture, "Buildings", Castellar15, Color.White, null, null));
            tabButtons.Add(new Button(new Rectangle(0, 0, 150, 50), unselectedButtonTexture, selectedButtonTexture, "Ships", Castellar15, Color.White, null, null));
            changePlanetNameTextBox = new TextBox(new Rectangle(0, 0, 200, 50), 3, 20, Color.Black, Color.White, Color.White, Color.White, GraphicsDevice, Arial15);
            PlanetNames = new List<string>();
            PlanetRectangles = new List<Rectangle>();
            shipIconRects = new List<Rectangle>();
            playerList = new List<Player>();
            temporaryPlanetRect = new Rectangle(0, 0, 0, 0);
            positionsDrawnAlready = new List<Vector2>();
            skipPlanet = false;
            planetManagementMenuOpen = false;
            planetMenuLastFrame = false;
            shipNotAlreadySelected = true;
            topLine = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
            lineUnderResources = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
        }

        public void Update(Texture2D playerFlagTexture, MouseState mouse, MouseState oldMouse, KeyboardState kb, KeyboardState oldKb)
        {
            if(shipIconRects.Count > 0)
            {
                shipIconRects.Clear();
            }

            ironAmount = playerList[playerID].getResources()[3];
            uraniumAmount = playerList[playerID].getResources()[5];
            tungstenAmount = playerList[playerID].getResources()[4];
            hydrogenAmount = playerList[playerID].getResources()[0];
            nitrogenAmount = playerList[playerID].getResources()[2];
            oxygenAmount = playerList[playerID].getResources()[1];

            foreach (Line line in lines)
            {
                line.Update();
            }
            flagTexture = playerFlagTexture;
            endTurnButton.Update(mouse, oldMouse);
            techTreeButton.Update(mouse, oldMouse);
            if (positionsDrawnAlready.Count > 0)
                positionsDrawnAlready.Clear();
            if (planetMenu && planetMenuLastFrame && mouse.X >= (int)currentPlanet.position.X - (int)(currentPlanet.size * 2.5) && mouse.X <= ((int)currentPlanet.position.X - (int)(currentPlanet.size * 2.5)) + (currentPlanet.size * 30) && mouse.Y >= (int)currentPlanet.position.Y - (int)(currentPlanet.size * 2.5) && mouse.Y <= ((int)currentPlanet.position.Y - (int)(currentPlanet.size * 2.5)) + (currentPlanet.size * 30) && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
            {
                for(int x = 0; x < playerList[playerID].ownedPlanets.Count; x++)
                {
                    if(playerList[playerID].ownedPlanets[x].position == currentPlanet.position)
                    {
                        indexOfPlanetSelected = x;
                        break;
                    }
                }
                planetManagementMenuOpen = true;
                shipsSelected.Clear();
            }
            if (planetMenu == false)
                planetManagementMenuOpen = false;
            if (planetManagementMenuOpen)
            {
                playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.Update(mouse, oldMouse);
                closePlanetManagementMenuButton.buttonRect.X = playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Right - closePlanetManagementMenuButton.buttonRect.Width;
                closePlanetManagementMenuButton.buttonRect.Y = playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y;
                closePlanetManagementMenuButton.Update(mouse, oldMouse);
                changePlanetNameTextBox.Update(mouse, oldMouse, kb, oldKb);
                changePlanetNameTextBox.outlineRect.X = playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X;
                changePlanetNameTextBox.outlineRect.Y = playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + (int)Castellar20.MeasureString(playerList[playerID].ownedPlanets[indexOfPlanetSelected].planetName).Y;
                changePlanetNameTextBox.boxRect.X = changePlanetNameTextBox.outlineRect.X + changePlanetNameTextBox.textBoxOutlineWidth;
                changePlanetNameTextBox.boxRect.Y = changePlanetNameTextBox.outlineRect.Y + changePlanetNameTextBox.textBoxOutlineWidth;
                confirmChangePlanetNameButton.buttonRect.X = changePlanetNameTextBox.boxRect.Right + 10;
                confirmChangePlanetNameButton.buttonRect.Y = changePlanetNameTextBox.boxRect.Center.Y - (confirmChangePlanetNameButton.buttonRect.Height / 2);
                confirmChangePlanetNameButton.Update(mouse, oldMouse);
                if (confirmChangePlanetNameButton.isClicked)
                    playerList[playerID].ownedPlanets[indexOfPlanetSelected].planetName = changePlanetNameTextBox.text;
                for(int x = tabButtons.Count - 1; x > -1; x--)
                {
                    if(x == tabButtons.Count - 1)
                        tabButtons[x].buttonRect.X = playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Right - tabButtons[x].buttonRect.Width;
                    else
                        tabButtons[x].buttonRect.X = tabButtons[x + 1].buttonRect.X - 1 - tabButtons[x].buttonRect.Width;
                    tabButtons[x].buttonRect.Y = playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Bottom - tabButtons[x].buttonRect.Height;
                    tabButtons[x].Update(mouse, oldMouse);
                }
                if (tabButtons[0].isClicked)
                    tabSelected = Tabs.Buildings;
                if (tabButtons[1].isClicked)
                {
                    tabSelected = Tabs.Ships;
                    //playerList[playerID].ownedPlanets[indexOfPlanetSelected].planetShips.Add(new Ship(1, 1, 1, 1, "test"));
                }

                topLine.p1 = new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 100);
                topLine.p2 = new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Right, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 100);
                topLine.Update();
                lineUnderResources.p1 = new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 140);
                lineUnderResources.p2 = new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Right, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 140);
                lineUnderResources.Update();

                if(tabSelected == Tabs.Ships)
                {
                    for(int x = 0; x < playerList[playerID].ownedPlanets[indexOfPlanetSelected].planetShips.Count; x++)
                    {
                        if(x == 0)
                        {
                            shipIconRects.Add(new Rectangle(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 200, 100, 100));
                        }
                        else
                        {
                            if(shipIconRects[x - 1].Right + 150 <= playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Right)
                            {
                                shipIconRects.Add(new Rectangle(shipIconRects[x - 1].Right + 1, shipIconRects[x - 1].Y, 100, 100));
                            }
                            else
                            {
                                shipIconRects.Add(new Rectangle(shipIconRects[0].X, shipIconRects[x - 1].Bottom + 15, 100, 100));
                            }
                        }
                    }
                    for(int x = 0; x < shipIconRects.Count; x++)
                    {
                        shipNotAlreadySelected = true;
                        if(mouse.X >= shipIconRects[x].X && mouse.X <= shipIconRects[x].Right && mouse.Y >= shipIconRects[x].Y && mouse.Y <= shipIconRects[x].Bottom && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                        {
                            for(int y = shipsSelected.Count - 1; y > -1; y--)
                            {
                                if (shipsSelected[y] == currentPlanet.planetShips[x])
                                {
                                    shipNotAlreadySelected = false;
                                    shipsSelected.RemoveAt(y);
                                }
                            }
                            if (shipNotAlreadySelected)
                                shipsSelected.Add(currentPlanet.planetShips[x]);
                        }
                    }
                }

                if (closePlanetManagementMenuButton.isClicked)
                {
                    planetManagementMenuOpen = false;
                    tabSelected = Tabs.Buildings;
                }
            }

            planetMenuLastFrame = planetMenu;
        }

        public void InitBoard(Board b)
        {
            board = b;
        }

        //Called When a Player Clicks on a Planet
        public static void drawPlanetMenu(Planet p, List<Planet> nearby)
        {
            currentPlanet = p;
            selection = new Rectangle((int)p.position.X - (int)(p.size * 2.5), (int)p.position.Y - (int)(p.size * 2.5), p.size * 30, p.size * 30);
            nearbyPlanetLines(nearby);
            planetMenu = true;
        }

        private static void nearbyPlanetLines(List<Planet> nearby)
        {
            lines = new List<Line>();
            foreach (Planet p in nearby)
            {
                Vector2 temp1 = new Vector2(currentPlanet.position.X + ((currentPlanet.size * 25) /2), currentPlanet.position.Y + ((currentPlanet.size * 25) / 2));
                Vector2 temp2 = new Vector2(p.position.X + ((p.size * 25) / 2), p.position.Y + ((p.size * 25) / 2));
                Line temp = new Line(temp1, temp2, 3, Color.White * .25f, Game1.whiteTexture);
                lines.Add(temp);
            }
        }

        public static void closeMenus()
        {
            planetMenu = false;
            techMenu = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(barTexture, barRect, Color.White);
            spriteBatch.Draw(flagTexture, flagRect, Color.White);
            //spriteBatch.Draw(whiteTexture, ironResourceRect, Color.Black);
            spriteBatch.Draw(ironResourceTexture, ironResourceRect, Color.White);
            Vector2 textSize = Arial15.MeasureString("Iron: " + ironAmount);
            spriteBatch.DrawString(Arial15, "Iron: " + ironAmount, new Vector2(ironResourceRect.Right + 10, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            //spriteBatch.Draw(whiteTexture, uraniumResourceRect, Color.Black);
            spriteBatch.Draw(uraniumResourceTexture, uraniumResourceRect, Color.White);
            textSize = Arial15.MeasureString("Uranium: " + uraniumAmount);
            spriteBatch.DrawString(Arial15, "Uranium: " + uraniumAmount, new Vector2(uraniumResourceRect.Right + 10, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            //spriteBatch.Draw(whiteTexture, tungstenResourceRect, Color.Black);
            spriteBatch.Draw(tungstenResourceTexture, tungstenResourceRect, Color.White);
            textSize = Arial15.MeasureString("Tungsten: " + tungstenAmount);
            spriteBatch.DrawString(Arial15, "Tungsten: " + tungstenAmount, new Vector2(tungstenResourceRect.Right + 10, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            //spriteBatch.Draw(whiteTexture, hydrogenResourceRect, Color.Black);
            spriteBatch.Draw(hydrogenResourceTexture, hydrogenResourceRect, Color.White);
            textSize = Arial15.MeasureString("Hydrogen: " + hydrogenAmount);
            spriteBatch.DrawString(Arial15, "Hydrogen: " + hydrogenAmount, new Vector2(hydrogenResourceRect.Right + 10, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            //spriteBatch.Draw(whiteTexture, nitrogenResourceRect, Color.Black);
            spriteBatch.Draw(nitrogenResourceTexture, nitrogenResourceRect, Color.White);
            textSize = Arial15.MeasureString("Nitrogen: " + nitrogenAmount);
            spriteBatch.DrawString(Arial15, "Nitrogen: " + nitrogenAmount, new Vector2(nitrogenResourceRect.Right + 10, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            //spriteBatch.Draw(whiteTexture, oxygenResourceRect, Color.Black);
            spriteBatch.Draw(oxygenResourceTexture, oxygenResourceRect, Color.White);
            textSize = Arial15.MeasureString("Oxygen: " + oxygenAmount);
            spriteBatch.DrawString(Arial15, "Oxygen: " + oxygenAmount, new Vector2(oxygenResourceRect.Right + 10, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            endTurnButton.Draw(spriteBatch);
            techTreeButton.Draw(spriteBatch);

            if (planetMenu == true)
            {
                foreach (Line line in lines)
                {
                    line.Draw(spriteBatch);
                }
                spriteBatch.Draw(Game1.whiteCircle, selection, Color.White * 0.25f);
            }

            for(int x = 0; x < playerList.Count; x++)
            {
                for(int y = 0; y < playerList[x].ownedPlanets.Count; y++)
                {
                    textSize = Arial15.MeasureString(playerList[x].ownedPlanets[y].planetName);
                    temporaryPlanetRect = new Rectangle((int)playerList[x].ownedPlanets[y].position.X, (int)playerList[x].ownedPlanets[y].position.Y, playerList[x].ownedPlanets[y].size * 25, playerList[x].ownedPlanets[y].size * 25);
                    spriteBatch.DrawString(Arial15, playerList[x].ownedPlanets[y].planetName, new Vector2(temporaryPlanetRect.Center.X - (textSize.X / 2), temporaryPlanetRect.Bottom), playerList[x].empireColor);
                    positionsDrawnAlready.Add(new Vector2(temporaryPlanetRect.Center.X - (textSize.X / 2), temporaryPlanetRect.Bottom));
                }
            }

            for (int x = 0; x < PlanetNames.Count; x++)
            {
                skipPlanet = false;
                textSize = Arial15.MeasureString(PlanetNames[x]);
                for(int y = 0; y < positionsDrawnAlready.Count; y++)
                {
                    if (new Vector2(PlanetRectangles[x].Center.X - (textSize.X / 2), PlanetRectangles[x].Bottom) == positionsDrawnAlready[y])
                        skipPlanet = true;
                }
                if (skipPlanet)
                    continue;
                spriteBatch.DrawString(Arial15, PlanetNames[x], new Vector2(PlanetRectangles[x].Center.X - (textSize.X / 2), PlanetRectangles[x].Bottom), Color.White);
            }

            if (planetManagementMenuOpen)
            {
                spriteBatch.Draw(whiteTexture, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle, Color.Navy);
                closePlanetManagementMenuButton.Draw(spriteBatch);
                spriteBatch.DrawString(Castellar20, "Planet Name: " + playerList[playerID].ownedPlanets[indexOfPlanetSelected].planetName, new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y), Color.White);
                changePlanetNameTextBox.Draw(spriteBatch);
                confirmChangePlanetNameButton.Draw(spriteBatch);
                topLine.Draw(spriteBatch);
                lineUnderResources.Draw(spriteBatch);
                spriteBatch.DrawString(Castellar20, "Resources: ", new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 105), Color.White);
                spriteBatch.Draw(ironResourceTexture, new Rectangle(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + (int)Castellar20.MeasureString("Resources: ").X, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 110, 25, 25), Color.White);
                spriteBatch.DrawString(Castellar20, "" + playerList[playerID].ownedPlanets[indexOfPlanetSelected].resourceNumbers[0], new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + (int)Castellar20.MeasureString("Resources: ").X + 35, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 105), Color.White);
                spriteBatch.Draw(uraniumResourceTexture, new Rectangle(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 250, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 108, 25, 25), Color.White);
                spriteBatch.DrawString(Castellar20, "" + playerList[playerID].ownedPlanets[indexOfPlanetSelected].resourceNumbers[1], new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 280, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 105), Color.White);
                spriteBatch.Draw(tungstenResourceTexture, new Rectangle(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 325, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 108, 25, 25), Color.White);
                spriteBatch.DrawString(Castellar20, "" + playerList[playerID].ownedPlanets[indexOfPlanetSelected].resourceNumbers[2], new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 355, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 105), Color.White);
                spriteBatch.Draw(hydrogenResourceTexture, new Rectangle(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 410, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 108, 25, 25), Color.White);
                spriteBatch.DrawString(Castellar20, "" + playerList[playerID].ownedPlanets[indexOfPlanetSelected].resourceNumbers[3], new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 445, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 105), Color.White);
                spriteBatch.Draw(nitrogenResourceTexture, new Rectangle(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 490, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 108, 25, 25), Color.White);
                spriteBatch.DrawString(Castellar20, "" + playerList[playerID].ownedPlanets[indexOfPlanetSelected].resourceNumbers[4], new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 520, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 105), Color.White);
                spriteBatch.Draw(oxygenResourceTexture, new Rectangle(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 570, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 108, 25, 25), Color.White);
                spriteBatch.DrawString(Castellar20, "" + playerList[playerID].ownedPlanets[indexOfPlanetSelected].resourceNumbers[5], new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X + 605, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y + 105), Color.White);
                for(int x = 0; x < tabButtons.Count; x++)
                {
                    tabButtons[x].Draw(spriteBatch);
                }

                if (tabSelected == Tabs.Ships)
                {
                    for(int x = 0; x < shipIconRects.Count; x++)
                    {
                        for(int y = 0; y < shipsSelected.Count; y++)
                        {
                            if (currentPlanet.planetShips[x] == shipsSelected[y])
                                spriteBatch.Draw(whiteTexture, shipIconRects[x], Color.White * 0.50f);
                        }
                        spriteBatch.Draw(shipTexture, shipIconRects[x], Color.White);
                        textSize = Castellar15.MeasureString(playerList[playerID].ownedPlanets[indexOfPlanetSelected].planetShips[x].getName());
                        spriteBatch.DrawString(Castellar15, playerList[playerID].ownedPlanets[indexOfPlanetSelected].planetShips[x].getName(), new Vector2(shipIconRects[x].Center.X - (textSize.X / 2), shipIconRects[x].Bottom), Color.White);
                    }
                }
            }
        }
    }
}
