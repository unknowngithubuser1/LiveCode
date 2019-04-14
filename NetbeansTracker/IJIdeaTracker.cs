using System.ComponentModel.Composition;
using livecode.ComponentBase.AppPoint;

namespace livecode.NetbeansTracker
{
    [Export(typeof(AppHook))]
    public class NetbeansTracker : AppHook
    {
        public NetbeansTracker()
        {
            APIKey = Identification.key;

            ProcessNameRegx = "^netbeans(64|32)?$";
            WindowTitleRegx = ".*\\sNetBeans\\sIDE.*";
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
