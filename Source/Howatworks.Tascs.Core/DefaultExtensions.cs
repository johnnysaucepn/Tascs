using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public static class DefaultExtensions
    {
        public static Target Exec(this Target target, string command, params Arg[] cmdParams)
        {
            target.AddTasc(new ExecTasc(command, cmdParams)
            {
                RunWindowed = false
            });

            return target;
        }

        public static Target ExecWindowed(this Target target, string command, params Arg[] cmdParams)
        {
            target.AddTasc(new ExecTasc(command, cmdParams)
            {
                RunWindowed = true
            });

            return target;
        }

        private static string QuoteIfWhitespace(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            return s.Any(char.IsWhiteSpace) ? String.Format(@"""{0}""", s) : s;
        }
    }
}
