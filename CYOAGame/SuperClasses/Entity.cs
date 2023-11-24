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
        public float damage;
        public Entity(string name, float health, float damage)
        {
            this.name = name;
            this.health = health;
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

        public float GetHealth()
        {
            return health;
        }
        public void SetHealth(float newHealth)
        {
            health = newHealth;
        }
        public float GetDamage()
        {
            return damage;
        }
        public void SetDamage(float newDamage)
        {
            damage = newDamage;
        }

        public void TakeDamage(float _damage)
        {
            health = health - _damage;
        }

        public virtual void Attack(Entity target)
        {
            target.TakeDamage(GetDamage());
        }
    }
}
