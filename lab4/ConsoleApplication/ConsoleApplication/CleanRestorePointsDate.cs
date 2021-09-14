using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class CleanRestorePointsDate : CleanRestorePoints
    {
        private DateTime _dLimit;

        public DateTime DateLimit
        {
            get => _dLimit;
            set => _dLimit = value;
        }
        
        public CleanRestorePointsDate(DateTime dLimit) : base(3)
        {
            _dLimit = dLimit;
        }

        public override bool IsRightPoint(List<RestorePoint> restorePoints, int i)
        {
            return restorePoints[i].Date <= _dLimit;
        }
    }
}