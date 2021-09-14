using System;
using BLL;
using ChangeDate;

namespace UIL
{
    public class TaskController : IController<Task, BLL.Task>, ITaskController
    {
        private ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        public Task Convert(BLL.Task obj)
        {
            return new Task(obj.Id, obj.Name, obj.Text, obj.State, obj.Comments, obj.CreationDate, obj.ClosingDate, obj.EmployeeId);
        }

        public BLL.Task Convert(Task obj)
        {
            return new BLL.Task(obj.Id, obj.Name, obj.Text, obj.State, obj.Comments, obj.CreationDate, obj.ClosingDate,  obj.EmployeeId);
        }

        public int Add(Task obj)
        {
            return _taskService.Add(Convert(obj));
        }

        public void AddComment(int employeeId, int taskId, string comment)
        {
            _taskService.AddComment(employeeId, taskId, comment);
        }
        

        public void MakeActive(int employeeId, int taskId)
        {
            _taskService.MakeActive(employeeId, taskId);
        }

        public void MakeResolved(int employeeId, int taskId)
        {
            _taskService.MakeResolved(employeeId, taskId);
        }

        public void Get(int id)
        {
            Convert(_taskService.Get(id)).Show();
        }

        public void AppointTask(int taskId, int leaderId, int employeeId)
        {
            _taskService.AppointTask(taskId, leaderId,employeeId);
        }

        public void GetEmployeeTask(int employeeId)
        {
            var BLLtasks = _taskService.GetEmployeeTask(employeeId);
            foreach (var blLtask in BLLtasks)
            {
                Convert(blLtask).Show();
            }
        }

        public void GetSubordinatesTasks(int employeeId)
        {
            var BLLtasks = _taskService.GetSubordinatesTasks(employeeId);
            foreach (var blLtask in BLLtasks)
            {
                Convert(blLtask).Show();
            }
        }

        public void FindTaskByCreationDate(DateTime date)
        {
            var BLLtasks = _taskService.FindTaskByCreationDate(date);
            foreach (var blLtask in BLLtasks)
            {
                Convert(blLtask).Show();
            }
        }

        public void FindTaskByLastChangeDate(DateTime date)
        {
            var BLLtasks = _taskService.FindTaskByLastChangeDate(date);
            foreach (var blLtask in BLLtasks)
            {
                Convert(blLtask).Show();
            }
        }

        public void FindTaskByEmployeeChange(int employeeId)
        {
            var BLLtasks = _taskService.FindTaskByEmployeeChange(employeeId);
            foreach (var blLtask in BLLtasks)
            {
                Convert(blLtask).Show();
            }
        }

        public void FindTodayResolvedTask(int employeeId)
        {
            var BLLtasks = _taskService.GetEmployeeResolvedTask(employeeId);
            foreach (var blLtask in BLLtasks)
            {
                var task = Convert(blLtask);
                if (task.ClosingDate == Date.MyDate)
                    task.Show();
            }
        }
        
    }
}