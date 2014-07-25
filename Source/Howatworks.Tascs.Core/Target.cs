using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TopologicalSorting;

namespace Howatworks.Tascs.Core
{
    public class Target
    {
        private static readonly Dictionary<string, Target> KnownTargets = new Dictionary<string, Target>();

        private readonly DependencyGraph<Target> _dependencyGraph = new DependencyGraph<Target>();
        private readonly IList<ITasc> _tascs = new List<ITasc>();
        private readonly OrderedProcess<Target> _thisProcess;

        private Target()
        {
            _thisProcess = new OrderedProcess<Target>(_dependencyGraph, this);
        }

        public string Name { get; protected set; }

        public static Target Create(string name)
        {
            var uncasedName = name.ToLower();
            if (KnownTargets.ContainsKey(uncasedName))
            {
                throw new DuplicateTargetException(name);
            }

            var newTarget = new Target {Name = name};
            KnownTargets[uncasedName] = newTarget;

            return newTarget;
        }

        public static Target Named(string name)
        {
            var uncasedName = name.ToLower();
            if (KnownTargets.ContainsKey(uncasedName))
            {
                return KnownTargets[uncasedName];
            }

            throw new TargetNotFoundException(name);
        }

        public static void Execute(params string[] targetNames)
        {
            // TODO: proper graph dependency

            foreach (var name in targetNames)
            {
                Target.Named(name).Execute();
            }

        }

        public void AddTasc(ITasc tasc)
        {
            _tascs.Add(tasc);
        }

        public ITascResult Execute()
        {
            ITascResult result = null;
            // Ensure all dependent targets are built first

            IEnumerable<IEnumerable<OrderedProcess<Target>>> sortedProcesses = _dependencyGraph.CalculateSort();
 
            
            foreach (var depSet in sortedProcesses)
            {
                foreach (var dep in depSet.TakeWhile(dep => dep != _thisProcess))
                {
                    result = dep.Item.Execute();
                }
            }
            try
            {
                foreach (var tasc in _tascs)
                {
                    result = tasc.Execute();
                }
            }
            finally
            {
                foreach (var tasc in _tascs)
                {
                    tasc.Cleanup();
                }
            }

            return result;
        }

        public Target DependsOn(Target dependency)
        {
            // TODO: identify circular references

            var dependentProcess = new OrderedProcess<Target>(_dependencyGraph, dependency);

            dependentProcess.Before(_thisProcess);

            return this;
        }
    }
}