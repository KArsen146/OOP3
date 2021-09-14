using System.Collections.Generic;
using ChangeDate;

namespace BLL
{
    public interface IReportService : IService<Report, DAL.Report>
    {
        void StartSprint();
        int Add(CommandReport entity);
        void AddText(int reportId, string text);
        void AddCommandText(int employeeId, string text);
        void ResetText(int reportId, string text = null);
        void ResetCommandText(int employeeId, string text);
        void AddTask(int reportId, int taskId);
        void AddDailyReport(int reportId, int addedReportId);
        void AddSprintReport(int employeeId, int reportId);
        IEnumerable<Report> GetReports(ReportType type);
        void AddAllTasks(int reportId);
        void AddAllDailyReports(int reportId);
        List<int> GetEmployeeDailyReports(int employeeId);
        CommandReport FinishSprint(int employeeId);
        DAL.CommandReport Convert(CommandReport obj);
        CommandReport Convert(DAL.CommandReport obj);
        ReportState CheckReportState(int employeeId, int reportId);
        void FinishReport(int employeeId, int reportId);
    }
}