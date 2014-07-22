using System.Collections.Generic;

namespace Howatworks.Tascs.Core
{
    public class TascOptions : Dictionary<string, string>
    {
        public new string this[string key]
        {
            get { return ContainsKey(key) ? base[key] : null; }
        }
    }
}