using System.Collections.Generic;
using Howatworks.Tascs.Core;

namespace Howatworks.Tascs.MSBuild
{
    public static class MSBuildExtensions
    {
        public static TascProject UseMSBuild(this TascProject project, string target = null, string configuration = null, string platform = null, string outputPath = null)
        {
            if (!project.ProjectProperties.ContainsKey(MSBuildOption.Target)) project.ProjectProperties.Add(MSBuildOption.Target, null);
            if (!project.ProjectProperties.ContainsKey(MSBuildOption.Configuration)) project.ProjectProperties.Add(MSBuildOption.Configuration, null);
            if (!project.ProjectProperties.ContainsKey(MSBuildOption.Platform)) project.ProjectProperties.Add(MSBuildOption.Platform, null);
            if (!project.ProjectProperties.ContainsKey(MSBuildOption.OutputFolder)) project.ProjectProperties.Add(MSBuildOption.OutputFolder, null);

            if (target != null) project.ProjectProperties[MSBuildOption.Target] = target;
            if (configuration != null) project.ProjectProperties[MSBuildOption.Configuration] = configuration;
            if (platform != null) project.ProjectProperties[MSBuildOption.Platform] = platform;
            if (outputPath != null) project.ProjectProperties[MSBuildOption.OutputFolder] = outputPath;
            return project;
        }

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
