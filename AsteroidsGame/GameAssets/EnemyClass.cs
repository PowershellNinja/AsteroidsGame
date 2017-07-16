using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using TexturePackerMonoGameDefinitions;
using TexturePackerLoader;
using AsteroidsGame;
using AsteroidsGame.GameAssets.SpriteAnimations;

namespace AsteroidsGame.GameAssets
{
    public class EnemyClass
    {
        public Vector2 enemyPosition;
        public int enemyHealth;        
        public SpriteBatch spriteBatch;
        public Vector2 shipScale = new Vector2(0.3f, 0.3f);
        public float enemySpeed = 2;
        public float enemyBaseDamage;
        public float enemyDamageMultiplier;
        public int enemyResultingDamage;
        public bool enemyActive;
        public EnemyDeathAnimation enemyDeathAnimation;
        public SpriteRender enemySpriteRender;
        public SpriteSheet enemySpriteSheet;
        public bool enemyDestroyAnimationFinished = false;
        public static Texture2D enemyTexture;
        public HealthBar enemyHealthBar;  

        //EventHandler for EnemyDestroyed Animation
        //public event EnemyDestroyedHandler EnemyDestroyEvent;
        //public delegate void EnemyDestroyedHandler(EnemyClass enemy, EventArgs e, float elapsedSeconds);
        //public EventArgs e = null;


        public EnemyClass(ContentManager content, Random random, int screenWidth, SpriteBatch spriteBatch, SpriteSheetLoader spriteSheetLoader)
        {

            this.enemyHealth = 100;
            this.enemyBaseDamage = 25;
            this.enemyDamageMultiplier = 1f;
            this.enemyActive = true;
            this.spriteBatch = spriteBatch;
            this.enemySpriteRender = new SpriteRender(this.spriteBatch);
            this.enemySpriteSheet = TextureManager.enemySpriteSheet;

            this.enemyDeathAnimation = new EnemyDeathAnimation(this.spriteBatch, this.enemySpriteSheet, this.enemySpriteRender);


            Vector2 initialPosition = new Vector2(random.Next(100, (int)(screenWidth - (enemyTexture.Width * shipScale.X))), (0 - (enemyTexture.Height * shipScale.X)));
            this.enemyPosition = initialPosition;

            float hbScale = 0.2f;
            Vector2 initialEnemyHBPosition = (this.enemyPosition + (new Vector2((((enemyTexture.Width * shipScale.X) / 2) - (((HealthBar.healthBarTexture.Width * hbScale) / 2) + 5)),(enemyTexture.Height * shipScale.X) + 5)));            
            enemyHealthBar = new HealthBar(spriteBatch, initialEnemyHBPosition, this.enemyHealth, hbScale);            

        }

        public void DrawFrame()
        {


            this.spriteBatch.Begin();

            this.spriteBatch.Draw(enemyTexture, this.enemyPosition, scale: shipScale);

            this.spriteBatch.End();

            this.enemyHealthBar.DrawFrame();

        }

        public void UpdateEnemy(int screenHeight, float elapsedSeconds)
        {
            //Update Position only if Enemy is alive and not out of screen
            if((this.enemyPosition.Y < screenHeight) && (this.enemyHealth > 0))
            {
                this.enemyPosition.Y += enemySpeed;

                this.enemyResultingDamage = (int)(this.enemyBaseDamage * this.enemyDamageMultiplier);
            }

            this.enemyHealthBar.healthBarPosition = (this.enemyPosition + (new Vector2((((enemyTexture.Width * shipScale.X) / 2) - (((HealthBar.healthBarTexture.Width * this.enemyHealthBar.healthBarScale) / 2) + 5)), (enemyTexture.Height * shipScale.X) + 5)));

            this.enemyHealthBar.Update(0, HealthBar.HealthAction.Decrease);

            //Set Enemy to "dead" if position out of screen or health below or equal 0
            if((this.enemyPosition.Y > screenHeight) || (this.enemyHealth <= 0))
            {
                this.enemyActive = false;

            }            



        }


    }
}
