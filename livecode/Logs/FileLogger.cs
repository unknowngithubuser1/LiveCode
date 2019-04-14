using System;
using System.IO;
using livecode.wpf.Properties;

namespace livecode.wpf.Logs
{
    public class FileLogger : ILogger
    {
        public string Source
        {
            get { return Environment.CurrentDirectory + "\\" + Settings.Default.LOG_FILE; }
            set
            {
                Settings.Default["LOG_FILE"] = value;
                Settings.Default.Save();
            }
        }

        public void RevertSource()
        {
            Settings.Default["LOG_FILE"] = Settings.Default.DEFAULT_LOG_FILE;
            Settings.Default.Save();
        }

        public void Log(string operation)
        {
            File.AppendAllText(Source, operation + "\r\n");
        }
    }
}