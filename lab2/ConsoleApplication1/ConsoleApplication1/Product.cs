using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Product
    {
        private static Dictionary<int, Product> products = new Dictionary<int, Product>();
        public readonly string name;
        private static int product_id = 0;
        public readonly int id;
        public Product(string _name)
        {
            product_id++;
            id = product_id;
            name = _name;
            products.Add(id, this);
        }

        public static bool is_in(int _id)
        {
            return products.ContainsKey(_id);
        }
        public static Product get_product(int _id)
        {
            try
            {
                if (!is_in(_id))
                    throw new Shop_Exception("No this product");
                return products[_id];
            }

            catch (Shop_Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}