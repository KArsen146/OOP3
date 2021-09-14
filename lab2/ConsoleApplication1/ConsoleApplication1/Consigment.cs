using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Consigment
    {
        public readonly Dictionary<Product, int> c;
        
        public Consigment()
        {
            c = new Dictionary<Product, int>();
        }
        public void Add(Product key, int value)
        {
            if (c.ContainsKey(key))
                c[key] += value;
            else
                c.Add(key, value);
        }

        public int this[Product ind]
        {
            get
            {
                return c[ind];
            }
            set
            {
                c[ind] = value;
            }
        }
    }
}