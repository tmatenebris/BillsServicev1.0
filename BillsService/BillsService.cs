using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Timers;
using BillsServiceLibrary;
using System.ServiceModel;
using System.Configuration;

namespace MyBillsService
{


    public partial class BillsService : ServiceBase
    {
        private static BillsServiceFileWatcher _watcher;
        private static EventLog _serviceEventLog;
        public ServiceHost serviceHost = null;



       
        private static int eventId = 1;
        public BillsService()
        {
            InitializeComponent();
            _serviceEventLog = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("BillsServiceSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "BillsServiceSource", "BillsServiceLog");
            }
            _serviceEventLog.Source = "BillsServiceSource";
            _serviceEventLog.Log = "BillsServiceLog";
           

            _watcher = new BillsServiceFileWatcher(@ConfigurationManager.AppSettings["FolderPath"]);

        }

  

        protected override void OnStart(string[] args)
        {
            _serviceEventLog.WriteEntry("Starting serivce...");
            serviceHost?.Close();
            //_serviceEventLog.WriteEntry("In OnStart.");
            serviceHost = new ServiceHost(typeof(MyBillsService.BillsServiceController));
            serviceHost.Open();
            _serviceEventLog.WriteEntry("Service running...");
        }

        protected override void OnStop()
        {
            _serviceEventLog.WriteEntry("Stoping serivce...");

            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }

            _serviceEventLog.WriteEntry("Service stoped...");
            _serviceEventLog.Close();
        }
    }
}
