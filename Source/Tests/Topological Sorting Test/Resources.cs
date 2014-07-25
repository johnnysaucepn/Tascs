﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TopologicalSorting;

namespace Topological_Sorting_Test
{
    [TestClass]
    public class Resources
    {
        /// <summary>
        ///     tests if basic resource resolution works
        /// </summary>
        [TestMethod]
        public void BasicResourceResolution()
        {
            var g = new DependencyGraph<int>();

            var res = new Resource<int>(g, 123);

            var a = new OrderedProcess<int>(g, 1);
            var b = new OrderedProcess<int>(g, 2);
            var c = new OrderedProcess<int>(g, 3);

            a.Before(b);
            a.Before(c);

            b.Requires(res);
            c.Requires(res);

            IEnumerable<IEnumerable<OrderedProcess<int>>> s = g.CalculateSort();

            Assert.AreEqual(3, s.Count());

            Assert.AreEqual(1, s.Skip(0).First().Count());
            Assert.AreEqual(a, s.Skip(0).First().First());

            Assert.AreEqual(1, s.Skip(1).First().Count());
            Assert.IsTrue(s.Skip(1).First().First() == b || s.Skip(1).First().First() == c);

            Assert.AreEqual(1, s.Skip(0).First().Count());
            Assert.IsTrue(s.Skip(2).First().First() == b || s.Skip(2).First().First() == c);

            Assert.AreNotEqual(s.Skip(1).First().First(), s.Skip(2).First().First());
        }

        /// <summary>
        ///     Test if resource resolution works on a complex branching graph
        /// </summary>
        [TestMethod]
        public void BranchingResourceResolution()
        {
            var g = new DependencyGraph<string>();

            var a = new OrderedProcess<string>(g, "A");
            var b1 = new OrderedProcess<string>(g, "B1");
            var b2 = new OrderedProcess<string>(g, "B2");
            var c1 = new OrderedProcess<string>(g, "C1");
            var c2 = new OrderedProcess<string>(g, "C2");
            var c3 = new OrderedProcess<string>(g, "C3");
            var c4 = new OrderedProcess<string>(g, "C4");
            var d = new OrderedProcess<string>(g, "D");

            a.Before(b1, b2).Before(c1, c2, c3, c4).Before(d);

            var resource = new Resource<string>(g, "Resource");
            resource.UsedBy(c1, c3);

            IEnumerable<IEnumerable<OrderedProcess<string>>> s = g.CalculateSort();

            //check that A comes first
            Assert.AreEqual(1, s.Skip(0).First().Count());
            Assert.AreEqual(a, s.Skip(0).First().First());

            //check that D comes last
            Assert.AreEqual(1, s.Skip(4).First().Count());
            Assert.AreEqual(d, s.Skip(4).First().First());

            //check that no set contains both c1 and c3
            Assert.AreEqual(0, s.Count(set => set.Contains(c1) && set.Contains(c3)));
        }
    }
}