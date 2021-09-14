using System;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class EmployeeService :  IEmployeeService
    {
        private IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        
        public int Add(Employee entity)
        {
            var id= _repository.Create(Convert(entity));
            if (entity.LeaderId != 0)
            {
                var obj = Convert(_repository.Get(entity.LeaderId));
                obj.AddChild(id);
                _repository.Update(Convert(obj));
            }
            else
            {
                if (_repository.GetQuantity() != 1)
                    throw new Exception("Teamlead is already exist");
                _repository.Update(id);
            }
            return id;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = _repository.GetAll();
            var BLLemployees = new List<Employee>();
            foreach (var emp in employees)
            {
                BLLemployees.Add(Convert(emp));
            }

            return BLLemployees;
        }

        public void ChangeLeader(int empId, int leadId)
        {
            if (empId == _repository.GetTeamleadId())
                throw new Exception("You can't change leader of Teamlead");
            var emp = Convert(_repository.Get(empId));
            var lead = Convert(_repository.Get(leadId));
            var prevlead = Convert(_repository.Get(emp.LeaderId));
            prevlead.SubordinatesId.Remove(empId);
            _repository.Update(Convert(prevlead));
            lead.AddChild(empId);
            _repository.Update(Convert(lead));
            emp.LeaderId = leadId;
            _repository.Update(Convert(emp));
        }

        public DAL.Employee Convert(Employee obj)
        {
            return new DAL.Employee(obj.Id, obj.Name, obj.LeaderId, obj.SubordinatesId);
        }

        public Employee GetTeamlead()
        {
            var tlId = _repository.GetTeamleadId();
            if (tlId == 0)
                throw new Exception("No employees");
            return Convert(_repository.Get(tlId));
        }

        public int GetTeadLeadId()
        {
            return _repository.GetTeamleadId();
        }

        public Employee Convert(DAL.Employee obj)
        {
            return new Employee(obj.Id, obj.Name, obj.LeaderId, obj.SubordinatesId);
        }
        
        public List<int> GetSuboridnates(int employeeId)
        {
            var subs = new List<int>();
            var emp = Convert(_repository.Get(employeeId));
            GetSubordinates(emp, subs);
            return subs;
        }

        public bool CheckIfEmployeeExsits(int emloyeeId)
        {
            return _repository.CheckIfExsits(emloyeeId);
        }
        
        public void GetSubordinates(Employee emp, List<int> subs)
        {
            foreach (var employeeId in emp.SubordinatesId)
            {
                subs.Add(employeeId);
                var sub = Convert(_repository.Get(employeeId));
                GetSubordinates(sub, subs);
            }
        }

        public int GetLeaderId(int employeeId)
        {
            var emp = Convert(_repository.Get(employeeId));
            return emp.LeaderId;
        }
        
    }
}