using System.Collections.Generic;
using Howatworks.Tascs.Core;

namespace Howatworks.Tascs.MSBuild
{
    public static class MSBuildExtensions
    {
        public static ITascTarget BuildProject(this ITascTarget tascTarget, string projectFile=null, string target=null, string configuration=null, string platform=null, string outputPath=null, IDictionary<string, string> flags=null)
        {
            return tascTarget.Do(new MSBuildTasc(projectFile, target, configuration,platform,outputPath,flags));
        }

        public static ITascResult BuildProject(this TascContext context, string projectFile = null, string target = null, string configuration = null, string platform = null, string outputPath = null, IDictionary<string, string> flags = null)
        {
            return new MSBuildTasc(projectFile, target, configuration, platform, outputPath, flags).Execute(context);
        }

    }
}
