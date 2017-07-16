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
    public class PlayerLaserShot
    {

        public Vector2 laserPosition;
        public Texture2D laserTexture;
        public SpriteBatch spriteBatch;
        public Vector2 laserScale = new Vector2(0.7f, 0.7f);
        public float laserSpeed = 10;
        public Vector2 laserMoveVector;
        public int laserDamage;                
        public bool laserActive;


        public PlayerLaserShot(Vector2 initalPosition, Vector2 laserMoveVector, SpriteBatch spriteBatch, int laserDamage)
        {
            this.laserPosition = initalPosition;
            this.laserTexture = PlayerShotHandler.laserShotTexture;
            this.laserActive = true;
            this.laserDamage = laserDamage;
            this.spriteBatch = spriteBatch;
            this.laserMoveVector = laserMoveVector;
        }

        public void DrawFrame()
        {
            this.spriteBatch.Begin();

            this.spriteBatch.Draw(this.laserTexture, this.laserPosition, scale: this.laserScale);

            this.spriteBatch.End();
        }

        public void UpdateLaser(int screenHeight, int screenWidth)
        {
            this.laserPosition = this.laserPosition + (this.laserMoveVector * this.laserSpeed);

            //Set Laser to "inactive" if position out of screen
                    if((this.laserPosition.Y >= screenHeight) || (this.laserPosition.Y <= 0)
                || (this.laserPosition.X >= screenWidth) || (this.laserPosition.X <= 0)
                )
            {
                this.laserActive = false;

            }

        }


    }

    public class PlayerLaserShotUpgradeStatusList
    {

        public ArrayList upgradeStatus;

        public PlayerLaserShotUpgradeStatusList() {

            upgradeStatus = new ArrayList();

            //Level1 (Vector2(X,Y)
            Vector2 l1MoveVector = new Vector2(0, -1);
            Vector2 l1InitialPositionCorrection = new Vector2(0, 0);
            this.upgradeStatus.Add(new LasershotLevel(1, l1MoveVector, l1InitialPositionCorrection));

            //Level2 Shot 1
            Vector2 l2MoveVector = new Vector2(0, -1);
            Vector2 l2S1InitialPositionCorrection = new Vector2(-10, 0);
            this.upgradeStatus.Add(new LasershotLevel(2, l2MoveVector, l2S1InitialPositionCorrection));

            //Level2 Shot 2
            Vector2 l2S2MoveVector = new Vector2(0, -1);
            Vector2 l2S2InitialPositionCorrection = new Vector2(10, 0);
            this.upgradeStatus.Add(new LasershotLevel(2, l2S2MoveVector, l2S2InitialPositionCorrection));

            //Level3 Shot 1
            Vector2 l3s1MoveVector = new Vector2(-1, -1);
            Vector2 l3s1InitialPositionCorrection = new Vector2(-1, 0);
            this.upgradeStatus.Add(new LasershotLevel(3, l3s1MoveVector, l3s1InitialPositionCorrection));

            //Level3 Shot 2
            Vector2 l3s2MoveVector = new Vector2(0, -1);
            Vector2 l3s2InitialPositionCorrection = new Vector2(0, 0);
            this.upgradeStatus.Add(new LasershotLevel(3, l3s2MoveVector, l3s2InitialPositionCorrection));

            //Level3 Shot 3
            Vector2 l3s3MoveVector = new Vector2(1, -1);
            Vector2 l3s3InitialPositionCorrection = new Vector2(1, 0);
            this.upgradeStatus.Add(new LasershotLevel(3, l3s3MoveVector, l3s3InitialPositionCorrection));


        }

    }

    public class LasershotLevel
    {

        public int upgradeLevel;
        public Vector2 moveVector;
        public Vector2 initialPositionCorrection;

        public LasershotLevel(int upgradeLevel, Vector2 moveVector, Vector2 initialPositionCorrection)
        {
            this.upgradeLevel = upgradeLevel;
            this.moveVector = moveVector;
            this.initialPositionCorrection = initialPositionCorrection;
        }

    }

    public class PlayerShotHandler
    {        
        public PlayerLaserShotUpgradeStatusList playerShotDefinitionList;
        public List<PlayerLaserShot> playerLaserShotList;
        public float playerLaserShootingSpeed;
        public float playerShootingSpawnCounter;
        public Vector2 shootingStartPositionCorrection;
        public static Texture2D laserShotTexture;
        public SpriteBatch spriteBatch;

        public PlayerShotHandler(ContentManager content, GraphicsDevice graphicsDevice)
        {
            laserShotTexture = content.Load<Texture2D>("LaserShotsOrange.png");
            this.playerShotDefinitionList = new PlayerLaserShotUpgradeStatusList();
            this.spriteBatch = new SpriteBatch(graphicsDevice);
            this.playerLaserShotList = new List<PlayerLaserShot>();
        }

        public void AddShot(int shotLevel,Vector2 initialPosition, int laserDamage)
        {                        

            var laserShotsToBuild = from LasershotLevel lasershotLevel in this.playerShotDefinitionList.upgradeStatus
                                    where lasershotLevel.upgradeLevel == shotLevel
                                    select lasershotLevel;

            foreach(LasershotLevel laserShot in laserShotsToBuild)
            {
                Vector2 correctedPosition = initialPosition + laserShot.initialPositionCorrection;
                this.playerLaserShotList.Add(new PlayerLaserShot(correctedPosition, laserShot.moveVector, this.spriteBatch, laserDamage));

            }

        }

        public void RemoveInactiveShots()
        {

            this.playerLaserShotList.RemoveAll(laserShot => laserShot.laserActive == false);            

        }
        

        public void RemoveAllShots()
        {            
            this.playerLaserShotList.Clear();
        }

        public void DrawShots()
        {
            foreach(PlayerLaserShot laserShot in this.playerLaserShotList)
            {
                laserShot.DrawFrame();
            }
        }

        public void UpdateShots(int screenHeight, int screenWidth)
        {
            foreach(PlayerLaserShot laserShot in this.playerLaserShotList)
            {
                laserShot.UpdateLaser(screenHeight, screenWidth);
            }
        }


    }


}
