namespace Howatworks.Tascs.Core.Exec
{
    public static class Arg
    {
        private const char Quote = '"';

        public static string Literal(string arg)
        {
            return arg;
        }

        public static string Quoted(string arg)
        {
            return string.Format("{0}{1}{0}", Quote, arg);
        }

    }
}
