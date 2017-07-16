using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidsGame.Buttons;
using AsteroidsGame.GameAssets;
using AsteroidsGame.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;

namespace AsteroidsGame.Buttons
{
    public class UpgradeButton : AbstractButtonClass
    {

        public new event onClickHandler onClick;
        public delegate void onClickHandler(EventArgs e);

        public UpgradeButton(Texture2D upgradeButtonSprite, int screenWidth, GraphicsDevice graphicsDevice)
        {
            buttonSprite = upgradeButtonSprite;
            buttonScale = new Vector2(0.8f, 0.8f);
            buttonPosition = new Vector2((screenWidth / 2) - (buttonSprite.Width * buttonScale.X / 2), 480);
            spriteBatch = new SpriteBatch(graphicsDevice);
            buttonBoundingbox.X = (int)buttonPosition.X;
            buttonBoundingbox.Y = (int)buttonPosition.Y;
            buttonBoundingbox.Height = (int)(buttonSprite.Height * buttonScale.Y);
            buttonBoundingbox.Width = (int)(buttonSprite.Width * buttonScale.X);

        }

        public override void DrawButton()
        {

            this.spriteBatch.Begin();
            spriteBatch.Draw(buttonSprite, buttonPosition, scale: buttonScale);
            this.spriteBatch.End();

        }

        public override void UpdateButton(MouseState mouseState)
        {
            // if there are click listener check if the mouse is over the button
            if(this.onClick != null && this.onClick.GetInvocationList().Length > 0)
            {
                this.mouseBoundingbox.X = mouseState.X;
                this.mouseBoundingbox.Y = mouseState.Y;

                if(this.buttonBoundingbox.Intersects(this.mouseBoundingbox))
                {                    

                    if(mouseState.LeftButton == ButtonState.Pressed)
                        this.OnClick(new EventArgs());
                }
            }
        }

        public override void OnClick(EventArgs e)
        {

            onClick?.Invoke(e);


        }


    }
}
