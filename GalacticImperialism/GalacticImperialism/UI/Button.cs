using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GalacticImperialism
{
    public class Button
    {
        public Rectangle buttonRect;       //This rectangle will be the rectangle that sets the dimensions of the button and will have its values passed in via the constructor.

        public bool isClicked;      //Boolean that will say if the button was clicked on each update.
        public bool isSelected;     //Boolean that will say if the mouse is hovering over the button on each update.
        public bool wasSelected;       //Boolean that will say if the mouse was hovering over the button on the previous update.

        Texture2D unselectedButtonTexture;      //This texture will be drawn on the button's rectangle if the isSelected boolean is false meaning that the mouse is not hovering over the button's rectangle.
        Texture2D selectedButtonTexture;        //This texture will be drawn on the button's rectangle if the isSelected boolean is true meaning that the mouse is hovering over the button's rectangle.

        public string buttonText;      //This string will drawn perfectly centered on the button's rectangle.

        SpriteFont buttonFont;      //This font will be the font that the text passed in via the constructor is drawn in.

        Color buttonTextColor;      //This color will be the color that the text passed in via the constructor is drawn in.

        SoundEffect buttonSelectedSoundEffect;     //This sound effect will play if the isSelected boolean is true and the wasSelected boolean is false.
        SoundEffect buttonClickedSoundEffect;      //This sound effect will play if the isClicked boolean is true.

        public Button(Rectangle rectangle, Texture2D unselectedTexture, Texture2D selectedTexture, string text, SpriteFont font, Color textColor, SoundEffect selectedSoundEffect, SoundEffect clickedSoundEffect)
        {
            buttonRect = rectangle;
            unselectedButtonTexture = unselectedTexture;
            selectedButtonTexture = selectedTexture;
            buttonText = text;
            buttonFont = font;
            buttonTextColor = textColor;
            buttonSelectedSoundEffect = selectedSoundEffect;
            buttonClickedSoundEffect = clickedSoundEffect;
            Initialize();
        }

        public void Initialize()
        {
            isClicked = false;
            isSelected = false;
            wasSelected = false;
        }

        public void Update(MouseState mouse, MouseState oldMouse)
        {
            wasSelected = isSelected;   //Sets the boolean wasSelected to the value that isSelected was at the end of the previous update.
            isSelected = false; //Resets the isSelected boolean.
            isClicked = false; //Resets the isClicked boolean.

            if (mouse.X >= buttonRect.Left && mouse.X <= buttonRect.Right && mouse.Y >= buttonRect.Top && mouse.Y <= buttonRect.Bottom) //Conditional statement that detects if the mouse is hovering over the button.
                isSelected = true;

            if (isSelected && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)    //Conditional statement that detects if the button was clicked on each frame.
                isClicked = true;
        }

        public void playSelectedSoundEffect(float volume)       //Allows the user to pass in a volume float that can range from 0-1.
        {
            if (buttonSelectedSoundEffect != null)      //Conditional statement that detects if a selected sound effect was passed in via the constructor.
                buttonSelectedSoundEffect.Play(volume, 0.0f, 0.0f);     //Plays the selected sound effect at the passed in volume.
        }

        public void playClickedSoundEffect(float volume)       //Allows the user to pass in a volume float that can range from 0-1.
        {
            if (buttonClickedSoundEffect != null)       //Conditional statement that detects if a clicked sound effect was passed in via the constructor.
                buttonClickedSoundEffect.Play(volume, 0.0f, 0.0f);      //Plays the clicked sound effect at the passed in volume.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isSelected)     //Conditional statement that detects if the button is being hovered over by the mouse.
                spriteBatch.Draw(selectedButtonTexture, buttonRect, Color.White);       //Draws the button to the screen with the selected button texture.
            else
                spriteBatch.Draw(unselectedButtonTexture, buttonRect, Color.White);     //Draws the button to the screen with the unseleced button texture.

            Vector2 textSize = buttonFont.MeasureString(buttonText); //Measures the size of the text on the button using the sprite font that was passed in via the constructor.
            spriteBatch.DrawString(buttonFont, buttonText, new Vector2(buttonRect.Center.X - (textSize.X / 2), buttonRect.Center.Y - (textSize.Y / 2)), Color.White);   //Draws the text passed in via the constructor perfectly centered on the button.
        }
    }
}
