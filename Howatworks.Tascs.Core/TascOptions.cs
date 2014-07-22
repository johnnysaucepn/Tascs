using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class TascOptions : Dictionary<string,string>
    {
        public new string this[string key]
        {
            get {
                return ContainsKey(key) ? base[key] : null;
            }
        }
        
    }
}
