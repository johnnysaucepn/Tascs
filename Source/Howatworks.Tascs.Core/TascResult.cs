namespace Howatworks.Tascs.Core
{
    public class TascResult : ITascResult
    {
        public static readonly TascResult Pass = new TascResult();
        public static readonly TascResult Fail = new TascResult();
        public static readonly TascResult Inconclusive = new TascResult();

    }

    
}
