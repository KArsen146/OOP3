using System;
using System.Collections.Generic;
using ChangeDate;

namespace UIL
{
    public class Report
    {
        public Report(int id, DateTime date, int employeeId, ReportType type, ReportState state, string text, List<int> tasks, List<int> dailyReports)
        {
            Id = id;
            EmployeeId = employeeId;
            Date = date;
            Text = text;
            Type = type;
            State = state;
            Tasks = tasks == null ? new List<int>() :new List<int>(tasks);
            DailyReports= dailyReports== null ? new List<int>() :new List<int>(dailyReports);
        }
        public Report(int employeeId, string text = ""):
            this(0, ChangeDate.Date.MyDate, employeeId, ReportType.Daily, ReportState.Open, text,new List<int>(),new List<int>() )
        {
        }
        public int EmployeeId{ get; }
        public DateTime Date{ get;}
        public ReportType Type{ get; internal set; }

        private ReportState state;
        public ReportState State {
            get
            {
                if (state == ReportState.Closed)
                    return state;
                if (Type == ReportType.Daily)
                {
                    if (Date == ChangeDate.Date.MyDate)
                        return ReportState.Open;
                    return ReportState.Closed;
                }
                return state;
            }
            protected set
            {
                state = value;
            }
        }
        public string Text { get; protected set; }
        public int Id { get; internal set; }
        
        public List<int> Tasks { get; internal set; }
        
        public List<int> DailyReports { get; internal set; }

        public void Show()
        {
            Console.WriteLine($"Report {Id}");
            Console.WriteLine($"ID : {Id}");
            Console.WriteLine($"EmoloyeeId : {EmployeeId}");
            Console.WriteLine($"Creation Date : {Date}");
            Console.WriteLine($"State : {State}");
            Console.WriteLine($"Text : {Text}");
            Console.WriteLine($"ReportType : {Type}");
            
            if (Type == ReportType.Daily)
            {
                Console.WriteLine($"TasksId :");
                foreach (var taskid in Tasks)
                {
                    Console.WriteLine($"{taskid}");
                }
            }
            else
            {
                Console.WriteLine($"DailyReportsId :");
                foreach (var rep in DailyReports)
                {
                    Console.WriteLine($"{rep}");
                } 
            }
            Console.WriteLine();
        }
    }
}