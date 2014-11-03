namespace Howatworks.Tascs.Core.Exec
{
    public static class ExecExtensions
    {
        public static ITascTarget Exec(this ITascTarget target, string command, params string[] cmdParams)
        {
            return target.Do(new ExecTasc(command, cmdParams)
            {
                RunWindowed = false
            });
        }

        public static ITascTarget ExecWindowed(this ITascTarget target, string command, params string[] cmdParams)
        {
            return target.Do(new ExecTasc(command, cmdParams)
            {
                RunWindowed = true
            });
        }
    }
}