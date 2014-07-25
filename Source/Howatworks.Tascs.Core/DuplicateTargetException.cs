using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class DuplicateTargetException : Exception
    {
        private readonly string _name;

        public DuplicateTargetException(string name)
        {
            _name = name;
        }

        public override string Message
        {
            get { return string.Format("Target '{0}' already exists", _name); }
        }
    }
}
