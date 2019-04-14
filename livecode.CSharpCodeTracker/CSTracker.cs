using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.ComponentBase.IdentificationPoint;

namespace livecode.CSharpCodeTracker
{
    [Export(typeof(ComponentIdentification))]
    public class CSTracker : ComponentIdentification
    {
        internal static string key = "9e388534-e32e-427d-861d-550fb4cd4516";

        public CSTracker()
        {
            this.Key = key;

            this.Name = "livecode C# Code Tracker";
            this.Description = "Tracks code changes to Csharp files in a directory to create logs.";
            this.IconKind = "LanguageCsharp";

            this.Permissions = Capablities.DirectoryScan | Capablities.CodeScan;
        }
    }
}
