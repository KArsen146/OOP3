using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class RestorePoint
    {
        public readonly RestorePoint PreviousPoint;
        
        public readonly long Size;
        
        public readonly DateTime Date;

        public readonly int Ind;

        private static int _ind;

        public readonly List<FileRestoreCopyInfo> Files;
        
        public RestorePoint(RestorePoint lastRestorePoint, List<FileRestoreCopyInfo> files, DateTime date)
        {
            _ind++;
            Ind = _ind;
            Date = date;
            PreviousPoint = lastRestorePoint;
            Size = 0;
            Files = files;
            foreach (var file in Files)
            {
                Size += file.FileSize;
            }
        }
        
        public bool IsIncrement()
        {
            return PreviousPoint != null;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Restore point {Ind}");
            Console.WriteLine($"Creation Date {Date}");
            Console.WriteLine($"Size {Size}");
            Console.WriteLine($"There are {Files.Count} files");
            int i = 0;
            foreach (var file in Files)
            {
                i++;
                Console.WriteLine($"File number {i}");
                file.PrintInfo();
            }
        }
    }
}