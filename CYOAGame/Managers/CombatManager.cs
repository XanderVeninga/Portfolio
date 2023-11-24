using System;
using System.Collections.Generic;
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
        int monstersDefeated = 0;
        public void InitCombat(GameManager gm, Player player)
        {
            gameManager = gm;
            var enemyList = new List<Enemy>()
            {
                new Enemy("Goblin", 50 * gm.difficulty, 5 * gm.difficulty),
                new Enemy("Kobold", 100 * gm.difficulty, 10 * gm.difficulty),
                new Enemy("Undead Knight", 150 * gm.difficulty, 15 * gm.difficulty),
                new Enemy("Demon", 200 * gm.difficulty, 20 * gm.difficulty),
                new Enemy("Dragon", 250 * gm.difficulty, 25 * gm.difficulty)
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
                Console.WriteLine($"You are fighting a {enemy.GetName()}, it has {enemy.GetHealth()} health remaining");
                Console.WriteLine($"Health: {player.GetHealth()}    {player.GetName()}\n what do you want to do\n[1]: Attack\n[2]: Heal\n[3]: Surrender");
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
                                Console.WriteLine($"{enemy.GetName()} has died!");
                                Thread.Sleep(2000);
                                player.AddPotion(2, 3);
                                Thread.Sleep(2000);
                                inCombat = false;
                                break;
                            }
                            Thread.Sleep(2500);
                            Console.WriteLine($"\nThe {enemy.GetName()} has attacked you dealing {enemy.GetDamage()} damage!");
                            
                            player.TakeDamage(enemy.GetDamage());
                            if (player.GetHealth() <= 0)
                            {
                                Console.Clear();
                                Console.WriteLine($"You have died!");
                                Thread.Sleep(3000);
                                gameManager.ChangeGameState(GameState.GAMEOVER);
                                inCombat = false;
                                break;
                            }
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;
                        case 2:
                            player.ViewPotionBag();
                            if(Int32.TryParse(Console.ReadLine(), out int potionChoice))
                            {
                                player.Heal(potionChoice + 1);
                            }

                            break;
                        case 3:
                            //code for surrendering
                            break;
                        default:
                            Console.WriteLine("That was not an option");
                            break;
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
