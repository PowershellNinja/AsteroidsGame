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
using TexturePackerMonoGameDefinitions;
using TexturePackerLoader;


namespace AsteroidsGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AsteroidsGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        MouseState currentMouseState;
        SpriteSheetLoader spriteSheetLoader;
        List<EnemyClass> enemies;
        float enemySpawnTime;
        float spawnTimeCounter;
        Random random;
        //EnemyDestroyedAnimationListener enemyDAL;        
        PlayerShotHandler playerShotHandler;
        public SpriteFont arialFont;
        public SpriteFont arialFont28;

        public enum GameInputType {
            Touch,
            Keyboard,
            Mouse
        }

        public GameInputType inputType = GameInputType.Keyboard;

        GameStateWatcher gameStateWatcher = new GameStateWatcher(GameStateWatcher.GameState.StartMenu);

        
        StartMenuScreen startMenuScreen;
        GameOverScreen gameOverScreen;
        UpgradeMenuScreen upgradeMenuScreen;

        Texture2D startButtonSprite;
        Texture2D upgradeButtonSprite;
        Texture2D exitButtonSprite;
        Texture2D startMenuBackgroundSprite;

        int screenWidth;
        int screenHeight;
        PlayerClass player;
        Vector2 initialPlayerPosition;

        MovingBackgroundAnimation backgroundAnimation;

        

        public AsteroidsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            //graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();

            //graphics.PreferredBackBufferWidth = 1200;  // set this value to the desired width of your window
            //graphics.PreferredBackBufferHeight = 800;   // set this value to the desired height of your window
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;            
            graphics.ApplyChanges();

            //Took it out as debugging is somehow hard with this!
            //graphics.ToggleFullScreen();

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            //this.Window.AllowUserResizing = true;
            //this.Window.Position = new Point(((GraphicsDevice.DisplayMode.Width / 2) - screenWidth /2), ((GraphicsDevice.DisplayMode.Height / 2) - screenHeight / 2));


            //Create and Load Player
            player = new PlayerClass();
            initialPlayerPosition = new Vector2(screenWidth / 2, screenHeight - 200);


            //Create the Background
            backgroundAnimation = new MovingBackgroundAnimation();


            //Create Start Menu Screen
            startMenuScreen = new StartMenuScreen();
            gameOverScreen = new GameOverScreen();
            upgradeMenuScreen = new UpgradeMenuScreen();

            //Load Event Listener

            //Enemy Destroyed Listener
            //enemyDAL = new EnemyDestroyedAnimationListener();

            TouchPanel.EnabledGestures = GestureType.FreeDrag;

            enemies = new List<EnemyClass>();
            spawnTimeCounter = 0;
            enemySpawnTime = 2.0f;
            random = new Random();         
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteSheetLoader = new SpriteSheetLoader(Content);

            startMenuBackgroundSprite = Content.Load<Texture2D>("StartScreen.png");


            backgroundAnimation.LoadBackGround(Content);
            backgroundAnimation.spriteBatch = new SpriteBatch(GraphicsDevice);

            
            HealthBar.healthBarTexture = Content.Load<Texture2D>("HealthBar.png");
            EnemyClass.enemyTexture = Content.Load<Texture2D>("EnemyShip.png");


            player.InitializeAndLoadPlayer(Content, initialPlayerPosition, new SpriteBatch(GraphicsDevice));
            player.spriteBatch = new SpriteBatch(GraphicsDevice);

            playerShotHandler = new PlayerShotHandler(Content, GraphicsDevice);
            playerShotHandler.playerShootingSpawnCounter = 0;
            playerShotHandler.shootingStartPositionCorrection = new Vector2((player.playerTexture.Width * player.shipScale.X) / 2, 0);


            arialFont = Content.Load<SpriteFont>("Arial");
            arialFont28 = Content.Load<SpriteFont>("Arial_28");

            TextureManager.enemySpriteSheet = spriteSheetLoader.Load("EnemyDeathAnimationTexture.png");

            startButtonSprite = Content.Load<Texture2D>("StartButton.png");
            upgradeButtonSprite = Content.Load<Texture2D>("UpgradesButton.png");
            exitButtonSprite = Content.Load<Texture2D>("ExitButton.png");

            startMenuScreen.LoadStartMenu(GraphicsDevice, Content, screenWidth, startMenuBackgroundSprite,startButtonSprite,upgradeButtonSprite,exitButtonSprite);
            startMenuScreen.spriteBatch = new SpriteBatch(GraphicsDevice);

            gameStateWatcher.SubscribeStartButton(startMenuScreen.startButton);
            gameStateWatcher.SubscribeUpgradeButton(startMenuScreen.upgradeButton);
            gameStateWatcher.SubscribeExitButton(startMenuScreen.exitButton);

            gameOverScreen.LoadGameOverScreen(GraphicsDevice, Content, screenWidth, upgradeButtonSprite, exitButtonSprite);

            gameStateWatcher.SubscribeUpgradeButton(gameOverScreen.upgradeButton);
            gameStateWatcher.SubscribeExitButton(gameOverScreen.exitButton);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            

            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            if(gameStateWatcher.currentGameState == GameStateWatcher.GameState.PendingExit)
            {
                    Exit();                
            }
            else if(gameStateWatcher.currentGameState == GameStateWatcher.GameState.StartMenu)
            {
                this.IsMouseVisible = true;
                startMenuScreen.UpdateStartMenu(currentMouseState);

                if(currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }


            }
            else if(gameStateWatcher.currentGameState == GameStateWatcher.GameState.UpgradeMenu)
            {
                this.IsMouseVisible = true;

                if(currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
            }
            else if(gameStateWatcher.currentGameState == GameStateWatcher.GameState.GameOver)
            {
                this.IsMouseVisible = true;

                //Clear any remaining Shots as this is Game Over
                if(playerShotHandler.playerLaserShotList.Count > 0)
                {
                    playerShotHandler.RemoveAllShots();
                }

                //Clear any remaining Enemies as this is Game Over
                if(enemies.Count > 0)
                {
                    enemies.Clear();
                }

                if(currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }

                //Update Screen (GameState)
                gameOverScreen.UpdateGameOverScreen(currentMouseState);

            }
            else if(gameStateWatcher.currentGameState == GameStateWatcher.GameState.RunningLevel)
            {

                float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Turn off Mouse Pointer Display for Running Game
                if(this.IsMouseVisible == true)
                {
                    this.IsMouseVisible = false;
                }

                player.UpdatePlayer(currentKeyboardState, screenWidth, screenHeight, mousePosition, inputType);

                if(currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }

                if(currentKeyboardState.IsKeyDown(Keys.D1))
                {
                    player.playerShotLevel = 1;
                }
                if(currentKeyboardState.IsKeyDown(Keys.D2))
                {
                    player.playerShotLevel = 2;
                }
                if(currentKeyboardState.IsKeyDown(Keys.D3))
                {
                    player.playerShotLevel = 3;
                }
                

                if(currentKeyboardState.IsKeyDown(Keys.Space))
                {
                    player.isShooting = true;
                }
                else
                {
                    player.isShooting = false;
                }

                playerShotHandler.playerShootingSpawnCounter += elapsedSeconds;
                playerShotHandler.playerLaserShootingSpeed = player.playerLaserShootingSpeed;

                if(player.isShooting && playerShotHandler.playerShootingSpawnCounter > playerShotHandler.playerLaserShootingSpeed)
                {
                    Vector2 shotInitialPosition = player.playerPosition + playerShotHandler.shootingStartPositionCorrection;
                    playerShotHandler.AddShot(player.playerShotLevel,shotInitialPosition,player.playerResultingDamage);
                    playerShotHandler.playerShootingSpawnCounter = 0;
                }

                //Update Player Laser Shots
                playerShotHandler.UpdateShots(screenHeight,screenWidth);

                //Process possible Collisions between Enemies and Lasers
                CollisionMethods.ProcessEnemyLaserShotCollision(enemies,playerShotHandler.playerLaserShotList);

                //Process possible Collisions between Enemies and the Player
                CollisionMethods.ProcessPlayerEnemyCollision(player, enemies);

                //Update all existing Enemies
                foreach(EnemyClass enemy in enemies)
                {
                    enemy.UpdateEnemy(screenHeight, elapsedSeconds);
                }

                //Remove Inactive Laser Shots
                playerShotHandler.RemoveInactiveShots();
                                

                //Remove Inactive Enemies
                enemies.RemoveAll(deadEnemy => deadEnemy.enemyDestroyAnimationFinished == true);
               
                spawnTimeCounter += elapsedSeconds;

                if(spawnTimeCounter >= enemySpawnTime)
                {
                    //Old: AddEnemy(enemyDAL);
                    AddEnemy();
                    spawnTimeCounter = 0;
                    enemySpawnTime = random.Next(0, 4);
                }

            }

            previousKeyboardState = currentKeyboardState;

            //If this is a running game and the players health is 0 or below, go to GameOver
            if(player.playerHealth <= 0 && gameStateWatcher.currentGameState == GameStateWatcher.GameState.RunningLevel)
            {
                gameStateWatcher.currentGameState = GameStateWatcher.GameState.GameOver;
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;                        


            if(gameStateWatcher.currentGameState == GameStateWatcher.GameState.StartMenu)
            {
                startMenuScreen.DrawFrame();

            }
            else if(gameStateWatcher.currentGameState == GameStateWatcher.GameState.UpgradeMenu)
            {

            }
            else if(gameStateWatcher.currentGameState == GameStateWatcher.GameState.GameOver)
            {

                gameOverScreen.DrawFrame(spriteBatch,backgroundAnimation,elapsedGameTime,screenHeight,screenWidth,arialFont28);

            }
            else if(gameStateWatcher.currentGameState == GameStateWatcher.GameState.RunningLevel)
            {

                //Draw Background first
                backgroundAnimation.DrawFrame(elapsedGameTime, screenHeight);                

                //Then draw Player
                player.DrawFrame();

                playerShotHandler.DrawShots();

                //Then draw Enemies
                foreach(EnemyClass enemy in enemies.Where(item => item.enemyActive == true))
                {
                    enemy.DrawFrame();
                }
                //Draw Enemy death animation if needed
                foreach(EnemyClass enemy in enemies.Where(item => item.enemyActive == false))
                {
                    //If the Animation Position Vector is Empty, set it one time
                    if(enemy.enemyDeathAnimation.animationPosition.X == 0f && enemy.enemyDeathAnimation.animationPosition.Y == 0f)
                    {

                        enemy.enemyDeathAnimation.animationPosition = enemy.enemyPosition + enemy.enemyDeathAnimation.positionCorrection;
                    }

                    //Call DeathAnimation DrawFrame
                    enemy.enemyDeathAnimation.DrawFrame(elapsedGameTime, enemy);                    
                }

            }


            base.Draw(gameTime);
        }

        //Old: public void AddEnemy(EnemyDestroyedAnimationListener enemyDAL)
        public void AddEnemy()
        {

            EnemyClass newEnemy = new EnemyClass(Content,random,screenWidth, (new SpriteBatch(GraphicsDevice)), spriteSheetLoader);
            //enemyDAL.Subscribe(newEnemy);
            enemies.Add(newEnemy);
        }

        

    }
}
