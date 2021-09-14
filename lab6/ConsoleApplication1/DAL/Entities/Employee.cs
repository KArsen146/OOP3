using System.Collections.Generic;

namespace DAL
{
    public class Employee : IEntity
    {
        public Employee(int id, string name, int leaderId, List<int> subid)
        {
            Id = id;
            Name = name;
            LeaderId = leaderId;
            SubordinatesId = subid != null ?  new List<int>(subid) : new List<int>();
        }
        
        public int Id { get; internal set; }
        public string Name { get; }
        public int LeaderId { get; }
        
        public List<int> SubordinatesId { get; }
    }
}