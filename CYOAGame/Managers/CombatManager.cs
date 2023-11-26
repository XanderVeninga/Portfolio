using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CYOAGame.Classes;

namespace CYOAGame.Managers
{
    internal class CombatManager
    {
        GameManager gameManager;
        public int monstersDefeated = 0;
        public void InitCombat(GameManager gm, Player player)
        {
            gameManager = gm;
            var enemyList = new List<Enemy>()
            {
                new Enemy("Goblin", 25 , 25 , 5 * gm.difficulty, 50, 1, 3),
                new Enemy("Kobold", 50 , 50, 20 * gm.difficulty, 100, 1, 5),
                new Enemy("Undead Knight", 100, 100, 25 * gm.difficulty, 200, 2, 3),
                new Enemy("Demon", 200, 200, 50 * gm.difficulty, 250, 2, 5),
                new Enemy("Dragon", 250, 250, 100 * gm.difficulty, 300, 3, 3)
            };
            RunCombat(player, enemyList);
        }
        public void RunCombat(Player player, List<Enemy> combatList)
        {
            bool inCombat = true;
            Enemy enemy;
            enemy = combatList[monstersDefeated];
            while(inCombat)
            {
                Console.Clear();
                Console.WriteLine($"Enemy Type: {enemy.GetName()}  Enemy Health: {enemy.GetHealth()}/{enemy.GetMaxHealth()}   Damage: {enemy.GetDamage()}");
                Console.WriteLine($"Your Name: {player.GetName()}   Health: {player.GetHealth()}/{player.GetMaxHealth()}   Level: {player.GetLvl()}   Monsters Defeated: {monstersDefeated}" +
                                  $"\n what do you want to do\n[1]: Attack\n[2]: Heal\n[3]: Surrender");
                if (Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"\nYou attacked the {enemy.GetName()} you dealt {player.GetDamage()} to it");
                            enemy.TakeDamage(player.GetDamage());
                            if (enemy.GetHealth() <= 0)
                            {
                                monstersDefeated++;
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.WriteLine($"\n{enemy.GetName()} has died!\n");
                                Console.BackgroundColor = ConsoleColor.Black;
                                Thread.Sleep(1250);
                                player.AddPotion(enemy.GetPotionReward(), enemy.GetPotionAmount());
                                player.IncreaseXP(enemy.GetXpReward());
                                Thread.Sleep(1000);
                                gameManager.ChangeGameState(GameState.STORY);
                                inCombat = false;
                                break;
                            }
                            Thread.Sleep(2500);
                            player.TakeDamage(enemy.GetDamage());
                            Console.WriteLine($"\nThe {enemy.GetName()} has attacked you dealing {enemy.GetDamage()} damage!");
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;
                        case 2:
                            player.ViewPotionBag();
                            if(Int32.TryParse(Console.ReadLine(), out int potionChoice))
                            {
                                player.Heal(potionChoice);
                            }

                            break;
                        case 3:
                            //code for surrendering
                            player.TakeDamage(player.health);
                            break;
                        default:
                            Console.WriteLine("That was not an option");
                            break;
                    }
                    if (player.GetHealth() <= 0)
                    {
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine($"You surrender and died of cowardice!\n");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("\n Press Enter to continue.");
                        Console.ReadLine();
                        gameManager.ChangeGameState(GameState.GAMEOVER);
                        inCombat = false;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number!");
                }
            }
        }
    }
}
