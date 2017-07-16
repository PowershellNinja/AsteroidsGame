using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidsGame.Buttons;

namespace AsteroidsGame.GameAssets
{
    public class GameStateWatcher
    {

        public enum GameState
        {
            StartMenu,
            UpgradeMenu,
            RunningLevel,
            GameOver,
            PendingExit
        }

        public GameState currentGameState;

       
        public GameStateWatcher(GameState newGameState)
        {
            this.currentGameState = newGameState;
        }

        public void SubscribeStartButton(StartButton sb)
        {
            sb.onClick += new StartButton.onClickHandler(SetGameStateRunning);
        }

        public void SubscribeUpgradeButton(UpgradeButton ub)
        {
            ub.onClick += new UpgradeButton.onClickHandler(SetGameStateUpgrade);
        }

        public void SubscribeExitButton(ExitButton eb)
        {
            eb.onClick += new ExitButton.onClickHandler(SetGameStateExitGame);
        }

        public void SetGameStateExitGame(EventArgs e)
        {
            currentGameState = GameStateWatcher.GameState.PendingExit;
        }

        public void SetGameStateStartMenu(EventArgs e)
        {
            currentGameState = GameStateWatcher.GameState.StartMenu;
        }
        public void SetGameStateUpgrade(EventArgs e)
        {
            currentGameState = GameStateWatcher.GameState.UpgradeMenu;            
        }
        public void SetGameStateRunning(EventArgs e)
        {
            currentGameState = GameStateWatcher.GameState.RunningLevel;
        }
        public void SetGameStateGameOver(EventArgs e)
        {
            currentGameState = GameStateWatcher.GameState.GameOver;
        }







    }
}
