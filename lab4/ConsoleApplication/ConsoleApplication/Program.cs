using System.Collections.Generic;

namespace ConsoleApplication
{
    internal class Program
    {
        static void Case_1()
        {
            var backup = new Backup(new List<string>(new[] { "1.txt", "2.txt" }));
            backup.CreateRestorePoint("Full");
            backup.CreateRestorePoint("Full");
            backup.PrintInfo();
            backup.AddCleaner(new CleanRestorePointsQuantity(1));
            backup.PrintInfo();
            backup.CreateRestorePoint("Full");
            backup.PrintInfo();
        }
        static void Case_2()
        {
            var backup = new Backup(new List<string>(new [] { "1.txt"}));
            backup.CreateRestorePoint("Full");
            backup.Add("2.txt");
            backup.CreateRestorePoint("Incremental");
            backup.PrintInfo();
            backup.AddCleaner(new CleanRestorePointsSize(15));
            backup.PrintInfo();
        }
        static void Case_3()
        {
            var backup = new Backup(new List<string>(new [] { "1.txt", "2.txt" }));
            backup.CreateRestorePoint("Full");
            backup.Add("3.txt");
            backup.CreateRestorePoint( "Incremental");
            backup.PrintInfo();
            backup.AddCleaner(new CleanRestorePointsQuantity(1));
            backup.PrintInfo();
        }
        
        static void Case_4()
        {
            var backup = new Backup(new List<string>(new [] { "1.txt" }));
            backup.CreateRestorePoint("Full");
            backup.Add("2.txt");
            backup.CreateRestorePoint("Full");
            backup.Add("3.txt");
            backup.CreateRestorePoint("Full");
            backup.PrintInfo();
            backup.AddCleaner(
                new List<CleanRestorePoints> {new CleanRestorePointsQuantity(2), new CleanRestorePointsSize(20)},
                "DeleteIfAllLimits");
            backup.PrintInfo();
        }
        public static void Main(string[] args)
        {
            Case_1();
        }
    }
}