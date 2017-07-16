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
using TexturePackerMonoGameDefinitions;
using TexturePackerLoader;

namespace AsteroidsGame.GameAssets.SpriteAnimations
{
    public class EnemyDeathAnimation
    {
        public List<String> enemyDeathSteps = new List<String>();
        public SpriteBatch spriteBatch;
        public SpriteSheet spriteSheet;
        public SpriteRender spriteRender;
        // an index of the current frame being shown
        public int frameIndex;
        // total number of frames in our spritesheet
        public int totalFrames = 62;
        public float frameTime = 0.0001f;
        public float elapsedTime;
        public int targetFramesPerCall = 4;
        public Vector2 animationPosition;
        public Vector2 positionCorrection = new Vector2(70, 80);

        public EnemyDeathAnimation(SpriteBatch spriteBatch, SpriteSheet spriteSheet, SpriteRender spriteRender)
        {

            this.spriteBatch = spriteBatch;
            this.spriteSheet = spriteSheet;
            this.spriteRender = spriteRender;

            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_000);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_001);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_002);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_003);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_004);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_005);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_006);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_007);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_008);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_009);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_010);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_011);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_012);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_013);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_014);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_015);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_016);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_017);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_018);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_019);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_020);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_021);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_022);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_023);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_024);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_025);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_026);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_027);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_028);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_029);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_030);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_031);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_032);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_033);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_034);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_035);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_036);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_037);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_038);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_039);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_040);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_041);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_042);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_043);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_044);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_045);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_046);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_047);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_048);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_049);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_050);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_051);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_052);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_053);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_054);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_055);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_056);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_057);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_058);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_059);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_060);
            this.enemyDeathSteps.Add(TexturePackerMonoGameDefinitions.EnemyDeathAnimationProject.BlueShipDeathAnimationPNGs_061);


        }


        public void DrawFrame(float elapsedSeconds, EnemyClass enemy)
        {
                        
            this.elapsedTime += elapsedSeconds;

            if(this.elapsedTime > this.frameTime)
            {

                int[] sequence = Enumerable.Range(1, this.targetFramesPerCall).ToArray();

                foreach(int i in sequence)
                {
                    if(this.frameIndex <= (this.totalFrames - 1))
                    {

                        this.spriteBatch.Begin();

                        this.spriteRender.Draw(
                                this.spriteSheet.Sprite(
                                    this.enemyDeathSteps[frameIndex]
                                ),
                                this.animationPosition,
                                scale: enemy.shipScale.X
                            );


                        this.spriteBatch.End();

                        // Play the next frame in the SpriteSheet
                        this.frameIndex++;

                    }

                }

                // reset elapsed time
                this.elapsedTime = 0f;

                if(this.frameIndex >= (this.totalFrames - 1))
                {
                    enemy.enemyDestroyAnimationFinished = true;

                }


            }
        }

    }
}
