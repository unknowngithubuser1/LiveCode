using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.ComponentBase.IdentificationPoint;

namespace livecode.VisualStudioTracker
{
    [Export(typeof(ComponentIdentification))]
    public class Identification : ComponentIdentification
    {
        internal static string key = "0b51078d-31bb-47ab-9945-d88f241b4581";

        public Identification()
        {
            this.Key = key;

            this.Name = "livecode Visual Studio Tracker";
            this.Description = "Tracks foreground usages of visual studio to create time logs.";
            this.IconKind = "Microsoft";

            this.Permissions = Capablities.ForegroundApp;
        }
    }
}
