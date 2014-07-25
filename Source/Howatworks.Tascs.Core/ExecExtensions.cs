namespace Howatworks.Tascs.Core
{
    public static class ExecExtensions
    {
        public static Target Exec(this Target target, string command, params string[] cmdParams)
        {
            target.AddTasc(new ExecTasc(command, cmdParams)
            {
                RunWindowed = false
            });

            return target;
        }

        public static Target ExecWindowed(this Target target, string command, params string[] cmdParams)
        {
            target.AddTasc(new ExecTasc(command, cmdParams)
            {
                RunWindowed = true
            });

            return target;
        }
    }
}