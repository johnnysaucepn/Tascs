using System;

namespace Howatworks.Tascs.Core
{
    public static class GenericExtensions
    {
        public static ITascTarget Tasc(this ITascTarget target, Action action)
        {
            return target.Do(new GenericTasc(() =>
            {
                try
                {
                    action();
                }
                catch (TascException)
                {
                    return TascResult.Fail;
                }
                return TascResult.Pass;
            }));
        }

        public static ITascTarget Tasc(this ITascTarget target, Func<ITascResult> action)
        {
            return target.Do(new GenericTasc(action));
        }

    }
}
