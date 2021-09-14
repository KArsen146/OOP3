using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class Backup
    {
        private readonly Dictionary<string, DateTime> _files;

        private List<RestorePoint> _restorePoints;

        private readonly DateTime _date;
        
        private int _rpId;
        
        private Storage _storage;

        private readonly Cleaner _cleaner;

        private long _size;

        public Backup(List<string> filenames, List<CleanRestorePoints> cl = null)
        {
            _date = DateTime.Now;
            _files = new Dictionary<string, DateTime>();
            foreach (var file in filenames)
            {
                _files.Add(file, DateTime.Now);
                var finfo = new FileInfo(file);
                _size += finfo.Length;
            }
            _cleaner = cl == null ? new Cleaner() : new Cleaner(cl);
            _storage = new StorageSeparate();
            _restorePoints = new List<RestorePoint>();
            _rpId = 0;
        }
        public Backup() : this(new List<string>())
        {
        }
        
        public void ResetCleaner(CleanRestorePoints cleanRestorePoints)
        {
            ResetCleaner(new List<CleanRestorePoints> {cleanRestorePoints});
        }
        public void ResetCleaner(List<CleanRestorePoints> cleanRestorePoints, string mode = "DeleteIfOneLimit")
        {
            var modeBool = mode == "DeleteIfOneLimit";
            _cleaner.ResetCleaners(cleanRestorePoints, modeBool);
        }

        public void AddCleaner(CleanRestorePoints cleanRestorePoints)
        {
            AddCleaner(new List<CleanRestorePoints> {cleanRestorePoints});
        }

        public void AddCleaner(List<CleanRestorePoints> cleanRestorePoints, string mode = "DeleteIfOneLimit")
        {
            var modeBool = mode == "DeleteIfOneLimit";
            _cleaner.Add(cleanRestorePoints, modeBool);
            _restorePoints = _cleaner.Clean(_restorePoints);
        }
        
        
        public void Add(string file)
        {
            var finfo = new FileInfo(file);
            _size += finfo.Length;
            _files.Add(file, DateTime.Now);
        }

        public void Remove(string file)
        {
            var finfo = new FileInfo(file);
            _size -= finfo.Length;
            _files.Remove(file);
        }

        public void CreateRestorePoint(string type, Storage st = null)
        {
            try
            {
                _rpId++;
                if (st != null)
                    _storage = st;
                switch (type)
                {
                    case "Full":
                    {

                        var rp = _storage.CreatePoint(_files, _rpId);
                        _restorePoints.Add(rp);
                        break;
                    }
                    case "Incremental":
                    {
                        if (_restorePoints.Count == 0)
                            throw new BackupException("First point must be Full");
                        var rp = _storage.CreatePoint(_files, _rpId, _restorePoints.Last());
                        _restorePoints.Add(rp);
                        break;
                    }
                    default:
                    {
                        throw new BackupException("Incorrect point type");
                    }
                }
                _restorePoints = _cleaner.Clean(_restorePoints);
            }
            catch (BackupException e)
            {
                Console.WriteLine(e);
            }
        }
        
        public void PrintInfo()
        {
            Console.WriteLine("Backup Info");
            Console.WriteLine($"Date {_date}");
            Console.WriteLine($"Size {_size}");
            Console.WriteLine($"There are {_files.Count} files in backup");
            foreach (var file in _files)
            {
                Console.WriteLine(file.Key);
            }
            Console.WriteLine($"There are {_restorePoints.Count} restore points");
            foreach (var rp in _restorePoints)
            {
                rp.PrintInfo();
            }
            Console.WriteLine();
        }
    }
}