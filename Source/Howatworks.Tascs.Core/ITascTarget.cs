using System;

namespace Howatworks.Tascs.Core
{
    public interface ITascTarget
    {
        string Name { get; set; }
        event EventHandler<GenerateExecutionContextArgs> ApplyProjectSettingsToExecutionContext;
        ITascResult Execute();
        ITascTarget Do(Tasc tasc);
        ITascTarget DependsOn(string dependency);
    }
}