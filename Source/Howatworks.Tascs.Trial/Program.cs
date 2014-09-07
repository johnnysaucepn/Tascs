using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
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
            var project = TascProject.Instance;
            // Get the user's current directory, not the path of the script

            project.Root = PathUtils.Resolve(Directory.GetCurrentDirectory(), @"..\..\..\..");
            
            var defaultBuildOptions = new MSBuildOptions
            {
                OutputFolder = @"BuildOutput\Debug",
                Configuration = @"Debug",
                Platform = @"AnyCPU",
                BuildTargets = "Clean, Build"
            };

            project.Target("Build")
                .BuildProject(new MSBuildOptions
                {
                    ProjectFile = @"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj",
                    OutputFolder = @"BuildOutput\Release"
                })
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do build"));

            project.Target("Deploy")
                .DependsOn("Build")
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do deploy"))
                .BuildProject(new MSBuildOptions(defaultBuildOptions)
                {
                    ProjectFile = @"Source\Howatworks.Tascs.MSBuild\Howatworks.Tascs.MSBuild.csproj"
                });

            project.Target("Unnecessary")
                .DependsOn("Deploy")
                .Echo("Unnecessary!");

            project.Target("Downstream")
                .Echo("Downstream!");

            project.Target("Chained")
                .DependsOn("Downstream")
                
                .Echo("Chained!")
                .Do(new GenericTasc(() => Console.WriteLine("Oh, and this happened.")));

            project.Target("Unconnected")
                .DependsOn("NonExistent")
                .Echo("I know nothing!");

            project.Build("Deploy", "Chained");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}