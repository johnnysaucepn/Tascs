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
        public static Target BuildProject(this Target target, string projectFilePath, string outputPath)
        {
            var options = new MSBuildOptions()
            {
                ProjectFile = projectFilePath,
                OutputFolder = outputPath
            };
            target.AddTasc(new MSBuildTasc(options));

            return target;
            
        }

        public static Target BuildProject(this Target target, MSBuildOptions options)
        {
            target.AddTasc(new MSBuildTasc(options));

            return target;

        }

    }
}
