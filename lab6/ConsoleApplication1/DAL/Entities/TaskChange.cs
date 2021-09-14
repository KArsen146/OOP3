using System;
using ChangeDate;

namespace DAL
{
    public class TaskChange : IEntity
    {
        public int Id { get; internal set; }

        public int TaskId { get; internal set; }

        public string Change { get; protected set; }
        
        public DateTime ChangeDate { get; }

        public int EmployeeId;

        public TaskChange(int taskId, int employeeId, string change, DateTime date, int id=0)
        {
            TaskId = taskId;
            Change = change;
            EmployeeId = employeeId;
            ChangeDate = date;
            Id = id;
        }
    }
}