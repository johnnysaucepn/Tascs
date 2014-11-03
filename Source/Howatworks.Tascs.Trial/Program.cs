using System;
using System.IO;
using Howatworks.Tascs.Core;
using Howatworks.Tascs.Core.Echo;
using Howatworks.Tascs.Core.Exec;
using Howatworks.Tascs.MSBuild;

namespace Howatworks.Tascs.Trial
{
    internal static class Program
    {
        private static void Main()
        {
            var project = TascProject.Instance;
            // Get the user's current directory, not the path of the script

            project.Root = PathUtils.Resolve(Directory.GetCurrentDirectory(), @"..\..\..\..");
            
            var debugBuildOptions = new MSBuildOptions
            {
                OutputFolder = @"BuildOutput\Debug",
                Configuration = @"Debug",
                Platform = @"AnyCPU",
                BuildTargets = "Clean, Build"
            };

            project.Target("Build")
                .BuildProject(@"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj", new MSBuildOptions
                {
                    OutputFolder = @"BuildOutput\Release"
                })
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do build"))
                ;


            project.Target("Deploy")
                .DependsOn("Build")
                .Echo("Deploy!")
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do deploy"))
                .BuildProject(@"Source\Howatworks.Tascs.MSBuild\Howatworks.Tascs.MSBuild.csproj", debugBuildOptions)
                .Tasc(() =>
                {
                    Console.WriteLine("Pass this!");
                    return TascResult.Pass;
                })
                ;

            project.Target("Unnecessary")
                .DependsOn("Deploy")
                .Echo("Unnecessary!")
                ;

            project.Target("Downstream")
                .Echo("Downstream!")
                ;

            project.Target("Chained")
                .DependsOn("Downstream")
                .Echo("Chained!")
                .Tasc(() => Console.WriteLine("Oh, and this happened."))
                .Tasc(() => Console.WriteLine("And this too."))
                ;

            project.Target("Unconnected")
                .DependsOn("NonExistent")
                .Echo("I know nothing!")
                ;

            project.Build("Deploy", "Chained");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}