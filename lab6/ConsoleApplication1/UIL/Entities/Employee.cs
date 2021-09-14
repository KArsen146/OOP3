using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace UIL
{
    public class Employee
    {
        public int Id { get; internal set; }
        public Employee(int id, string name, int leaderId, List<int> isub = null)
        {
            Id = id;
            Name = name;
            LeaderId = leaderId;
            SubordinatesId = isub != null ?  new List<int>(isub) : new List<int>();
        }

        public Employee(string name, int leaderId, List<int> isub = null) : this(-1, name, leaderId, isub)
        {
        }
        
        public Employee(string name, int leaderId = 0) : this(name, leaderId, null)
        {
        }
        public string Name;
        public int LeaderId;
        public List<int> SubordinatesId { get; }

        public void Show(Dictionary<int, Employee> emps, int i = 0)
        {
            for(int ind=0; ind < i; ind++)
                Console.Write("-");
            Console.WriteLine($"{Name} {Id}");
            foreach (var emp in SubordinatesId)
            {
                emps[emp].Show(emps, i+1);
            }
        }
    }
}