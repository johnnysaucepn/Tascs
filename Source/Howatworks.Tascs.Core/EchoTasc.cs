using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class EchoTasc : Tasc
    {
        public string Line { get; protected set; }

        public EchoTasc(string line)
        {
            Line = line;
        }


        protected override ITascResult ExecuteThisTasc()
        {
            Console.WriteLine(Line);
            return null;
        }

        public override void Cleanup()
        {
            
        }

    }
}
