namespace Howatworks.Tascs.Core
{
    public static class EchoExtensions
    {
        public static Target Echo(this Target target, string line)
        {
            target.AddTasc(new EchoTasc(line));

            return target;
        }

    }
}