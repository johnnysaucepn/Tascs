namespace Howatworks.Tascs.Core
{
    public interface ITascTarget
    {
        string Name { get; set; }
        ITascResult Build();
    }
}