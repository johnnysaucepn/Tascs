using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Howatworks.Tascs.Core;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using Microsoft.Build.Execution;

namespace Howatworks.Tascs.MSBuild
{
    public static class MSBuildExtensions
    {
        public static TascTarget BuildProject(this TascTarget target, string projectFile, MSBuildOptions options = null)
        {
            target.Do(new MSBuildTasc(projectFile, options));

            return target;

        }

    }
}
