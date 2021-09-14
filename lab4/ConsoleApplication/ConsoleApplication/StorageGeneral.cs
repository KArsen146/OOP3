using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication
{ 
    class StorageGeneral:Storage
    {
        
        protected override List<FileRestoreCopyInfo> SaveFiles(Dictionary<string, DateTime> files, int ind, DateTime datePrev, DateTime date)
        {
            long size = 0;    
            List<FileRestoreCopyInfo> fileInfo = new List<FileRestoreCopyInfo>();
            foreach (var file in files)
            {
                var finfo = new FileInfo(file.Key);
                if (datePrev < finfo.LastWriteTime)
                    size += finfo.Length;
            }
            fileInfo.Add(new FileRestoreCopyInfo($"Restore_point_{ind}", size, date));
            return fileInfo;
        }
    }
}