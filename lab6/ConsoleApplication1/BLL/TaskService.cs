using System;
using System.Collections.Generic;
using ChangeDate;
using DAL;

namespace BLL
{
    public class TaskService :  ITaskService
    {
        private ITaskRepository _taskRepository;

        private IEmployeeService _employeeService;

        private ITaskChangeRepository _taskChangeRepository;
        public TaskService(ITaskRepository rep1, IEmployeeService rep2, ITaskChangeRepository rep3)
        {
            _taskRepository = rep1;
            _employeeService = rep2;
            _taskChangeRepository = rep3;
        }
        
        public int Add(Task entity)
        {
            return _taskRepository.Create(Convert(entity));
        }

        public void AddComment(int employeeId, int taskId, string comment)
        {
            var task = Convert(_taskRepository.Get(taskId));
            task.AddComment(comment);
            _taskRepository.Update(Convert(task));
            _taskChangeRepository.Create(Convert (new TaskChange(taskId, employeeId, "Added comment", Date.MyDate)));
        }

        public void MakeActive(int employeeId, int taskId)
        {
            var task = Convert(_taskRepository.Get(taskId));
            if (task.EmployeeId != employeeId)
                throw new Exception("It is not your task");
            if (task.State != _State.Open)
                throw new Exception("Task is not Open");
            task.ChangeState(_State.Active);
            _taskRepository.Update(Convert(task));
            _taskChangeRepository.Create(Convert(new TaskChange(taskId, employeeId, "Task made Active", task.CreationDate)));
        }

        public void MakeResolved(int employeeId, int taskId)
        {
            var task = Convert(_taskRepository.Get(taskId));
            if (task.EmployeeId != employeeId)
                throw new Exception("It is not your task");
            if (task.State != _State.Active)
                throw new Exception("Task is not Active");
            task.ChangeState(_State.Resolved);
            task.ClosingDate = Date.MyDate;
            _taskRepository.Update(Convert(task));
            _taskChangeRepository.Create(Convert(new TaskChange(taskId, employeeId, "Task made Resolved", task.CreationDate)));
        }
        
        public Task Get(int id)
        {
            return Convert(_taskRepository.Get(id));
        }

        public void AppointTask(int taskId, int leaderId, int employeeId)
        {
            var task = Convert(_taskRepository.Get(taskId));
            if (!_employeeService.GetSuboridnates(leaderId).Contains(employeeId))
                throw new Exception("You are not a leader of this employee");
            task.AddEmployee(employeeId);
            _taskRepository.Update(Convert(task));
            _taskChangeRepository.Create(Convert(new TaskChange(taskId, employeeId, $"{employeeId} appointed to the task", task.CreationDate)));
        }

        public IEnumerable<Task> GetEmployeeTask(int emloyeeId, bool t = true)
        {
            if(!_employeeService.CheckIfEmployeeExsits(emloyeeId))
                throw new Exception("No employee with this id");
            var tasks = new List<Task>();
            foreach (var task in _taskRepository.GetEmployeeTask(emloyeeId))
            {
                var Task = Convert(task);
                if ((t) || (Task.State==_State.Resolved))
                    tasks.Add(Task);
            }
            return tasks;
        }

        public IEnumerable<Task> GetEmployeeResolvedTask(int emloyeeId)
        {
            return GetEmployeeTask(emloyeeId, false);
        }
        public IEnumerable<Task> GetSubordinatesTasks(int employeeId)
        {
            var tasks = new List<Task>();
            var subs = _employeeService.GetSuboridnates(employeeId);
            var allTasks = Convert(GetAll());
            foreach (var sub in subs)
            {
                if (allTasks.ContainsKey(sub))
                {
                    foreach (var task in allTasks[sub])
                    {
                        tasks.Add(task);
                    }
                }
            }
            return tasks; 
        }
        
        public IEnumerable<Task> GetAll()
        {
            var dalTasks = _taskRepository.GetAll();
            var tasks = new List<Task>();
            foreach (var task in dalTasks)
            {
                tasks.Add(Convert(task));
            }

            return tasks;
        }

        public DAL.Task Convert(Task obj)
        {
            return new DAL.Task(obj.Id, obj.Name, obj.Text, obj.State, obj.Comments, obj.CreationDate, obj.ClosingDate, obj.EmployeeId);
        }

        public Task Convert(DAL.Task obj)
        {
            return new Task(obj.Id, obj.Name, obj.Text, obj.State, obj.Comments, obj.CreationDate, obj.ClosingDate,  obj.EmployeeId);
        }

        public TaskChange Convert(DAL.TaskChange obj)
        {
            return new TaskChange(obj.TaskId, obj.EmployeeId, obj.Change, obj.ChangeDate);
        }
        
        public DAL.TaskChange Convert(TaskChange obj)
        {
            return new DAL.TaskChange(obj.TaskId, obj.EmployeeId, obj.Change, obj.ChangeDate);
        }

        public Dictionary<int, List<Task>> Convert(IEnumerable<Task> tasks)
        {
            var bllTasks = new Dictionary<int, List<Task>>();
            foreach (var task in tasks)
            {
                if (!bllTasks.ContainsKey(task.EmployeeId))
                    bllTasks.Add(task.EmployeeId, new List<Task>());
                bllTasks[task.EmployeeId].Add(task);
            }
            return bllTasks;
        }

        public IEnumerable<Task> FindTaskByCreationDate(DateTime date)
        {
            var DALtasks = _taskRepository.GetAll();
            var tasks = new List<Task>();
            foreach (var DALtask in DALtasks)
            {
                var task = Convert(DALtask);
                if (task.CreationDate == date)
                    tasks.Add(task);
            }

            return tasks;
        }

        public IEnumerable<Task> FindTaskByLastChangeDate(DateTime date)
        {
            var dalTaskChanges = _taskChangeRepository.GetAll();
            var tasks = new List<Task>();
            var tasksId = new List<int>();
            foreach (var dalTaskChange in dalTaskChanges)
            {
                var taskchange = Convert(dalTaskChange);
                if (!tasksId.Contains(taskchange.TaskId) && taskchange.ChangeDate == date)
                    tasksId.Add(taskchange.TaskId);
            }
            foreach (var taskId in tasksId)
                tasks.Add(Convert(_taskRepository.Get(taskId)));
            return tasks;
        }

        public IEnumerable<Task> FindTaskByEmployeeChange(int employeeId)
        {
            var dalTaskChanges = _taskChangeRepository.GetAll();
            var tasks = new List<Task>();
            var tasksId = new List<int>();
            foreach (var dalTaskChange in dalTaskChanges)
            {
                var taskchange = Convert(dalTaskChange);
                if (!tasksId.Contains(taskchange.TaskId) && taskchange.EmployeeId == employeeId)
                    tasksId.Add(taskchange.TaskId);
            }
            foreach (var taskId in tasksId)
                tasks.Add(Convert(_taskRepository.Get(taskId)));
            return tasks;
        }

        public bool CheckIfTaskExists(int taskId)
        {
            return _taskRepository.CheckIfExsits(taskId);
        }
        
        public void NewDay()
        {
            
        }
    }
}