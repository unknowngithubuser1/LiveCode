using System.ComponentModel.Composition;
using livecode.ComponentBase.AppPoint;

namespace livecode.PyCharmTracker
{
    [Export(typeof(AppHook))]
    public class PCTracker : AppHook
    {
        public PCTracker()
        {
            APIKey = Identification.key;

            ProcessNameRegx = "^pycharm(64|32)?$";
            WindowTitleRegx = ".*\\sPyCharm.*";
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
