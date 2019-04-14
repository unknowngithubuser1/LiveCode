using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.ComponentBase.AppPoint;

namespace livecode.VisualStudioTracker
{
    [Export(typeof(AppHook))]
    public class VisualStudioMainWindowHook : AppHook
    {
        public VisualStudioMainWindowHook()
        {
            this.WindowTitleRegx = ".*-\\s{1}Microsoft Visual Studio(\\s{1}\\(Not Responding\\))?\\s?$";
            this.ProcessNameRegx = "^devenv$";
            
            this.APIKey = Identification.key;
        }
        
        public override void WindowDeactivated(string windowTitle, string processName)
        {
            return;
        }

        public override AppType WindowActivated(string windowTitle, string processName)
        {
            return ResolveType(windowTitle, processName);
        }

        public override AppType ResolveType(string windowTitle, string processName)
        {
            if (windowTitle.Contains("(Not Responding)"))
                return AppType.IDEWaiting;

            if (windowTitle.Contains("(Running)"))
                return AppType.IDETesting;

            if (windowTitle.Contains("(Debugging)"))
                return AppType.IDEDebugging;

            return AppType.IDE;
            
        }
    }

    [Export(typeof(AppHook))]
    public class VisualStudioExceptionHook : AppHook
    {
        public VisualStudioExceptionHook()
        {
            this.WindowTitleRegx = "^Exception Helper Window$";
            this.ProcessNameRegx = "^devenv$";
            
            this.APIKey = Identification.key;
        }
    }
}
