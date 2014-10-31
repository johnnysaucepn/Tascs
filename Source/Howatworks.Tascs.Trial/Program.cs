using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.IO;
using Howatworks.Tascs.Core;
using Howatworks.Tascs.Core.Echo;
using Howatworks.Tascs.Core.Exec;
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
            
            var debugBuildOptions = new MSBuildOptions
            {
                OutputFolder = @"BuildOutput\Debug",
                Configuration = @"Debug",
                Platform = @"AnyCPU",
                BuildTargets = "Clean, Build"
            };

            project.Target("Build")
                .Do(() =>
                {
                    new MSBuildTasc(@"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj", new MSBuildOptions
                    {
                        OutputFolder = @"BuildOutput\Release"
                    });
                    Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do build"));
                    return TascResult.Pass;
                });

            project.Target("Deploy")
                .DependsOn("Build")
                .Do(x =>
                {
                    x.Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do deploy"));
                    x.BuildProject(@"Source\Howatworks.Tascs.MSBuild\Howatworks.Tascs.MSBuild.csproj", debugBuildOptions);
                    return TascResult.Pass;
                });

            project.Target("Unnecessary")
                .DependsOn("Deploy")
                .Echo("Unnecessary!");

            project.Target("Downstream")
                .Echo("Downstream!");

            project.Target("Chained")
                .DependsOn("Downstream")

                .Do(x => x.Echo("Chained!"))
                .Do(x => Console.WriteLine("Oh, and this happened."))
                .Do(x => Console.WriteLine("And this too."));

            project.Target("Unconnected")
                .DependsOn("NonExistent")
                .Echo("I know nothing!");

            project.Build("Deploy", "Chained");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}