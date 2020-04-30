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
    public class UnitProduction
    {
        private List<object> productionQueue;
        private int production;

        private Rectangle Selectable_Production_Menu;
        private Rectangle Production_Menu;
        private Rectangle Menu_Back;
        private Rectangle Complete_Production_Bar;//Rectangle showing complete area of production
        private Rectangle Production_Bar;//Rectangle showing incomplete area of Production
        private Rectangle[] Build_Options;//Boxes to show the research options
        private Rectangle Production_Box;//Boxes to show the research currently being researched


        private Texture2D Menu_Texture;
        private Texture2D Menu_Backdrop;
        private Texture2D Production_Texture;
        private Texture2D Production_Bar_Texture;//Texture for production bar

        private int shipPage;//page on the shiplist
        private int armyPage;//page on the shiplist

        Boolean Open;

        SpriteFont spriteFont;

        enum GameState
        {
            Armies,
            Ships,
        }
        GameState menuState;
        static Army[] armyList = new Army[] { new Army(2, 2, 2, 800, "Army"), new Army(1, 1, 2, 400, "Clone-Army"), new Army(3, 4, 2, 1400, "Gene-Warriors"), new Army(2, 3, 3, 1000, "Motorized Army"), new Army(5, 4, 3, 4000, "Space Marine"), new Army(1, 3, 1, 1400, "Artillary")};
        static Ship[] shipList = new Ship[] { new Ship(1, 1, 5, 1200, "Corvette"), new Ship(2, 2, 3, 2000, "Destroyer"), new Ship(3, 4, 3, 3600, "Cruiser"), new Ship(5, 4, 2, 4000, "BattleShip"), new Ship(7, 7, 2, 10000, "Capital Ship"), new Ship(1, 1, 5, 500, "Swarm Ship"), new Ship(4, 1, 3, 1200, "Fleet Buster"), new Ship(1, 4, 1, 3, "Shield Ship") };
        public UnitProduction()
        {
            Open = false;
            menuState = GameState.Armies;
            productionQueue = new List<object>();
            production = 0;
        }

        public UnitProduction(Texture2D m_Tex, Texture2D t_Tex, Texture2D ResearchB_Tex, Texture2D MenuB_Tex)
        {
            Open = false;
            menuState = GameState.Armies;
            productionQueue = new List<object>();
            production = 0;
            Menu_Texture = m_Tex;
            Production_Texture = t_Tex;
            Production_Bar_Texture = ResearchB_Tex;
            Menu_Backdrop = MenuB_Tex;
        }

        public UnitProduction(Texture2D m_Tex, Texture2D t_Tex, Texture2D ResearchB_Tex, Texture2D MenuB_Tex, SpriteFont sf)
        {
            Open = false;
            menuState = GameState.Armies;
            productionQueue = new List<object>();
            production = 0;
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

        public void add(object obj)
        {
            productionQueue.Add(obj);
        }

        public void productionAdd()
        {
            production++;
            if (productionQueue.ElementAt(0) is Ship)//checks if first in queue is a ship or army
            {
                Ship temp = (Ship)productionQueue.ElementAt(0);
                if (temp.getConCost() <= production)
                {
                    //player.AddShip(new Ship(temp.getAttack(), temp.getDefence(), temp.getMoves(), temp.getConCost(), temp.getName()));
                    production -= temp.getConCost();
                    productionQueue.RemoveAt(0);
                }
            }

            if (productionQueue.ElementAt(0) is Army)//checks if first in queue is a ship or army
            {
                Army temp = (Army)productionQueue.ElementAt(0);
                if (temp.getConCost() <= production)
                {
                    //player.AddArmy(new Army(temp.getAttack(),temp.getDefence(),temp.getMoves(),temp.getConCost(),temp.getName()));
                    production -= temp.getConCost();
                    productionQueue.RemoveAt(0);
                    Complete_Production_Bar.Width = 0;
                }
            }
        }

        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse, MouseState oldMouse)
        {
            if (productionQueue.Count != 0) {
                if (productionQueue.ElementAt(0) is Ship)
                {
                    Ship temp = (Ship)productionQueue.ElementAt(0);
                    Complete_Production_Bar.Width = (int)(((double)production / temp.getConCost()) * 720);
                }
                if (productionQueue.ElementAt(0) is Army)
                {
                    Army temp = (Army)productionQueue.ElementAt(0);
                    Complete_Production_Bar.Width = (int)(((double)production / temp.getConCost()) * 720);
                }
            }
            if (productionQueue.Count!=0)
            {
                productionAdd();
            }
            if (menuState == GameState.Armies) {
                if (mouse.LeftButton == ButtonState.Pressed
                        && oldMouse.LeftButton == ButtonState.Released
                            && mouse.X >= 870 && mouse.X <= 1590
                                && mouse.Y >= 115 && mouse.Y <= 220
                                    && Open == true)
                {
                    add(armyList[0 + armyPage * 3]);
                }
                if (mouse.LeftButton == ButtonState.Pressed
                    && oldMouse.LeftButton == ButtonState.Released
                        && mouse.X >= 870 && mouse.X <= 1590
                            && mouse.Y >= 235 && mouse.Y <= 340
                                && Open == true)
                {
                    add(armyList[1 + armyPage * 3]);
                }
                if (mouse.LeftButton == ButtonState.Pressed
                    && oldMouse.LeftButton == ButtonState.Released
                        && mouse.X >= 870 && mouse.X <= 1590
                            && mouse.Y >= 355 && mouse.Y <= 460
                                && Open == true)
                {
                    add(armyList[2 + armyPage * 3]);
                }
            }

            if (menuState == GameState.Ships)
            {
                if (mouse.LeftButton == ButtonState.Pressed
                        && oldMouse.LeftButton == ButtonState.Released
                            && mouse.X >= 870 && mouse.X <= 1590
                                && mouse.Y >= 115 && mouse.Y <= 220
                                    && Open == true)
                {
                    add(shipList[0 + shipPage * 3]);
                }
                if (mouse.LeftButton == ButtonState.Pressed
                    && oldMouse.LeftButton == ButtonState.Released
                        && mouse.X >= 870 && mouse.X <= 1590
                            && mouse.Y >= 235 && mouse.Y <= 340
                                && Open == true)
                {
                    add(shipList[1 + shipPage * 3]);
                }
                if (mouse.LeftButton == ButtonState.Pressed
                    && oldMouse.LeftButton == ButtonState.Released
                        && mouse.X >= 870 && mouse.X <= 1590
                            && mouse.Y >= 355 && mouse.Y <= 460
                                && Open == true)
                {
                    add(shipList[2 + shipPage * 3]);
                }
            }
            if (mouse.RightButton == ButtonState.Pressed
                    && oldMouse.RightButton == ButtonState.Released
                        && mouse.X >= 110 && mouse.X <= 830
                            && mouse.Y >= 235 && mouse.Y <= 340
                                && Open == true)
            {
                productionQueue.RemoveAt(0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Open == true)
            {
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
                if (menuState == GameState.Armies)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        spriteBatch.DrawString(spriteFont, armyList[j].toString(), new Vector2(880, 115 + j * 120), Color.Black);
                    }
                }
                else if (menuState == GameState.Ships)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        spriteBatch.DrawString(spriteFont, shipList[j].toString(), new Vector2(880, 115 + j * 120), Color.Black);
                    }
                }
                if (productionQueue.Count != 0)
                {
                    if (productionQueue.ElementAt(0) is Ship)
                    {
                        Ship temp = (Ship)productionQueue.ElementAt(0);
                        spriteBatch.DrawString(spriteFont, "" + temp.toString(), new Vector2(110, 235), Color.Black);
                    }
                    else if (productionQueue.ElementAt(0) is Army)
                    {
                        Army temp = (Army)productionQueue.ElementAt(0);
                        spriteBatch.DrawString(spriteFont, "" + temp.toString(), new Vector2(110, 235), Color.Black);
                    }
                }
                else
                    spriteBatch.DrawString(spriteFont, "Select a Unit", new Vector2(110, 235), Color.Black);
            }

        }
    }
}
