using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using AsteroidsGame.Buttons;
using AsteroidsGame.GameAssets;


namespace AsteroidsGame.Screens
{
    class GameOverScreen
    {
     
        public UpgradeButton upgradeButton;
        public ExitButton exitButton;

        public GameOverScreen()
        {

        }

        public void LoadGameOverScreen(GraphicsDevice graphicsDevice, ContentManager content, int screenWidth, Texture2D upgradeButtonSprite, Texture2D exitButtonSprite)
        {

            upgradeButton = new UpgradeButton(upgradeButtonSprite, screenWidth, graphicsDevice);
            exitButton = new ExitButton(exitButtonSprite, screenWidth, graphicsDevice);

        }


        public void DrawFrame(SpriteBatch spriteBatch, MovingBackgroundAnimation backgroundAnimation,float elapsedGameTime, int screenHeight, int screenWidth, SpriteFont arialFont28)
        {
            backgroundAnimation.DrawFrame(elapsedGameTime, screenHeight);

            spriteBatch.Begin();
            spriteBatch.DrawString(arialFont28, "It seems you died...", new Vector2((screenWidth / 2) - 160, (screenHeight / 2) - 300), Color.Red);
            spriteBatch.End();

            upgradeButton.DrawButton();
            exitButton.DrawButton();

        }

        public void UpdateGameOverScreen(MouseState mouseState)
        {
            
            upgradeButton.UpdateButton(mouseState);
            exitButton.UpdateButton(mouseState);

        }

    }
}
