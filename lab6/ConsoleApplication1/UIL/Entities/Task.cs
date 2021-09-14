using System;
using System.Collections.Generic;
using ChangeDate;

namespace UIL
{
    public class Task
    {
        public Task(int id, string name, string text, _State state, List<string> comments, DateTime date, DateTime cldate, int employeeId = 0)
        {
            Id = id;
            Name = name;
            State = state;
            EmployeeId = employeeId;
            Text = text;
            Comments = new List<string>(comments);
            CreationDate = date;
            ClosingDate = cldate;
        }

        public Task(string name, string text)
        {
            Id = 0;
            Name = name;
            State = _State.Open;
            EmployeeId = 0;
            Text = text;
            Comments = new List<string>();
            CreationDate = Date.MyDate;
        }
        public int Id { get; internal set; }

        public string Name { get; }

        public string Text { get; internal set; }
        
        public List<string> Comments { get; internal set; }
        

        public _State State { get; protected set; }
        
        public int EmployeeId;

        public DateTime CreationDate;
        
        public DateTime ClosingDate;

        public void Show()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Creation date: {CreationDate}");
            Console.WriteLine($"Text: {Text}");
            Console.WriteLine(EmployeeId == 0 ? "No appointed employee" : $"{EmployeeId} is appointed");
            Console.WriteLine($"State: {State}");
            Console.WriteLine("Comments:");
            foreach (var comment in Comments)
            {
                Console.WriteLine($"{comment}");
            }
            Console.WriteLine();
        }
    }
}