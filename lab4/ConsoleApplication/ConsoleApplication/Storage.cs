using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public abstract class Storage
    {
        public RestorePoint CreatePoint(Dictionary<string, DateTime> files, int ind, RestorePoint lastPoint = null)
        {
            var date = DateTime.Now;
            DateTime datePrev = DateTime.MinValue;
            if (lastPoint != null)
            {
                datePrev = lastPoint.Date;
            }
            return new RestorePoint(lastPoint, SaveFiles(files, ind, datePrev, date), date);
        }

        protected abstract List<FileRestoreCopyInfo> SaveFiles(Dictionary<string, DateTime> files, int ind,
            DateTime datePrev, DateTime date);
    }
}