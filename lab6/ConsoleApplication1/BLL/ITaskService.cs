using System;
using System.Collections.Generic;

namespace BLL
{
    public interface ITaskService : IService<Task, DAL.Task>
    {
        void AddComment(int employeeId, int taskId, string comment);
        void MakeActive(int employeeId, int taskId);
        void MakeResolved(int employeeId, int taskId);
        Task Get(int id);
        void AppointTask(int taskId, int leaderId, int employeeId);
        IEnumerable<Task> GetEmployeeTask(int emloyeeId, bool t = true);
        
        IEnumerable<Task> GetEmployeeResolvedTask(int emloyeeId);
        IEnumerable<Task> GetSubordinatesTasks(int employeeId);
        IEnumerable<Task> FindTaskByCreationDate(DateTime date);
        IEnumerable<Task> FindTaskByLastChangeDate(DateTime date);
        IEnumerable<Task> FindTaskByEmployeeChange(int employeeId);
        bool CheckIfTaskExists(int taskId);
    }
}