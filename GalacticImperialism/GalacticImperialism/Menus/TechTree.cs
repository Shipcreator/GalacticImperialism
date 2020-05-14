//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
//using System.IO;

//namespace GalacticImperialism
//{
//    public class TechTree
//    {
//        Player player; 

//        List<Tech> Economic;//all economic tech
//        List<Tech> AvaEconomic;//usable of the economic tech
//        Tech[] Selectable_Eco;//visable Economy Tech
//        Tech Selected_Eco;//Economic Tech selected to research

//        List<Tech> Construction;//all construction tech
//        List<Tech> AvaConstruction;//usable of the construction tech
//        Tech[] Selectable_Con;//visable Construction Tech
//        Tech Selected_Con;//Construction Tech selected to research

//        List<Tech> Military;//all Military tech
//        List<Tech> AvaMilitary;//usable of the military tech
//        Tech[] Selectable_Mil;//visable Military Tech
//        Tech Selected_Mil;//Military Tech selected to research

//        Rectangle Selectable_Research_Menu;
//        Rectangle Research_Menu;
//        Rectangle Menu_Back;
//        Rectangle[] Complete_Research_Bar;//Rectangle showing complete area of research
//        Rectangle[] Research_Bar;//Rectangle showing incomplete area of research
//        Rectangle[] Research_Options;//Boxes to show the research options
//        Rectangle[] Researching_Box;//Boxes to show the research currently being researched
//        Texture2D Menu_Texture;
//        Texture2D Menu_Backdrop;
//        Texture2D Tech_Texture;
//        Texture2D Research_Bar_Texture;//Texture for research bar

//        public bool Open;

//        SpriteFont spriteFont;

//        enum GameState
//        {
//            Eco,
//            Con,
//            Mil,
//            close,
//        }

//        GameState menuState;
//        public TechTree()
//        {
//        }
//        public TechTree(Texture2D m_Tex, Texture2D t_Tex, Texture2D MenuB_Tex,Texture2D ResearchB_Tex, SpriteFont sf)
//        {
            
//            Construction = new List<Tech>();
//            Economic = new List<Tech>();
//            Military = new List<Tech>();
//            AvaConstruction = new List<Tech>();
//            AvaEconomic = new List<Tech>();
//            AvaMilitary = new List<Tech>();
//            ReadTech("Content/Techs/ConstructionTech.txt");
//            ReadTech("Content/Techs/EconomicTech.txt");
//            ReadTech("Content/Techs/MilitaryTech.txt");
//            Research_Options = new Rectangle[] { new Rectangle(870, 115, 720, 105), new Rectangle(870, 235, 720, 105), new Rectangle(870, 355, 720, 105) };
//            Researching_Box = new Rectangle[] { new Rectangle(110, 115, 720, 105), new Rectangle(110, 235, 720, 105), new Rectangle(110, 355, 720, 105) };
//            Complete_Research_Bar = new Rectangle[] { new Rectangle(110, 210, 0, 10), new Rectangle(110, 330, 10, 10), new Rectangle(110, 450, 0, 10) };
//            Research_Bar = new Rectangle[] { new Rectangle(110, 210, 720, 10), new Rectangle(110, 330, 720, 10), new Rectangle(110, 450, 720, 10) };
//            Research_Menu = new Rectangle(100, 70, 750, 400);
//            Menu_Back = new Rectangle(105,95,730,370);
//            Selectable_Eco = new Tech[3];
//            Selectable_Con = new Tech[3];
//            Selectable_Mil = new Tech[3];
//            Menu_Texture = m_Tex;
//            Tech_Texture = t_Tex;
//            Research_Bar_Texture = ResearchB_Tex;
//            Menu_Backdrop = MenuB_Tex;
//            Open = false;
//            menuState = GameState.Eco;
//            Selectable_Research_Menu = new Rectangle(850,70,750,400);
//            spriteFont = sf;
//            Construction.Add(new Tech());
//            Economic.Add(new Tech());
//            Military.Add(new Tech());
//            SelectableT();
//            SelectReset();
//        }

//        public void SelectReset()
//        {
//            for (int i = 0; i < Selectable_Con.Length; i++)
//            {
//                if (Selectable_Con[i] == null ||Selectable_Con[i].done())
//                {
//                    SelectTech(Selectable_Con);
//                    break;
//                }
//            }
//            for (int i = 0; i < Selectable_Eco.Length; i++)
//            {
//                if (Selectable_Eco[i] == null || Selectable_Eco[i].done())
//                {
//                    SelectTech(Selectable_Eco);
//                    break;
//                }
//            }
//            for (int i = 0; i < Selectable_Mil.Length; i++)
//            {
//                if (Selectable_Mil[i] == null || Selectable_Mil[i].done())
//                {
//                    SelectTech(Selectable_Mil);
//                    break;
//                }
//            }
//        }

