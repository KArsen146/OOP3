using System;

namespace c_sharp1
{
    public class Book
    {
        public Book(string a, string t, string pb, int pg, int y)
        {
            Authtor = a;
            Title = t;
            Publisher = pb;
            Pages = pg;
            Year = y;
        }

        public Book() 
        { }

        public Book(string authtor, string title)
        {
            Authtor = authtor;
            Title = title;
        }

        public string Authtor { get; set; }
        
        public string Title { get; set; }
        
        public string Publisher { get; set; }
        
        public int Pages { get; set; }
        
        public int Year { get; set; }
        
        private static double price = 9;
        
        public static double Price
        {
            set
            {
                if (value > 9)
                    price = value;
            }
        }

        public void SetBook(string a, string t, string pb, int pg, int y)
        {
            Authtor = a;
            Title = t;
            Publisher = pb;
            Pages = pg;
            Year = y;
        }

        public static void SetPrice(double pr)
        {
            Book.price = pr;
        }

        public override string ToString()
        {
            string book = String.Format("Автор - {0} \nНазвание - {1} \nИздатель - {2} \nКол-во страниц - {3} \nГод - {4} \nЦена аренды - {5}",Authtor, Title, Publisher, Pages, Year, price);
            return book;
        }

        public void print()
        {
            Console.WriteLine(this);
        }

        public double RentByPeriod(int value)
        {
            return price * value;
        }
    }
}