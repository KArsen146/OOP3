using System;
using System.Collections.Generic;

namespace DAL
{
    public class CommandReport : IEntity
    {
        public int Id { get; internal set; }
        public List<int> _sprintreports { get; }
        public string Text { get; }
        public DateTime Date;

        public CommandReport(int id, List<int> sprintreports, string text, DateTime date)
        {
            Id = id;
            _sprintreports = sprintreports;
            Text = text;
            Date = date;
        }
        
    }
}