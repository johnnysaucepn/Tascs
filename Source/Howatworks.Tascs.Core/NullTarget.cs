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

        public event EventHandler<GenerateExecutionContextArgs> ApplyProjectSettingsToExecutionContext;

        public NullTarget(string name)
        {
            Name = name;
            ApplyProjectSettingsToExecutionContext += (sender, args) => { throw new NotImplementedException(string.Format("The TascTarget \"{0}\" has not been defined yet", Name)); };
        }

        public ITascResult Execute()
        {
            throw new NotImplementedException(string.Format("The TascTarget \"{0}\" has not been defined yet", Name));
        }
        
        public ITascTarget Do(Tasc tasc)
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
