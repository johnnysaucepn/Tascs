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
            return target.BuildProject(new TascOptions<MSBuildOption>
            {
                {MSBuildOption.ProjectFilePath, projectFilePath},
                {MSBuildOption.OutputPath, outputPath},
                {MSBuildOption.Configuration, configuration},
                {MSBuildOption.Platform, platform},
                {MSBuildOption.Targets, targets}
            });
            
        }

        public static Target BuildProject(this Target target, TascOptions<MSBuildOption> options)
        {
            var loggers = new List<ILogger> { new ConsoleLogger() };

            string projectFile = PathUtils.Resolve(options[MSBuildOption.ProjectFilePath]);

            var globalProperty = new Dictionary<string, string>
            {
                {"Configuration", options[MSBuildOption.Configuration]},
                {"Platform", options[MSBuildOption.Platform]},
                {"OutputPath", PathUtils.Resolve(options[MSBuildOption.OutputPath])}
            };

            var targets = options[MSBuildOption.Targets].Split(',').Select(x => x.Trim());

            try
            {
                var buildRequest = new BuildRequestData(projectFile, globalProperty, null, targets.ToArray(), null);

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

    }
}
