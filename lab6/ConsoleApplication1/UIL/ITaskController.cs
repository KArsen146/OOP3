using System;
using System.Collections.Generic;
using ChangeDate;

namespace UIL
{
    public interface ITaskController
    {
        void AddComment(int employeeId, int taskId, string comment);
        void MakeActive(int employeeId, int taskId);
        void MakeResolved(int employeeId, int taskId);
        void Get(int id);
        void AppointTask(int taskId, int leaderId, int employeeId);
        void GetEmployeeTask(int employeeId);
        void GetSubordinatesTasks(int employeeId);
        void FindTaskByCreationDate(DateTime date);
        void FindTaskByLastChangeDate(DateTime date);
        void FindTaskByEmployeeChange(int employeeId);
        
    }
}