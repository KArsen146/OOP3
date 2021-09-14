namespace ConsoleApplication2
{
    internal class Program
    {
        interface product
        {
            void get();
        }

        interface product1
        {
            void get();
        }

        class productt:product,product1
        {
            public void get()
            {
                throw new System.NotImplementedException();
            }
        }

        interface IFabric
        {
            product productA();
        }

        class fabric : IFabric
        {
            public product productA()
            {
                return new product1();
            }
        }
        public static void Main(string[] args)
        {
        }
    }
}