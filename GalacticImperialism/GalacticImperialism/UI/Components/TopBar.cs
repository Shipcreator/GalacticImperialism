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

namespace GalacticImperialism.UI.Components
{
    class TopBar
    {

        public int ironAmount;
        public int uraniumAmount;
        public int tungstenAmount;
        public int hydrogenAmount;
        public int nitrogenAmount;
        public int oxygenAmount;
        public int goldAmount;
        public int scienceAmount;
        private List<Player> playerList;

        Rectangle barRect;
        Rectangle flagRect;
        Rectangle ironResourceRect;
        Rectangle uraniumResourceRect;
        Rectangle tungstenResourceRect;
        Rectangle hydrogenResourceRect;
        Rectangle nitrogenResourceRect;
        Rectangle oxygenResourceRect;
        Rectangle goldResourceRect;
        Rectangle scienceResourceRect;

        PlayerUI ui;

        public TopBar(PlayerUI ui, Rectangle bar, GraphicsDevice g)
        {
            this.ui = ui;
            barRect = bar;
            Initialize(g);
        }

        public void Initialize(GraphicsDevice GraphicsDevice)
        {
            ironAmount = 0;
            uraniumAmount = 0;
            tungstenAmount = 0;
            hydrogenAmount = 0;
            nitrogenAmount = 0;
            oxygenAmount = 0;

            flagRect = new Rectangle((int)((barRect.Width / 1920.0f) * 15), (int)((barRect.Height / 135.0f) * 15), (int)((((int)(barRect.Height - ((barRect.Height / 135.0f) * 30))) / 3.0f) * 5.0f), (int)(barRect.Height - ((barRect.Height / 135.0f) * 30)));
            ironResourceRect = new Rectangle(110, barRect.Center.Y - (40 / 2), 40, 40);
            uraniumResourceRect = new Rectangle(258, barRect.Center.Y - (40 / 2), 40, 40);
            tungstenResourceRect = new Rectangle(441, barRect.Center.Y - (40 / 2), 40, 40);
            hydrogenResourceRect = new Rectangle(639, barRect.Center.Y - (40 / 2), 40, 40);
            nitrogenResourceRect = new Rectangle(842, barRect.Center.Y - (40 / 2), 40, 40);
            oxygenResourceRect = new Rectangle(1025, barRect.Center.Y - (40 / 2), 40, 40);
            goldResourceRect = new Rectangle(1208, barRect.Center.Y - (40 / 2), 40, 40);
            scienceResourceRect = new Rectangle(1396, barRect.Center.Y - (40 / 2), 40, 40);
        }

        public void Update(List<Player> playerList)
        {
            this.playerList = playerList;

            ironAmount = playerList[Game1.playerID].getResources()[3];
            uraniumAmount = playerList[Game1.playerID].getResources()[5];
            tungstenAmount = playerList[Game1.playerID].getResources()[4];
            hydrogenAmount = playerList[Game1.playerID].getResources()[0];
            nitrogenAmount = playerList[Game1.playerID].getResources()[2];
            oxygenAmount = playerList[Game1.playerID].getResources()[1];
            goldAmount = playerList[Game1.playerID].getGold();
            scienceAmount = playerList[Game1.playerID].getScience();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ui.barTexture, barRect, Color.White);
            spriteBatch.Draw(ui.flagTexture, flagRect, Color.White);

            spriteBatch.Draw(ui.ironResourceTexture, ironResourceRect, Color.White);
            Vector2 textSize = ui.Arial15.MeasureString("Iron: " + ironAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[3]);
            spriteBatch.DrawString(ui.Arial15, "Iron: " + ironAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[3], new Vector2(ironResourceRect.Right + 5, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            spriteBatch.Draw(ui.uraniumResourceTexture, uraniumResourceRect, Color.White);
            textSize = ui.Arial15.MeasureString("Uranium: " + uraniumAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[5]);
            spriteBatch.DrawString(ui.Arial15, "Uranium: " + uraniumAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[5], new Vector2(uraniumResourceRect.Right + 5, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            spriteBatch.Draw(ui.tungstenResourceTexture, tungstenResourceRect, Color.White);
            textSize = ui.Arial15.MeasureString("Tungsten: " + tungstenAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[4]);
            spriteBatch.DrawString(ui.Arial15, "Tungsten: " + tungstenAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[4], new Vector2(tungstenResourceRect.Right + 5, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            spriteBatch.Draw(ui.hydrogenResourceTexture, hydrogenResourceRect, Color.White);
            textSize = ui.Arial15.MeasureString("Hydrogen: " + hydrogenAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[0]);
            spriteBatch.DrawString(ui.Arial15, "Hydrogen: " + hydrogenAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[0], new Vector2(hydrogenResourceRect.Right + 5, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            spriteBatch.Draw(ui.nitrogenResourceTexture, nitrogenResourceRect, Color.White);
            textSize = ui.Arial15.MeasureString("Nitrogen: " + nitrogenAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[2]);
            spriteBatch.DrawString(ui.Arial15, "Nitrogen: " + nitrogenAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[2], new Vector2(nitrogenResourceRect.Right + 5, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            spriteBatch.Draw(ui.oxygenResourceTexture, oxygenResourceRect, Color.White);
            textSize = ui.Arial15.MeasureString("Oxygen: " + oxygenAmount);
            spriteBatch.DrawString(ui.Arial15, "Oxygen: " + oxygenAmount + " +" + playerList[Game1.playerID].resourcesPerTurn[1], new Vector2(oxygenResourceRect.Right + 5, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            spriteBatch.Draw(ui.goldResourceTexture, goldResourceRect, Color.White);
            textSize = ui.Arial15.MeasureString("Gold: " + goldAmount + " +" + playerList[Game1.playerID].goldPerTurn);
            spriteBatch.DrawString(ui.Arial15, "Gold: " + goldAmount + " +" + playerList[Game1.playerID].goldPerTurn, new Vector2(goldResourceRect.Right + 5, barRect.Center.Y - (textSize.Y / 2)), Color.White);
            spriteBatch.Draw(ui.scienceResourceTexture, scienceResourceRect, Color.White);
            textSize = ui.Arial15.MeasureString("Science: " + scienceAmount + " +" + playerList[Game1.playerID].sciencePerTurn);
            spriteBatch.DrawString(ui.Arial15, "Science: " + scienceAmount + " +" + playerList[Game1.playerID].sciencePerTurn, new Vector2(scienceResourceRect.Right + 5, barRect.Center.Y - (textSize.Y / 2)), Color.White);
        }
    }
}
