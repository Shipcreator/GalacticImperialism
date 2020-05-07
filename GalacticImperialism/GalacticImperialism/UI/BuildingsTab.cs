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
    class BuildingsTab
    {
        Texture2D whiteTexture;

        SpriteFont tabFont;

        public Planet planetSelected;

        Line rightSideLine;

        public BuildingsTab(Texture2D white, SpriteFont font)
        {
            whiteTexture = white;
            tabFont = font;
            Initialize();
        }

        public void Initialize()
        {
            rightSideLine = new Line(new Vector2(0, 0), new Vector2(0, 0), 1, Color.White, whiteTexture);
        }

        public void Update(Planet selectedPlanet)
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
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rightSideLine.Draw(spriteBatch);

            for (int x = 0; x < planetSelected.buildingSlotsList.Count; x++)
            {
                spriteBatch.Draw(whiteTexture, planetSelected.buildingSlotsList[x].buildingSlotRectangle, Color.White);
                spriteBatch.Draw(whiteTexture, new Rectangle(planetSelected.buildingSlotsList[x].buildingSlotRectangle.X + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Y + 5, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Width - 10, planetSelected.buildingSlotsList[x].buildingSlotRectangle.Height - 10), Color.Black);
                if (planetSelected.buildingSlotsList[x].typeOfBuilding == BuildingSlot.BuildingType.Empty)
                {
                    spriteBatch.DrawString(tabFont, "+", new Vector2(planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.X - (tabFont.MeasureString("+").X / 2), planetSelected.buildingSlotsList[x].buildingSlotRectangle.Center.Y - (tabFont.MeasureString("+").Y / 2)), Color.White);
                }
            }
        }
    }
}
