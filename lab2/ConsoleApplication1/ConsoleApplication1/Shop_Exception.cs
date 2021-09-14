using System;

namespace ConsoleApplication1
{
    public class Shop_Exception : Exception
    {
        public Shop_Exception(string message)
            : base(message)
        {}
    }
}