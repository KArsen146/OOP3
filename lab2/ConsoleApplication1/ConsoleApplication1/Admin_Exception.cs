using System;

namespace ConsoleApplication1
{
    public class Admin_Exception : Exception
    {
        public Admin_Exception(string message)
            : base(message)
        {}
    }
}