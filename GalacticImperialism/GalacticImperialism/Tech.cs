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
    public class Tech
    {
        String Name;//What is the tech
        String Txt;//What prints when the tech is clicked to tell the player what the tech does
        double modifier;//Modifier for the tech
        int ID;//ID of Tech
        int SourceID;//ID of the tech that must come before this Tech
        String[] Target;//target for modification
        Boolean complete;//if the Tech has be researched
        Boolean selectable;//if the Tech itself is pickable
        int invested;
        int pointReq;
        public Tech()
        {
            Txt = "";
            modifier = 0;
            ID = 0;
            SourceID = 0;
            complete = true;
            selectable = false;
        }

        public Tech(String n, String MSG, double m, String[] trg, int id, int Sid,int PRq)
        {
            Name = n;
            Txt = MSG;
            modifier = m;
            Target = trg;
            ID = id;
            SourceID = Sid;
            complete = false;
            selectable = false;
            pointReq = PRq;
            invested = 0;
        }

        public void investPoints(int p)
        {
            invested += p;
        }

        public void isComplete()
        {
            if (invested >= pointReq)
            {
                complete = true;
            }
        }

        public double getMod()
        {
            return modifier;
        }

        public int getID()
        {
            return ID;
        }

        public Boolean done()
        {
            return complete;
        }

        public Boolean getSelectable()
        {
            return selectable;
        }

        public void avaliable(object tch2)//check if the tech with ID that = SourceID is complete
        {
            Tech tec2 = (Tech)tch2;
            if (tec2.getID() == SourceID && tec2.done() == true)
            {
                selectable = true;
            }
            else
            {
                selectable = false;
            }
        }

        public void finish()
        {
            selectable = false;
            complete = true;
        }

        public String toString()
        {
            String n = Name + "                     " + invested + "/" + pointReq;
            for (int i = 0; i < Target.Count();i++) {
                n += "\n" + Target[i] +" increase "+ (modifier - 1) * 100 + "%";
                    }
            return n;
        }
    }
}