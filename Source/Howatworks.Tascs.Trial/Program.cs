using System;
using System.IO;
using Howatworks.Tascs.Core;
using Howatworks.Tascs.MSBuild;

namespace Howatworks.Tascs.Trial
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Get the user's current directory, not the path of the script
            PathUtils.Root = PathUtils.Resolve(Directory.GetCurrentDirectory(), @"..\..\..\..");

            Target.Named("Default")
                .BuildProject(@"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj", @"BuildOutput")
                //.ArchiveOutput(@"Source\Howatworks.Tascs.Core\bin\Debug\**");
                //.GenerateAssemblyInfo("1.2.3.4")
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"stupid wibble"));
                

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}