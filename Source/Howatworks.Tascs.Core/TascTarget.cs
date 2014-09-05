using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using QuickGraph.Algorithms.Observers;
using TopologicalSorting;

namespace Howatworks.Tascs.Core
{
    public class TascTarget
    {
        private readonly DependencyGraph<TascTarget> _dependencyGraph = new DependencyGraph<TascTarget>();
        private readonly IList<Tasc> _tascs = new List<Tasc>();
        private readonly OrderedProcess<TascTarget> _thisProcess;

        private TascTarget()
        {
            //_thisProcess = new OrderedProcess<TascTarget>(_dependencyGraph, this);
        }

        public string Name { get; set; }

        public static TascTarget Create()
        {
            return new TascTarget { };
        }
        
        public static TascTarget Named(string p)
        {
            return new TascTarget { Name = p };
            
        }

        public ITascResult Execute()
        {
            ITascResult result = null;
            // Ensure all dependent targets are built first

            IEnumerable<IEnumerable<OrderedProcess<TascTarget>>> sortedProcesses = _dependencyGraph.CalculateSort();


            foreach (var depSet in sortedProcesses)
            {
                foreach (var dep in depSet.TakeWhile(dep => dep != _thisProcess))
                {
                    result = dep.Item.Execute();
                }
            }
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

        public TascTarget DependsOn(TascTarget dependency)
        {
            // TODO: identify circular references

            var dependentProcess = new OrderedProcess<TascTarget>(_dependencyGraph, dependency);

            dependentProcess.Before(_thisProcess);

            return this;
        }

        public void Do(Tasc tasc)
        {
            _tascs.Add(tasc);
        }

        
    }
}