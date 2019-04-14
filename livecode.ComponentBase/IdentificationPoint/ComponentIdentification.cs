using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace livecode.ComponentBase.IdentificationPoint
{
    public abstract class ComponentIdentification
    {
        public string Key { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public ImageBrush Thumbnail { get; set; }

        public string IconKind { get; set; }
        
        public Capablities Permissions { get; set; }
    }
}
