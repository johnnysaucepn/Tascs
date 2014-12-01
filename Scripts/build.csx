#r "..\Source\Howatworks.Tascs.Core\bin\Release\Howatworks.Tascs.Core.dll"
#r "..\Source\Howatworks.Tascs.MSBuild\bin\Release\Howatworks.Tascs.MSBuild.dll"

using Howatworks.Tascs.Core;
using Howatworks.Tascs.Core.Echo;
using Howatworks.Tascs.Core.Exec;
using Howatworks.Tascs.Core.Generic;
using Howatworks.Tascs.MSBuild;

var project = TascProject.Instance;
// Get the user's current directory, not the path of the script

project.Root = PathUtils.Resolve(Directory.GetCurrentDirectory(), @"..\Source");
            
var outputFolder = @"BuildOutput\Debug";
var configuration = @"Debug";
var platform = @"AnyCPU";
var buildTargets = "Clean, Build";

project.Target("Build")
    .Tasc(x=>
    {
        x.BuildProject(@"Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj", targets: buildTargets,
        configuration:configuration,
            outputFolder: outputFolder, platform: platform);
        x.Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do build"));
    });


project.Target("Downstream")
    .Echo("Downstream!")
    ;

project.Target("Chained")
    .DependsOn("Downstream")
    .Echo("Chained!")
    .Tasc(x => Console.WriteLine("Oh, and this happened."))
    .Tasc(x => Console.WriteLine("And this too."))
    ;

project.Target("Unconnected")
    .DependsOn("NonExistent")
    .Echo("I know nothing!")
    ;

project.Target("Deferred")
    .Echo("Before!")
    .Tasc(x =>
    {
        x.Echo("deferred - woo!");

    })
    .Echo("After!");


project.Build("Deploy", "Chained");

project.Build("Deferred");
project.Build("Build");

project.Build("Unconnected");
