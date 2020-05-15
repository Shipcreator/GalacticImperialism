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
    class BuildingsTab
    {
        ContentManager Content;

        Texture2D whiteTexture;
        Texture2D researchFacilityBuildingTexture;
        Texture2D militaryBaseBuildingTexture;
        Texture2D factoryBuildingTexture;
        Texture2D unselectedButtonTexture;
        Texture2D selectedButtonTexture;

        SpriteFont tabFont;
        SpriteFont textFont;
        SpriteFont descriptionFont;
        SpriteFont smallestFont;

        public Planet planetSelected;

        Line rightSideLine;
        Line underButtonLine;
        Line underBuildingQueueTitleLine;
        Line underBuildingQueueTabsLine;
        Line typeFromSlotLine;
        Line slotFromTurnsLine;

        public int indexOfBuildingSlotSelected;
        public int whatToBuildType;
        int collectiveBuildCost;
        int numberOfFactories;
        int turnsToBuild;
        public int oxygenAmount;
        public int tungstenAmount;
        public int nitrogenAmount;

        Button demolishOrBuildButton;

        bool addBuildingToQueue;

        public BuildingsTab(ContentManager Content, Texture2D whiteTexture)
        {
            this.whiteTexture = whiteTexture;
            this.Content = Content;
            LoadContent();
            Initialize();
        }

        public void Initialize()
        {
            rightSideLine = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
            underButtonLine = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
            underBuildingQueueTitleLine = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
            underBuildingQueueTabsLine = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
            typeFromSlotLine = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
            slotFromTurnsLine = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
            indexOfBuildingSlotSelected = 0;
            whatToBuildType = 0;
            collectiveBuildCost = 0;
            numberOfFactories = 0;
            turnsToBuild = 0;
            oxygenAmount = 0;
            tungstenAmount = 0;
            nitrogenAmount = 0;
            demolishOrBuildButton = new Button(new Rectangle(0, 0, 200, 100), unselectedButtonTexture, selectedButtonTexture, "Build", textFont, Color.White, null, null);
            addBuildingToQueue = true;
        }

        public void LoadContent()
        {
            tabFont = Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point");
            textFont = Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point");
            descriptionFont = Content.Load<SpriteFont>("Sprite Fonts/Castellar15Point");
            smallestFont = Content.Load<SpriteFont>("Sprite Fonts/Castellar12Point");
            unselectedButtonTexture = Content.Load<Texture2D>("Button Textures/UnselectedButtonTexture1");
            selectedButtonTexture = Content.Load<Texture2D>("Button Textures/SelectedButtonTexture1");
            researchFacilityBuildingTexture = Content.Load<Texture2D>("Player UI/Planet Management Menu/Research Facility Icon");
            militaryBaseBuildingTexture = Content.Load<Texture2D>("Player UI/Planet Management Menu/Military Base Icon");
            factoryBuildingTexture = Content.Load<Texture2D>("Player UI/Planet Management Menu/Factory Icon");
        }

        public void Update(Planet selectedPlanet, MouseState mouse, MouseState oldMouse, int oxygen, int tungsten, int nitrogen)
        {
            oxygenAmount = oxygen;
            tungstenAmount = tungsten;
            nitrogenAmount = nitrogen;

            planetSelected = selectedPlanet;
            rightSideLine.p1.X = selectedPlanet.managementMenuObject.menuRectangle.X + 600;
            rightSideLine.p1.Y = selectedPlanet.managementMenuObject.menuRectangle.Y + 140;
            rightSideLine.p2.X = selectedPlanet.managementMenuObject.menuRectangle.X + 600;
            rightSideLine.p2.Y = selectedPlanet.managementMenuObject.menuRectangle.Bottom - 50;
            rightSideLine.Update();
            underButtonLine.p1.X = selectedPlanet.managementMenuObject.menuRectangle.Right - 300;
            underButtonLine.p1.Y = selectedPlanet.managementMenuObject.menuRectangle.Bottom - 300;
            underButtonLine.p2.X = selectedPlanet.managementMenuObject.menuRectangle.Right;
            underButtonLine.p2.Y = selectedPlanet.managementMenuObject.menuRectangle.Bottom - 300;
            underButtonLine.Update();
            underBuildingQueueTitleLine.p1.X = rightSideLine.p1.X + rightSideLine.thickness;
            underBuildingQueueTitleLine.p1.Y = planetSelected.managementMenuObject.menuRectangle.Bottom - 300 + descriptionFont.MeasureString("Building Queue").Y;
            underBuildingQueueTitleLine.p2.X = planetSelected.managementMenuObject.menuRectangle.Right;
            underBuildingQueueTitleLine.p2.Y = underBuildingQueueTitleLine.p1.Y;
            underBuildingQueueTitleLine.Update();
            underBuildingQueueTabsLine.p1.X = rightSideLine.p1.X + rightSideLine.thickness;
            underBuildingQueueTabsLine.p1.Y = underBuildingQueueTitleLine.p1.Y + underBuildingQueueTitleLine.thickness + descriptionFont.MeasureString("Type").Y;
            underBuildingQueueTabsLine.p2.X = planetSelected.managementMenuObject.menuRectangle.Right;
            underBuildingQueueTabsLine.p2.Y = underBuildingQueueTabsLine.p1.Y;
            underBuildingQueueTabsLine.Update();
            typeFromSlotLine.p1.X = planetSelected.managementMenuObject.menuRectangle.Right - 155;
            typeFromSlotLine.p1.Y = underBuildingQueueTitleLine.p1.Y;
            typeFromSlotLine.p2.X = typeFromSlotLine.p1.X;
            typeFromSlotLine.p2.Y = planetSelected.managementMenuObject.menuRectangle.Bottom - 50;
            typeFromSlotLine.Update();
            slotFromTurnsLine.p1.X = planetSelected.managementMenuObject.menuRectangle.Right - descriptionFont.MeasureString("Turns").X - 5;
            slotFromTurnsLine.p1.Y = underBuildingQueueTitleLine.p1.Y;
            slotFromTurnsLine.p2.X = slotFromTurnsLine.p1.X;
            slotFromTurnsLine.p2.Y = planetSelected.managementMenuObject.menuRectangle.Bottom - 50;
            slotFromTurnsLine.Update();

            for(int x = 0; x < planetSelected.buildingSlotsList.Count; x++)
            {
                if(x == 0)
                {
                    planetSelected.buildingSlotsList[x].Update(new Vector2(planetSelected.managementMenuObject.menuRectangle.X, planetSelected.managementMenuObject.menuRectangle.Y + 140));
                }
                else
                {
                    if(planetSelected.buildingSlotsList[x - 1].buildingSlotRectangle.Right + 100 <= rightSideLine.p1.X)
                    {
                        planetSelected.buildingSlotsList[x].Update(new Vector2(planetSelected.buildingSlotsList[x - 1].buildingSlotRectangle.Right, planetSelected.buildingSlotsList[x - 1].buildingSlotRectangle.Y));
                    }
                    else
                    {
                        planetSelected.buildingSlotsList[x].Update(new Vector2(planetSelected.managementMenuObject.menuRectangle.X, planetSelected.buildingSlotsList[x - 1].buildingSlotRectangle.Bottom));
                    }
                }
                if(mouse.X >= planetSelected.buildingSlotsList[x].buildingSlotRectangle.X && mouse.X <=planetSelected.buildingSlotsList[x].buildingSlotRectangle.Right && mouse.Y >= planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y && mouse.Y <= planetSelected.buildingSlotsList[x].buildingSlotRectangle.Bottom && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    indexOfBuildingSlotSelected = x;
                }
            }

            if(planetSelected.buildingSlotsList[indexOfBuildingSlotSelected].typeOfBuilding == BuildingSlot.BuildingType.Empty)
            {
                if (mouse.X >= (int)(rightSideLine.p1.X + 10) && mouse.X <= (int)(rightSideLine.p1.X + 10) + 100 && mouse.Y >= planetSelected.managementMenuObject.menuRectangle.Y + 150 && mouse.Y <= planetSelected.managementMenuObject.menuRectangle.Y + 250 && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                    whatToBuildType = 0;
                if (mouse.X >= (int)(rightSideLine.p1.X + 10) && mouse.X <= (int)(rightSideLine.p1.X + 10) + 100 && mouse.Y >= planetSelected.managementMenuObject.menuRectangle.Y + 260 && mouse.Y <= planetSelected.managementMenuObject.menuRectangle.Y + 360 && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                    whatToBuildType = 1;
                if (mouse.X >= (int)(rightSideLine.p1.X + 10) && mouse.X <= (int)(rightSideLine.p1.X + 10) + 100 && mouse.Y >= planetSelected.managementMenuObject.menuRectangle.Y + 370 && mouse.Y <= planetSelected.managementMenuObject.menuRectangle.Y + 470 && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                    whatToBuildType = 2;
            }

            demolishOrBuildButton.buttonRect.X = planetSelected.managementMenuObject.menuRectangle.Right - 250;
            demolishOrBuildButton.buttonRect.Y = planetSelected.managementMenuObject.menuRectangle.Bottom - 400;
            demolishOrBuildButton.Update(mouse, oldMouse);
            if (planetSelected.buildingSlotsList[indexOfBuildingSlotSelected].typeOfBuilding == BuildingSlot.BuildingType.Empty)
            {
                demolishOrBuildButton.buttonText = "Build";
                if (demolishOrBuildButton.isClicked)
                {
                    addBuildingToQueue = true;
                    for(int x = 0; x < planetSelected.buildingQueue.queuedBuildings.Count; x++)
                    {
                        if (planetSelected.buildingQueue.queuedBuildings[x].buildingSlotIndex == indexOfBuildingSlotSelected)
                            addBuildingToQueue = false;
                    }
                    if (addBuildingToQueue)
                    {
                        if (whatToBuildType == 0 && nitrogenAmount >= 5)
                        {
                            nitrogenAmount -= 5;
                            planetSelected.buildingQueue.AddBuildingToQueue("ResearchFacility", indexOfBuildingSlotSelected);
                        }
                        if (whatToBuildType == 1 && tungstenAmount >= 5)
                        {
                            tungstenAmount -= 5;
                            planetSelected.buildingQueue.AddBuildingToQueue("MilitaryBase", indexOfBuildingSlotSelected);
                        }
                        if (whatToBuildType == 2 && oxygenAmount >= 5)
                        {
                            oxygenAmount -= 5;
                            planetSelected.buildingQueue.AddBuildingToQueue("Factory", indexOfBuildingSlotSelected);
                        }
                    }
                }
            }
            else
            {
                demolishOrBuildButton.buttonText = "Demolish";
                if (demolishOrBuildButton.isClicked)
                    planetSelected.buildingSlotsList[indexOfBuildingSlotSelected].typeOfBuilding = BuildingSlot.BuildingType.Empty;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rightSideLine.Draw(spriteBatch);
            demolishOrBuildButton.Draw(spriteBatch);
            underButtonLine.Draw(spriteBatch);

            for (int x = 0; x < planetSelected.buildingSlotsList.Count; x++)
            {
                if (x != indexOfBuildingSlotSelected)
                    spriteBatch.Draw(whiteTexture, planetSelected.buildingSlotsList[x].buildingSlotRectangle, Color.White);
                else
                {
                    spriteBatch.Draw(whiteTexture, planetSelected.buildingSlotsList[x].buildingSlotRectangle, Color.Gold);
                    spriteBatch.DrawString(descriptionFont, "Building Slot: " + (x + 1), new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (descriptionFont.MeasureString("Building Slot: " + (x + 1)).X / 2), planetSelected.managementMenuObject.menuRectangle.Bottom - 400 - descriptionFont.MeasureString("Building Slot: " + (x + 1)).Y), Color.White);
                }
                spriteBatch.Draw(whiteTexture, new Rectangle(planetSelected.buildingSlotsList[x].buildingSlotRectangle.X + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Width - 10, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Height - 10), Color.Black);
                if (planetSelected.buildingSlotsList[x].typeOfBuilding == BuildingSlot.BuildingType.Empty)
                {
                    if(x != indexOfBuildingSlotSelected)
                        spriteBatch.DrawString(tabFont, "+", new Vector2(planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.X - (tabFont.MeasureString("+").X / 2), planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.Y - (tabFont.MeasureString("+").Y / 2)), Color.White);
                    else
                    {
                        spriteBatch.DrawString(tabFont, "+", new Vector2(planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.X - (tabFont.MeasureString("+").X / 2), planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.Y - (tabFont.MeasureString("+").Y / 2)), Color.Gold);
                        if(whatToBuildType != 0)
                            spriteBatch.Draw(whiteTexture, new Rectangle((int)(rightSideLine.p1.X + 10), planetSelected.managementMenuObject.menuRectangle.Y + 150, 100, 100), Color.White);
                        else
                            spriteBatch.Draw(whiteTexture, new Rectangle((int)(rightSideLine.p1.X + 10), planetSelected.managementMenuObject.menuRectangle.Y + 150, 100, 100), Color.Gold);
                        spriteBatch.Draw(whiteTexture, new Rectangle((int)(rightSideLine.p1.X + 10) + 5, planetSelected.managementMenuObject.menuRectangle.Y + 150 + 5, 90, 90), Color.Black);
                        spriteBatch.Draw(researchFacilityBuildingTexture, new Rectangle((int)(rightSideLine.p1.X + 10) + 5, planetSelected.managementMenuObject.menuRectangle.Y + 150 + 5, 90, 90), Color.White);
                        spriteBatch.DrawString(smallestFont, "Research Facility", new Vector2((int)(rightSideLine.p1.X + 10) + 100 + 10, planetSelected.managementMenuObject.menuRectangle.Y + 150), Color.White);
                        spriteBatch.DrawString(smallestFont, "Boosts empire's\nscience\nproduction", new Vector2((int)(rightSideLine.p1.X + 10) + 100 + 10, planetSelected.managementMenuObject.menuRectangle.Y + 150 + 35), Color.White);
                        if(whatToBuildType != 1)
                            spriteBatch.Draw(whiteTexture, new Rectangle((int)(rightSideLine.p1.X + 10), planetSelected.managementMenuObject.menuRectangle.Y + 260, 100, 100), Color.White);
                        else
                            spriteBatch.Draw(whiteTexture, new Rectangle((int)(rightSideLine.p1.X + 10), planetSelected.managementMenuObject.menuRectangle.Y + 260, 100, 100), Color.Gold);
                        spriteBatch.Draw(whiteTexture, new Rectangle((int)(rightSideLine.p1.X + 10) + 5, planetSelected.managementMenuObject.menuRectangle.Y + 260 + 5, 90, 90), Color.Black);
                        spriteBatch.Draw(militaryBaseBuildingTexture, new Rectangle((int)(rightSideLine.p1.X + 10) + 5, planetSelected.managementMenuObject.menuRectangle.Y + 260 + 5, 90, 90), Color.White);
                        spriteBatch.DrawString(smallestFont, "Military Base", new Vector2((int)(rightSideLine.p1.X + 10) + 100 + 10, planetSelected.managementMenuObject.menuRectangle.Y + 260), Color.White);
                        spriteBatch.DrawString(smallestFont, "Boosts the\nproduction of\nunits.", new Vector2((int)(rightSideLine.p1.X + 10) + 100 + 10, planetSelected.managementMenuObject.menuRectangle.Y + 260 + 35), Color.White);
                        if(whatToBuildType != 2)
                            spriteBatch.Draw(whiteTexture, new Rectangle((int)(rightSideLine.p1.X + 10), planetSelected.managementMenuObject.menuRectangle.Y + 370, 100, 100), Color.White);
                        else
                            spriteBatch.Draw(whiteTexture, new Rectangle((int)(rightSideLine.p1.X + 10), planetSelected.managementMenuObject.menuRectangle.Y + 370, 100, 100), Color.Gold);
                        spriteBatch.Draw(whiteTexture, new Rectangle((int)(rightSideLine.p1.X + 10) + 5, planetSelected.managementMenuObject.menuRectangle.Y + 370 + 5, 90, 90), Color.Black);
                        spriteBatch.Draw(factoryBuildingTexture, new Rectangle((int)(rightSideLine.p1.X + 10) + 5, planetSelected.managementMenuObject.menuRectangle.Y + 370 + 5, 90, 90), Color.White);
                        spriteBatch.DrawString(smallestFont, "Factory", new Vector2((int)(rightSideLine.p1.X + 10) + 100 + 10, planetSelected.managementMenuObject.menuRectangle.Y + 370), Color.White);
                        spriteBatch.DrawString(smallestFont, "Boosts the\nproduction of\nother buildings", new Vector2((int)(rightSideLine.p1.X + 10) + 100 + 10, planetSelected.managementMenuObject.menuRectangle.Y + 370 + 35), Color.White);
                    }
                }
                if(planetSelected.buildingSlotsList[x].typeOfBuilding == BuildingSlot.BuildingType.ResearchFacility)
                {
                    spriteBatch.Draw(researchFacilityBuildingTexture, new Rectangle(planetSelected.buildingSlotsList[x].buildingSlotRectangle.X + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Width - 10, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Height - 10), Color.White);
                    if(x == indexOfBuildingSlotSelected)
                    {
                        spriteBatch.DrawString(textFont, "Research Facility", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (textFont.MeasureString("Research Facility").X / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150), Color.White);
                        spriteBatch.Draw(researchFacilityBuildingTexture, new Rectangle(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (100 / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150 + (int)textFont.MeasureString("Research Facility").Y, 100, 100), Color.White);
                        spriteBatch.DrawString(descriptionFont, "Boosts empire's science", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (descriptionFont.MeasureString("Boosts empire's science").X / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150 + (int)textFont.MeasureString("Research Facility").Y + 100 + 10), Color.White);
                        spriteBatch.DrawString(descriptionFont, "per turn.", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (descriptionFont.MeasureString("per turn.").X / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150 + (int)textFont.MeasureString("Research Facility").Y + 100 + 10 + descriptionFont.MeasureString("Boosts empire's science").Y), Color.White);
                    }
                }
                if (planetSelected.buildingSlotsList[x].typeOfBuilding == BuildingSlot.BuildingType.MilitaryBase)
                {
                    spriteBatch.Draw(militaryBaseBuildingTexture, new Rectangle(planetSelected.buildingSlotsList[x].buildingSlotRectangle.X + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Width - 10, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Height - 10), Color.White);
                    if (x == indexOfBuildingSlotSelected)
                    {
                        spriteBatch.DrawString(textFont, "Military Base", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (textFont.MeasureString("Military Base").X / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150), Color.White);
                        spriteBatch.Draw(militaryBaseBuildingTexture, new Rectangle(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (100 / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150 + (int)textFont.MeasureString("Military Base").Y, 100, 100), Color.White);
                        spriteBatch.DrawString(descriptionFont, "Boosts the production", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (descriptionFont.MeasureString("Boosts the production").X / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150 + (int)textFont.MeasureString("Military Base").Y + 100 + 10), Color.White);
                        spriteBatch.DrawString(descriptionFont, "of units.", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (descriptionFont.MeasureString("of units.").X / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150 + (int)textFont.MeasureString("Military Base").Y + 100 + 10 + descriptionFont.MeasureString("Boosts the production").Y), Color.White);
                    }
                }
                if(planetSelected.buildingSlotsList[x].typeOfBuilding == BuildingSlot.BuildingType.Factory)
                {
                    spriteBatch.Draw(factoryBuildingTexture, new Rectangle(planetSelected.buildingSlotsList[x].buildingSlotRectangle.X + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Width - 10, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Height - 10), Color.White);
                    if (x == indexOfBuildingSlotSelected)
                    {
                        spriteBatch.DrawString(textFont, "Factory", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (textFont.MeasureString("Factory").X / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150), Color.White);
                        spriteBatch.Draw(factoryBuildingTexture, new Rectangle(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (100 / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150 + (int)textFont.MeasureString("Factory").Y, 100, 100), Color.White);
                        spriteBatch.DrawString(descriptionFont, "Boosts the production", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (descriptionFont.MeasureString("Boosts the production").X / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150 + (int)textFont.MeasureString("Factory").Y + 100 + 10), Color.White);
                        spriteBatch.DrawString(descriptionFont, "of other buildings.", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (descriptionFont.MeasureString("of other buildings.").X / 2), planetSelected.managementMenuObject.menuRectangle.Y + 150 + (int)textFont.MeasureString("Factory").Y + 100 + 10 + descriptionFont.MeasureString("Boosts the production").Y), Color.White);
                    }
                }
            }

            //This is where the building queue will be drawn.
            spriteBatch.DrawString(descriptionFont, "Building Queue", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150 - (descriptionFont.MeasureString("Building Queue").X / 2), underButtonLine.p1.Y + underButtonLine.thickness), Color.White);
            underBuildingQueueTitleLine.Draw(spriteBatch);
            spriteBatch.DrawString(descriptionFont, "Type", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 300, underBuildingQueueTitleLine.p1.Y + underBuildingQueueTitleLine.thickness), Color.White);
            spriteBatch.DrawString(descriptionFont, "Slot", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - 150, underBuildingQueueTitleLine.p1.Y + underBuildingQueueTitleLine.thickness), Color.White);
            spriteBatch.DrawString(descriptionFont, "Turns", new Vector2(planetSelected.managementMenuObject.menuRectangle.Right - descriptionFont.MeasureString("Turns").X, underBuildingQueueTitleLine.p1.Y + underBuildingQueueTitleLine.thickness), Color.White);
            underBuildingQueueTabsLine.Draw(spriteBatch);
            typeFromSlotLine.Draw(spriteBatch);
            slotFromTurnsLine.Draw(spriteBatch);
            for(int x = 0; x < planetSelected.buildingQueue.queuedBuildings.Count; x++)
            {
                if(x < 10)
                {
                    collectiveBuildCost = 0;
                    numberOfFactories = 0;
                    for(int y = 0; y < x + 1; y++)
                    {
                        collectiveBuildCost += planetSelected.buildingQueue.queuedBuildings[x].buildCost;
                    }
                    for(int y = 0; y < planetSelected.buildingSlotsList.Count; y++)
                    {
                        if (planetSelected.buildingSlotsList[y].typeOfBuilding == BuildingSlot.BuildingType.Factory)
                            numberOfFactories++;
                    }
                    turnsToBuild = (int)((collectiveBuildCost - planetSelected.buildingQueue.productionTowardsNextBuilding) / (1000 + (500.0f * numberOfFactories)) + 0.99);

                    if (planetSelected.buildingQueue.queuedBuildings[x].typeOfBuilding == BuildingQueued.BuildingType.ResearchFacility)
                    {
                        spriteBatch.DrawString(descriptionFont, "Research F.", new Vector2(rightSideLine.p1.X + rightSideLine.thickness, underBuildingQueueTabsLine.p1.Y + underBuildingQueueTabsLine.thickness + (x * 18)), Color.White);
                    }
                    if (planetSelected.buildingQueue.queuedBuildings[x].typeOfBuilding == BuildingQueued.BuildingType.MilitaryBase)
                    {
                        spriteBatch.DrawString(descriptionFont, "Military B.", new Vector2(rightSideLine.p1.X + rightSideLine.thickness, underBuildingQueueTabsLine.p1.Y + underBuildingQueueTabsLine.thickness + (x * 18)), Color.White);
                    }
                    if (planetSelected.buildingQueue.queuedBuildings[x].typeOfBuilding == BuildingQueued.BuildingType.Factory)
                    {
                        spriteBatch.DrawString(descriptionFont, "Factory", new Vector2(rightSideLine.p1.X + rightSideLine.thickness, underBuildingQueueTabsLine.p1.Y + underBuildingQueueTabsLine.thickness + (x * 18)), Color.White);
                    }
                    spriteBatch.DrawString(descriptionFont, "" + (planetSelected.buildingQueue.queuedBuildings[x].buildingSlotIndex + 1), new Vector2(typeFromSlotLine.p1.X + typeFromSlotLine.thickness + 5, underBuildingQueueTabsLine.p1.Y + underBuildingQueueTabsLine.thickness + (x * 18)), Color.White);
                    spriteBatch.DrawString(descriptionFont, "" + turnsToBuild, new Vector2(slotFromTurnsLine.p1.X + slotFromTurnsLine.thickness + 5, underBuildingQueueTabsLine.p1.Y + underBuildingQueueTabsLine.thickness + (x * 18)), Color.White);
                }
            }
        }
    }
}
