using System.Collections.Generic;

namespace ConsoleApplication
{
    public class CleanRestorePointsQuantity : CleanRestorePoints
    {
        private int _quantityLimit;

        public int QuantityLimit
        {
            get => _quantityLimit;
            set => _quantityLimit = value;
        }
        public CleanRestorePointsQuantity(int limit) : base(1)
        {
            _quantityLimit = limit;
        }

        public override bool IsRightPoint(List<RestorePoint> restorePoints, int i)
        {
            return restorePoints.Count - i <= _quantityLimit;
        }
    }
}