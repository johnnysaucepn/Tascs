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
            target.AddTasc(new MSBuildTasc(new TascOptions<MSBuildOption>
            {
                {MSBuildOption.ProjectFilePath, projectFilePath},
                {MSBuildOption.OutputPath, outputPath},
                {MSBuildOption.Configuration, configuration},
                {MSBuildOption.Platform, platform},
                {MSBuildOption.Targets, targets}
            }));

            return target;
            
        }

        public static Target BuildProject(this Target target, TascOptions<MSBuildOption> options)
        {
            target.AddTasc(new MSBuildTasc(options));

            return target;

        }

    }
}
