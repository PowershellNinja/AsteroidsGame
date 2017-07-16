using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using System.Collections;

namespace AsteroidsGame.GameAssets
{
    public class HealthBar
    {

        public SpriteBatch spriteBatch;
        public static Texture2D healthBarTexture;
        public int visibleHealth;
        public int actualHealth;
        public int maximumHealth;
        public Vector2 healthBarPosition;
        public int lastDamageSustained;
        public float healthBarScale;
        public bool healthBarVisible;

        public enum HealthAction {
            Increase,
            Decrease
        }



        //Use this:
        //http://gamedev.stackexchange.com/questions/63034/how-do-i-make-a-health-bar-that-drains-when-the-player-takes-damage-c-xna
        //And this: http://www.xnadevelopment.com/tutorials/notsohealthy/NotSoHealthy.shtml

        public HealthBar(SpriteBatch spriteBatch, Vector2 initialPosition,int initialHealth, float healthBarScale)
        {
            this.spriteBatch = spriteBatch;
            this.healthBarPosition = initialPosition;
            this.visibleHealth = this.actualHealth = this.maximumHealth = initialHealth;
            this.healthBarScale = healthBarScale;
            this.healthBarVisible = true;
        }


        public void DrawFrame()
        {

            if(this.healthBarVisible == true)
            {

                this.spriteBatch.Begin();


                /* White Background, just for testing and color contrast
                 * because on the dark background some stuff is hard to see :D
                 * 
                Rectangle background = new Rectangle(0, 20, 800, 800);
                Rectangle backgroundSource = new Rectangle(50, 50, 1, 1);        
                 this.spriteBatch.Draw(healthBarTexture, sourceRectangle: backgroundSource,
                    destinationRectangle: background, color: Color.SkyBlue);
                */


                //Build Source and Target Rectangle
                //Source Rectangle: The area of the sprite that should be drawn
                //Target Rectangle: The area the source rectangle should be drawn to (Scaled if not matching)
                //Color: Anything White for original colors


                int xPosition = (int)this.healthBarPosition.X;
                int yPosition = (int)this.healthBarPosition.Y;
                int hbWidth = healthBarTexture.Width;
                int scaledHBWidth = (int)(hbWidth * healthBarScale);
                int scaledHBHeight = (int)(44 * healthBarScale);

                //Gray background, so there is a visible "guideline" for the eye
                Rectangle sourceRectangleGrey = new Rectangle(0, 45, hbWidth, 44);
                Rectangle destinationRectangleGrey = new Rectangle(xPosition, yPosition, scaledHBWidth, scaledHBHeight);

                this.spriteBatch.Draw(healthBarTexture, sourceRectangle: sourceRectangleGrey,
                    destinationRectangle: destinationRectangleGrey, color: Color.Gray);

                //Healthbar width calculations
                int redCalculatedWidth = (int)((float)scaledHBWidth * (float)((float)this.actualHealth / this.maximumHealth));
                int greenCalculatedWidth = (int)((float)scaledHBWidth * (float)((float)this.visibleHealth / this.maximumHealth));

                //Green

                Rectangle sourceRectangleGreen = new Rectangle(0, 45, hbWidth, 44);
                Rectangle destinationRectangleGreen = new Rectangle(xPosition, yPosition, greenCalculatedWidth, scaledHBHeight);

                this.spriteBatch.Draw(healthBarTexture, sourceRectangle: sourceRectangleGreen,
                    destinationRectangle: destinationRectangleGreen, color: Color.Green);

                //Red            
                Rectangle sourceRectangleRed = new Rectangle(0, 45, hbWidth, 44);
                Rectangle destinationRectangleRed = new Rectangle(xPosition, yPosition, redCalculatedWidth, scaledHBHeight);

                this.spriteBatch.Draw(healthBarTexture, sourceRectangle: sourceRectangleRed,
                    destinationRectangle: destinationRectangleRed, color: Color.Red);


                //Numbers
                //spriteBatch.DrawString(arialFont, "Health:", new Vector2(0, 10), Color.White);
                //spriteBatch.DrawString(arialFont, this.actualHealth.ToString(), new Vector2(100, 10), Color.White);

                //spriteBatch.DrawString(arialFont, "RedCalculatedWidth:", new Vector2(150, 10), Color.White);
                //spriteBatch.DrawString(arialFont, redCalculatedWidth.ToString(), new Vector2(350, 10), Color.White);

                //spriteBatch.DrawString(arialFont, "TextureWidth:", new Vector2(500, 10), Color.White);
                //spriteBatch.DrawString(arialFont, healthBarTexture.Width.ToString(), new Vector2(700, 10), Color.White);





                this.spriteBatch.End();
            }


        }

        public void Update(int healthModifier,HealthAction healthAction)
        {
            //Only do this if there was an actual Health change
            if(healthModifier != 0)
            {
                if(healthAction == HealthAction.Increase)
                {
                    //Update actual Health
                    this.actualHealth += healthModifier;
                }
                else if(healthAction == HealthAction.Decrease)
                {
                    //Update actual Health
                    this.actualHealth -= healthModifier;
                    this.lastDamageSustained = healthModifier;
                }
            }

            
            //Check what should be done
            //If the visible health is more than the actual, decrease the visible
            //If the actual is equal or bigger the visible health, stop or increase the visible health
            if(this.actualHealth < this.visibleHealth)
            {
                float percentageCalculator = 0.01f;                

                int calculatedDecrease = (int)Math.Ceiling(this.lastDamageSustained * percentageCalculator);

                this.visibleHealth -= calculatedDecrease;
            }
            else if(this.actualHealth >= this.visibleHealth){
                this.visibleHealth = this.actualHealth;
            }
            
        }

    }
}
