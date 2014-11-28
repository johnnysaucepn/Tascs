using System.Collections.Generic;
using Howatworks.Tascs.Core;

namespace Howatworks.Tascs.MSBuild
{
    public static class MSBuildExtensions
    {
        public static ITascTarget BuildProject(this ITascTarget tascTarget, string projectFile, string target, string configuration, string platform, string outputPath, IDictionary<string, string> flags)
        {
            return tascTarget.Do(new MSBuildTasc(projectFile, target, configuration,platform,outputPath,flags));
        }

        public static ITascResult BuildProject(this TascContext context, string projectFile, string target, string configuration, string platform, string outputPath, IDictionary<string, string> flags)
        {
            return new MSBuildTasc(projectFile, target, configuration, platform, outputPath, flags).Execute(context);
        }

    }
}
