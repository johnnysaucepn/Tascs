using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public abstract class Tasc
    {
        private Tasc _successor;

        public Tasc Then(Tasc successor)
        {
            _successor = successor;
            return this;
        }

        protected abstract ITascResult ExecuteThisTasc();

        public ITascResult Execute()
        {
            ExecuteThisTasc();
            return _successor.Execute();
        }

        public virtual void Cleanup()
        {
            
        }
    }
}
