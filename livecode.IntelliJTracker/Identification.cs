using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.ComponentBase.IdentificationPoint;

namespace livecode.IntelliJTracker
{
    [Export(typeof(ComponentIdentification))]
    public class Identification : ComponentIdentification
    {
        internal static string key = "be4aada4-9146-47cc-bfc3-e82504bc6fbb";

        public Identification()
        {
            this.Key = key;

            this.Name = "livecode IntelliJ Idea Tracker";
            this.Description = "Tracks foreground usages of IntelliJ Idea to create time logs.";
            this.IconKind = "Instapaper";

            this.Permissions = Capablities.ForegroundApp;
        }
    }
}
