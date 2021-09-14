using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using BLL;

namespace UIL
{
    public class EmployeeController : IEmployeeController
    {
        private IEmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }
        
        public int Add(Employee obj)
        {
            return _service.Add(Convert(obj));
        }

        public void ChangeLeader(int empId, int leadId)
        {
            _service.ChangeLeader(empId,leadId);
        }

        public Employee Convert(BLL.Employee obj)
        {
            return new Employee(obj.Id, obj.Name, obj.LeaderId, obj.SubordinatesId);
        }

        public void Show()
        {
            var employees = new Dictionary<int, Employee>();
            foreach (var emp in _service.GetAll())
            {
                employees.Add(emp.Id, Convert(emp));
            }
            var tl = Convert(_service.GetTeamlead());
            tl.Show(employees);
            Console.WriteLine();
        }
        public BLL.Employee Convert(Employee obj)
        {
            return new BLL.Employee(obj.Id, obj.Name, obj.LeaderId, obj.SubordinatesId);
        }
    }
}