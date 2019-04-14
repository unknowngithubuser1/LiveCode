using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using livecode.wpf.Components.Logs;

namespace livecode.wpf.Listeners
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class KaVEFilesWatcher
    {
        private FileSystemWatcher watcher;

        public void RunFileWatcher()
        {
            watcher = new FileSystemWatcher
            {
                Path = Properties.Settings.Default.KaVEDirectory,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.*"
            };

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;

            watcher.EnableRaisingEvents = true;
        }

        public void StopFileWatcher()
        {
            watcher.EnableRaisingEvents = false;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!e.Name.Contains(DateTime.Now.ToString("yyyy-MM-dd")))
                return; // Only logs for today

            OnFileChange?.Invoke(e);
        }

        public static event Action<FileSystemEventArgs> OnFileChange;
    }
}
