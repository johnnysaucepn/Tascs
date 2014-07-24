using System;
using System.IO;
using Howatworks.Tascs.Core;
using Howatworks.Tascs.MSBuild;
using log4net.Config;

namespace Howatworks.Tascs.Trial
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Get the user's current directory, not the path of the script
            PathUtils.Root = PathUtils.Resolve(Directory.GetCurrentDirectory(), @"..\..\..\..");

            var defaultBuildOptions = new TascOptions<MSBuildOption>
            {
                {MSBuildOption.OutputPath, @"BuildOutput\Debug"},
                {MSBuildOption.Configuration, @"Debug"},
                {MSBuildOption.Platform, @"AnyCPU"},
                {MSBuildOption.Targets, "Clean, Build"}
            };


            //Target.Named("Default")
            //    .BuildProject(@"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj", @"BuildOutput")
                //.ArchiveOutput(@"Source\Howatworks.Tascs.Core\bin\Debug\**");
                //.GenerateAssemblyInfo("1.2.3.4")
            //    .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"stupid wibble"));

            Target.Named("Weird")
                .BuildProject(new TascOptions<MSBuildOption>
                {
                    {MSBuildOption.ProjectFilePath, @"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj"},
                    {MSBuildOption.OutputPath, @"BuildOutput\Release"}
                });

            Target.Named("Minimal")
                .BuildProject(TascOptions<MSBuildOption>.Merge(defaultBuildOptions, new TascOptions<MSBuildOption>
                {
                    {MSBuildOption.ProjectFilePath, @"Source\Howatworks.Tascs.MSBuild\Howatworks.Tascs.MSBuild.csproj"}
                }));
            

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}