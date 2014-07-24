using System.Collections.Generic;

namespace Howatworks.Tascs.Core
{
    public class Target
    {
        private readonly IList<Target> _postDependencies = new List<Target>();
        private readonly IList<Target> _preDependencies = new List<Target>();
        private readonly IList<ITasc> _tascs = new List<ITasc>();

        private Target()
        {
        }

        public string Name { get; set; }

        public static Target Named(string name)
        {
            var newTarget = new Target {Name = name};
            return newTarget;
        }

        public void AddTasc(ITasc tasc)
        {
            _tascs.Add(tasc);
        }

        public ITascResult Execute()
        {
            ITascResult result = null;
            foreach (Target dep in _preDependencies)
            {
                result = dep.Execute();
            }
            try
            {
                foreach (ITasc tasc in _tascs)
                {
                    result = tasc.Execute();
                }
            }
            finally
            {
                foreach (ITasc tasc in _tascs)
                {
                    tasc.Cleanup();
                }
            }

            foreach (Target dep in _postDependencies)
            {
                result = dep.Execute();
            }

            return result;
        }

        public Target DependsOn(Target dependency)
        {
            // TODO: identify circular references

            if (!_preDependencies.Contains(dependency) && ! _postDependencies.Contains(dependency))
            {
                _preDependencies.Add(dependency);
            }
            return this;
        }

        public Target PostDependsOn(Target dependency)
        {
            // TODO: identify circular references

            if (!_postDependencies.Contains(dependency) && !_postDependencies.Contains(dependency))
            {
                _postDependencies.Add(dependency);
            }

            return this;
        }
    }
}