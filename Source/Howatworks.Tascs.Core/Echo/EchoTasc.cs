using System;

namespace Howatworks.Tascs.Core.Echo
{
    public class EchoTasc : Tasc
    {
        public string Line { get; protected set; }

        public EchoTasc(string line)
        {
            Line = line;
        }

        public override ITascResult Execute(TascTarget target)
        {
            Console.WriteLine(Line);
            return null;
        }
    }
}
