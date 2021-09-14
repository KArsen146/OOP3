using System;
using BLL;
using ChangeDate;
using DAL;
using UIL;
namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var rep = new EmployeeRepository();
                var rep2 = new TaskRepository();
                var rep3 = new TaskChangesRepository();
                var rep4 = new ReportRepository();
                var serv = new EmployeeService(rep);
                var serv2 = new TaskService(rep2, serv, rep3);
                var serv3 = new ReportService(rep4, serv2, serv);
                var cont = new EmployeeController(serv);
                var cont2 = new TaskController(serv2);
                var cont3 = new ReportController(serv3);
                int a = cont.Add(new UIL.Employee("1st"));
                int b = cont.Add(new UIL.Employee("2nd", a));
                int c = cont.Add(new UIL.Employee("3rd", a));
                int d = cont.Add(new UIL.Employee("4th", b));
                cont.Show();
                cont.ChangeLeader(d, c);
                cont.Show();
                cont.ChangeLeader(c, b);
                cont.Show();
                int ind1 = cont2.Add(new UIL.Task("1", "a"));
                int ind2 = cont2.Add(new UIL.Task("2", "b"));
                int ind3 = cont2.Add(new UIL.Task("3", "c"));
                int ind4 = cont2.Add(new UIL.Task("4", "d"));
                cont2.AppointTask(ind1, 1, 4);
                cont2.AppointTask(ind2, 1, 4);
                cont2.AppointTask(ind3, 1, 4);
                cont2.MakeActive(4, 1);
                //cont2.AppointTask(ind2, 2, 1);
                //cont2.FindTaskByCreationDate(Date.MyDate);
                cont2.AddComment(2, 1, "ахахха");
                Date.MyDate = Date.MyDate.AddDays(1);
                cont2.FindTaskByLastChangeDate(Date.MyDate.AddDays(-1));
                cont3.StartSprint();
                cont3.Add(new UIL.Report(4));
                //Date.MyDate = Date.MyDate.AddDays(1);
                cont3.AddText(4, "100");
                cont2.MakeResolved(4, ind1);
                cont3.AddAllTasks(3);
                cont3.FinishReport(4,3);
                cont3.AddSprintReport(1,3);
                cont3.GetDailyReports();
                cont3.FinishSprint(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }
    }
}