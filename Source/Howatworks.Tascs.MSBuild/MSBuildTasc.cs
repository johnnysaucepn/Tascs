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
    public class MSBuildTasc : ITasc
    {
        protected TascOptions<MSBuildOption> Options { get; set; }

        public MSBuildTasc(TascOptions<MSBuildOption> options)
        {
            Options = options;
        }

        public ITascResult Execute()
        {
            var loggers = new List<ILogger> { new ConsoleLogger() };

            string projectFile = PathUtils.Resolve(Options[MSBuildOption.ProjectFilePath]);

            var globalProperty = new Dictionary<string, string>
            {
                {"Configuration", Options[MSBuildOption.Configuration]},
                {"Platform", Options[MSBuildOption.Platform]},
                {"OutputPath", PathUtils.Resolve(Options[MSBuildOption.OutputPath])}
            };

            var targets = Options[MSBuildOption.Targets].Split(',').Select(x => x.Trim());

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


        public void Cleanup()
        {
            
        }
    }
}
