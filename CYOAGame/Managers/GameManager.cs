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
                        combatManager.monstersDefeated = 0;
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
                        Console.Clear();
                        Console.WriteLine("Do you want to start over?\n[1]: New Journey\n[2]: Quit Game");
                        if (Int32.TryParse(Console.ReadLine(), out int choice))
                        {
                            if (choice == 1)
                            {
                                gameState = GameState.INFO;
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Clear();
                            }
                            else if (choice == 2)
                            {
                                Environment.Exit(0);
                            }
                        }
                        break;
                }
            }
        }

        public void ChangeGameState(GameState newState)
        {
            gameState = newState;
        }
    }
}
