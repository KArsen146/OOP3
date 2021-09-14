using System;
using System.Collections.Generic;
using ChangeDate;

namespace DAL
{
    public class Report : IEntity
    {
        public int EmployeeId{ get; }
        public DateTime Date{ get;}
        public ReportType Type{ get; internal set; }
        public ReportState State { get; protected set; }
        public string Text { get; internal set; }
        public int Id { get; internal set; }
        
        public List<int> Tasks;
        
        public List<int> DailyReports { get; internal set; }
        

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
    }
}