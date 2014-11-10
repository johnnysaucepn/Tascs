using System;
using Howatworks.Tascs.Core;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Howatworks.Tascs.MSBuild
{
    public class MSBuildTasc : Tasc
    {
        public string ProjectFile { get; set; }
    
        protected MSBuildOptions Options { get; set; }

        public MSBuildTasc(string projectFile, MSBuildOptions options = null)
        {
            ProjectFile = projectFile;
            Options = options ?? new MSBuildOptions();
        }

        public override ITascResult Execute(TascContext context)
        {
            var loggers = new List<ILogger> { new ConsoleLogger() };

            var projectFile = PathUtils.Resolve(TascProject.Instance.Root, ProjectFile);

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
                var buildParams = new BuildParameters {Loggers = loggers};

                //build solution
                var buildResult = BuildManager.DefaultBuildManager.Build(buildParams, buildRequest);
                return new MSBuildResult(buildResult);
            }
            catch (Exception)
            {
                return TascResult.Fail;
            }
            
        }
    }
}
