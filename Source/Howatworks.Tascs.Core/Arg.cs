using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class Arg
    {
        private const char Quote = '"';

        public string Value { get; private set; }

        private Arg(string arg)
        {
            Value = arg;
        }

        public static Arg Literal(string arg)
        {
            return new Arg(arg);
        }

        public static Arg Quoted(string arg)
        {
            return new Arg(string.Format("{0}{1}{0}", Quote, arg));
        }

    }
}
