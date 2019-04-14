using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using livecode.ComponentBase.Util;

namespace livecode.ComponentBase.FilePoint
{
    public class CodeHistory
    {
        public Guid Id { get; set; }
        
        private Dictionary<string, IMeasure> _measurements = new Dictionary<string, IMeasure>();

        public CodeHistory(CodeFile parent, DateTime savedAt)
        {
            Id = Guid.NewGuid();

            getContent(parent);

            Parent = parent;
            SavedAt = savedAt;
        }

        public CodeHistory(CodeFile parent) : this(parent, parent.LastEdit)
        {
            
        }

        public CodeFile Parent { get; set; }

        public string Content { get; set; }

        public CodeChangeType Changes { get; set; } = CodeChangeType.None;

        public DateTime SavedAt { get; set; }

        public void AddMeasurement(ICounter c, bool forceRecalculation)
        {
            string type = c.MeasureType.FullName ?? "";

            if (_measurements.ContainsKey(type))
            {
                if (forceRecalculation)
                    _measurements.Remove(type);
                else
                    return;
            }

            try
            {
                _measurements.Add(type, c.Measure(this));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public IMeasure GetMeasurement(string type)
        {
            return _measurements[type];
        }

        private void getContent(CodeFile parent)
        {
            bool succeed = false;

            while (!succeed)
            {
                try
                {
                    Content = File.ReadAllText(parent.FullPath);
                    succeed = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    Thread.Sleep(50);
                }
            }
        }

    }
}
