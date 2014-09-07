using Howatworks.Tascs.Core;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.MSBuild
{
    public class MSBuildTasc : Tasc
    {
        protected MSBuildOptions Options { get; set; }

        public MSBuildTasc(MSBuildOptions options)
        {
            Options = options;
        }

        public override ITascResult Execute()
        {
            var loggers = new List<ILogger> { new ConsoleLogger() };

            string projectFile = PathUtils.Resolve(TascProject.Instance.Root, Options.ProjectFile);

            var globalProperty = new Dictionary<string, string>
            {
                {"Configuration", Options.Configuration},
                {"Platform", Options.Platform},
                {"OutputPath", PathUtils.Resolve(TascProject.Instance.Root, Options.OutputFolder)}
            };

            var targets = Options.BuildTargets.Split(',').Select(x => x.Trim());

            try
            {
                var buildRequest = new BuildRequestData(projectFile, globalProperty, null, targets.ToArray(), null);

                //register file logger using BuildParameters
                var buildParams = new BuildParameters { Loggers = loggers };

                //build solution
                var buildResult = BuildManager.DefaultBuildManager.Build(buildParams, buildRequest);
                return buildResult as ITascResult;
            }
            finally
            {
            }
        }

    }
}
