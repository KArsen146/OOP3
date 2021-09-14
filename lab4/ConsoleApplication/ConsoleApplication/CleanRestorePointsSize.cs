using System.Collections.Generic;

namespace ConsoleApplication
{
    public class CleanRestorePointsSize : CleanRestorePoints
    {
        private static long _sLimit;

        public static long SizeLimit
        {
            get => _sLimit;
            set => _sLimit = value;
        }
        public CleanRestorePointsSize(long limit):base(2)
        {
            _sLimit = limit;
        }
        
        public override bool IsRightPoint(List<RestorePoint> restorePoints, int i)
        {
            long size = 0;
            for (int index = i; index < restorePoints.Count; index++)
                size += restorePoints[index].Size;
            return size <= _sLimit;
        }
    }
}