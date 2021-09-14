using System.Collections.Generic;

namespace DAL
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        int GetTeamleadId();
        void Update(int teamleadId);

        int GetQuantity();

        bool CheckIfExsits(int emloyeeId);
        
        
    }
}