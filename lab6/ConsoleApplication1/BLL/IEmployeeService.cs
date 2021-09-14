using System.Collections.Generic;

namespace BLL
{
    public interface IEmployeeService : IService<Employee, DAL.Employee>
    {
        List<int> GetSuboridnates(int employeeId);
        bool CheckIfEmployeeExsits(int emloyeeId);
        int GetLeaderId(int employeeId);
        void GetSubordinates(Employee emp, List<int> subs);
        int GetTeadLeadId();
        void ChangeLeader(int empId, int leadId);
        Employee GetTeamlead();
    }
}