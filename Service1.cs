using IntelligentMemoryScavenger.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace IntelligentMemoryScavenger
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            var skipLogging = GlobalConstants.Instance.SkipLogging;
            if (skipLogging)
            {
                Logger = new AirLogger();
            }
            else
            {
                Logger = new FileLogger();
            }
            MemoryScavenger = new MemoryScavenger(Logger);
        }

        Timer timer = new Timer();
        ILogger Logger;
        MemoryScavenger MemoryScavenger;

        protected override void OnStart(string[] args)
        {
            Logger.Log("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = GlobalConstants.Instance.MemoryCleanTimeInterval; //number in milisecinds  
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            Logger.Log("Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            MemoryScavenger.CleanMemory();
        }
    }
}
