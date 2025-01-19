using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class MainDish : Product
    {
        public int Grams { get; private set; }
        public double Calories { get; private set; }

        public MainDish(string name, int grams, decimal price)
                        : base(name, price)
        {
            this.Grams = grams;
            this.Calories = grams * 1;
        }
        public override string ToString()
        {
            return $"{Name}, {Grams}гр., {Price}лв., {Calories}kcal";
        }
    }
}
