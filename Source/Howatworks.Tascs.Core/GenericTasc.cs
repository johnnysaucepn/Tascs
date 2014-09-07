using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class GenericTasc : Tasc
    {
        private Action _action;
        public GenericTasc(Action action)
        {
            _action = action;
        }

        public override ITascResult Execute()
        {
            if (_action != null)
            {
                _action();
            }

            return null;
        }
    }
}
