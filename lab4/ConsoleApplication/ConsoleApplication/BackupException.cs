using System;

namespace ConsoleApplication
{
    public class BackupException : Exception
    {
        public BackupException(string message) : base(message)
        {
        }
    }
}