//        public void SelectTech(Tech[] TechT)
//        {
//            Random rand = new Random();
//            int rep = 0;
//            for (int c = 0; c < TechT.Length; c++)//goes through avaliable space of the submitted tech selection
//            {
//                do//makes sure that the selected tech does not repeat
//                {
//                    rep = 0;
//                    if (TechT == Selectable_Con)//replaces all
//                    {
//                        int rp = 0;//how many times a tech repeats in a TechT
//                        int i = rand.Next(AvaConstruction.Count);
//                        for (int j = 0; j < TechT.Length; j++)
//                        {
//                            if (rp >= 1)
//                            {
//                                rep++;
//                            }
//                            else if (TechT[j] == AvaConstruction.ElementAt(i))
//                                rp++;
//                        }
//                        if (rep == 0)
//                        {
//                            TechT[c] = AvaConstruction.ElementAt(i);
//                        }
//                    }
//                    if (TechT == Selectable_Mil)//replaces all
//                    {
//                        int rp = 0;//how many times a tech repeats in a TechT
//                        int i = rand.Next(AvaMilitary.Count);
//                        for (int j = 0; j < TechT.Length; j++)
//                        {
//                            if (rp >= 1)
//                            {
//                                rep++;
//                            }
//                            else if (TechT[j] == AvaMilitary.ElementAt(i))
//                                rp++;
//                        }
//                        if (rep == 0)
//                        {
//                            TechT[c] = AvaMilitary.ElementAt(i);
//                        }
//                    }
//                    if (TechT == Selectable_Eco)//replaces all
//                    {
//                        int rp = 0;//how many times a tech repeats in a TechT
//                        int i = rand.Next(AvaEconomic.Count);
//                        for (int j = 0; j < TechT.Length; j++)
//                        {
//                            if (rp >= 1)
//                            {
//                                rep++;
//                            }
//                            else if (TechT[j] == AvaEconomic.ElementAt(i))
//                                rp++;
//                        }
//                        if (rep == 0)
//                        {
//                            TechT[c] = AvaEconomic.ElementAt(i);
//                        }
//                    }
//                } while (rep != 0);
//            }
            
//        }
        
//        private void ReadTech(string path)
//        {
//            try
//            {
//                using (StreamReader reader = new StreamReader(path))
//                {
//                    while (!reader.EndOfStream)
//                    {
//                        String name = reader.ReadLine();//What prints when the tech is clicked to tell the player what the tech does
//                        String Txt = reader.ReadLine();//What prints when the tech is clicked to tell the player what the tech does
//                        double modifier = Convert.ToDouble(reader.ReadLine());//Modifier for the tech
//                        String line = reader.ReadLine();
//                        String[] T = line.Split(' ');//What the User modifies
//                        int ID = Convert.ToInt32(reader.ReadLine());//ID of Tech
//                        int SourceID = Convert.ToInt32(reader.ReadLine());//ID of the tech that must come before this Tech
//                        int PointReq = Convert.ToInt32(reader.ReadLine());//Points required before completion
//                        if (path.Equals(@"Content/Techs/MilitaryTech.txt"))
//                        {
//                            Military.Add(new Tech(name, Txt, modifier, T, ID, SourceID, PointReq));
//                        }
//                        if (path.Equals(@"Content/Techs/EconomicTech.txt"))
//                        {
//                            Economic.Add(new Tech(name, Txt, modifier, T, ID, SourceID, PointReq));
//                        }
//                        if (path.Equals(@"Content/Techs/ConstructionTech.txt"))
//                        {
//                            Construction.Add(new Tech(name, Txt, modifier, T, ID, SourceID, PointReq));
//                        }
//                        reader.ReadLine();
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("The file could not be read:");
//                Console.WriteLine(e.Message);
//            }
//        }

//        public void SelectableT()//is used to sort what is selectable or not
//        {
//            for (int x = 0; x < Economic.Count();x++)//checks all Economy techs if they are selectable
//            {
//                if (Economic.ElementAt(x).done() == false)
//                {
//                    searchID(Economic.ElementAt(x));
//                }
//                if (Economic.ElementAt(x).getSelectable() == true && AvaEconomic.Contains(Economic.ElementAt(x)) == false)
//                {
//                    AvaEconomic.Add(Economic.ElementAt(x));
//                }
//            }
//            for (int x = 0; x < Construction.Count(); x++)//checks all Construction techs if they are selectable
//            {
//                if (Construction.ElementAt(x).done() == false)
//                {
//                    searchID(Construction.ElementAt(x));
//                }
//                if (Construction.ElementAt(x).getSelectable() == true && AvaConstruction.Contains(Construction.ElementAt(x)) == false)
//                {
//                    AvaConstruction.Add(Construction.ElementAt(x));
//                }
//            }
//            for (int x = 0; x < Military.Count(); x++)//checks all Military techs if they are selectable
//            {
//                if (Military.ElementAt(x).done() == false) {
//                    searchID(Military.ElementAt(x));
//                }
//                if (Military.ElementAt(x).getSelectable() == true && AvaMilitary.Contains(Military.ElementAt(x)) == false)
//                {
//                    AvaMilitary.Add(Military.ElementAt(x));
//                }
//            }
//        }

