using System;
using System.Collections.Generic;

namespace DAL
{
    public class ReportRepository : IReportRepository
    {
        private Dictionary<int, Report> _reports;

        private Dictionary<int, Report> _closedReports;

        private CommandReport _commandReport;

        private static int _commandrepid;

        private Dictionary<int, CommandReport> _closedCommandReport;

        private static int _id;

        public ReportRepository()
        {
            _reports = new Dictionary<int, Report>();
            _closedCommandReport = new Dictionary<int, CommandReport>();
            _closedReports = new Dictionary<int, Report>();
        }
        public IEnumerable<Report> GetAll()
        {
            var reports = new List<Report>();
            foreach (var report in _reports.Values)
                reports.Add(report);
            return reports;
        }

        public void Reset()
        {
            _closedCommandReport.Add(_commandReport.Id, _commandReport);
            foreach (var rep in _reports)
                _closedReports.Add(rep.Key, rep.Value);
            _reports = new Dictionary<int, Report>();
            _commandReport = null;
        }
        
        public Report Get(int ind)
        {
            if (!CheckIfExsits(ind))
                throw new Exception("No report with this number");
            return _reports[ind];
        }

        public CommandReport GetCommandReport()
        {
            return _commandReport;
        }
        public int Create(Report entity)
        {
            entity.Id = ++_id;
            _reports.Add(entity.Id, entity);
            return entity.Id;
        }

        public int Create(CommandReport entity)
        {
            entity.Id = ++_commandrepid;
            _commandReport = entity;
            return entity.Id;
        }

        public void Update(Report entity)
        {
            _reports[entity.Id] = entity;
        }

        public void Update(CommandReport entity)
        {
            _commandReport = entity;
        }
        public void Delete(int id)
        {
            _reports.Remove(id);
        }

        public bool CheckIfExsits(int Id)
        {
            return _reports.ContainsKey(Id);
        }
    }
}