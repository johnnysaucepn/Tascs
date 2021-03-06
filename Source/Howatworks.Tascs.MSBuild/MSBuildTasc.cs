﻿using System;
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

        public string Target { get; set; }
        public string Configuration{ get; set; }
        public string Platform { get; set; }
        public string OutputPath { get; set; }

        public IDictionary<string,string> Flags { get; set; }

        public MSBuildTasc(string projectFile, string target = null, string configuration = null, string platform = null, string outputPath = null, IDictionary<string, string> flags = null)
        {
            ProjectFile = projectFile;
            Target = target ?? "Build";
            Configuration = configuration ?? "Release";
            Platform = platform ?? "x86";
            OutputPath = outputPath;
            Flags = flags ?? new Dictionary<string, string>();
        }

        public override ITascResult Execute(TascContext context)
        {
            Target = Target ?? context.Properties[MSBuildOption.OutputFolder].ToString();
            Configuration = Configuration ?? context.Properties[MSBuildOption.Configuration].ToString();
            Platform = Platform ?? context.Properties[MSBuildOption.Platform].ToString();
            OutputPath = OutputPath ?? context.Properties[MSBuildOption.OutputFolder].ToString();

            var loggers = new List<ILogger> { new ConsoleLogger() };

            var projectFile = PathUtils.Resolve(TascProject.Instance.Root, ProjectFile);

            var globalProperty = new Dictionary<string, string>
            {
                {"Configuration", Configuration},
                {"Platform", Platform}
            };
            if (!string.IsNullOrEmpty(OutputPath))
            {
                globalProperty["OutputPath"] = PathUtils.Resolve(TascProject.Instance.Root, OutputPath);
            }

            var targets = Target.Split(',').Select(x => x.Trim());

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
