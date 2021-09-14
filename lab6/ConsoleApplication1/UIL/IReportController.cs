using System.Collections.Generic;
using ChangeDate;

namespace UIL
{
    public interface IReportController : IController<Report, BLL.Report>
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
        void AddAllTasks(int reportId);
        void AddAllDailyReports(int reportId);
        void GetEmployeeDailyReports(int employeeId);
        void FinishSprint(int employeeId);
        void CheckReportState(int employeeId, int reportId);
        BLL.CommandReport Convert(CommandReport obj);
        CommandReport Convert(BLL.CommandReport obj);
        void FinishReport(int employeeId, int reportId);
    }
}