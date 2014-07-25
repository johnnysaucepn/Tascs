using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public static class Arg
    {
        private const char Quote = '"';

        public static string Literal(string arg)
        {
            return arg;
        }

        public static string Quoted(string arg)
        {
            return string.Format("{0}{1}{0}", Quote, arg);
        }

    }
}
