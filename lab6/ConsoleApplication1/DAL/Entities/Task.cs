using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using ChangeDate;

namespace DAL
{
    public class Task : IEntity
    {
        public Task(int id, string name, string text, _State state, List<string> comments, DateTime date, DateTime cldate,int employeeId = 0)
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
        public int Id { get; internal set; }

        public string Name { get; }

        public string Text { get; internal set; }
        
        public List<string> Comments { get; internal set; }
        public _State State { get; protected set; }
        
        public int EmployeeId;

        public DateTime CreationDate;

        public DateTime ClosingDate;
    }
}