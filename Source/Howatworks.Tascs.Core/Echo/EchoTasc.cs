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

        public override ITascResult Execute(TascContext context)
        {
            try
            {
                Console.WriteLine(Line);
                return TascResult.Pass;
            }
            catch (Exception)
            {
                return TascResult.Fail;
            }
            
            
        }
    }
}
