using System;

namespace ConsoleApplication1
{
    public class Admin
    {
        public static Shop find_cheapest_product(Product p)
        {
            double min_price = -1;
            var shop_name = new Shop("");
            foreach (var shop in Shop.shops.Values)
            {
                if (shop.is_in(p.id))
                {
                    if ((shop.get_info(p.id).price < min_price) || (min_price == -1))
                    {
                        min_price = shop.get_info(p.id).price;
                        shop_name = shop;
                    }
                }
            }

            try
            {
                if (shop_name.name == "")
                    throw  new Admin_Exception("There are no that products in all shops");
                Console.WriteLine("Cheapest product is in shop {0} for the price {1}", shop_name.name, min_price);
            }
            catch (Admin_Exception e)
            {
                Console.WriteLine(e);
            }
            return shop_name;
        }
        
        private static bool try_to_buy(Shop s, Consigment consignment)
        {
            bool check = false;
            foreach (var p in consignment.c)
            {
                if ((!s.is_in(p.Key.id)) || (s.get_info(p.Key.id).quantity < p.Value))
                    check = true;
            }
            return check;
        }

        public static Shop find_cheapest_consignment(Consigment consignment)
        {
            double min_summ = -1, summ = 0;
            Shop result = new Shop("");
            foreach (var shop in Shop.shops.Values)
            {

                if (!try_to_buy(shop, consignment))
                {
                    summ = 0;
                    foreach (var p in consignment.c)
                    {
                        summ += (shop.get_info(p.Key.id).price * p.Value);
                    }
                    
                    if ((summ < min_summ) || (min_summ == -1))
                    {
                        min_summ = summ;
                        result = shop;
                    }
                }
            }

            try
            {
                if (min_summ == -1)
                    throw new Admin_Exception("You can't buy this consigment in all shops");
               // Console.WriteLine("You can buy this consigment in {0} for the price {1}", result.name,min_summ);
            }
            catch (Admin_Exception e)
            {
                Console.WriteLine(e);

            }
            return result;
        }
    }
}