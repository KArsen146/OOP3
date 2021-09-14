using System;
using System.Collections.Generic;

namespace UIL
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

        public CommandReport(string text=""):this(0, new List<int>(), text, ChangeDate.Date.MyDate )
        {
            
        }

        public void Show()
        {
            Console.WriteLine("Отчет за спринт");
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Started Date: {Date}");
            Console.WriteLine($"Text : {Text}");
            Console.WriteLine("Список id отчетов");
            foreach (var rep in _sprintreports)
            {
                Console.Write($"{rep} ");
            }
        }
    }
}