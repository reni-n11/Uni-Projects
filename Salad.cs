using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Salad : Product
    {
        public int Grams { get; private set; }

        public Salad(string name, int grams, decimal price)
            :base(name, price)
        {
            this.Grams = grams;
        }
        public override string ToString()
        {
            return $"{Name}, {Grams}гр., {Price}лв.";
        }
    }
}
