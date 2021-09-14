using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
    public abstract class CleanRestorePoints
    {
        public abstract bool IsRightPoint(List<RestorePoint> restorePoints, int i);

        public readonly int Type;

        protected CleanRestorePoints(int type)
        {
            Type = type;
        }
        public List<RestorePoint> Clean(List<RestorePoint> restorePoints)
        {
        List<RestorePoint> remainingPoints = new List<RestorePoint>();
            int i = restorePoints.Count - 1;
            while (i > -1 && IsRightPoint(restorePoints, i))
            {
                remainingPoints.Add(restorePoints[i]);
                i--;
            }

            RestorePoint point = null;
            if (remainingPoints.Count != 0)
                point = remainingPoints.Last();
            if (point != null)
            {
                while (point.IsIncrement())
                {
                    remainingPoints.Add(point.PreviousPoint);
                    point = point.PreviousPoint;
                }

                remainingPoints.Reverse();
            }

            return remainingPoints;
        }
    }
}