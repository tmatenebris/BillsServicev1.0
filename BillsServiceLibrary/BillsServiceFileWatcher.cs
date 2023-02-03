using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BillsServiceLibrary
{
    public class BillsServiceFileWatcher : FileSystemWatcher
    {
      
        private static EventLog _serviceEventLog = new EventLog();
        public BillsServiceFileWatcher(string path)
        {
            this.Path = path;
            this.NotifyFilter = NotifyFilters.Attributes
                               | NotifyFilters.CreationTime
                               | NotifyFilters.DirectoryName
                               | NotifyFilters.FileName
                               | NotifyFilters.LastAccess
                               | NotifyFilters.LastWrite
                               | NotifyFilters.Security
                               | NotifyFilters.Size;
            this.Filter = "*.csv";
            this.IncludeSubdirectories = true;
            this.EnableRaisingEvents = true;

            this.Created += OnCreated;
            _serviceEventLog.Source = "BillsServiceSource";
            _serviceEventLog.Log = "BillsServiceLog";

        }
        private static async void OnCreated(object sender, FileSystemEventArgs e)
        {
            _serviceEventLog.WriteEntry("File created: " + e.FullPath);
            await Task.Factory.StartNew(()=>BillsRepository.AddBills(e.FullPath));
        }
    }
}
