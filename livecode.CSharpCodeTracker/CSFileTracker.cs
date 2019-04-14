using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.ComponentBase.FilePoint;
using livecode.ComponentBase.Measures;
using livecode.ComponentBase.Util;
using livecode.CSharpCodeTracker.Counters;

namespace livecode.CSharpCodeTracker
{
    [Export(typeof(FileHook))]
    public class CSFileTracker : FileHook
    {
        public CSFileTracker()
        {
            Files = new List<CodeFile>();

            this.APIKey = CSTracker.key;
        }

        public override bool CheckAddress(string rootDir, string fileName)
        {
            if (Files.Find(c => c.FullPath == fileName) != null)
                return true;

            var address = fileName.Replace(rootDir, "");

            if (!fileName.EndsWith(".cs"))
                return false;

            if (fileName.EndsWith(".g.cs"))
                return false;

            if (address.StartsWith("\\packages\\"))
                return false;

            if (address.EndsWith("\\Properties\\AssemblyInfo.cs") ||
                address.EndsWith("\\Properties\\Resources.Designer.cs") ||
                address.EndsWith("\\Properties\\Settings.Designer.cs"))
                return false;

            if (address.Contains("\\bin\\Debug\\") ||
                address.Contains("\\bin\\Release\\") ||
                address.Contains("\\obj\\Debug\\") ||
                address.Contains("\\obj\\Release\\"))
                return false;

            return true;
        }


        public override CodeFile FileCreated(string fullPath, FileSystemEventArgs e)
        {
            var file = new CodeFile(fullPath);
            Files.Add(file);

            return file;
        }

        public override CodeFile FileChanged(string fullPath, FileSystemEventArgs e)
        {
            var find = Files.FirstOrDefault(c => c.FullPath == fullPath);

            if (find == null)
            {
                find = new CodeFile(fullPath);
                Files.Add(find);
            }

            //WARNING? 
            //if ((e.ChangeType & WatcherChangeTypes.Changed) == 0) return find;

            var history = new CodeHistory(find);
            if (find.Changes.Count > 0)
            {
                if (find.Changes.Last().Content == history.Content)
                    return null;
            }

            find.Exists = true;
            find.Changes.Add(history);

            return find;
        }

        public override CodeFile FileRenamed(string fullPath, RenamedEventArgs e)
        {
            CodeFile find = findFileOrigin(e);

            if (find == null)
            {
                Files.Add(new CodeFile(e.FullPath.RemoveTempPostfix()));
            }
            else
            {
                find.Exists = true;
                find.PathHistory.Add(e.OldFullPath.RemoveTempPostfix());
                find.FullPath = e.FullPath.RemoveTempPostfix();

                find.Changes.Add(new CodeHistory(find));
            }

            return find;
        }

        public override CodeFile FileDeleted(string fullPath, FileSystemEventArgs e)
        {
            var find = Files.FirstOrDefault(c => c.FullPath == e.FullPath.RemoveTempPostfix());

            if (find != null)
                find.Exists = false;

            return null;
        }

        public override void MeasureCodeFiles<T>()
        {
            if (typeof(T) == typeof(PSPSizeMeasure))
            {
                MeasureCodeFiles(new CSharpSizeCounter());
            }
        }

        private void MeasureCodeFiles(params ICounter[] c)
        {
            foreach (var file in Files)
            {
                file.AddMeasurements(false, c);
            }
        }

        private CodeFile findFileOrigin(RenamedEventArgs e)
        {
            return e.OldFullPath.Contains("~") ? Files.FirstOrDefault(c => c.FullPath == e.FullPath) : Files.FirstOrDefault(c => c.FullPath == e.OldFullPath.RemoveTempPostfix());
        }


    }
}
