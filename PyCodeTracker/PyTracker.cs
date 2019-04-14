using System.ComponentModel.Composition;
using livecode.ComponentBase.IdentificationPoint;

namespace livecode.PyCodeTracker
{
    [Export(typeof(ComponentIdentification))]
    public class PyTracker : ComponentIdentification
    {
        internal static string key = "4b8ea839-8cd2-4d57-9344-00b74160ba8e";

        public PyTracker()
        {
            this.Key = key;

            this.Name = "livecode Python Code Tracker";
            this.Description = "Tracks code changes to python files in a directory to create logs.";
            this.IconKind = "LanguagePythonText";
            
            this.Permissions = Capablities.DirectoryScan | Capablities.CodeScan;
        }
    }
}


