using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public static class DefaultExtensions
    {
        public static Target Exec(this Target target, string command, params Arg[] cmdParams)
        {
            var formattedParams =
                (cmdParams != null)
                    ? string.Join(" ", cmdParams.Select(x => x.Value).ToArray())
                    : "";
            
            var processStartInfo = new ProcessStartInfo(command, formattedParams)
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
                process.StartInfo.WorkingDirectory = PathUtils.Root;
                process.Start();

                // Handle Standard Output
                process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();
            }

            return target;
        }

        public static Target ExecWindowed(this Target target, string command, params Arg[] cmdParams)
        {
            var formattedParams =
                (cmdParams != null)
                    ? string.Join(" ", cmdParams.Select(x => x.Value).ToArray())
                    : "";

            var processStartInfo = new ProcessStartInfo(command, formattedParams)
            {
                CreateNoWindow = false
            };

            using (var process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.EnableRaisingEvents = false;
                process.StartInfo.WorkingDirectory = PathUtils.Root;
                process.Start();
            }

            return target;
        }

        private static string QuoteIfWhitespace(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            return s.Any(char.IsWhiteSpace) ? String.Format(@"""{0}""", s) : s;
        }
    }
}
