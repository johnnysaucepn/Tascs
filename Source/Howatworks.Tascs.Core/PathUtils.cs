using System.IO;

namespace Howatworks.Tascs.Core
{
    public static class PathUtils
    {
        public static string Resolve(string root, string path)
        {
            return new DirectoryInfo(Path.Combine(root, path)).FullName;
        }

    }
}