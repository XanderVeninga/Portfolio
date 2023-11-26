using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYOAGame.SuperClasses
{
    internal class Entity
    {
        public string name;
        public float health;
        public float maxHealth;
        public float damage;
        public Entity(string name, float health, float maxHealth, float damage)
        {
            this.name = name;
            this.health = health;
            this.maxHealth = maxHealth;
            this.damage = damage;
        }

        public string GetName()
        {
            return name;
        }
        public void SetName(string newName)
        {
            name = newName;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }
        public void ChangeMaxHealth(float amount)
        {
            maxHealth = amount;
        }
        public float GetHealth()
        {
            return health;
        }
        public void SetHealth(float newHealth)
        {
            health = newHealth;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }
        public float GetDamage()
        {
            return damage;
        }
        public void SetDamage(float newDamage)
        {
            damage = newDamage;
        }

        public virtual void TakeDamage(float _damage)
        {
            health = health - _damage;
            if(health < 0)
            {
                GameOver();
            }
        }

        public virtual void Attack(Entity target)
        {
            target.TakeDamage(GetDamage());
        }

        public virtual void GameOver()
        {
            health = 0;

            //extend if needed;
        }
    }
}
