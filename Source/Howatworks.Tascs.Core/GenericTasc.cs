using System;

namespace Howatworks.Tascs.Core
{
    public class GenericTasc : Tasc
    {
        private readonly Func<ITascResult> _action;
        public GenericTasc(Func<ITascResult> action)
        {
            _action = action;
        }

        public override ITascResult Execute(TascTarget target)
        {
            return _action != null ? _action() : TascResult.Pass;
        }
    }
}
