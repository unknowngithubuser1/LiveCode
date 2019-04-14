using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livecode.ComponentBase.FilePoint
{
    public interface ICounter
    {
        Type MeasureType { get; }

        IMeasure Measure(CodeHistory history);
    }
}
