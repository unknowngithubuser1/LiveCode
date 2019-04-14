using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using livecode.ComponentBase.FilePoint;
using livecode.ComponentBase.Measures;
using livecode.CSharpCodeTracker.Language;

namespace livecode.CSharpCodeTracker.Counters
{
    public class CSharpSizeCounter : ICounter
    {
        private static CSharpLOCCounter loc = new CSharpLOCCounter();

        public Type MeasureType => typeof(PSPSizeMeasure);

        public IMeasure Measure(CodeHistory history)
        {
            int index = history.Parent.Changes.IndexOf(history);

            if (index == 0) // This is the First version of file
            {
                return countAsBase(history);
            }
            else
            {
                CodeHistory previous = history.Parent.Changes[index - 1];
                return countAsModification(history, previous);
            }
        }

        private PSPSizeMeasure countAsBase(CodeHistory current)
        {
            var c = (LOCMeasure)loc.Measure(current);

            PSPSizeMeasure m = new PSPSizeMeasure()
            {
                Base = c.PhysicalLOC,
                Added = 0,
                Deleted = 0,
                Modified = 0,
                NewReuse = 0,
                Reused = 0,
                Total = c.PhysicalLOC
            };

            return m;
        }

        private PSPSizeMeasure countAsModification(CodeHistory current, CodeHistory previous)
        {
            var c1 = (LOCMeasure)loc.Measure(previous);
            var c2 = (LOCMeasure) loc.Measure(current);

            var changes = DiffHelper.SimpleDiff(current.Content, previous.Content);

            PSPSizeMeasure m = new PSPSizeMeasure()
            {
                Base = c1.PhysicalLOC,
                Added = changes.Count(t => t == ChangeType.Inserted),
                Deleted = changes.Count(t => t == ChangeType.Deleted),
                Modified = changes.Count(t => t == ChangeType.Modified),
                Total = c2.PhysicalLOC
            };
            
            return m;
        }
    }
}
