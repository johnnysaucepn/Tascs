using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.IO;
using Howatworks.Tascs.Core;
using Howatworks.Tascs.MSBuild;
using log4net.Config;
using System.Collections.Generic;

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

            var build = project.Target()
                .BuildProject(new MSBuildOptions
                {
                    ProjectFile = @"Source\Howatworks.Tascs.Core\Howatworks.Tascs.Core.csproj",
                    OutputFolder = @"BuildOutput\Release"
                })
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do build"));

            var deploy = project.Target("Deploy")
                .DependsOn(build)
                .Exec(@"cmd.exe", Arg.Literal(@"/c"), Arg.Literal(@"echo"), Arg.Quoted(@"do deploy"))
                .BuildProject(new MSBuildOptions(defaultBuildOptions)
                {
                    ProjectFile = @"Source\Howatworks.Tascs.MSBuild\Howatworks.Tascs.MSBuild.csproj"
                });

            var unnecessary = project.Target()
                .DependsOn(TascTarget.Named("Deploy"))
                .Echo("Unnecessary!");

            var chained = project.Target("Chained")
                .DependsOn(
                    project.Target("Downstream")
                    .Echo("Downstream!")
                    )
                .Echo("Chained!");

            //project.Execute("Chained", "Minimal");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    internal class TascProject
    {
        private static Lazy<TascProject> _instance = new Lazy<TascProject>(
            ()=> new TascProject());


        public static TascProject Instance
        {
            get { return _instance.Value; }
        }

        public string Root { get; set; }
        protected readonly IList<TascTarget> _targets = new List<TascTarget>();

        private TascProject()
        {

        }


        internal TascTarget Target()
        {
            return TascTarget.Create();
        }

        internal TascTarget Target(string p)
        {
            return TascTarget.Named(p);
        }
    }
}