#r "..\Source\Howatworks.Tascs.Core\bin\Release\Howatworks.Tascs.Core.dll"
#r "..\Source\Howatworks.Tascs.MSBuild\bin\Release\Howatworks.Tascs.MSBuild.dll"

using Howatworks.Tascs.Core;
using Howatworks.Tascs.Core.Echo;
using Howatworks.Tascs.Core.Exec;
using Howatworks.Tascs.Core.Generic;
using Howatworks.Tascs.MSBuild;

var project = TascProject.Instance;
// Get the user's current directory, not the path of the script

project.Root = PathUtils.Resolve(Directory.GetCurrentDirectory(), @"..\..\..\monza_3.0.0_jhowat\");
            
var dotNetBuildOptions = new MSBuildOptions
{
    OutputFolder = @"BuildOutput\Release",
    Configuration = @"Release",
    Platform = @"AnyCPU",
    BuildTargets = "Clean, Build"
};

var vsBuildOptions = new MSBuildOptions
{
    OutputFolder = @"BuildOutput\Release",
    Configuration = @"Release",
    Platform = @"Win32",
    BuildTargets = "Build"
};

var vsCleanOptions = new MSBuildOptions
{
    OutputFolder = @"BuildOutput\Release",
    Configuration = @"Release",
    Platform = @"Win32",
    BuildTargets = "Clean"
};

var codegearCleanOptions = new MSBuildOptions
{
    OutputFolder = @"BuildOutput\Release",
    Configuration = @"Release",
    Platform = @"Win32",
    BuildTargets = "Clean, Build"
};

var codegearBuildOptions = new MSBuildOptions
{
    OutputFolder = @"BuildOutput\Release",
    Configuration = @"Release",
    Platform = @"Win32",
    BuildTargets = "Clean, Build"
};

project.Target("Clean")
    .Tasc(x=>
    {
        x.BuildProject(@"v2vantage\Component Library\CRS 2007\COM\DataAccessTLBs.cbproj", vsCleanOptions);
    });


project.Target("Build")
    .DependsOn("Clean")
    .Tasc(x=>
    {
        x.BuildProject(@"v2vantage\Component Library\CRS 2007\COM\DataAccessTLBs.cbproj", vsBuildOptions);
    });


project.Build("Build");
