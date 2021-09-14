using System;
namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string s = "Diksi";
            var v = new Shop(s);
            Console.WriteLine(v.name);
            var v1 = new Shop("Ashan");
            var p = new Product("pelmens");
            v.Add(p, 10, 100);
            var p1 = new Product("pizza");
            var p2 = new Product("Mango");
            v.Add(p1, 11, 10);
            v.Add(p2, 11, 1000);
            v1.Add(p, 10, 20);
            v1.Add(p1, 101, 1000);
            v1.Add(p2, 101, 100);
            Consigment l = new Consigment();
            l.Add(p, 1);
            l.Add(p1, 1);
            l.Add(p2, 10);
            l.Add(p, 9);
            Admin.find_cheapest_product(p2);
            v1.find_products_for_price(101);
            Console.WriteLine("You can buy this consigment in this shop for the price {0}", v.buy_products(l));
            Admin.find_cheapest_consignment(l);
        }
    }
}