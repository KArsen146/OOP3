using System.Collections.Generic;

namespace DAL
{
    public interface ITaskRepository : IRepository<Task>
    {
        List<Task> GetEmployeeTask(int employeeId);
    }
}