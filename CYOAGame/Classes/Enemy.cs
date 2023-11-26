using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CYOAGame.SuperClasses;

namespace CYOAGame.Classes
{
    internal class Enemy : Entity
    {
        int potionReward;
        int potionAmount;
        int xpReward;
        public Enemy(string name, float health, float maxHealth, float damage, int xpReward, int potionReward, int potionAmount) : base(name, health, maxHealth, damage)
        {
            this.name = name;
            this.health = health;
            this.maxHealth = maxHealth;
            this.damage = damage;
            this.xpReward = xpReward;
            this.potionReward = potionReward;
            this.potionAmount = potionAmount;
        }
        public override void Attack(Entity target)
        {
            base.Attack(target);
        }

        public int GetXpReward()
        {
            return xpReward;
        }
        public int GetPotionReward()
        {
            return potionReward;
        }
        public int GetPotionAmount()
        {
            return potionAmount;
        }
    }
}
