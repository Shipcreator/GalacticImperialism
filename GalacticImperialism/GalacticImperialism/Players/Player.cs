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
    [Serializable] class Player
    {
        int gold, science, hydrogen, oxygen, nitrogen, iron, tungsten, uranium; //All Item Values That every Player stores
        public List<Planet> ownedPlanets = new List<Planet>(); //Owned Planets
        public List<Ship> ships = new List<Ship>();
        public List<Army> armies = new List<Army>();

        protected Board board; // Holds Current Board

        //Creates Base Player
        public Player(int startingGold, Board b)
        {
            gold = startingGold;
            science = hydrogen = oxygen = nitrogen = iron = tungsten = uranium = 0;
            board = b;
        }

        //Adds Ownership of a Planet
        public void AddPlanet(Planet p)
        {
            ownedPlanets.Add(p);
        }
        //Removes Ownership of a Planet
        public void RemovePlanet(Planet p)
        {
            ownedPlanets.Remove(p);
        }
        public void AddShip(Ship s)
        {
            ships.Add(s);
        }
        public void RemoveShip(Ship s)
        {
            ships.Remove(s);
        }
        public void AddArmy(Army a)
        {
            armies.Add(a);
        }
        public void RemoveArmy(Army a)
        {
            armies.Remove(a);
        }

        public void Update(GameTime gt) //General Update
        {

        }

        public void OnTurn() //General On Turn Start 
        {

        }

        public void EndTurn() //Called On Turn End
        {
            board.NextTurn();
        }

        //Getters And Setters Below
        public int getGold()
        {
            return gold;
        }
        public int getScience()
        {
            return science;
        }
        public int[] getResources()
        {
            return new int[] {hydrogen, oxygen, nitrogen, iron, tungsten, uranium}; // Order of Resources Get and Set
        }

        public void setGold(int g)
        {
            gold = g;
        }
        public void setScience(int s)
        {
            science = s;
        }
        public void addResources(int[] r)
        {
            r[0] += hydrogen; r[1] += oxygen; r[2] += nitrogen; r[3] += iron; r[4] += tungsten; r[5] += uranium; //Sets All Resource Back
        }
        public void subResources(int[] r)
        {
            r[0] -= hydrogen; r[1] -= oxygen; r[2] -= nitrogen; r[3] -= iron; r[4] -= tungsten; r[5] -= uranium; //Sets All Resource Back
        }
    }
}
