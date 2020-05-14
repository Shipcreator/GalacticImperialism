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
using System.IO;

namespace GalacticImperialism.Components
{
    public class TechHandler
    {

        public List<Tech> techs;

        public TechHandler()
        {
            techs = new List<Tech>();
            LoadTech();

            foreach (Tech t in techs)
            {
                Console.WriteLine(t.ToString());
            }
        }

        public void Update(int science)
        {
            foreach (Tech t in techs)
            {
                if (t.researching)
                {
                    t.Update(science);
                }
            }
        }

        private void LoadTech()
        {
            try
            {
                using (StreamReader reader = new StreamReader(@"Content/Techs/Tech.txt"))
                {
                    int index = 0;
                    string name = "";
                    string description = "";
                    int cost = 0;
                    TechEnum type = 0;
                    float amount = 0;

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        switch (index)
                        {
                            case 0:
                                name = line;
                                index++;
                                break;
                            case 1:
                                description = line;
                                index++;
                                break;
                            case 2:
                                cost = int.Parse(line);
                                index++;
                                break;
                            case 3:
                                type = (TechEnum) Enum.Parse(typeof (TechEnum), line);
                                index++;
                                break;
                            case 4:
                                amount = float.Parse(line);
                                techs.Add(new Tech(name, description, cost, type, amount));
                                index++;
                                break;
                            case 5:
                                index = 0;
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
