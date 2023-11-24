using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYOAGame.Classes
{
    internal class Potion
    {
        public enum potionType
        {
            SMALL,
            MEDIUM,
            LARGE
        }

        potionType type;
        int amount;
        public void AddPotions(int _amount)
        {
            this.amount += _amount;
        }
        public void RemovePotions(int _amount)
        {
            this.amount -= _amount;
        }
        public int GetAmount() { return this.amount; }
        public Potion(potionType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }
        public void SetPotionType(int id)
        {
            type = id switch
            {
                1 => potionType.SMALL,
                2 => potionType.MEDIUM,
                3 => potionType.LARGE,
                _ => potionType.SMALL,
            };
        }
        public int GetPotionType()
        {
            return this.type switch
            {
                potionType.SMALL => 1,
                potionType.MEDIUM => 2,
                potionType.LARGE => 3,
                _ => 0,
            };
        }
        public int GetHealAmmount()
        {
            if(this.type == potionType.SMALL)
            {
                return 50;
            }
            else if(this.type == potionType.MEDIUM)
            {
                return 100;
            }
            else if( this.type == potionType.LARGE)
            {
                return 150;
            }
            return 0;
        }
    }
}
