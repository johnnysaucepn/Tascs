using System;

namespace Howatworks.Tascs.Core.Generic
{
    public class GenericTasc : Tasc
    {
        private readonly Func<TascContext, ITascResult> _action;
        public GenericTasc(Func<TascContext, ITascResult> action)
        {
            _action = action;
        }

        public override ITascResult Execute(TascContext context)
        {
            return _action != null ? _action(context) : TascResult.Pass;
        }
    }
}
