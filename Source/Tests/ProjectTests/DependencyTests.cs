using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Howatworks.Tascs.Core;

namespace ProjectTests
{
    [TestClass]
    public class DependencyTests
    {
        [TestMethod]
        public void Topology()
        {

            TascProject.Instance
                .Target("Build")
                .Echo("Build");

            TascProject.Instance
                .Target("Deploy")
                .DependsOn("Build")
                .Echo("Deploy");

            TascProject.Instance
                .Target("Run")
                .DependsOn("Deploy")
                .Echo("Run");

            TascProject.Instance
                .Target("Report")
                .DependsOn("Run")
                .Echo("Report");

            TascProject.Instance.Build("Report");
        }
    }
}
