using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Howatworks.Tascs.Core;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using Microsoft.Build.Execution;

namespace Howatworks.Tascs.MSBuild
{
    public static class MSBuildExtensions
    {
        public static Target BuildProject(this Target target, string projectFilePath, string outputPath, string configuration = "Release", string platform = "AnyCPU", string targets = "Clean,Build")
        {
            var loggers = new List<ILogger> { new ConsoleLogger() };

            string projectFile = PathUtils.Resolve(projectFilePath);

            var globalProperty = new Dictionary<string, string>
            {
                {"Configuration", configuration},
                {"Platform", platform},
                {"OutputPath", PathUtils.Resolve(outputPath)}
            };

            try
            {
                var buildRequest = new BuildRequestData(projectFile, globalProperty, null, targets.Split(','), null);

                //register file logger using BuildParameters
                var buildParams = new BuildParameters { Loggers = loggers };

                //build solution
                var buildResult = BuildManager.DefaultBuildManager.Build(buildParams, buildRequest);
            }
            finally
            {
            }

            return target;
        }

        public static Target UpdateVersionNumber(this Target target, int major, int minor, int build, int revision)
        {
            
            return target;
        }
    }
}
