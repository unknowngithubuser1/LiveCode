using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using livecode.ComponentBase.AppPoint;

namespace livecode.PyCharmTracker
{
    [Export(typeof(ProcessHook))]
    public class PythonTracker : ProcessHook
    {
        public PythonTracker()
        {
            APIKey = Identification.key;

            ProcessName = "python";
            CommandLineRegx = ".*";
        }

        public override AppType ProcessDiscovered(string processName, string commandline)
        {
            if (commandline.Contains("pydev") && commandline.Contains(" --client "))
                return AppType.IDEDebugging;

            if (commandline.Contains("|PROJECT|"))
                return AppType.IDETesting;
            
            return AppType.None;
        }
    }
}
