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
    class StartMenuScreen
    {

        public SpriteBatch spriteBatch;
        public Vector2 backgroundScale = new Vector2(0.64f, 0.6f);
        public Vector2 backgroundPosition;
        public Texture2D backgroundSprite;
        public StartButton startButton;
        public UpgradeButton upgradeButton;
        public ExitButton exitButton;

        public StartMenuScreen(){
        
        }

        public void LoadStartMenu(GraphicsDevice graphicsDevice, ContentManager content, int screenWidth, Texture2D startMenuBackgroundSprite, Texture2D startButtonSprite, Texture2D upgradeButtonSprite, Texture2D exitButtonSprite)
        {

            //Load Background
            backgroundSprite = startMenuBackgroundSprite;
            backgroundPosition = new Vector2(0, 0);

            //Load Buttons

            startButton = new StartButton(startButtonSprite, screenWidth, graphicsDevice);            
            upgradeButton = new UpgradeButton(upgradeButtonSprite, screenWidth, graphicsDevice);
            exitButton = new ExitButton(exitButtonSprite, screenWidth, graphicsDevice);
            
        }

        public void DrawFrame()
        {

            this.spriteBatch.Begin();
            spriteBatch.Draw(backgroundSprite, backgroundPosition, scale: backgroundScale);
            this.spriteBatch.End();

            startButton.DrawButton();
            upgradeButton.DrawButton();
            exitButton.DrawButton();

        }

        public void UpdateStartMenu(MouseState mouseState)
        {
            startButton.UpdateButton(mouseState);
            upgradeButton.UpdateButton(mouseState);
            exitButton.UpdateButton(mouseState);

        }

    }
}

