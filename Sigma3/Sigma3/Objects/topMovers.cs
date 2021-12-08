using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma3.Objects
{
    internal class topMovers
    {
        stock s;
        List<stock> stocks = new List<stock>();
        public void Add(String name, String symbol, double price)
        {
            s = new stock(name, symbol, price);
            stocks.Add(s);
        }
        public List<stock> getStocks()
        {
            return stocks;
        }

    }
}
