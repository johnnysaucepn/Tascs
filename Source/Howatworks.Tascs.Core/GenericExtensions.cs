using System;

namespace Howatworks.Tascs.Core
{
    public static class GenericExtensions
    {
        public static ITascTarget Tasc(this ITascTarget target, Action<TascContext> action)
        {
            return target.Do(new GenericTasc(context =>
            {
                try
                {
                    action(context);
                }
                catch (TascException)
                {
                    return TascResult.Fail;
                }
                return TascResult.Pass;
            }));
        }

        public static ITascTarget Tasc(this ITascTarget target, Func<TascContext, ITascResult> action)
        {
            return target.Do(new GenericTasc(action));
        }

    }
}
