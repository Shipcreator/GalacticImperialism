using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace GalacticImperialism
{
    public class UnitProductionMenu
    {
        private Rectangle Selectable_Production_Menu;
        private Rectangle Production_Menu;
        private Rectangle Menu_Back;
        public Rectangle Complete_Production_Bar;//Rectangle showing complete area of production
        private Rectangle Production_Bar;//Rectangle showing incomplete area of Production
        private Rectangle[] Build_Options;//Boxes to show the research options
        private Rectangle Production_Box;//Boxes to show the research currently being researched


        private Texture2D Menu_Texture;
        private Texture2D Menu_Backdrop;
        private Texture2D Production_Texture;
        private Texture2D Production_Bar_Texture;//Texture for production bar

        SpriteFont spriteFont;

        public int shipPage;//page on the shiplist

        public UnitProductionMenu(Texture2D m_Tex, Texture2D t_Tex, Texture2D ResearchB_Tex, Texture2D MenuB_Tex, SpriteFont sf)
        {
            shipPage = 0;
            Menu_Texture = m_Tex;
            Production_Texture = t_Tex;
            Production_Bar_Texture = ResearchB_Tex;
            Menu_Backdrop = MenuB_Tex;
            spriteFont = sf;

            Build_Options = new Rectangle[] { new Rectangle(870, 115, 720, 105), new Rectangle(870, 235, 720, 105), new Rectangle(870, 355, 720, 105) };
            Production_Box = new Rectangle(110, 235, 720, 105);
            Complete_Production_Bar = new Rectangle(110, 330, 0, 10);
            Production_Bar = new Rectangle(110, 330, 720, 10);
            Production_Menu = new Rectangle(100, 70, 750, 400);
            Selectable_Production_Menu = new Rectangle(850, 70, 750, 400);
        }
        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse, MouseState oldMouse, UnitProduction un)
        {
            if (mouse.LeftButton == ButtonState.Pressed
                    && oldMouse.LeftButton == ButtonState.Released
                        && mouse.X >= 870 && mouse.X <= 1590
                            && mouse.Y >= 115 && mouse.Y <= 220
                                && UnitProduction.Open == true)
            {
                un.add(UnitProduction.shipList[0 + shipPage * 3]);
            }
            if (mouse.LeftButton == ButtonState.Pressed
                && oldMouse.LeftButton == ButtonState.Released
                    && mouse.X >= 870 && mouse.X <= 1590
                        && mouse.Y >= 235 && mouse.Y <= 340
                            && UnitProduction.Open == true)
            {
                un.add(UnitProduction.shipList[1 + shipPage * 3]);
            }
            if (mouse.LeftButton == ButtonState.Pressed
                && oldMouse.LeftButton == ButtonState.Released
                    && mouse.X >= 870 && mouse.X <= 1590
                        && mouse.Y >= 355 && mouse.Y <= 460
                            && UnitProduction.Open == true)
            {
                un.add(UnitProduction.shipList[2 + shipPage * 3]);
            }
            if (mouse.RightButton == ButtonState.Pressed
                    && oldMouse.RightButton == ButtonState.Released
                        && mouse.X >= 110 && mouse.X <= 830
                            && mouse.Y >= 235 && mouse.Y <= 340
                                && UnitProduction.Open == true)
            {
                un.productionQueue.RemoveAt(0);
            }
        }

        public void Draw(SpriteBatch spriteBatch, UnitProduction unitProduction)
        {
            UnitProduction.Open = true;
            spriteBatch.Draw(Menu_Texture, Production_Menu, Color.White);
            spriteBatch.Draw(Menu_Backdrop, Menu_Back, Color.White);
            spriteBatch.Draw(Menu_Texture, Selectable_Production_Menu, Color.White);
            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(Production_Texture, Build_Options[i], Color.White);
            }
            spriteBatch.Draw(Production_Texture, Production_Box, Color.White);
            //---------------------------------------------------------------------------------------------------------------
            //---------------------------------------------------------------------------------------------------------------
            //---------------------------------------------------------------------------------------------------------------
            //---------------------------------------------------------------------------------------------------------------
            spriteBatch.Draw(Production_Bar_Texture, Production_Bar, Color.Gray);
            spriteBatch.Draw(Production_Bar_Texture, Complete_Production_Bar, Color.White);
            for (int j = 0; j < 3; j++)
            {
                spriteBatch.DrawString(spriteFont, UnitProduction.shipList[j].toString(), new Vector2(880, 115 + j * 120), Color.Black);
            }
            if (unitProduction.productionQueue.Count != 0)
            {
                if (unitProduction.productionQueue.ElementAt(0) is Ship)
                {
                    Ship temp = (Ship)unitProduction.productionQueue.ElementAt(0);
                    spriteBatch.DrawString(spriteFont, "" + temp.toString(), new Vector2(110, 235), Color.Black);
                }
            }
            else
                spriteBatch.DrawString(spriteFont, "Select a Unit", new Vector2(110, 235), Color.Black);
        }
    }
}
