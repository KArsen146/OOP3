using System;
using System.Collections.Generic;

namespace DAL
{
    public class TaskChangesRepository : ITaskChangeRepository
    {
        private Dictionary<int,TaskChange> _changes;

        private int _id;

        public TaskChangesRepository()
        {
            _changes = new Dictionary<int, TaskChange>();
        }
        public IEnumerable<TaskChange> GetAll()
        {
            var changes = new List<TaskChange>();
            foreach (var change in _changes.Values)
            {
                changes.Add(change);
            }
            return changes;
        }

        public TaskChange Get(int ind)
        {
            if (!CheckIfExsits(ind))
                throw new Exception("No change with this number");
            return _changes[ind];
        }

        public int Create(TaskChange entity)
        {
            entity.Id = ++_id;
            _changes.Add(entity.Id, entity);
            return entity.Id;
        }

        public void Update(TaskChange entity)
        {
            _changes[entity.Id] = entity;
        }

        public void Delete(int id)
        {
            _changes.Remove(id);
        }

        public bool CheckIfExsits(int Id)
        {
            return _changes.ContainsKey(Id);
        }
    }
}