using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words.Fields;
using Aspose.Words.Layout;
using CYOAGame.SuperClasses;
using static CYOAGame.Classes.Potion;

namespace CYOAGame.Classes
{
    internal class Player : Entity
    {
        private List<Potion> potionList = new List<Potion>();
        int level;
        int xp;
        public Player(string name, float health, float damage, int level, int xp) : base(name, health, damage)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.level = 0;
            this.xp = xp;
        }

        public void LevelUp()
        {

        }
        public void IncreaseXP()
        {

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
                Console.WriteLine($"what kind of potion do you want to use?");
                Console.WriteLine("You currently have: ");
                for (int i = 0; i < potionList.Count; i++)
                {
                    var temp = potionList[i].GetPotionType();
                    string tempType = temp switch
                    {
                        1 => "Small",
                        2 => "Medium",
                        3 => "Large",
                    };

                    Console.WriteLine($"[{i+1}]: {potionList[i].GetAmount()} {tempType} Potions");
                }
            }
            else
            {
                Console.WriteLine("You have no potions");
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
                        Console.WriteLine($"You have received {amount} {tempType} potions");
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    potionList.Add(new Potion(tempType, amount));
                    Console.WriteLine($"You have received {amount} {tempType} potions");
                    Thread.Sleep(2000);
                }

            }
        }
        public void Heal(int potionType)
        {
            for(int i = 0; i < potionList.Count; i++)
            {
                if (potionList[i].GetPotionType() == potionType)
                {
                    health += potionList[i].GetHealAmmount();
                    float amountHealed;
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
                    Console.WriteLine($"You have healed {amountHealed} health");
                    potionList[i].RemovePotions(1);
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
