using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class TascResult : ITascResult
    {
        public static readonly TascResult Pass = new TascResult();
        public static readonly TascResult Fail = new TascResult();
        public static readonly TascResult Inconclusive = new TascResult();

    }

    
}
