using System.Collections.Generic;

namespace BLL
{
    public class Employee : IEntity
    {
        public int Id { get; internal set; }
        public Employee(int id, string name, int leaderId, List<int> isub = null)
        {
            Id = id;
            Name = name;
            LeaderId = leaderId;
            SubordinatesId = isub != null ?  new List<int>(isub) : new List<int>();
        }

        public void AddChild(int id)
        {
            SubordinatesId.Add(id);
        }
        public string Name;
        public int LeaderId;
        public List<int> SubordinatesId { get; }
        
    }
}