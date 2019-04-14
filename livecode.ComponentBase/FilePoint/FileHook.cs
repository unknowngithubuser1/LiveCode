using System.Collections.Generic;
using System.IO;

namespace livecode.ComponentBase.FilePoint
{
    public abstract class FileHook
    {
        public string APIKey { get; set; }
        
        public virtual bool CheckAddress(string rootDir, string fileName)
        {
            return false;
        }

        public List<CodeFile> Files { get; protected set; }

        public virtual CodeFile FileCreated(string fullPath, FileSystemEventArgs e)
        {
            return null;
        }

        public virtual CodeFile FileRenamed(string fullPath, RenamedEventArgs e)
        {
            return null;
        }

        public virtual CodeFile FileChanged(string fullPath, FileSystemEventArgs e)
        {
            return null;
        }

        public virtual CodeFile FileDeleted(string fullPath, FileSystemEventArgs e)
        {
            return null;
        }

        public virtual void MeasureCodeFiles<T>() where T : IMeasure
        {
        }


    }
}
