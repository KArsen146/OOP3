using System;
using System.Collections.Generic;

namespace DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static int _id;
        public EmployeeRepository()
        {
            _employees = new Dictionary<int, Employee>();
        }
        
        private Dictionary<int, Employee> _employees;

        private int _teamleadId;
        
        public IEnumerable<Employee> GetAll()
        {
            var employees = new List<Employee>();
            foreach (var employee in _employees.Values)
            {
                employees.Add(employee);
            }
            return employees;
        }

        public Employee Get(int id)
        {
            if (!CheckIfExsits(id))
                throw new Exception("No such leader");
            return _employees[id];
        }

        public int Create(Employee entity)
        {
            entity.Id = ++_id;
            _employees.Add(entity.Id, entity);
            return entity.Id;
        }

        public void Update(int teamleadId)
        {
            _teamleadId = teamleadId;
        }

        public void Update(Employee entity)
        {
            _employees[entity.Id] = entity;
        }
        

        public void Delete(int id)
        {
            if (!CheckIfExsits(id))
                _employees.Remove(id);
        }

        public int GetTeamleadId()
        {
            return _teamleadId;
        }

        public int GetQuantity()
        {
            return _employees.Count;
        }

        public bool CheckIfExsits(int emloyeeId)
        {
            return _employees.ContainsKey(emloyeeId);
        }
        
    }
}