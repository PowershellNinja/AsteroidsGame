using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AsteroidsGame.GameAssets
{
    class MovingBackgroundAnimation
    {


        public SpriteBatch spriteBatch;
        public Vector2 scale = new Vector2(1.0f,1.0f);
        public Vector2 background1Position;
        public Vector2 background2Position;
        public Texture2D backgroundSprite1;
        public Texture2D backgroundSprite2;
        public Vector2 backgroundDirection = new Vector2(0, 1);
        public Vector2 backgroundSpeed = new Vector2(0, 100);


        public MovingBackgroundAnimation()
        {


        }


        public void LoadBackGround(ContentManager content)
        {

            //Load Background
            backgroundSprite1 = content.Load<Texture2D>("GalaxyBackgroundLarge_1920x1080.png");
            backgroundSprite2 = content.Load<Texture2D>("GalaxyBackgroundLarge_1920x1080.png");
            background1Position = new Vector2(0, 0);
            background2Position = new Vector2(0, 0 - backgroundSprite1.Height);

        }

        public void DrawFrame(float elapsedGameTime, int screenHeight)
        {
            

            //Calculate new Background Positions
            if(background1Position.Y > screenHeight)
            {
                background1Position.Y = background2Position.Y - backgroundSprite1.Height;
                
            }

            if(background2Position.Y > screenHeight)
            {
                background2Position.Y = background1Position.Y - backgroundSprite1.Height;
            }




            //Move the Backgrounds
            background1Position += backgroundDirection * backgroundSpeed * elapsedGameTime;
            background2Position += backgroundDirection * backgroundSpeed * elapsedGameTime;


            this.spriteBatch.Begin();

            spriteBatch.Draw(backgroundSprite1, background1Position, scale: scale);
            spriteBatch.Draw(backgroundSprite2, background2Position, scale: scale);


            this.spriteBatch.End();

        }
    }
}
