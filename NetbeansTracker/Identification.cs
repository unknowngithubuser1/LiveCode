using System.ComponentModel.Composition;
using livecode.ComponentBase.IdentificationPoint;

namespace livecode.NetbeansTracker
{
    [Export(typeof(ComponentIdentification))]
    public class Identification : ComponentIdentification
    {
        internal static string key = "fbd6c2f9-b9e7-41f3-abc7-c33c364ed2bf";

        public Identification()
        {
            this.Key = key;

            this.Name = "livecode Netbeans IDE Tracker";
            this.Description = "Tracks foreground usages of Netbeans IDE Idea to create time logs.";
            this.IconKind = "Cube";

            this.Permissions = Capablities.ForegroundApp;
        }
    }
}
