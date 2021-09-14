using System;
using System.Collections.Generic;

namespace BLL
{
    public class CommandReport
    {
        public int Id { get; internal set; }
        public List<int> _sprintreports { get; }
        public string Text { get; protected set; }
        public DateTime Date;

        public CommandReport(int id, List<int> sprintreports, string text, DateTime date)
        {
            Id = id;
            _sprintreports = sprintreports;
            Text = text;
            Date = date;
        }

        public void AddText(string text)
        {
            Text += text;
        }

        public void ResetText(string text)
        {
            Text = text;
        }

        public void Add(Report r)
        {
            _sprintreports.Add(r.Id);
        }
    }
}