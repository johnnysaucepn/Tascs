using System.Collections.Generic;

namespace Howatworks.Tascs.Core
{
    public class TascTarget : ITascTarget
    {
        private readonly IList<Tasc> _tascs = new List<Tasc>();

        private TascTarget()
        {
        }

        public string Name { get; set; }

        public static TascTarget Create(string name)
        {
            return new TascTarget { Name = name };
        }

        public ITascResult Build()
        {
            ITascResult result = null;

            foreach (var tasc in _tascs)
            {
                try
                {
                    result = tasc.Execute();
                }
                finally
                {
                    tasc.Cleanup();
                }
            }
            return result;
        }

        public TascTarget DependsOn(string dependency)
        {
            // TODO: identify circular references

            TascProject.Instance.AddDependency(this, dependency);

            return this;
        }

        public TascTarget Do(Tasc tasc)
        {
            _tascs.Add(tasc);
            return this;
        }

        
    }
}