using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public static class PathUtils
    {
        public static string Root { get; set; }

        public static string Resolve(string path)
        {
            var parts = path.Split('~');

            return Path.Combine(Root, parts.Last().TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        }

    }
}