//        private void searchID(Tech one)//goes through the tech tree to see if the sourceID is complete
//        {
//            for (int x = 0; x < Economic.Count(); x++)
//            {
//                one.avaliable(Economic.ElementAt(x));
//                if(one.getSelectable() == true)
//                {
//                    break;
//                }
//            }
//            for (int x = 0; x < Construction.Count(); x++)
//            {
//                one.avaliable(Construction.ElementAt(x));
//                if (one.getSelectable() == true)
//                {
//                    break;
//                }
//            }
//            for (int x = 0; x < Military.Count(); x++)
//            {
//                one.avaliable(Military.ElementAt(x));
//                if (one.getSelectable() == true)
//                {
//                    break;
//                }
//            }
//        }
        
//        public void Research(int conPoint, int ecoPoint, int milPoint)
//        {
//            Selected_Con.investPoints(conPoint);
//            Selected_Eco.investPoints(ecoPoint);
//            Selected_Mil.investPoints(milPoint);
//        }


//        public void Update(KeyboardState kb, KeyboardState oldKb,MouseState mouse, MouseState oldMouse)
//        {
//            SelectableT();
//            SelectReset();
//            /*if (kb.IsKeyDown(Keys.T) && kb!=oldKb && Open == false)
//            {
//                Open = true;
//            }
//            else if (kb.IsKeyDown(Keys.T) && kb != oldKb && Open == true)
//            {
//                Open = false;
//            }*/

//            if (mouse.LeftButton == ButtonState.Pressed
//                && oldMouse.LeftButton == ButtonState.Released 
//                    && mouse.X >= 110 && mouse.X <= 830
//                        && mouse.Y >= 115 && mouse.Y <= 220
//                            && Open == true)
//            {
//                menuState = GameState.Con;
//            }
//            if (mouse.LeftButton == ButtonState.Pressed
//                && oldMouse.LeftButton == ButtonState.Released
//                    && mouse.X >= 110 && mouse.X <= 830
//                        && mouse.Y >= 235 && mouse.Y <= 340
//                            && Open == true)
//            {
//                menuState = GameState.Eco;
//            }
//            if (mouse.LeftButton == ButtonState.Pressed
//                && oldMouse.LeftButton == ButtonState.Released
//                    && mouse.X >= 110 && mouse.X <= 830
//                        && mouse.Y >= 355 && mouse.Y <= 460
//                            && Open == true)
//            {
//                menuState = GameState.Mil;
//            }

//            if (menuState == GameState.Con) {//changes selected con
//                if (mouse.LeftButton == ButtonState.Pressed
//                    && oldMouse.LeftButton == ButtonState.Released
//                        && mouse.X >= 870 && mouse.X <= 1590
//                            && mouse.Y >= 115 && mouse.Y <= 220
//                                && Open == true)
//                {
//                    Selected_Con = Selectable_Con[0];
//                }
//                if (mouse.LeftButton == ButtonState.Pressed
//                    && oldMouse.LeftButton == ButtonState.Released
//                        && mouse.X >= 870 && mouse.X <= 1590
//                            && mouse.Y >= 235 && mouse.Y <= 340
//                                && Open == true)
//                {
//                    Selected_Con = Selectable_Con[1];
//                }
//                if (mouse.LeftButton == ButtonState.Pressed
//                    && oldMouse.LeftButton == ButtonState.Released
//                        && mouse.X >= 870 && mouse.X <= 1590
//                            && mouse.Y >= 355 && mouse.Y <= 460
//                                && Open == true)
//                {
//                    Selected_Con = Selectable_Con[2];
//                }
//            }

//            if (menuState == GameState.Eco)//changes selected eco
//            {
//                if (mouse.LeftButton == ButtonState.Pressed
//                    && oldMouse.LeftButton == ButtonState.Released
//                        && mouse.X >= 870 && mouse.X <= 1590
//                            && mouse.Y >= 115 && mouse.Y <= 220
//                                && Open == true)
//                {
//                    Selected_Eco = Selectable_Eco[0];
//                }
//                if (mouse.LeftButton == ButtonState.Pressed
//                    && oldMouse.LeftButton == ButtonState.Released
//                        && mouse.X >= 870 && mouse.X <= 1590
//                            && mouse.Y >= 235 && mouse.Y <= 340
//                                && Open == true)
//                {
//                    Selected_Eco = Selectable_Eco[1];
//                }
//                if (mouse.LeftButton == ButtonState.Pressed
//                    && oldMouse.LeftButton == ButtonState.Released
//                        && mouse.X >= 870 && mouse.X <= 1590
//                            && mouse.Y >= 355 && mouse.Y <= 460
//                                && Open == true)
//                {
//                    Selected_Eco = Selectable_Eco[2];
//                }
//            }

