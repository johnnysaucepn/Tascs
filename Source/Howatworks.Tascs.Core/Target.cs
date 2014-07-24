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
        private readonly IList<ITasc> _tascs = new List<ITasc>();

        private Target()
        {
        }

        public static Target Named(string name)
        {
            var newTarget = new Target {Name = name};
            return newTarget;
        }

        public void AddTasc(ITasc tasc)
        {
            _tascs.Add(tasc);
        }

        public ITascResult Execute()
        {
            ITascResult result = null;
            foreach (var tasc in _tascs)
            {
                result = tasc.Execute();
            }
            return result;
        }
    }

}
