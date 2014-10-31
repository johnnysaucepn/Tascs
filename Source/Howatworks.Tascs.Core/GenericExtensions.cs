using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public static class GenericExtensions
    {
        public static ITascTarget Do(this ITascTarget target, Action action)
        {
            return target.Do(new GenericTasc(x =>
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

        public static ITascTarget Do(this ITascTarget target, Func<ITascResult> func)
        {
            return target.Do(new GenericTasc(x =>
            {
                return func();
            }));
        }

    }
}
