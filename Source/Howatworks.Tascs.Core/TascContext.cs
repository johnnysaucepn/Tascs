using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class TascContext
    {
        public TascContext(TascTarget tascTarget)
        {
            this.Target = tascTarget;
        }
        public TascTarget Target { get; set; }

        public readonly IDictionary<object, object> Properties = new Dictionary<object, object>();
    }
}
