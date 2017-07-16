using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;

namespace AsteroidsGame.GameAssets
{
    class PlayerClass
    {


        public Vector2 playerPosition;
        public int playerHealth;
        public Texture2D playerTexture;
        public SpriteBatch spriteBatch;
        public Vector2 shipScale = new Vector2(0.07f, 0.07f);
        public float playerSpeed = 10;
        public int playerBaseDamage;
        public float playerDamageMultiplier;
        public int playerResultingDamage;
        public bool isShooting = false;
        public float playerLaserShootingSpeed = 0.25f;
        public int playerShotLevel = 1;
        public HealthBar playerHealthBar;
        public int healthbarXCorrection = 0;
        

        public PlayerClass()
        {

        }

        public void InitializeAndLoadPlayer(GraphicsDevice device, ContentManager content,Vector2 initalPosition, SpriteBatch hbSpriteBatch)
        {
            this.playerPosition = initalPosition;
            this.playerHealth = 100;
            this.playerBaseDamage = 25;
            this.playerDamageMultiplier = 1f;
            this.playerResultingDamage = (int)(this.playerBaseDamage * this.playerDamageMultiplier);
            this.playerTexture = PngLoader.Load(device, "Content/SpaceshipPlayer.png");
            this.healthbarXCorrection = 5;

            float hbScale = 0.4f;
            Vector2 initialPlayerHBPosition = (this.playerPosition + (new Vector2(((((playerTexture.Width * this.shipScale.X) / 2) - (((HealthBar.healthBarTexture.Width * hbScale) / 2) + 5)) + healthbarXCorrection), (playerTexture.Height * this.shipScale.X) + 7)));            
            playerHealthBar = new HealthBar(hbSpriteBatch, initialPlayerHBPosition, this.playerHealth, hbScale);

        }


        public void DrawFrame()
        {


            this.spriteBatch.Begin();

            this.spriteBatch.Draw(this.playerTexture, this.playerPosition,scale: shipScale);

            this.spriteBatch.End();

            this.playerHealthBar.DrawFrame();

        }

        public void UpdatePlayer(KeyboardState currentKeyboardState, int screenWidth, int screenHeight, Vector2 mousePosition,AsteroidsGame.GameInputType inputType)
        {

            if(inputType == AsteroidsGame.GameInputType.Touch)
            {

                //Touch Input
                while(TouchPanel.IsGestureAvailable)
                {
                    GestureSample gesture = TouchPanel.ReadGesture();
                    if(gesture.GestureType == GestureType.FreeDrag)
                    {
                        this.playerPosition += gesture.Delta;
                    }
                }
            }

            if(inputType == AsteroidsGame.GameInputType.Mouse)
            {
                //Mouse Input
                Vector2 posDelta = mousePosition - this.playerPosition;

                posDelta.Normalize();

                posDelta = posDelta * this.playerSpeed;

                this.playerPosition = this.playerPosition + posDelta;
            }


            if(inputType == AsteroidsGame.GameInputType.Keyboard)
            {
                //Keyboard Input
                if(currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    //if(this.playerPosition.Y > 0)
                    //{
                    this.playerPosition.Y -= playerSpeed;
                    //}
                }

                if(currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    //if(this.playerPosition.Y < screenHeight - (this.playerTexture.Height * this.shipScale.Y))
                    //{
                    this.playerPosition.Y += playerSpeed;
                    //}
                }

                if(currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    //if(this.playerPosition.X > 0)
                    //{
                    this.playerPosition.X -= playerSpeed;
                    //}
                }

                if(currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    //if(this.playerPosition.X < screenWidth - (this.playerTexture.Width * this.shipScale.X))
                    //{
                    this.playerPosition.X += playerSpeed;
                    //}
                }
            }

            //Clamp the Player to the Screen
            this.playerPosition.X = MathHelper.Clamp(this.playerPosition.X, 0, screenWidth - (this.playerTexture.Width * shipScale.X));

            this.playerPosition.Y = MathHelper.Clamp(this.playerPosition.Y, 0, screenHeight - (this.playerTexture.Height * shipScale.X));


            this.playerHealthBar.healthBarPosition = (this.playerPosition + (new Vector2(((((playerTexture.Width * shipScale.X) / 2) - (((HealthBar.healthBarTexture.Width * this.playerHealthBar.healthBarScale) / 2) + 5)) + healthbarXCorrection), (playerTexture.Height * shipScale.X) + 5)));

            this.playerHealthBar.Update(0, HealthBar.HealthAction.Decrease);


            //Calculate current resulting Damage
            this.playerResultingDamage = (int)(this.playerBaseDamage * this.playerDamageMultiplier);
            


    }



    }
}
