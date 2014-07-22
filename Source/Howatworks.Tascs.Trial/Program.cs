using System;
using System.IO;
using Howatworks.Tascs.Core;

namespace Howatworks.Tascs.Trial
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Get the user's current directory, not the path of the script
            PathUtils.Root = Directory.GetCurrentDirectory();

            //var projectFilePath = Env.ScriptArgs[0];
            //var outputPath = Env.ScriptArgs[1];

            new Build.Build(new TascOptions
            {
                {"Project", @"..\..\..\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj"},
                {"Output", @"..\..\..\..\BuildOutput"}
            }).Run();


            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}