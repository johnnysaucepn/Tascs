using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    internal class NullTarget : ITascTarget
    {
        public string Name
        {
            get; set;
        }

        public NullTarget(string name)
        {
            Name = name;
        }

        public ITascResult Build()
        {
            throw new NotImplementedException(string.Format("The TascTarget \"{0}\" has not been defined yet", Name));
        }
        
        public ITascTarget Do(Tasc tasc)
        {
            throw new NotImplementedException(string.Format("The TascTarget \"{0}\" has not been defined yet", Name));
        }

        public ITascTarget Do(Action<ITascTarget> action)
        {
            throw new NotImplementedException(string.Format("The TascTarget \"{0}\" has not been defined yet", Name));
        }

        public ITascTarget Do(Func<ITascTarget, ITascResult> action)
        {
            throw new NotImplementedException(string.Format("The TascTarget \"{0}\" has not been defined yet", Name));
        }

        public ITascTarget DependsOn(string dependency)
        {
            // TODO: identify circular references

            TascProject.Instance.AddDependency(this, dependency);

            return this;
        }


        
    }
}
