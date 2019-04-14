using System.ComponentModel.Composition;
using livecode.ComponentBase.IdentificationPoint;

namespace livecode.JavaCodeTracker
{
    [Export(typeof(ComponentIdentification))]
    public class JTracker : ComponentIdentification
    {
        internal static string key = "2c13215b-e747-40f8-817e-e4f964bbc9af";

        public JTracker()
        {
            this.Key = key;

            this.Name = "livecode Java Code Tracker";
            this.Description = "Tracks code changes to java files in a directory to create logs.";

            this.Permissions = Capablities.DirectoryScan | Capablities.CodeScan | Capablities.BackgroundProcesses;
        }
    }
}
