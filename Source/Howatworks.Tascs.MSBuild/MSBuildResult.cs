using Howatworks.Tascs.Core;
using Microsoft.Build.Execution;

namespace Howatworks.Tascs.MSBuild
{
    public class MSBuildResult : ITascResult
    {
        public BuildResult BuildResult { get; set; }

        public MSBuildResult(BuildResult buildResult)
        {
            BuildResult = buildResult;
        }
    }
}
