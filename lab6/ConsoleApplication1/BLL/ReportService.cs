using System;
using System.Collections.Generic;
using ChangeDate;
using DAL;

namespace BLL
{
    public class ReportService : IReportService
    {
        private IReportRepository _reportRepository;

        private ITaskService _taskService;

        private IEmployeeService _employeeService;

        public ReportService(IReportRepository repository, ITaskService taskService, IEmployeeService employeeService)
        {
            _reportRepository = repository;
            _taskService = taskService;
            _employeeService = employeeService;

        }

        public void StartSprint()
        {
            var employees = _employeeService.GetSuboridnates(_employeeService.GetTeadLeadId());
            foreach (var emp in employees)
            {
                Add(new Report(0, Date.MyDate, emp, ReportType.Sprint, ReportState.Open, "", new List<int>(), new List<int>()));
            }

            Add(new CommandReport(0, new List<int>(), "", Date.MyDate));
        }
        public int Add(Report entity)
        {
            return _reportRepository.Create(Convert(entity));
        }

        public int Add(CommandReport entity)
        {
            return _reportRepository.Create(Convert(entity));
        }

        public IEnumerable<Report> GetAll()
        {
            var reports = new List<Report>();
            foreach (var dalReport in _reportRepository.GetAll())
            {
                reports.Add(Convert(dalReport));
            }

            return reports;
        }
        
        public void AddText(int reportId, string text)
        {
            var report = Convert(_reportRepository.Get(reportId));
            if (report.State==ReportState.Closed)
                throw new Exception("Report is closed");
            report.AddText(text);
            _reportRepository.Update(Convert(report));
        }

        public void AddCommandText(int employeeId, string text)
        {
            if (employeeId != _employeeService.GetTeadLeadId())
                throw new Exception("Only teamlead could edit commmand report");
            var commandrep = Convert(_reportRepository.GetCommandReport());
            commandrep.AddText(text);
            _reportRepository.Update(Convert(commandrep));
        }
        public void ResetText(int reportId, string text = null)
        {
            var report = Convert(_reportRepository.Get(reportId));
            if (report.State==ReportState.Closed)
                throw new Exception("Report is closed");
            report.ResetText(text);
            _reportRepository.Update(Convert(report));
        }
        public void ResetCommandText(int employeeId, string text)
        {
            if (employeeId != _employeeService.GetTeadLeadId())
                throw new Exception("Only teamlead could edit commmand report");
            var commandrep = Convert(_reportRepository.GetCommandReport());
            commandrep.ResetText(text);
            _reportRepository.Update(Convert(commandrep));
        }
        
        public void AddTask(int reportId, int taskId)
        {
            var report = Convert(_reportRepository.Get(reportId));
            if (report.State==ReportState.Closed)
                throw new Exception("Report is closed");
            if (report.Type != ReportType.Daily)
                throw new Exception("It is not sprint report");
            if(!_taskService.CheckIfTaskExists(taskId))
                throw new Exception("No task with this id");
            report.AddTask(taskId);
            _reportRepository.Update(Convert(report));
        }
        
        public void AddDailyReport(int reportId, int addedReportId)
        {
            var report = Convert(_reportRepository.Get(reportId));
            if (report.State==ReportState.Closed)
                throw new Exception("Report is closed");
            if (report.Type != ReportType.Daily)
                throw new Exception("It is not sprint report");
            var addedReport = Convert(_reportRepository.Get(addedReportId));
            if (addedReport.Type!= ReportType.Daily)
                throw new Exception("You need to add sprint report");
            report.AddReport(addedReportId);
            _reportRepository.Update(Convert(report));
        }

        public void AddSprintReport(int employeeId, int reportId)
        {
            if (employeeId != _employeeService.GetTeadLeadId())
                throw new Exception("Only teamlead could edit commmand report");
            var report = Convert(_reportRepository.Get(reportId));
            if (report.Type!=ReportType.Sprint)
                throw new Exception("It is not sprint report");
            if (report.State==ReportState.Open)
                throw new Exception("Sprint is open");
            var command = Convert(_reportRepository.GetCommandReport());
            command.Add(report);
            _reportRepository.Update(Convert(command));
        }
        
        public IEnumerable<Report> GetReports(ReportType type)
        {
            var reports = new List<Report>();
            foreach (var dalReport in _reportRepository.GetAll())
                if (Convert(dalReport).Type == type)
                    reports.Add(Convert(dalReport));
            return reports;
        }
        
        public void AddAllTasks(int reportId)
        {
            var report = Convert(_reportRepository.Get(reportId));
            if (report.State==ReportState.Closed)
                throw new Exception("Report is closed");
            foreach (var task in _taskService.GetEmployeeResolvedTask(report.EmployeeId))
            {
                if (task.ClosingDate == Date.MyDate)
                    report.AddTask(task.Id);
            }
            _reportRepository.Update(Convert(report));
        }

        public void AddAllDailyReports(int reportId)
        {
            var report = Convert(_reportRepository.Get(reportId));
            if (report.State==ReportState.Closed)
                throw new Exception("Report is closed");
            foreach (var rep in GetEmployeeDailyReports(report.EmployeeId))
            {
                report.AddReport(rep);
            }
            _reportRepository.Update(Convert(report));
        }
        public List<int> GetEmployeeDailyReports(int employeeId)
        {
            var reports = new List<int>();
            foreach (var rep in _reportRepository.GetAll())
            {
                var rep1 = Convert(rep);
                if (rep.EmployeeId == employeeId && rep1.Type==ReportType.Daily)
                    reports.Add(rep1.Id);
            }

            return reports;
        }
        
        public CommandReport FinishSprint(int employeeId)
        {
            if (employeeId != _employeeService.GetTeadLeadId())
                throw new Exception("Only teamlead could finish sprint");
            foreach (var rep in _reportRepository.GetAll())
            {
                var report = Convert(rep);
                report.ChangeState();
                _reportRepository.Update(Convert(report));
            }

            CommandReport commandReport = Convert(_reportRepository.GetCommandReport());
            _reportRepository.Reset();
            StartSprint();
            return commandReport;
        }

        public ReportState CheckReportState(int employeeId, int reportId)
        {
            if (employeeId != _employeeService.GetTeadLeadId())
                throw new Exception("Only teamlead could see report state");
            return Convert(_reportRepository.Get(reportId)).State;
        }

        public void FinishReport(int employeeId, int reportId)
        {
            var report = Convert(_reportRepository.Get(reportId));
            if (report.EmployeeId != employeeId)
                throw new Exception("It is not your report");
            report.ChangeState();
            _reportRepository.Update(Convert(report));
        }

        public DAL.Report Convert(Report obj)
        {
            return new DAL.Report(obj.Id, obj.Date, obj.EmployeeId, obj.Type, obj.State,obj.Text, obj.Tasks, obj.DailyReports);
        }

        public Report Convert(DAL.Report obj)
        {
            return new Report(obj.Id, obj.Date, obj.EmployeeId, obj.Type, obj.State,obj.Text, obj.Tasks, obj.DailyReports);
        }

        public DAL.CommandReport Convert(CommandReport obj)
        {
            return new DAL.CommandReport(obj.Id, obj._sprintreports, obj.Text, obj.Date);
        }
        
        public CommandReport Convert(DAL.CommandReport obj)
        {
            return new CommandReport(obj.Id, obj._sprintreports, obj.Text, obj.Date);
        }
    }
}