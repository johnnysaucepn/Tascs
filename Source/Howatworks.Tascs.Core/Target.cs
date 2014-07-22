using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class Target
    {
        public string Name { get; set; }

        private Target()
        {
        }

        public static Target Named(string name)
        {
            Target newTarget = new Target {Name = name};
            return newTarget;
        }

    }

}
