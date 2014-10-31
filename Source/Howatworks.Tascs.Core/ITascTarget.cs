using System;

namespace Howatworks.Tascs.Core
{
    public interface ITascTarget
    {
        string Name { get; set; }
        ITascResult Build();
        ITascTarget Do(Tasc tasc);
        ITascTarget DependsOn(string dependency);
    }
}