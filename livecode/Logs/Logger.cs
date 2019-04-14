using System;

namespace livecode.wpf.Logs
{
    public delegate void LogEventHandler(object sender, string time, string feedback);

    public class Logger
    {
        #region Singleton

        private static Logger _instance;
        private static object _lock = true;

        public static Logger Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new Logger());
                }
            }
        }

        #endregion

        public event LogEventHandler LogInProgress;
        public ILogger _logger { get; set; }

        private Logger()
        {
            _logger = new FileLogger();
        }

        private void onLog(string time, string e)
        {
            if (LogInProgress != null)
                LogInProgress(this, time, e);
        }

        public void Log(string feedback)
        {
            try
            {
                string time = DateTime.Now.ToString("G").PadRight(40);
                _logger.Log(string.Format("{0}{1}", time, feedback));

                onLog(time, feedback);
            }
            catch
            {}
        }
        
    }
}