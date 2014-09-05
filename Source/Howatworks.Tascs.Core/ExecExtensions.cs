namespace Howatworks.Tascs.Core
{
    public static class ExecExtensions
    {
        public static TascTarget Exec(this TascTarget target, string command, params string[] cmdParams)
        {
            target.Do(new ExecTasc(command, cmdParams)
            {
                RunWindowed = false
            });

            return target;
        }

        public static TascTarget ExecWindowed(this TascTarget target, string command, params string[] cmdParams)
        {
            target.Do(new ExecTasc(command, cmdParams)
            {
                RunWindowed = true
            });

            return target;
        }
    }
}