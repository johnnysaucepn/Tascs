namespace Howatworks.Tascs.Core.Echo
{
    public static class EchoExtensions
    {
        public static ITascTarget Echo(this ITascTarget target, string line)
        {
            return target.Do(new EchoTasc(line));
        }

    }
}