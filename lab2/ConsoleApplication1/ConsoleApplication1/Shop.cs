using System;
using System.Collections.Generic;
namespace ConsoleApplication1
{
    public class Shop
    {
        public readonly static Dictionary<int, Shop> shops = new Dictionary<int, Shop>();
        private Dictionary<int, Product_In_Shop> range;
        public readonly string name;
        public readonly int id = 0;
        private static int shop_id = 0;
        public Shop(string n)
        {
            name = n;
            shop_id++;
            id = shop_id;
            range = new Dictionary<int, Product_In_Shop>();
            shops.Add(id, this);
        }

        public void delete_shop(int _id)
        {
            
        }
        public void Add(Product p, int product_quantity, int product_price)
        {
            if (range.ContainsKey(p.id))
            {
                range[p.id].price = product_price;
                range[p.id].quantity += product_quantity;
            }
            else
            {
                range.Add(p.id, new Product_In_Shop(product_quantity, product_price, p.name, p.id));
            }    
        }
    
        public void change_price(Product p, int product_price)
        {
            try
            {
                if (!range.ContainsKey(p.id))
                {
                    throw new Shop_Exception("There no this procuts in this shop");
                }

                range[p.id].price = product_price;
            }
            catch (Shop_Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public List<KeyValuePair<Product, int>> find_products_for_price(double price)
        {
            var l = new List<KeyValuePair<Product, int>>(); 
            int count_products = 0;
            foreach (var product in range.Values) 
            { 
                int count = Math.Min(product.quantity, (int)(price / product.price));
                if (count > 0)
                { 
                    Console.WriteLine("You can buy {0} {1}", count, product.name); 
                    l.Add(new KeyValuePair<Product, int>(Product.get_product(product.id), count));
                    count_products++; 
                }
            }
            try
            {
                if (count_products == 0)
                    throw new Shop_Exception("There are no products in this shop");
            }
            catch (Admin_Exception e)
            {
                Console.WriteLine(e);
            }

            return l;
        }
        
        private bool try_to_buy(Consigment consignment)
        {
            bool check = false;
            foreach (var p in consignment.c)
            {
                if ((!is_in(p.Key.id)) || (get_info(p.Key.id).quantity < p.Value))
                    check = true;
            }
            return check;
        }
        public double buy_products(Consigment  consignment)
        {
            double summ = 0;
            bool check = try_to_buy(consignment);
            try
            {
                if (check)
                    throw new Admin_Exception("You can't buy this consigment in this shop");
                foreach (var p in consignment.c)
                {
                    summ += (range[p.Key.id].price * p.Value);
                    range[p.Key.id].quantity -= p.Value;
                    if (range[p.Key.id].quantity == 0)
                        range.Remove(p.Key.id);
                }

                return summ;
            }
            catch (Admin_Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public bool is_in(int _id)
        {
            return range.ContainsKey(_id);
        }

        public Product_In_Shop get_info(int _id)
        {
            return range[_id];
        }
    }
}