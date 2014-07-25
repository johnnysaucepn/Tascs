using System;
using System.IO;
using Howatworks.Tascs.Core;
using Howatworks.Tascs.MSBuild;
using log4net.Config;

namespace Howatworks.Tascs.Trial
{
    internal static class Program
    {
        private static void Main()
        {
            // Get the user's current directory, not the path of the script
            PathUtils.Root = PathUtils.Resolve(Directory.GetCurrentDirectory(), @"..\..\..\..");

            var defaultBuildOptions = new MSBuildOptions
            {
                OutputFolder = @"BuildOutput\Debug",
                Configuration = @"Debug",
                Platform = @"AnyCPU",
                BuildTargets = "Clean, Build"
            };

            Target.Create("Weird")
                .BuildProject(new MSBuildOptions
                {
                    ProjectFile = @"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj",
                    OutputFolder = @"BuildOutput\Release"
                })
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do weird"));

            Target.Create("Minimal")
                .DependsOn(Target.Named("Weird"))
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do minimal"))
                .BuildProject(MSBuildOptions.Merge(defaultBuildOptions, new MSBuildOptions
                {
                    ProjectFile = @"Source\Howatworks.Tascs.MSBuild\Howatworks.Tascs.MSBuild.csproj"
                }));

            Target.Create("Unnecessary")
                .DependsOn(Target.Named("Weird"))
                .Exec(@"cmd.exe", @"/c", @"echo", Arg.Quoted(@"do unnecessary - don't do this"));

            Target.Create("Chained")
                .DependsOn(Target.Create("Downstream").Echo("Downstream!"))
                .Echo("Chained!");

            Target.Execute("Chained", "Minimal");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}