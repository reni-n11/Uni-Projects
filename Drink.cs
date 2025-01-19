using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Drink : Product
    {
        public int Mililiters { get; private set; }
        public double Calories { get; private set; }

        public Drink(string name, int mililiters, decimal price) 
                    : base(name, price)
        {
            this.Mililiters = mililiters;
            this.Calories = mililiters * 1.5;
        }
        public override string ToString()
        {
            return $"{Name}, {Mililiters}мл., {Price}лв., {Calories}kcal";
        }
    }
}
