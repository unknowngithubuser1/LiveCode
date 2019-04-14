using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livecode.ComponentBase.IdentificationPoint
{
    /// <summary>
    /// Capablities that a livecode component can have
    /// </summary>
    [Flags]
    public enum Capablities
    {
        /// <summary>
        /// Recieve Updates on [1 or more] Foreground app's name usage.
        /// </summary>
        ForegroundApp = 1,

        /// <summary>
        /// Recieve Updates on [1 or more] Project Directory's file changes
        /// </summary>
        DirectoryScan = 2,

        /// <summary>
        /// Recieve Updates on [1 or more] Code File changes and contents
        /// </summary>
        CodeScan = 4,

        /// <summary>
        /// Recieve Updates on [1 or more] Background processes' names.
        /// </summary>
        BackgroundProcesses = 8
    }
}
