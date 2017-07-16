using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using System.Collections;


namespace AsteroidsGame.GameAssets
{
    class CollisionMethods
    {
        public static void ProcessPlayerEnemyCollision(PlayerClass playerObject, List<EnemyClass> enemyList)
        {
            // Use the Rectangle’s built-in intersect function to
            // determine if two objects are overlapping
            Rectangle rectangle1;
            Rectangle rectangle2;

            //Create the rectangle for the player
            rectangle1 = new Rectangle((int)playerObject.playerPosition.X, (int)playerObject.playerPosition.Y,
            (int)(playerObject.playerTexture.Width * playerObject.shipScale.X),
            (int)(playerObject.playerTexture.Height * playerObject.shipScale.Y));

            // Do the collision between the player and the enemies            
            foreach(EnemyClass enemy in enemyList)
            {

                rectangle2 = new Rectangle((int)enemy.enemyPosition.X, (int)enemy.enemyPosition.Y,
                (int)(EnemyClass.enemyTexture.Width * enemy.shipScale.X),
                (int)(EnemyClass.enemyTexture.Height * enemy.shipScale.Y));
                // Determine if the player collides with an enemy

                if(rectangle1.Intersects(rectangle2))
                {

                    if(enemy.enemyActive == true)
                    {

                        // Subtract the health from the player based on the enemy damage
                        playerObject.playerHealth -= enemy.enemyResultingDamage;

                        playerObject.playerHealthBar.Update(enemy.enemyResultingDamage, HealthBar.HealthAction.Decrease);

                        // Since the enemy collided with the player destroy it
                        enemy.enemyHealth = 0;
                        enemy.enemyActive = false;
                    }
                  
                }

            }
        }


        public static void ProcessEnemyLaserShotCollision(List<EnemyClass> enemyList, List<PlayerLaserShot> playerLaserShotList)
        {
            // Use the Rectangle’s built-in intersect function to
            // determine if two objects are overlapping            

            Rectangle enemyRectangle;
            Rectangle laserShotRectangle;

            // Do the collision between the player and the enemies            
            foreach(EnemyClass enemy in enemyList)
            {

                enemyRectangle = new Rectangle((int)enemy.enemyPosition.X, (int)enemy.enemyPosition.Y,
                (int)(EnemyClass.enemyTexture.Width * enemy.shipScale.X),
                (int)(EnemyClass.enemyTexture.Height * enemy.shipScale.Y));

                foreach(PlayerLaserShot laserShot in playerLaserShotList)
                {

                    laserShotRectangle = new Rectangle((int)laserShot.laserPosition.X, (int)laserShot.laserPosition.Y,
                    (int)(laserShot.laserTexture.Width * laserShot.laserScale.X),
                    (int)(laserShot.laserTexture.Height * laserShot.laserScale.Y));

                    // Determine if the enemy collides with a laserShot
                    if(enemyRectangle.Intersects(laserShotRectangle))
                    {
                        // Subtract the health from the player based on the enemy damage
                        enemy.enemyHealth -= laserShot.laserDamage;
                        enemy.enemyHealthBar.Update(laserShot.laserDamage, HealthBar.HealthAction.Decrease);

                        laserShot.laserActive = false;

                        if(enemy.enemyHealth <= 0)
                        {
                            enemy.enemyActive = false;
                            enemy.enemyHealthBar.healthBarVisible = false;
                        }
                    }
                    
                }
                
            }
        }

    }
}
