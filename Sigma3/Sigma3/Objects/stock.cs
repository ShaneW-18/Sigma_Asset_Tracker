using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma3.Objects
{
    internal class stock
    {
        public String Name { get; set; }
        public String Symbol { get; set; }
        public double Price { get; set; }

        public stock(string name, String symbol, double price)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.Price = price;
        }
    }
}
