using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentMemoryScavenger
{
    public sealed class GlobalConstants
    {
        public static readonly Lazy<GlobalConstants> globalConstants = new Lazy<GlobalConstants>(() => new GlobalConstants());
        private GlobalConstants()
        {
            InitializeGlobalConstants();
        }

        private void InitializeGlobalConstants()
        {
            var appSettings = ConfigurationManager.AppSettings;
            MemoryCleanTimeInterval = appSettings["MemoryScavenger.SweepTimeIntervalMinutes"] != null ? int.Parse(appSettings["MemoryScavenger.SweepTimeIntervalMinutes"]) * 60 * 1000 : 3600000;
            LogFolderPath = appSettings["MemoryScavenger.LogFolderPath"] ?? AppDomain.CurrentDomain.BaseDirectory;
            SkipLogging = appSettings["MemoryScavenger.SkipLogging"] != null ? bool.Parse(appSettings["MemoryScavenger.SkipLogging"]) : true;
        }

        public static GlobalConstants Instance
        {
            get
            {
                return globalConstants.Value;
            }
        }
        public int MemoryCleanTimeInterval { get; set; }

        public string LogFolderPath { get; set; }

        public bool SkipLogging { get; set; }
    }
}
