using System;
using System.Collections.Generic;

namespace Howatworks.Tascs.Core
{
    public class GenerateExecutionContextArgs : EventArgs
    {
        public TascContext Context { get; set; }

        public GenerateExecutionContextArgs(TascContext context)
        {
            Context = context;
        }
    }

    public class TascTarget : ITascTarget
    {
        private readonly IList<Tasc> _tascs = new List<Tasc>();

        private TascTarget()
        {
        }

        public string Name { get; set; }
        public event EventHandler<GenerateExecutionContextArgs> ApplyProjectSettingsToExecutionContext = delegate { };

        public static ITascTarget Create(string name)
        {
            return new TascTarget { Name = name };
        }

        public ITascResult Execute()
        {
            var context = new TascContext(this);

            ApplyProjectSettingsToExecutionContext(this, new GenerateExecutionContextArgs(context));

            ITascResult result = null;

            foreach (var tasc in _tascs)
            {
                try
                {
                    result = tasc.Execute(context);
                }
                finally
                {
                    tasc.Cleanup();
                }
            }
            return result;
        }

        public ITascTarget DependsOn(string dependency)
        {
            // TODO: identify circular references

            TascProject.Instance.AddDependency(this, dependency);

            return this;
        }

        public ITascTarget Do(Tasc tasc)
        {
            _tascs.Add(tasc);
            return this;
        }

    }
}