﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public class ExecTasc : Tasc
    {
        public string Command { get; internal set; }
        public IEnumerable<string> CommandParams { get; internal set; }
        public bool RunWindowed { get; internal set; }

        public ExecTasc(string command, params string[] args)
        {
            Command = command;
            CommandParams = args;
        }

        public override ITascResult Execute()
        {
            ITascResult result = null;

            var formattedParams =
                (CommandParams != null)
                    ? string.Join(" ", CommandParams)
                    : "";

            if (RunWindowed)
            {
                var processStartInfo = new ProcessStartInfo(Command, formattedParams)
                {
                    CreateNoWindow = false
                };

                using (var process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.EnableRaisingEvents = false;
                    process.StartInfo.WorkingDirectory = TascProject.Instance.Root;
                    process.Start();
                }
            }
            else
            {
                var processStartInfo = new ProcessStartInfo(Command, formattedParams)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.EnableRaisingEvents = false;
                    process.StartInfo.WorkingDirectory = TascProject.Instance.Root;
                    process.Start();

                    // Handle Standard Output
                    process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                    process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                }
            }

            return result;

        }

    }
}
