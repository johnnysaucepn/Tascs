using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework.XamlTypes;
using Microsoft.Build.Logging;
using Microsoft.Build.Framework;
using Howatworks.Tascs.Core;

namespace Howatworks.Tascs.Build
{
    public class Build : ITasc
    {

        public Build(TascOptions options)
        {
            _options = options;
        }

        private TascOptions _options;

        public TascOptions Options
        {
            get { return _options; }
            set { _options = value; }
        }

        public void Run()
        {
            var projectFilePath = Options["Project"];
            var outputPath = Options["Output"];
            var configuration = Options["Configuration"] ?? "Release";
            var platform = Options["Platform"] ?? "AnyCPU";
            var targets = Options["Targets"] ?? "Clean,Build";

            var loggers = new List<ILogger> {new ConsoleLogger()};

            var projectFile = PathUtils.Resolve(projectFilePath);

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
                var buildResult = BuildManager.DefaultBuildManager.Build(buildParams, buildRequest);
            }
            finally
            {

            }

        }

    }
}
