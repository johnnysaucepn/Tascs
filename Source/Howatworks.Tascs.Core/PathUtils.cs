using System.IO;

namespace Howatworks.Tascs.Core
{
    public static class PathUtils
    {
        public static string Root { get; set; }

        public static string Resolve(string path)
        {
            return new DirectoryInfo(Path.Combine(Root, path)).FullName;
        }

        public static string Resolve(string root, string path)
        {
            return new DirectoryInfo(Path.Combine(root, path)).FullName;
        }
    }
}