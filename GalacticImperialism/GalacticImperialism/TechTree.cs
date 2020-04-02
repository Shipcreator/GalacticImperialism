using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GalacticImperialism
{
    class TechTree
    {
        List<Tech> Economic;//all economic tech
        List<Tech> AvaEconomic;//usable of the economic tech
        List<Tech> Construction;//all construction tech
        List<Tech> AvaConstruction;//usable of the construction tech
        List<Tech> Military;//all Military tech
        List<Tech> AvaMilitary;//usable of the military tech
        public TechTree()
        {
            ReadTech(@"Content/ConstructionTech");
            ReadTech(@"Content/EconomicTech");
            ReadTech(@"Content/MilitaryTech");
        }

        private void ReadTech(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        String name = reader.ReadLine();//What prints when the tech is clicked to tell the player what the tech does
                        String Txt = reader.ReadLine();//What prints when the tech is clicked to tell the player what the tech does
                        double modifier = Convert.ToDouble(reader.ReadLine());//Modifier for the tech
                        String line = reader.ReadLine();
                        String[] T = line.Split(' ');//What the User modifies
                        int ID = Convert.ToInt32(reader.ReadLine());//ID of Tech
                        int SourceID = Convert.ToInt32(reader.ReadLine());//ID of the tech that must come before this Tech
                        if (path.Equals(@"Content/MilitaryTech"))
                        {
                            Military.Add(new Tech(name,Txt,modifier,T,ID,SourceID));
                        }
                        if (path.Equals(@"Content/EconomicTech"))
                        {
                            Economic.Add(new Tech(name, Txt, modifier, T, ID, SourceID));
                        }
                        if (path.Equals(@"Content/ConstructionTech"))
                        {
                            Construction.Add(new Tech(name, Txt, modifier, T, ID, SourceID));
                        }
                        reader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void SelectableT()//is used to sort what is selectable or not
        {
            for (int x = 0; x < Economic.Count();x++)//checks all Economy techs if they are selectable
            {
                if (Economic.ElementAt(x).done() == false)
                {
                    searchID(Economic.ElementAt(x));
                }
                if (Economic.ElementAt(x).getSelectable() == true && AvaEconomic.Contains(Economic.ElementAt(x)) == false)
                {
                    AvaEconomic.Add(Economic.ElementAt(x));
                }
            }
            for (int x = 0; x < Construction.Count(); x++)//checks all Construction techs if they are selectable
            {
                if (Construction.ElementAt(x).done() == false)
                {
                    searchID(Construction.ElementAt(x));
                }
                if (Construction.ElementAt(x).getSelectable() == true && AvaConstruction.Contains(Construction.ElementAt(x)) == false)
                {
                    AvaConstruction.Add(Construction.ElementAt(x));
                }
            }
            for (int x = 0; x < Military.Count(); x++)//checks all Military techs if they are selectable
            {
                if (Military.ElementAt(x).done() == false) {
                    searchID(Military.ElementAt(x));
                }
                if (Military.ElementAt(x).getSelectable() == true && AvaMilitary.Contains(Military.ElementAt(x)) == false)
                {
                    AvaMilitary.Add(Military.ElementAt(x));
                }
            }
        }

        private void searchID(Tech one)//goes through the tech tree to see if the sourceID is complete
        {
            for (int x = 0; x < Economic.Count(); x++)
            {
                one.avaliable(Economic.ElementAt(x));
                if(one.getSelectable() == true)
                {
                    break;
                }
            }
            for (int x = 0; x < Construction.Count(); x++)
            {
                one.avaliable(Construction.ElementAt(x));
                if (one.getSelectable() == true)
                {
                    break;
                }
            }
            for (int x = 0; x < Military.Count(); x++)
            {
                one.avaliable(Military.ElementAt(x));
                if (one.getSelectable() == true)
                {
                    break;
                }
            }
        }


    }
}
