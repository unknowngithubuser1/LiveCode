using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KaVE.Commons.Model.Events;
using KaVE.Commons.Utils.Json;
using KaVEData.Tools;
using livecode.Common.Events;
using livecode.wpf.Listeners;
using livecode.wpf.Logs;

namespace livecode.wpf.Components.Logs
{
    public class LogAggregator
    {
        private Dictionary<string, int> _fileHeaders = new Dictionary<string, int>();

        public LogAggregator()
        {
            KaVEFilesWatcher.OnFileChange += e => FileChanged(e.FullPath);
            GlobalTimer.ActivityOccured += ActivityOccured;
        }

        private void ActivityOccured()
        {
            DBInsertAgent.Instance.Visit(new KaVEData.Tools.ActivityEvent());
        }

        public void FileChanged(string fullPath)
        {
            if (!_fileHeaders.ContainsKey(fullPath))
                _fileHeaders.Add(fullPath, 0);

            string[] newData = collect(fullPath);

            if (newData != null)
                parse(newData);
        }

        private int _ts = 0;
        private void parse(string[] newData)
        {
            foreach (var log in newData)
            {
                IDEEvent e = (IDEEvent)log.ParseJsonTo(typeof(IIDEEvent));
                DBInsertAgent.Instance.Visit(e);
            }

            _ts = (_ts + 1) % 10;
            if (newData.Length < 10 && _ts < 9)
                return;

            try
            {
                DBInsertAgent.Instance.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Instance.Log(e.ToString());
                return;
            }

            Logger.Instance.Log($"I Just parsed {newData.Length} files.");
        }

        private string[] collect(string fullPath)
        {
            int head = _fileHeaders[fullPath];

            string[] newData;
            try
            {
                newData = File.ReadLines(fullPath).Skip(head).ToArray();
            }
            catch (Exception e)
            {
                Logger.Instance.Log(e.ToString());
                return null;
            }

            _fileHeaders[fullPath] += newData.Length;

            Logger.Instance.Log($"Read {newData.Length} log lines. new Head position: {head}");

            if (newData.Length > 500)
                return newData.Reverse().Take(500).Reverse().ToArray();

            return newData;
        }
    }
}
