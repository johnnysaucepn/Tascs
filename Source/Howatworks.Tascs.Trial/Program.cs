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

            var weird = Target.Named("Weird")
                .BuildProject(new MSBuildOptions
                {
                    ProjectFile = @"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj",
                    OutputFolder = @"BuildOutput\Release"
                })
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"stupid wibble"));

            var minimal = Target.Named("Minimal")
                .DependsOn(weird)
                .BuildProject(MSBuildOptions.Merge(defaultBuildOptions, new MSBuildOptions
                {
                    ProjectFile = @"Source\Howatworks.Tascs.MSBuild\Howatworks.Tascs.MSBuild.csproj"
                }));

            minimal.Execute();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}