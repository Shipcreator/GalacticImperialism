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
    class FlagCreation
    {
        enum Selected
        {
            Background,
            Symbol
        }

        GraphicsDevice GraphicsDevice;

        SpriteFont fontOfTitle;
        SpriteFont fontOfText;

        Color backgroundColor;
        Color symbolColor;

        Texture2D whiteTexture;
        Texture2D[] symbolTextures;
        Texture2D sliderBackgroundTexture;
        Texture2D sliderCursorTexture;
        Texture2D unselectedButtonTexture;
        Texture2D selectedButtonTexture;
        Texture2D flagBackgroundTexture;
        public Texture2D flagTexture;       //This is the final texture that you want to access.

        int symbolSelected;

        Rectangle flagRectangle;

        Selected selectedObject;

        bool selectedChangedOnFrame;

        Slider[,] sliders;
        Button[] symbolChangingButtons;

        Color[] CombinedColorData;
        Color[] TempColorData;
        Color[] backgroundColorArray;

        public FlagCreation(SpriteFont titleFont, SpriteFont textFont, Texture2D[] symbolTexturesArray, Texture2D textureOfSliderBackground, Texture2D textureOfSliderCursor, Texture2D unselectedTextureOfButton, Texture2D selectedTextureOfButton, GraphicsDevice GraphicsDevice)
        {
            fontOfTitle = titleFont;
            fontOfText = textFont;
            symbolTextures = symbolTexturesArray;
            sliderBackgroundTexture = textureOfSliderBackground;
            sliderCursorTexture = textureOfSliderCursor;
            unselectedButtonTexture = unselectedTextureOfButton;
            selectedButtonTexture = selectedTextureOfButton;
            this.GraphicsDevice = GraphicsDevice;
            Initialize();
        }

        public void Initialize()
        {
            symbolSelected = 0;
            flagRectangle = new Rectangle((GraphicsDevice.Viewport.Width / 2) - (500 / 2), (GraphicsDevice.Viewport.Height / 2) - (300 / 2), 500, 300);
            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });
            backgroundColor = Color.White;
            symbolColor = Color.Black;

            flagBackgroundTexture = new Texture2D(GraphicsDevice, 500, 300);
            backgroundColorArray = new Color[500 * 300];
            for(int x = 0; x < backgroundColorArray.Length; x++)
            {
                backgroundColorArray[x] = backgroundColor;
            }
            flagBackgroundTexture.SetData<Color>(backgroundColorArray);
            CombinedColorData = new Color[500 * 300];
            TempColorData = new Color[500 * 300];
            flagBackgroundTexture.GetData(CombinedColorData);
            symbolTextures[symbolSelected].GetData(TempColorData);
            for(int i = 0; i < CombinedColorData.Length; i++)
            {
                if (TempColorData[i].A != 0)
                    CombinedColorData[i] = symbolColor;
            }
            flagTexture = new Texture2D(GraphicsDevice, 500, 300);
            flagTexture.SetData<Color>(CombinedColorData);

            selectedObject = Selected.Background;
            selectedChangedOnFrame = false;
            sliders = new Slider[3, 2];
            sliders[0, 0] = new Slider(new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((255 + 15) / 2), 800, 255 + 15, 20), new Vector2(15, 30), sliderBackgroundTexture, sliderCursorTexture);
            sliders[1, 0] = new Slider(new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((255 + 15) / 2), 880, 255 + 15, 20), new Vector2(15, 30), sliderBackgroundTexture, sliderCursorTexture);
            sliders[2, 0] = new Slider(new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((255 + 15) / 2), 960, 255 + 15, 20), new Vector2(15, 30), sliderBackgroundTexture, sliderCursorTexture);
            sliders[0, 1] = new Slider(new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((255 + 15) / 2), 800, 255 + 15, 20), new Vector2(15, 30), sliderBackgroundTexture, sliderCursorTexture);
            sliders[1, 1] = new Slider(new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((255 + 15) / 2), 880, 255 + 15, 20), new Vector2(15, 30), sliderBackgroundTexture, sliderCursorTexture);
            sliders[2, 1] = new Slider(new Rectangle((GraphicsDevice.Viewport.Width / 2) - ((255 + 15) / 2), 960, 255 + 15, 20), new Vector2(15, 30), sliderBackgroundTexture, sliderCursorTexture);
            for(int x = 0; x < sliders.GetLength(0); x++)
            {
                sliders[x, 1].SetPercentage(0.0f);
            }
            symbolChangingButtons = new Button[2];
            symbolChangingButtons[0] = new Button(new Rectangle(125, (GraphicsDevice.Viewport.Height / 2) - ((693 / 4) / 2), (1894 / 4), (693 / 4)), unselectedButtonTexture, selectedButtonTexture, "Previous Symbol", fontOfText, Color.White, null, null);
            symbolChangingButtons[1] = new Button(new Rectangle(1325, (GraphicsDevice.Viewport.Height / 2) - ((693 / 4) / 2), (1894 / 4), (693 / 4)), unselectedButtonTexture, selectedButtonTexture, "Next Symbol", fontOfText, Color.White, null, null);
        }

        public void Update(KeyboardState kb, KeyboardState oldKb, MouseState mouse, MouseState oldMouse)
        {
            selectedChangedOnFrame = false;

            if(selectedObject == Selected.Background)
            {
                if((kb.IsKeyDown(Keys.Right) && !oldKb.IsKeyDown(Keys.Right)) ||(kb.IsKeyDown(Keys.Left) && !oldKb.IsKeyDown(Keys.Left)))
                {
                    if(selectedChangedOnFrame == false)
                    {
                        selectedObject = Selected.Symbol;
                        selectedChangedOnFrame = true;
                    }
                }
                for(int x = 0; x < sliders.GetLength(0); x++)
                {
                    sliders[x, 0].Update(mouse, oldMouse);
                    backgroundColor = new Color((int)(sliders[0, 0].percentage * 255), (int)(sliders[1, 0].percentage * 255), (int)(sliders[2, 0].percentage * 255));
                }
            }

            if(selectedObject == Selected.Symbol)
            {
                if ((kb.IsKeyDown(Keys.Right) && !oldKb.IsKeyDown(Keys.Right)) || (kb.IsKeyDown(Keys.Left) && !oldKb.IsKeyDown(Keys.Left)))
                {
                    if (selectedChangedOnFrame == false)
                    {
                        selectedObject = Selected.Background;
                        selectedChangedOnFrame = true;
                    }
                }
                for(int x = 0; x < symbolChangingButtons.Length; x++)
                {
                    symbolChangingButtons[x].Update(mouse, oldMouse);
                    if(x == 0)
                    {
                        if (symbolChangingButtons[x].isClicked)
                        {
                            if (symbolSelected > 0)
                                symbolSelected--;
                            else
                                symbolSelected = symbolTextures.Length - 1;
                        }
                    }
                    if(x == 1)
                    {
                        if (symbolChangingButtons[x].isClicked)
                        {
                            if (symbolSelected < symbolTextures.Length - 1)
                                symbolSelected++;
                            else
                                symbolSelected = 0;
                        }
                    }
                }
                for (int x = 0; x < sliders.GetLength(0); x++)
                {
                    sliders[x, 1].Update(mouse, oldMouse);
                    symbolColor = new Color((int)(sliders[0, 1].percentage * 255), (int)(sliders[1, 1].percentage * 255), (int)(sliders[2, 1].percentage * 255));
                }
            }

            //Combines the flag background and symbol into a single texture!!!!!
            //This took me a long ass time to figure out!!!!!!!!
            flagBackgroundTexture = new Texture2D(GraphicsDevice, 500, 300);
            backgroundColorArray = new Color[500 * 300];
            for (int x = 0; x < backgroundColorArray.Length; x++)
            {
                backgroundColorArray[x] = backgroundColor;
            }
            flagBackgroundTexture.SetData<Color>(backgroundColorArray);
            CombinedColorData = new Color[500 * 300];
            TempColorData = new Color[500 * 300];
            flagBackgroundTexture.GetData(CombinedColorData);
            symbolTextures[symbolSelected].GetData(TempColorData);
            for (int i = 0; i < CombinedColorData.Length; i++)
            {
                if (TempColorData[i].A != 0)
                    CombinedColorData[i] = symbolColor;
            }
            flagTexture = new Texture2D(GraphicsDevice, 500, 300);
            flagTexture.SetData<Color>(CombinedColorData);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = fontOfTitle.MeasureString("Flag Creation");
            spriteBatch.DrawString(fontOfTitle, "Flag Creation", new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), (GraphicsDevice.Viewport.Height / 6) - (textSize.Y / 2)), Color.White);
            spriteBatch.Draw(whiteTexture, flagRectangle, backgroundColor);
            spriteBatch.Draw(symbolTextures[symbolSelected], flagRectangle, symbolColor);
            textSize = fontOfText.MeasureString("Use Left And Right Arrow Keys To Switch Modes");
            spriteBatch.DrawString(fontOfText, "Use Left And Right Arrow Keys To Switch Modes", new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), flagRectangle.Y - 90), Color.White);
            textSize = fontOfText.MeasureString("Press Escape To Return To Previous Menu");
            spriteBatch.DrawString(fontOfText, "Press Escape To Return To Previous Menu", new Vector2((GraphicsDevice.Viewport.Width / 2) - (textSize.X / 2), GraphicsDevice.Viewport.Height - textSize.Y), Color.White);
            if (selectedObject == Selected.Background)
            {
                textSize = fontOfText.MeasureString("Background");
                spriteBatch.DrawString(fontOfText, "Background", new Vector2(flagRectangle.Center.X - (textSize.X / 2), flagRectangle.Y - 125), Color.White);
                for (int x = 0; x < sliders.GetLength(0); x++)
                {
                    sliders[x, 0].Draw(spriteBatch);
                    if(x == 0)
                        spriteBatch.DrawString(fontOfText, "Red: " + (int)(sliders[x, 0].percentage * 255), new Vector2(1125, 793), Color.White);
                    if (x == 1)
                        spriteBatch.DrawString(fontOfText, "Green: " + (int)(sliders[x, 0].percentage * 255), new Vector2(1125, 793 + 80), Color.White);
                    if(x == 2)
                        spriteBatch.DrawString(fontOfText, "Blue: " + (int)(sliders[x, 0].percentage * 255), new Vector2(1125, 793 + 160), Color.White);
                }
            }
            if (selectedObject == Selected.Symbol)
            {
                for(int x = 0; x < symbolChangingButtons.Length; x++)
                {
                    symbolChangingButtons[x].Draw(spriteBatch);
                }
                textSize = fontOfText.MeasureString("Symbol");
                spriteBatch.DrawString(fontOfText, "Symbol", new Vector2(flagRectangle.Center.X - (textSize.X / 2), flagRectangle.Y - 125), Color.White);
                for (int x = 0; x < sliders.GetLength(0); x++)
                {
                    sliders[x, 1].Draw(spriteBatch);
                    if (x == 0)
                        spriteBatch.DrawString(fontOfText, "Red: " + (int)(sliders[x, 1].percentage * 255), new Vector2(1125, 793), Color.White);
                    if (x == 1)
                        spriteBatch.DrawString(fontOfText, "Green: " + (int)(sliders[x, 1].percentage * 255), new Vector2(1125, 793 + 80), Color.White);
                    if (x == 2)
                        spriteBatch.DrawString(fontOfText, "Blue: " + (int)(sliders[x, 1].percentage * 255), new Vector2(1125, 793 + 160), Color.White);
                }
            }
        }
    }
}
