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

        public Planet planetSelected;

        Line rightSideLine;

        int indexOfBuildingSlotSelected;

        Button demolishOrBuildButton;

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
            indexOfBuildingSlotSelected = 0;
            demolishOrBuildButton = new Button(new Rectangle(0, 0, 200, 100), unselectedButtonTexture, selectedButtonTexture, "Build", textFont, Color.White, null, null);
        }

        public void LoadContent()
        {
            tabFont = Content.Load<SpriteFont>("Sprite Fonts/Castellar60Point");
            textFont = Content.Load<SpriteFont>("Sprite Fonts/Castellar20Point");
            unselectedButtonTexture = Content.Load<Texture2D>("Button Textures/UnselectedButtonTexture1");
            selectedButtonTexture = Content.Load<Texture2D>("Button Textures/SelectedButtonTexture1");
            researchFacilityBuildingTexture = Content.Load<Texture2D>("Player UI/Planet Management Menu/Research Facility Icon");
            militaryBaseBuildingTexture = Content.Load<Texture2D>("Player UI/Planet Management Menu/Military Base Icon");
            factoryBuildingTexture = Content.Load<Texture2D>("Player UI/Planet Management Menu/Factory Icon");
        }

        public void Update(Planet selectedPlanet, MouseState mouse, MouseState oldMouse)
        {
            planetSelected = selectedPlanet;
            rightSideLine.p1.X = selectedPlanet.managementMenuObject.menuRectangle.X + 600;
            rightSideLine.p1.Y = selectedPlanet.managementMenuObject.menuRectangle.Y + 140;
            rightSideLine.p2.X = selectedPlanet.managementMenuObject.menuRectangle.X + 600;
            rightSideLine.p2.Y = selectedPlanet.managementMenuObject.menuRectangle.Bottom - 50;
            rightSideLine.Update();

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

            demolishOrBuildButton.buttonRect.X = planetSelected.managementMenuObject.menuRectangle.Right - 250;
            demolishOrBuildButton.buttonRect.Y = planetSelected.managementMenuObject.menuRectangle.Bottom - 175;
            demolishOrBuildButton.Update(mouse, oldMouse);
            if (planetSelected.buildingSlotsList[indexOfBuildingSlotSelected].typeOfBuilding == BuildingSlot.BuildingType.Empty)
            {
                demolishOrBuildButton.buttonText = "Build";
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

            for (int x = 0; x < planetSelected.buildingSlotsList.Count; x++)
            {
                if (x != indexOfBuildingSlotSelected)
                    spriteBatch.Draw(whiteTexture, planetSelected.buildingSlotsList[x].buildingSlotRectangle, Color.White);
                else
                    spriteBatch.Draw(whiteTexture, planetSelected.buildingSlotsList[x].buildingSlotRectangle, Color.Gold);
                spriteBatch.Draw(whiteTexture, new Rectangle(planetSelected.buildingSlotsList[x].buildingSlotRectangle.X + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Width - 10, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Height - 10), Color.Black);
                if (planetSelected.buildingSlotsList[x].typeOfBuilding == BuildingSlot.BuildingType.Empty)
                {
                    if(x != indexOfBuildingSlotSelected)
                        spriteBatch.DrawString(tabFont, "+", new Vector2(planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.X - (tabFont.MeasureString("+").X / 2), planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.Y - (tabFont.MeasureString("+").Y / 2)), Color.White);
                    else
                        spriteBatch.DrawString(tabFont, "+", new Vector2(planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.X - (tabFont.MeasureString("+").X / 2), planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.Y - (tabFont.MeasureString("+").Y / 2)), Color.Gold);
                }
                if(planetSelected.buildingSlotsList[x].typeOfBuilding == BuildingSlot.BuildingType.ResearchFacility)
                {
                    spriteBatch.Draw(researchFacilityBuildingTexture, new Rectangle(planetSelected.buildingSlotsList[x].buildingSlotRectangle.X + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Width - 10, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Height - 10), Color.White);
                }
                if (planetSelected.buildingSlotsList[x].typeOfBuilding == BuildingSlot.BuildingType.MilitaryBase)
                {
                    spriteBatch.Draw(militaryBaseBuildingTexture, new Rectangle(planetSelected.buildingSlotsList[x].buildingSlotRectangle.X + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Width - 10, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Height - 10), Color.White);
                }
                if(planetSelected.buildingSlotsList[x].typeOfBuilding == BuildingSlot.BuildingType.Factory)
                {
                    spriteBatch.Draw(factoryBuildingTexture, new Rectangle(planetSelected.buildingSlotsList[x].buildingSlotRectangle.X + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Width - 10, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Height - 10), Color.White);
                }
            }
        }
    }
}
