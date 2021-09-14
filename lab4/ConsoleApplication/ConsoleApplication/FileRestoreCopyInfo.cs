using System;


namespace ConsoleApplication
{
    public class FileRestoreCopyInfo
    {
        public readonly DateTime Date;
        
        public readonly long FileSize;
        
        public readonly string FilePath;

        public FileRestoreCopyInfo(string filepath, long filesize, DateTime date)
        {
            FilePath = filepath;
            FileSize = filesize;
            Date = date;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"File path {FilePath}");
            Console.WriteLine($"File Size {FileSize}");
            Console.WriteLine($"Date {Date}");
        }
    }
}