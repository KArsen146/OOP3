using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication
{
    class StorageSeparate:Storage
        {
            protected override List<FileRestoreCopyInfo> SaveFiles(Dictionary<string, DateTime> files, int ind, DateTime datePrev, DateTime date)
            {
                List<FileRestoreCopyInfo> fileInfo = new List<FileRestoreCopyInfo>();
                foreach (var file in files)
                {
                    var finfo = new FileInfo(file.Key);
                    if (datePrev < finfo.LastWriteTime || datePrev < file.Value)
                    {
                        fileInfo.Add(new FileRestoreCopyInfo($"Rp{ind}/" + file.Key + $"_{ind}", finfo.Length, finfo.LastWriteTime));
                    }
                }
                return fileInfo;
            }
        }
}