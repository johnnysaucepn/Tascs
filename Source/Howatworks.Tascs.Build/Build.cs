using System.Collections.Generic;
using Howatworks.Tascs.Core;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;

namespace Howatworks.Tascs.Build
{
    public class Build : ITasc
    {
        public Build(TascOptions options)
        {
            Options = options;
        }

        public TascOptions Options { get; set; }

        public void Run()
        {
            string projectFilePath = Options["Project"];
            string outputPath = Options["Output"];
            string configuration = Options["Configuration"] ?? "Release";
            string platform = Options["Platform"] ?? "AnyCPU";
            string targets = Options["Targets"] ?? "Clean,Build";

            var loggers = new List<ILogger> {new ConsoleLogger()};

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
                var buildParams = new BuildParameters {Loggers = loggers};

                //build solution
                BuildResult buildResult = BuildManager.DefaultBuildManager.Build(buildParams, buildRequest);
            }
            finally
            {
            }
        }
    }
}