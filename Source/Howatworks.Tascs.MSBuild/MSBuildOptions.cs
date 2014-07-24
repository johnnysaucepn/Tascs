using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Howatworks.Tascs.Core;

namespace Howatworks.Tascs.MSBuild
{
    public class MSBuildOptions
    {
        public string ProjectFile { get; set; }
        public string OutputFolder { get; set; }
        public string Configuration { get; set; }
        public string Platform { get; set; }
        public string BuildTargets { get; set; }

        public MSBuildOptions()
        {
            Configuration = "Release";
            Platform = "AnyCPU";
            BuildTargets = "Clean,Build";
        }

        public static MSBuildOptions Merge(MSBuildOptions original, MSBuildOptions incoming)
        {
            var newOptions = (MSBuildOptions)original.MemberwiseClone();
            newOptions.ProjectFile = newOptions.ProjectFile ?? incoming.ProjectFile;
            newOptions.OutputFolder = newOptions.OutputFolder ?? incoming.OutputFolder;
            newOptions.Configuration = newOptions.Configuration ?? incoming.Configuration;
            newOptions.Platform = newOptions.Platform ?? incoming.Platform;
            newOptions.BuildTargets = newOptions.BuildTargets ?? incoming.BuildTargets;
            return newOptions;
        }
    }
}
