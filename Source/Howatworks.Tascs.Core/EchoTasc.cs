using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class EchoTasc : ITasc
    {
        public string Line { get; protected set; }

        public EchoTasc(string line)
        {
            Line = line;
        }


        public ITascResult Execute()
        {
            Console.WriteLine(Line);
            return null;
        }

        public void Cleanup()
        {
            
        }
    }
}
