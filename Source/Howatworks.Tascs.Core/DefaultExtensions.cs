using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public static class DefaultExtensions
    {
        public static Target Exec(this Target target, string command, IEnumerable<string> cmdParams = null,
            bool quoteSpaces = true, bool showWindow = false)
        {

            var formattedParams = (cmdParams != null)
                ? string.Join(" ", (!quoteSpaces ? cmdParams : cmdParams.Select(QuoteIfWhitespace).ToArray()))
                : "";
            
            var processStartInfo = new ProcessStartInfo(command, formattedParams)
            {
                CreateNoWindow = !showWindow,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = processStartInfo,
            };

            process.OutputDataReceived += process_OutputDataReceived;
            process.ErrorDataReceived += process_OutputDataReceived;

            process.Start();

            process.WaitForExit();

            // Retrieve the app's exit code
            var exitCode = process.ExitCode;

            return target;
        }

        static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private static string QuoteIfWhitespace(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            return s.Any(char.IsWhiteSpace) ? String.Format(@"""{0}""", s) : s;
        }
    }
}
