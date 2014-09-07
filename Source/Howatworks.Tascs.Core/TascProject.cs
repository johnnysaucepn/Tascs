using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;

namespace Howatworks.Tascs.Core
{
    public class TascProject
    {
        private static readonly Lazy<TascProject> _instance = new Lazy<TascProject>(
            () => new TascProject());


        public static TascProject Instance
        {
            get { return _instance.Value; }
        }

        public string Root { get; set; }
        private readonly IDictionary<string, ITascTarget> _targets = new Dictionary<string, ITascTarget>();

        private readonly AdjacencyGraph<string, SEdge<string>> _dependencies = new AdjacencyGraph<string, SEdge<string>>();


        private TascProject()
        {

        }

        public TascTarget Target(string name)
        {
            var target = TascTarget.Create(name);
            if (_targets.ContainsKey(name))
            {
                _targets.Remove(name);
            }
            _targets.Add(name, target);
            if (!_dependencies.ContainsVertex(name))
            {
                _dependencies.AddVertex(name);
            }
            return target;
        }

        internal void AddDependency(ITascTarget tascTarget, string dependentTargetName)
        {
            ITascTarget dependencyTarget;
            if (!_targets.ContainsKey(dependentTargetName))
            {
                dependencyTarget = new NullTarget(dependentTargetName);
                _targets.Add(dependentTargetName, dependencyTarget);
            }
            else
            {
                dependencyTarget = _targets[dependentTargetName];

            }

            _dependencies.AddVerticesAndEdge(new SEdge<string>(tascTarget.Name, dependencyTarget.Name));
        }

        public void Build(IEnumerable<string> targetNames)
        {
            var baseTargetNames = targetNames.Where(_targets.ContainsKey);
            //var baseTargets = baseTargetNames
            //    .Select(x => _targets[x])
            //    .ToList();

            foreach (var target in _dependencies.TopologicalSort().Reverse())
            {
                //Console.WriteLine(target);
                _targets[target].Build();
            }

        }

        public void Build(params string[] targetNames)
        {
            Build(targetNames.AsEnumerable());
        }

    }
}