//            if (menuState == GameState.Mil)//changes Selected_mil
//            {
//                if (mouse.LeftButton == ButtonState.Pressed
//                    && oldMouse.LeftButton == ButtonState.Released
//                        && mouse.X >= 870 && mouse.X <= 1590
//                            && mouse.Y >= 115 && mouse.Y <= 220
//                                && Open == true)
//                {
//                    Selected_Mil = Selectable_Mil[0];
//                }
//                if (mouse.LeftButton == ButtonState.Pressed
//                    && oldMouse.LeftButton == ButtonState.Released
//                        && mouse.X >= 870 && mouse.X <= 1590
//                            && mouse.Y >= 235 && mouse.Y <= 340
//                                && Open == true)
//                {
//                    Selected_Mil = Selectable_Mil[1];
//                }
//                if (mouse.LeftButton == ButtonState.Pressed
//                    && oldMouse.LeftButton == ButtonState.Released
//                        && mouse.X >= 870 && mouse.X <= 1590
//                            && mouse.Y >= 355 && mouse.Y <= 460
//                                && Open == true)
//                {
//                    Selected_Mil = Selectable_Mil[2];
//                }
//            }
            

//        }


//        public void Draw(SpriteBatch spriteBatch)
//        {
//            if (Open == true)
//            {
//                spriteBatch.Draw(Menu_Texture, Research_Menu, Color.White);
//                spriteBatch.Draw(Menu_Backdrop, Menu_Back, Color.White);
//                spriteBatch.Draw(Menu_Texture, Selectable_Research_Menu, Color.White);
//                for (int i = 0; i < Research_Options.Length; i++)
//                {
//                    spriteBatch.Draw(Tech_Texture, Research_Options[i], Color.White);
//                    spriteBatch.Draw(Tech_Texture, Researching_Box[i], Color.White);
//                }
//                //---------------------------------------------------------------------------------------------------------------
//                if (Selected_Con != null)
//                {
//                    spriteBatch.DrawString(spriteFont, Selected_Con.toString(), new Vector2(110, 115), Color.Black);
//                }
//                else
//                    spriteBatch.DrawString(spriteFont, "Select Construction Technology", new Vector2(110, 115), Color.Black);
//                //---------------------------------------------------------------------------------------------------------------
//                if (Selected_Eco != null)
//                {
//                    spriteBatch.DrawString(spriteFont, Selected_Eco.toString(), new Vector2(110, 235), Color.Black);
//                }
//                else
//                    spriteBatch.DrawString(spriteFont, "Select Economy Technology", new Vector2(110, 235), Color.Black);
//                //---------------------------------------------------------------------------------------------------------------
//                if (Selected_Mil != null)
//                {
//                    spriteBatch.DrawString(spriteFont, Selected_Mil.toString(), new Vector2(110, 355), Color.Black);
//                }
//                else
//                    spriteBatch.DrawString(spriteFont, "Select Military Technology", new Vector2(110, 355), Color.Black);
//                //---------------------------------------------------------------------------------------------------------------
//                for (int j = 0; j < Research_Bar.Length; j++)
//                {
//                    spriteBatch.Draw(Research_Bar_Texture, Research_Bar[j], Color.Gray);
//                    spriteBatch.Draw(Research_Bar_Texture, Complete_Research_Bar[j], Color.White);
//                }

//                if (menuState == GameState.Eco)
//                {
//                    for (int j = 0; j < Selectable_Eco.Length; j++)
//                    {
//                        spriteBatch.DrawString(spriteFont, Selectable_Eco.ElementAt(j).toString(), new Vector2(880, 115 + j * 120), Color.Black);
//                    }
//                }
//                else if (menuState == GameState.Mil)
//                {
//                    for (int j = 0; j < Selectable_Mil.Length; j++)
//                    {
//                        spriteBatch.DrawString(spriteFont, Selectable_Mil.ElementAt(j).toString(), new Vector2(880, 115 + j * 120), Color.Black);
//                    }
//                }
//                else if (menuState == GameState.Con)
//                {
//                    for (int j = 0; j < Selectable_Con.Length; j++)
//                    {
//                        spriteBatch.DrawString(spriteFont, Selectable_Con.ElementAt(j).toString(), new Vector2(880, 115 + j * 120), Color.Black);
//                    }
//                }
//            }
//        }
//    }
//}
