using System.ComponentModel.Composition;
using livecode.ComponentBase.IdentificationPoint;

namespace livecode.PyCharmTracker
{
    [Export(typeof(ComponentIdentification))]
    public class Identification : ComponentIdentification
    {
        internal static string key = "42826545-b59e-4532-b733-8d5ab8be58f1";

        public Identification()
        {
            this.Key = key;

            this.Name = "livecode PyCharm Tracker";
            this.Description = "Tracks foreground usages of PyCharm and python process to create time logs.";
            this.IconKind = "LanguagePython";

            this.Permissions = Capablities.ForegroundApp | Capablities.BackgroundProcesses;
        }
    }
}
