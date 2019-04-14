using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using livecode.ComponentBase.AppPoint;

namespace livecode.JavaCodeTracker
{
    [Export(typeof(ProcessHook))]
    public class JavaTracker : ProcessHook
    {
        public JavaTracker()
        {
            APIKey = JTracker.key;

            ProcessName = "java";
            CommandLineRegx = ".*(-agentlib:|\"-javaagent|-Xdebug|\\|PROJECT\\|).*";
        }

        public override AppType ProcessDiscovered(string processName, string commandline)
        {
            if (!commandline.Contains("idea_rt") && !Regex.IsMatch(commandline, CommandLineRegx))
                return AppType.None;

            if (commandline.Contains(" -agentlib:"))
                return AppType.IDEDebugging;

            if (commandline.Contains(" -javaagent"))
                return AppType.IDETesting;

            if (commandline.Contains(" -Xdebug"))
                return AppType.IDEDebugging;

            if (commandline.Contains("|PROJECT|"))
                return AppType.IDETesting;

            return AppType.None;
        }
    }
}
