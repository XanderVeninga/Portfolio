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

        public Enemy(string name, float health, float damage, int xpReward, int potionReward, int potionAmount : base(name, health, damage)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.potionReward = potionReward;
            this.potionAmount = potionAmount;
        }
        public override void Attack(Entity target)
        {
            base.Attack(target);
        }
    }
}
