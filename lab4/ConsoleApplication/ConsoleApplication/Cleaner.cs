using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Cleaner
    {
        private List<CleanRestorePoints> _cleaners;
        
        private int _type;
        
        private bool _mode; // true - удаляем точку, если вышла хотя б за один лимит

        public Cleaner()
        {
            _type = -1;
            _cleaners = new List<CleanRestorePoints>();
            _mode = true;
        }
        
        public Cleaner(List<CleanRestorePoints> cleaners, bool mode = true)
        {
            _cleaners = new List<CleanRestorePoints>(cleaners);
            _mode = mode;
            _type = 4;
        }

        public void Add(List<CleanRestorePoints> cleaners, bool mode = true)
        {
            foreach (var cl in cleaners)
            {
                _cleaners.Add(cl);
            }
            _mode = mode;
            _type = 4;
        }
        public void ResetCleaners(List<CleanRestorePoints> cleaners, bool mode=true)
        {
            _cleaners.Clear();
            _cleaners = new List<CleanRestorePoints>(cleaners);
            _mode = mode;
            _type = 4;
        }

        public List<RestorePoint> Clean(List<RestorePoint> restorePoints)
        {
            if (_cleaners.Count == 0)
                return restorePoints;
            var newRp = new List<RestorePoint>(_cleaners[0].Clean(restorePoints));
            var cleaners = new List<CleanRestorePoints>();
            foreach (var cl in _cleaners)
            {
                var buf = cl.Clean(restorePoints);
                if (_mode && newRp.Count > buf.Count)
                    newRp = buf;
                if (!_mode && newRp.Count <= buf.Count)
                {
                    newRp = buf;
                    cleaners.Add(cl);
                }
            }

            bool countIncorrectPoints = false;
            try
            {
                if (_mode)
                    cleaners = _cleaners;
                foreach (var cl in cleaners)
                {
                    if (!cl.IsRightPoint(newRp, 0))
                    {
                        countIncorrectPoints = true;
                        break;
                    }
                }

                if (countIncorrectPoints)
                    throw new BackupException("There are some saved points, that get out of limits");
            }
            catch (BackupException e)
            {
                Console.WriteLine(e);
            }

            return newRp;
        }
    }
}