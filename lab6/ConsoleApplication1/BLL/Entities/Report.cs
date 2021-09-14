using System;
using System.Collections.Generic;
using ChangeDate;

namespace BLL
{
    public class Report
    {
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
            set
            {
                state = value;
            }
        }

        public void ChangeState()
        {
            state = ReportState.Closed;
        }
        public string Text { get; protected set; }
        public int Id { get; internal set; }
        
        public List<int> Tasks { get; internal set; }
        
        public List<int> DailyReports { get; internal set; }

        public void AddTask(int Id)
        {
            Tasks.Add(Id);
        }

        public void AddReport(int Id)
        {
            DailyReports.Add(Id);
        }

        public void Close()
        {
            state = ReportState.Closed;
        }
        
        public void AddText(string text)
        {
            Text += text;
        }

        public void ResetText(string text)
        {
            Text = text;
        }
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