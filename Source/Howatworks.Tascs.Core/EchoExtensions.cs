namespace Howatworks.Tascs.Core
{
    public static class EchoExtensions
    {
        public static TascTarget Echo(this TascTarget target, string line)
        {
            target.Do(new EchoTasc(line));

            return target;
        }

    }
}