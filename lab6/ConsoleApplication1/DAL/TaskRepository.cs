using System;
using System.Collections.Generic;

namespace DAL
{
    public class TaskRepository : ITaskRepository
    {
        Dictionary<int, Task> _tasks;

        private Dictionary<int, List<string>> _changes;

        private static int _id;

        public TaskRepository()
        {
            _tasks= new Dictionary<int, Task>();
        }
        public IEnumerable<Task> GetAll()
        {    
            var tasks = new List<Task>();
            foreach (var task in _tasks.Values)
            {
                tasks.Add(task);
            }
            return tasks;
        }
        
        public Task Get(int ind)
        {
            if (!_tasks.ContainsKey(ind))
                throw new Exception("No tasks with this number");
            return _tasks[ind];
        }

        public int Create(Task entity)
        {
            entity.Id = ++_id;
            _tasks.Add(entity.Id, entity);
            return entity.Id;
        }

        public void Update(Task entity) 
        {
            if ( !_tasks.ContainsKey(entity.Id))
                throw new Exception("No tasks with this number");
            _tasks[entity.Id] = entity;
        }

        public void Delete(int id)
        {
            if (_tasks.ContainsKey(id))
                _tasks.Remove(id);
        }

        public bool CheckIfExsits(int Id)
        {
            return _changes.ContainsKey(Id);
        }

        public List<Task> GetEmployeeTask(int employeeId)
        {
            var tasks = new List<Task>();
            foreach (var task in _tasks.Values)
            {
                if (task.EmployeeId == employeeId)
                    tasks.Add(task);
            }

            return tasks;
        }
    }
}