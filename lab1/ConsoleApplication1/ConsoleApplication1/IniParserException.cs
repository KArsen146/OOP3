using System;

namespace ConsoleApplication1
{
    public class IniParserException: ArgumentException
    {
        public IniParserException(string message)
            : base(message)
        {}
    }
}