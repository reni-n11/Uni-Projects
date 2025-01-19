using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Soup:Product
    {
        public int Grams { get; private set; }

        public Soup(string name, int grams, decimal price)
            : base(name, price)
        {
            this.Grams = grams;
        }
        public override string ToString()
        {
            return $"{Name}, {Grams}гр., {Price}лв.";
        }
    }
}
