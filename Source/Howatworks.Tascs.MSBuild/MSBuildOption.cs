using System.ComponentModel;

namespace Howatworks.Tascs.MSBuild
{
    public enum MSBuildOption
    {
                                        ProjectFilePath,
                                        OutputPath,
        [DefaultValue("Release")]       Configuration,
        [DefaultValue("AnyCPU")]        Platform,
        [DefaultValue("Clean,Build")]   Targets
    }

    
}
