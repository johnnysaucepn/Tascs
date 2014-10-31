using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class GenericTasc : Tasc
    {
        private readonly Func<TascTarget, ITascResult> _function;
        public GenericTasc(Func<TascTarget, ITascResult> function)
        {
            _function = function;
        }

        public override ITascResult Execute(TascTarget target)
        {
            return _function != null ? _function(target) : null;
        }
    }
}
