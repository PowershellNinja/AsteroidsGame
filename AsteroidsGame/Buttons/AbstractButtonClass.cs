using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;

namespace AsteroidsGame.Buttons
{
    public abstract class AbstractButtonClass
    {


            public Rectangle buttonBoundingbox;
            public Rectangle mouseBoundingbox;
            public Boolean toggle;
            public Boolean isActive;
            public Boolean IsActive
            {
                get { return isActive; }
                set { isActive = value; }
            }

            public Vector2 buttonPosition;
            public Texture2D buttonSprite;
            public Vector2 buttonScale;
            public SpriteBatch spriteBatch;

            public event EventHandler onClick;
            public Color color;        
            public Color defaultColor = new Color(140, 140, 140);
            public Color defaultHover = new Color(0, 133, 188);
            public Color defaultActive = Microsoft.Xna.Framework.Color.LightBlue;    
            public AbstractButtonClass()
            {

            }
        
            public virtual void UpdateButton(MouseState mouseState)
            {

                // if there are click listener check if the mouse is over the button
                if(this.onClick != null && this.onClick.GetInvocationList().Length > 0)
                {
                    this.mouseBoundingbox.X = mouseState.X;
                    this.mouseBoundingbox.Y = mouseState.Y;

                    if(this.buttonBoundingbox.Intersects(this.mouseBoundingbox))
                    {
                        this.color = defaultHover;   
                    
                        if(mouseState.LeftButton == ButtonState.Pressed)
                            this.OnClick(new EventArgs());
                    }
                    else
                    {
                        if(this.isActive)
                            this.color = defaultActive;
                        else
                            this.color = defaultColor;
                    }
                }
            }

            public virtual void OnClick(EventArgs e)
            {
                if(this.toggle && !this.isActive)
                    this.isActive = true;
                else if(this.toggle && this.isActive)
                    this.isActive = false;

            onClick?.Invoke(this, e);
        }

            public virtual void DrawButton() { }
        }
    }

