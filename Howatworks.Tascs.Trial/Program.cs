using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Howatworks.Tascs.Core;

namespace Howatworks.Tascs.Trial
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the user's current directory, not the path of the script
            PathUtils.Root = System.IO.Directory.GetCurrentDirectory();

            //var projectFilePath = Env.ScriptArgs[0];
            //var outputPath = Env.ScriptArgs[1];

            new Build.Build(new TascOptions()
            {
                {"Project", @"c:\projects\Tascs\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj"},
                {"Output", @"c:\projects\Tascs\BuildOutput"}
            }).Run();


            Console.WriteLine("Press any key...");
            Console.ReadKey();


        }
    }
}
