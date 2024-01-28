using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class ChipsetInfo
{
    public ChipsetInfo(
        bool isXmpSupported,
        bool hasWifiModule,
        IEnumerable<int> availableMemoryFrequency)
    {
        IsXmpSupported = isXmpSupported;
        HasWifiModule = hasWifiModule;
        AvailableMemoryFrequency = availableMemoryFrequency;
    }

    public bool IsXmpSupported { get; }
    public bool HasWifiModule { get; }
    public IEnumerable<int> AvailableMemoryFrequency { get; }
}
