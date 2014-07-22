using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public interface ITasc
    {
        TascOptions Options { get; set; }
        void Run();
    }
}
