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

        //Planet Stats Menu
        static Planet currentPlanet;
        static bool planetMenu;
        bool planetManagementMenuOpen;
        bool planetMenuLastFrame;

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

        public Button endTurnButton;
        public Button techTreeButton;

        public List<string> PlanetNames;
        public List<Rectangle> PlanetRectangles;
        public List<Player> playerList;
        Rectangle temporaryPlanetRect;
        public List<Vector2> positionsDrawnAlready;
        bool skipPlanet;

        Vector2 textSize;

        public PlayerUI(Texture2D topBarTexture, Texture2D flagTexture, Texture2D whiteTexture, Texture2D ironTexture, Texture2D uraniumTexture, Texture2D tungstenTexture, Texture2D hydrogenTexture, Texture2D nitrogenTexture, Texture2D oxygenTexture, Texture2D selectedButtonTexture, Texture2D unselectedButtonTexture, GraphicsDevice GraphicsDevice, SpriteFont arial15)
        {
            barTexture = topBarTexture;
            flagTexture = this.flagTexture;
            this.whiteTexture = whiteTexture;
            ironResourceTexture = ironTexture;
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
            textSize = new Vector2(0, 0);
            endTurnButton = new Button(new Rectangle(1755, barRect.Center.Y - (55 / 2) + 3, 150, 50), unselectedButtonTexture, selectedButtonTexture, "End Turn", Arial15, Color.White, null, null);
            techTreeButton = new Button(new Rectangle(1605, barRect.Center.Y - (55 / 2) + 3, 150, 50), unselectedButtonTexture, selectedButtonTexture, "Tech Tree", Arial15, Color.White, null, null);
            PlanetNames = new List<string>();
            PlanetRectangles = new List<Rectangle>();
            playerList = new List<Player>();
            temporaryPlanetRect = new Rectangle(0, 0, 0, 0);
            positionsDrawnAlready = new List<Vector2>();
            skipPlanet = false;
            planetManagementMenuOpen = false;
            planetMenuLastFrame = false;
        }

        public void Update(Texture2D playerFlagTexture, MouseState mouse, MouseState oldMouse)
        {
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
            }
            if (planetMenu == false)
                planetManagementMenuOpen = false;
            if (planetManagementMenuOpen)
            {
                playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.Update(mouse, oldMouse);
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
                spriteBatch.DrawString(Arial15, currentPlanet.planetShips.Count.ToString(), new Vector2(500,500), Color.White);
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
                spriteBatch.Draw(whiteTexture, new Rectangle((int)playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X, (int)playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y, (int)playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Z, (int)playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.W), Color.Blue);
                spriteBatch.DrawString(Arial15, playerList[playerID].ownedPlanets[indexOfPlanetSelected].planetName, new Vector2(playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.X, playerList[playerID].ownedPlanets[indexOfPlanetSelected].managementMenuObject.menuRectangle.Y), Color.White);
            }
        }
    }
}
