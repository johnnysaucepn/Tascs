using Howatworks.Tascs.Core;

namespace Howatworks.Tascs.MSBuild
{
    public static class MSBuildExtensions
    {
        public static ITascTarget BuildProject(this ITascTarget target, string projectFile, MSBuildOptions options = null)
        {
            return target.Do(new MSBuildTasc(projectFile, options));
        }

    }
}
