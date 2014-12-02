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
            var project = TascProject.Instance
                .BasePath(@"..\..\..\..")
                .UseMSBuild(configuration: "Release", platform: "x86", outputPath: @"BuildOutput\Release");

            project.Target("Clean")
                .Tasc(x =>
                {
                    x.BuildProject(@"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj", "Clean");
                });

            project.Target("Build")
                .Tasc(x =>
                {
                    x.BuildProject(@"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj", "Build");
                    x.Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do build"));
                });


            project.Target("Deploy")
                .DependsOn("Build")
                .Echo("Deploy!")
                .BuildProject(@"Source\Howatworks.Tascs.MSBuild\Howatworks.Tascs.MSBuild.csproj")
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