using System.ComponentModel.Composition;
using livecode.ComponentBase.IdentificationPoint;

namespace livecode.CCodeTracker
{
    [Export(typeof(ComponentIdentification))]
    public class CTracker : ComponentIdentification
    {
        internal static string key = "ff845e05-9315-49cc-9041-4ae286eb7c87";

        public CTracker()
        {
            this.Key = key;

            this.Name = "livecode C/C++ Code Tracker";
            this.Description = "Tracks code changes to C/C++ files in a directory to create logs.";
            this.IconKind = "LanguageCpp";

            this.Permissions = Capablities.DirectoryScan | Capablities.CodeScan;
        }
    }
}
