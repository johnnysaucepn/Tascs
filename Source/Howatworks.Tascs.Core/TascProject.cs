using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using Howatworks.Tascs.Core.Exec;
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

        public readonly dynamic ProjectProperties = new ExpandoObject();

        private readonly IDictionary<string, ITascTarget> _targets = new Dictionary<string, ITascTarget>();

        private readonly AdjacencyGraph<string, SEdge<string>> _dependencies = new AdjacencyGraph<string, SEdge<string>>();


        private TascProject()
        {

        }

        public TascProject BasePath(string relativePath)
        {
            Root = PathUtils.Resolve(Directory.GetCurrentDirectory(), @"..\..\..\..");
            return this;
        }

        public ITascTarget Target(string name)
        {
            var target = TascTarget.Create(name);
            target.ApplyProjectSettingsToExecutionContext += TargetOnApplyProjectSettingsToExecutionContext;
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

        private void TargetOnApplyProjectSettingsToExecutionContext(object sender, GenerateExecutionContextArgs args)
        {
            var projectPropertyDictionary = (IDictionary<string, object>) ProjectProperties;
            var contextPropertyDictionary = (IDictionary<string, object>) args.Context.Properties;
            foreach (var key in projectPropertyDictionary.Keys.Where(key => (!contextPropertyDictionary.ContainsKey(key) || contextPropertyDictionary[key] == null)))
            {
                args.Context.Properties[key] = ProjectProperties[key];
            }
        }

        public void AddDependency(ITascTarget tascTarget, string dependentTargetName)
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
            var subgraph = GetDependencyGraph(targetNames);

            foreach (var target in subgraph.TopologicalSort().Reverse())
            {
                _targets[target].Execute();
            }
        }

        private AdjacencyGraph<string, SEdge<string>> GetDependencyGraph(IEnumerable<string> targetNames)
        {
            var baseTargetNames = targetNames.Where(_targets.ContainsKey);
            
            var subgraph = new AdjacencyGraph<string, SEdge<string>>();

            foreach (var baseTarget in baseTargetNames)
            {
                var search = new BreadthFirstSearchAlgorithm<string, SEdge<string>>(_dependencies);
                subgraph.AddVertex(baseTarget);
                search.SetRootVertex(baseTarget);
                search.TreeEdge += x =>
                {
                    Console.WriteLine("{0}->{1}", x.Source, x.Target);
                    subgraph.AddVerticesAndEdge(x);
                };
                search.Compute();
            }
            return subgraph;
        }

        public void Build(params string[] targetNames)
        {
            Build(targetNames.AsEnumerable());
        }

    }
}