using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.ComponentBase.AppPoint;

namespace livecode.IntelliJTracker
{
    [Export(typeof(AppHook))]
    public class IJIdeaTracker : AppHook
    {
        public IJIdeaTracker()
        {
            APIKey = Identification.key;

            ProcessNameRegx = "^idea(64|32)?$";
            WindowTitleRegx = ".*\\sIntelliJ\\sIDEA.*";
        }

        public override AppType WindowActivated(string windowTitle, string processName)
        {
            return AppType.IDE;
        }

        public override AppType ResolveType(string windowTitle, string processName)
        {
            return AppType.IDE;
        }
        
    }
}
