using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CYOAGame.Classes;

namespace CYOAGame.Managers
{
    public enum GameState
    {
        INFO, COMBAT, STORY, SHOP, GAMEOVER
    }
    internal class GameManager
    {
        CombatManager combatManager = new CombatManager();
        StoryManager storyManager = new StoryManager();
        ShopManager shopManager = new ShopManager();
        CharacterCreator characterCreator = new CharacterCreator();
        Player player;

        bool gameOver = true;
        public int difficulty;
        public GameState gameState = GameState.INFO;

        public void RunGame()
        {
            while (gameOver)
            {
                switch (gameState)
                {
                    case GameState.INFO:
                        player = characterCreator.NewCharacter(this);
                        break;
                    case GameState.COMBAT:
                        combatManager.InitCombat(this, player);
                        break;
                    case GameState.STORY:
                        storyManager.RunStory(this, player);
                        break;
                    case GameState.SHOP:
                        shopManager.OpenShop(this);
                        break;
                    case GameState.GAMEOVER:
                        break;
                }
            }
        }

        public  void ChangeGameState(GameState newState)
        {
            gameState = newState;
        }
    }
}
