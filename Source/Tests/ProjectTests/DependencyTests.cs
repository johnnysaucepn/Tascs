using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Howatworks.Tascs.Core;

namespace ProjectTests
{
    [TestClass]
    public class DependencyTests
    {
        [TestInitialize]
        public void TreeGraph()
        {
            TascProject.Instance.Target("A").Echo("A");
            TascProject.Instance.Target("AA").DependsOn("A").Echo("AA");
            TascProject.Instance.Target("AAA").DependsOn("AA").Echo("AAA");
            TascProject.Instance.Target("AB").DependsOn("A").Echo("AB");
            TascProject.Instance.Target("AC").DependsOn("A").Echo("AC");
            TascProject.Instance.Target("B").Echo("B");
            TascProject.Instance.Target("BA").DependsOn("B").Echo("BA");
        }

        
        [TestMethod]
        public void Linear()
        {
            TascProject.Instance.Target("Build").Echo("Build");
            TascProject.Instance.Target("Deploy").DependsOn("Build").Echo("Deploy");
            TascProject.Instance.Target("Run").DependsOn("Deploy").Echo("Run");
            TascProject.Instance.Target("Report").DependsOn("Run").Echo("Report");
            TascProject.Instance.Build("Report");
        }

        [TestMethod]
        public void Tree1()
        {
            TascProject.Instance.Build("AAA");
        }

        [TestMethod]
        public void Tree2()
        {
            TascProject.Instance.Build("BA");
        }

        [TestMethod]
        public void Tree3()
        {
            TascProject.Instance.Build("AC");
        }
    }
}
