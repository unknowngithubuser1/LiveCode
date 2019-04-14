using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livecode.wpf.Components
{
    public abstract class RequestManager
    {
        public virtual void Compose()
        { }

        public virtual void Validate()
        { }
    }
}
