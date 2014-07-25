using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32.SafeHandles;
using TopologicalSorting;

namespace Topological_Sorting_Test
{
    [TestClass]
    public class Ordering
    {
        /// <summary>
        ///     Test that a simple A->B->C graph works with every possible restriction on ordering
        /// </summary>
        [TestMethod]
        public void BasicOrderAfter()
        {
            var g = new DependencyGraph<string>();

            var a = new OrderedProcess<string>(g, "A");
            var b = new OrderedProcess<string>(g, "B");
            var c = new OrderedProcess<string>(g, "C");

            a.Before(b).Before(c);

            c.After(b).After(a);

            IEnumerable<IEnumerable<OrderedProcess<string>>> s = g.CalculateSort();

            Assert.AreEqual(1, s.Skip(0).First().Count());
            Assert.AreEqual(a, s.Skip(0).First().First());

            Assert.AreEqual(1, s.Skip(1).First().Count());
            Assert.AreEqual(b, s.Skip(1).First().First());

            Assert.AreEqual(1, s.Skip(2).First().Count());
            Assert.AreEqual(c, s.Skip(2).First().First());
        }

        /// <summary>
        ///     Test that a simple A->B->C graph works with minimum restrictions on ordering
        /// </summary>
        [TestMethod]
        public void BasicOrderBefore()
        {
            var g = new DependencyGraph<int>();

            var a = new OrderedProcess<int>(g, 1);
            var b = new OrderedProcess<int>(g, 2);
            var c = new OrderedProcess<int>(g, 3);

            a.Before(b).Before(c);

            IEnumerable<IEnumerable<OrderedProcess<int>>> s = g.CalculateSort();

            Assert.AreEqual(1, s.Skip(0).First().Count());
            Assert.AreEqual(a, s.Skip(0).First().First());

            Assert.AreEqual(1, s.Skip(1).First().Count());
            Assert.AreEqual(b, s.Skip(1).First().First());

            Assert.AreEqual(1, s.Skip(2).First().Count());
            Assert.AreEqual(c, s.Skip(2).First().First());
        }

        /// <summary>
        ///     Tests that an impossible ordering constraint is detected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Unorderable()
        {
            var g = new DependencyGraph<decimal>();

            var a = new OrderedProcess<decimal>(g, 1.23m);
            var b = new OrderedProcess<decimal>(g, 2.46m);

            a.Before(b);
            b.Before(a);

            g.CalculateSort();
        }

        /// <summary>
        ///     Test that a graph with a split in the middle will order properly
        /// </summary>
        [TestMethod]
        public void BasicBranching()
        {
            var g = new DependencyGraph<DateTime>();

            var a = new OrderedProcess<DateTime>(g, DateTime.Today);
            var b1 = new OrderedProcess<DateTime>(g, DateTime.Today.AddDays(1));
            var b2 = new OrderedProcess<DateTime>(g, DateTime.Today.AddDays(2));
            var c = new OrderedProcess<DateTime>(g, DateTime.Today.AddDays(3));

            a.Before(b1, b2).Before(c);

            IEnumerable<IEnumerable<OrderedProcess<DateTime>>> s = g.CalculateSort();

            Assert.AreEqual(1, s.Skip(0).First().Count());
            Assert.AreEqual(a, s.Skip(0).First().First());

            Assert.AreEqual(2, s.Skip(1).First().Count());
            Assert.IsTrue(s.Skip(1).First().Contains(b1));
            Assert.IsTrue(s.Skip(1).First().Contains(b2));

            Assert.AreEqual(1, s.Skip(2).First().Count());
            Assert.AreEqual(c, s.Skip(2).First().First());
        }

        /// <summary>
        ///     Test a complex branching scheme
        /// </summary>
        [TestMethod]
        public void ComplexBranching()
        {
            var g = new DependencyGraph<char>();

            var a = new OrderedProcess<char>(g, 'A');
            var b1 = new OrderedProcess<char>(g, 'B');
            var b2 = new OrderedProcess<char>(g, 'b');
            var c1 = new OrderedProcess<char>(g, 'C');
            var c2 = new OrderedProcess<char>(g, 'c');
            var c3 = new OrderedProcess<char>(g, 'C');
            var c4 = new OrderedProcess<char>(g, 'c');
            var d = new OrderedProcess<char>(g, 'D');

            a.Before(b1, b2).Before(c1, c2, c3, c4).Before(d);

            IEnumerable<IEnumerable<OrderedProcess<char>>> s = g.CalculateSort();

            Assert.AreEqual(1, s.Skip(0).First().Count());
            Assert.AreEqual(a, s.Skip(0).First().First());

            Assert.AreEqual(2, s.Skip(1).First().Count());
            Assert.IsTrue(s.Skip(1).First().Contains(b1));
            Assert.IsTrue(s.Skip(1).First().Contains(b2));

            Assert.AreEqual(4, s.Skip(2).First().Count());
            Assert.IsTrue(s.Skip(2).First().Contains(c1));
            Assert.IsTrue(s.Skip(2).First().Contains(c2));
            Assert.IsTrue(s.Skip(2).First().Contains(c3));
            Assert.IsTrue(s.Skip(2).First().Contains(c4));

            Assert.AreEqual(1, s.Skip(3).First().Count());
            Assert.AreEqual(d, s.Skip(3).First().First());
        }

        /// <summary>
        ///     Tests that a complex branching system with an impossible constraint is detected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void BranchingUnorderable()
        {
            var g = new DependencyGraph<bool>();

            var a = new OrderedProcess<bool>(g, true);
            var b1 = new OrderedProcess<bool>(g, true);
            var b2 = new OrderedProcess<bool>(g, false);
            var c1 = new OrderedProcess<bool>(g, true);
            var c2 = new OrderedProcess<bool>(g, false);
            var c3 = new OrderedProcess<bool>(g, true);
            var c4 = new OrderedProcess<bool>(g, false);
            var d = new OrderedProcess<bool>(g, true);

            a.Before(b1, b2).Before(c1, c2, c3, c4).Before(d).Before(b1);

            g.CalculateSort();
        }

        [TestMethod]
        public void ExcludeIrrelevant()
        {
            var gr = new DependencyGraph<string>();

            var a = new OrderedProcess<string>(gr, "A");
            var b = new OrderedProcess<string>(gr, "B");
            var c = new OrderedProcess<string>(gr, "C");
            var d = new OrderedProcess<string>(gr, "D");
            var e = new OrderedProcess<string>(gr, "E");
            var f = new OrderedProcess<string>(gr, "F");
            var g = new OrderedProcess<string>(gr, "G");

            a.Before(c, d);
            b.Before(d, e);
            c.Before(f, g);
            d.Before(g);

            StringBuilder sb = new StringBuilder();
            IEnumerable<IEnumerable<OrderedProcess<string>>> sortedProcesses = gr.CalculateSort();



            foreach (var depSet in sortedProcesses)
            {
                foreach (var dep in depSet)
                {
                    sb.Append(dep.Item).Append(",");
                }
            }
            var output = sb.ToString();
            Console.Write(output);

        }
    }
}