using System;
using System.Collections.Generic;
using System.IO;

namespace livecode.ComponentBase.FilePoint
{
    public class CodeFile
    {
        public Guid Id { get; set; }

        public string FullPath { get; set; }

        public DateTime LastEdit { get; set; }

        public string Name => Path.GetFileName(FullPath);
        public string Extension => Path.GetExtension(FullPath);

        public bool Exists { get; set; } = true;

        public List<string> PathHistory { get; set; } = new List<string>();
        
        public List<CodeHistory> Changes { get; set; } = new List<CodeHistory>();
        
        public void AddMeasurements(bool forceRecalculation, params ICounter[] c)
        {
            foreach (var history in Changes)
            {
                foreach (var counter in c)
                {
                    history.AddMeasurement(counter, forceRecalculation);
                }
            }
        }

        public CodeFile(string fullPath) : this(fullPath, DateTime.Now)
        {
            Changes.Add(new CodeHistory(this));
        }

        public CodeFile(string fullPath, DateTime lastEdit)
        {
            Id = Guid.NewGuid();

            FullPath = fullPath;
            LastEdit = lastEdit;
        }

    }
}
