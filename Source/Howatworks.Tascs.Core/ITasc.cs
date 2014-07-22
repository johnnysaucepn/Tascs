namespace Howatworks.Tascs.Core
{
    public interface ITasc
    {
        TascOptions Options { get; set; }
        void Run();
    }
}