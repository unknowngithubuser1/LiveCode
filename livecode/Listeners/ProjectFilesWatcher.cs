using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace livecode.wpf.Listeners
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class ProjectFilesWatcher
    {
        private FileSystemWatcher watcher;

        public void Start()
        {
            watcher = new FileSystemWatcher
            {
                Path = Properties.Settings.Default.ProjectDirectory,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.*",
                IncludeSubdirectories = true
            };

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;

            watcher.EnableRaisingEvents = true;

            ScanAllFiles();
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }

        private void ScanAllFiles()
        {
            var allFiles = Directory.EnumerateFiles(watcher.Path, "*.*", SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                var directory = Path.GetDirectoryName(file) ?? " ";
                var filename = Path.GetFileName(file);

                OnChanged(this, new FileSystemEventArgs(WatcherChangeTypes.Created, directory, filename));
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            OnFileRename?.Invoke(e);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains("~") && e.FullPath.Contains("TMP"))
                return;

            if (string.IsNullOrWhiteSpace(Path.GetFileName(e.FullPath)))
                return;

            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    OnFileCreate?.Invoke(e);
                    break;
                case WatcherChangeTypes.Deleted:
                    OnFileDelete?.Invoke(e);
                    break;
                case WatcherChangeTypes.Changed:
                    OnFileChange?.Invoke(e);
                    break;
                case WatcherChangeTypes.Renamed:
                    break;
                case WatcherChangeTypes.All:
                    break;
                default:
                    return;
            }
        }

        public static event Action<RenamedEventArgs> OnFileRename;
        public static event Action<FileSystemEventArgs> OnFileCreate;
        public static event Action<FileSystemEventArgs> OnFileChange;
        public static event Action<FileSystemEventArgs> OnFileDelete;

    }
}
