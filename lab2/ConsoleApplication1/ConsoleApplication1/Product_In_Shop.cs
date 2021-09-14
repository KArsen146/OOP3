using System.Security.Cryptography;

namespace ConsoleApplication1
{
    public class Product_In_Shop
    {
        public int quantity;
        public double price;
        public readonly string name;
        public readonly int id;
        public Product_In_Shop(int q, double pr, string _name, int _id)
        {
            quantity = q;
            price = pr;
            name = _name;
            id = _id;
        }
    }
}