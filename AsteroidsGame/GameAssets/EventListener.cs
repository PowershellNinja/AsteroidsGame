using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using AsteroidsGame.GameAssets;
using AsteroidsGame.Screens;

namespace AsteroidsGame.GameAssets
{
    public class EnemyDestroyedAnimationListener
    {
        //******************************
        //This Class remains just for reference use for a possible future event handler
        //as it is not used currently in this Game (I am kind of Google-Lazy :P)
        //******************************

        public void Subscribe(EnemyClass enemy)
        {
            //enemy.EnemyDestroyEvent += new EnemyClass.EnemyDestroyedHandler(EnemyDestroyedAnimation);
        }
        private void EnemyDestroyedAnimation(EnemyClass enemy, EventArgs e, float elapsedSeconds)
        {
            System.Console.WriteLine("Enemy Destroyed!");
            //ToDo: Implement Enemy Destroyed Animation
            //enemy.enemyDeathAnimation.DrawFrame(elapsedSeconds, enemy);
        }
    }

}
