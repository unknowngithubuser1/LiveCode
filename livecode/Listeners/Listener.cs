using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.wpf.Components;
using livecode.wpf.Components.Logs;

namespace livecode.wpf.Listeners
{
    public class Listener
    {
        #region Singleton

        private static Listener _instance;
        private static object _lock = true;

        public static Listener Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new Listener());
                }
            }
        }

        public static void Initialize()
        {
            if (_instance == null) _instance = new Listener();
        }

        #endregion

        private GlobalTimer timer;
        private ProjectFilesWatcher pwatcher;
        private KaVEFilesWatcher kwatcher;
        private LogAggregator aggregator;

        public static bool IsEnabled { get; private set; }
        public int MinutesLeft
        {
            get => IsEnabled ? (int)(Properties.Settings.Default.FormInterval - DateTime.Now.Subtract(Instance.timer.LastPrompt).TotalMinutes + 1) : Properties.Settings.Default.FormInterval;
        }


        private Listener()
        {
            Registration.Initialize();
            Registration.Validate();

            timer = new GlobalTimer();
            pwatcher = new ProjectFilesWatcher();
            kwatcher = new KaVEFilesWatcher();
            aggregator = new LogAggregator();
        }

        public static bool CanStart()
        {
            string kave = Properties.Settings.Default.KaVEDirectory;
            string proj = Properties.Settings.Default.ProjectDirectory;

            if (string.IsNullOrWhiteSpace(kave) || string.IsNullOrWhiteSpace(proj))
            {
                return false;
            }
            else if (!Directory.Exists(kave) || !Directory.Exists(proj))
            {
                return false;
            }
            else
                return true;
        }

        public static void Start()
        {
            if (IsEnabled || !CanStart()) return;

            Instance.pwatcher.Start();
            Instance.kwatcher.RunFileWatcher();
            Instance.timer.Start();

            IsEnabled = true;
        }

        public static void Stop()
        {
            if (!IsEnabled) return;

            Instance.pwatcher.Stop();
            Instance.kwatcher.StopFileWatcher();
            Instance.timer.Stop();

            IsEnabled = false;
        }

        public static void ManualPrompt()
        {
            Instance.timer.ManualPrompt();
        }
    }
}
