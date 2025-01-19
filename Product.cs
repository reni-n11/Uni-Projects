using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Product
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }


        public Product(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }

        public override string ToString()
        {
            return $"{Name}, {Price}лв.,";
        }
    }


}
