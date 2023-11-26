using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words.Fields;
using Aspose.Words.Layout;
using CYOAGame.Managers;
using CYOAGame.SuperClasses;
using static CYOAGame.Classes.Potion;

namespace CYOAGame.Classes
{
    internal class Player : Entity
    {
        private List<Potion> potionList = new List<Potion>();
        int level;
        int xp;
        public Player(string name, float health, float maxHealth, float damage, int level, int xp) : base(name, health, maxHealth, damage)
        {
            this.name = name;
            this.health = health;
            this.maxHealth = maxHealth;
            this.damage = damage;
            this.level = level;
            this.xp = xp;
        }

        public void LevelUp()
        { 
            xp = xp - xp*level;
            level++;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"\nYour level has increased by 1 you are now level {GetLvl()}\n");
            Thread.Sleep(1500);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Random rnd = new Random();
            int incDmg = rnd.Next(1 * level, 10 * level);
            int incHp = rnd.Next(1 * level, 10 * level);
            SetDamage(damage + incDmg);
            ChangeMaxHealth(maxHealth + incHp);
            SetHealth(health + incHp);
            Console.WriteLine($"\nYour damage has increased by {incDmg} you now deal {GetDamage()} damage");
            Thread.Sleep(1500);
            Console.WriteLine($"\nYour health has increased by {incHp} you now have {GetMaxHealth()} health\n");
            Console.WriteLine("Press Enter to continue.\n");
            Console.ReadLine();
            Console.Clear();
        }
        public int GetLvl()
        {
            return level;
        }
        public void IncreaseXP(int xpAmount)
        {
            xp += xpAmount;
            if(xp > level*15)
            {
                LevelUp();
            }
        }
        
        public override void Attack(Entity target)
        {
            base.Attack(target);
        }
        public void ViewPotionBag()
        {
            if(potionList.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"what kind of potion do you want to use? type 0 to go back.");
                Console.WriteLine("You currently have: ");
                for (int i = 0; i < potionList.Count; i++)
                {
                    var temp = potionList[i].GetPotionType();
                    string tempType = temp switch
                    {
                        1 => "Small",
                        2 => "Medium",
                        3 => "Large",
                        _ => ""
                    };

                    Console.WriteLine($"[{i+1}]: {potionList[i].GetAmount()} {tempType} Potions ({potionList[i].GetHealAmmount()} health)");
                }
            }
            else
            {
                Console.WriteLine("\nYou have no potions! press Enter to continue.");
            }
        }
        public void AddPotion(int _id, int amount)
        {
            bool cancelAction = false;
            Potion.potionType tempType = new Potion.potionType();
            switch (_id)
            {
                case 1:
                    tempType = Potion.potionType.SMALL;
                    break;
                case 2:
                    tempType = Potion.potionType.MEDIUM;
                    break;
                case 3:
                    tempType = Potion.potionType.LARGE;
                    break;
                default:
                    cancelAction = true;
                    break;
            }
            if (!cancelAction)
            {
                if (potionList.Count > 0)
                {
                    bool newType = true;
                    for (int i = 0; i < potionList.Count; i++)
                    {
                        if (potionList[i].GetPotionType() == _id)
                        {
                            potionList[i].AddPotions(amount);
                            Console.WriteLine($"\nYou have received {amount} {tempType} potions\n");
                            Thread.Sleep(2000);
                            newType = false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (newType)
                    {
                        potionList.Add(new Potion(tempType, amount));
                        Console.WriteLine($"\nYou have received {amount} {tempType} potions\n");
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    potionList.Add(new Potion(tempType, amount));
                    Console.WriteLine($"You have received {amount} {tempType} potions\n");
                    Thread.Sleep(2000);
                }

            }
        }
        public void Heal(int potionType)
        {
            
            for (int i = 0; i < potionList.Count; i++)
            {
                if (potionList[i].GetPotionType() == potionType)
                {
                    float amountHealed;
                    string tempType;
                    health += potionList[i].GetHealAmmount();
                    if(health > maxHealth)
                    {
                        
                        float overHealed =  health + potionList[i].GetHealAmmount() - maxHealth;
                        amountHealed = potionList[i].GetHealAmmount() - overHealed;
                        health = maxHealth;
                    }
                    else
                    {
                        amountHealed = potionList[i].GetHealAmmount();
                    }
                    
                    switch(potionList[i].GetPotionType())
                    {
                        case 1:
                            tempType = "Small";
                            break;
                        case 2:
                            tempType = "Medium";
                            break;
                        case 3:
                            tempType = "Large";
                            break;
                        default:
                            tempType = "";
                            break;
                    }                        
                    Console.WriteLine($"\nYou have healed {amountHealed} health using a {tempType} Potion");
                    potionList[i].RemovePotions(1);
                    Thread.Sleep(2000);
                }
                else
                {
                    continue;
                }
            }
            
        }
    }
}
