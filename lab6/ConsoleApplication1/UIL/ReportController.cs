using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BLL;
using ChangeDate;

namespace UIL
{
    public class ReportController : IReportController
    {
        private IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }
       
        public int Add(Report obj)
        {
            return _service.Add(Convert(obj));
        }

        public void StartSprint()
        {
            _service.StartSprint();
        }

        public int Add(CommandReport entity)
        {
            return _service.Add(Convert(entity));
        }

        public void AddText(int reportId, string text)
        {
            _service.AddText(reportId,text);
        }

        public void AddCommandText(int employeeId, string text)
        {
            _service.AddCommandText(employeeId, text);
        }

        public void ResetText(int reportId, string text = null)
        {
            _service.ResetText(reportId, text);
        }

        public void ResetCommandText(int employeeId, string text)
        {
            _service.ResetCommandText(employeeId, text);
        }

        public void AddTask(int reportId, int taskId)
        {
            _service.AddTask(reportId, taskId);
        }

        public void AddDailyReport(int reportId, int addedReportId)
        {
            _service.AddDailyReport(reportId,addedReportId);
        }

        public void AddSprintReport(int employeeId, int reportId)
        {
            _service.AddSprintReport(employeeId,reportId);
        }

        public void GetDailyReports()
        {
            var reports = _service.GetReports(ReportType.Daily);
            foreach (var report in reports)
            {
                var rep = Convert(report);
                rep.Show();
            }
        }
        public void GetSprintReports()
        {
            var reports = _service.GetReports(ReportType.Sprint);
            foreach (var report in reports)
            {
                var rep = Convert(report);
                rep.Show();
            }
        }
        

        public void AddAllTasks(int reportId)
        {
            _service.AddAllTasks(reportId);
        }

        public void AddAllDailyReports(int reportId)
        {
            _service.AddAllDailyReports(reportId);
        }

        public void GetEmployeeDailyReports(int employeeId)
        {
            var emprep = _service.GetEmployeeDailyReports(employeeId);
            Console.WriteLine($"Daily Reports Id by {employeeId}");
            foreach (var rep in emprep)
            {
                Console.Write($"{rep} ");
            }
        }

        public void FinishSprint(int employeeId)
        {
            var command = Convert(_service.FinishSprint(employeeId));
            command.Show();
        }

        public void CheckReportState(int employeeId, int reportId)
        {
            Console.WriteLine($"State of {reportId} is {_service.CheckReportState(employeeId,reportId)}");
        }

        public BLL.CommandReport Convert(CommandReport obj)
        {
            return new BLL.CommandReport(obj.Id, obj._sprintreports, obj.Text, obj.Date);
        }

        public CommandReport Convert(BLL.CommandReport obj)
        {
            return new CommandReport(obj.Id, obj._sprintreports, obj.Text, obj.Date);
        }

        public void FinishReport(int employeeId, int reportId)
        {
            _service.FinishReport(employeeId,reportId);
        }

        public Report Convert(BLL.Report obj)
        {
            return new Report(obj.Id, obj.Date,obj.EmployeeId, obj.Type, obj.State, obj.Text, obj.Tasks, obj.DailyReports);
        }

        public BLL.Report Convert(Report obj)
        {
            return new BLL.Report(obj.Id, obj.Date,obj.EmployeeId, obj.Type, obj.State, obj.Text, obj.Tasks, obj.DailyReports);
        }

    }
}