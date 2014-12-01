using System;
using System.IO;
using Howatworks.Tascs.Core;
using Howatworks.Tascs.Core.Echo;
using Howatworks.Tascs.Core.Exec;
using Howatworks.Tascs.Core.Generic;
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

            var config = "Release";
            var platform = "x86";
            var outputFolder = @"BuildOutput\Release";

            project.Target("Build")
                .Tasc(x =>
                {
                    x.BuildProject(@"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj", outputPath: outputFolder);
                    x.Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do build"));
                });


            project.Target("Deploy")
                .DependsOn("Build")
                .Echo("Deploy!")
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do deploy"))
                .BuildProject(@"Source\Howatworks.Tascs.MSBuild\Howatworks.Tascs.MSBuild.csproj", outputPath: outputFolder, configuration: "Debug")
                .Tasc(x =>
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
                .Tasc(x => Console.WriteLine("Oh, and this happened."))
                .Tasc(x => Console.WriteLine("And this too."))
                ;

            project.Target("Unconnected")
                .DependsOn("NonExistent")
                .Echo("I know nothing!")
                ;

            project.Target("Deferred")
                .Tasc(x =>
                {
                    x.Echo("deferred - woo!");

                });


            project.Build("Deploy", "Chained");

            project.Build("Deferred");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}