using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public interface ITascResult
    {
        
    }

    public interface ITasc
    {
        ITascResult Execute();
        void Cleanup();

    }
}
