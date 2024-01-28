using System;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Must if CPU without graphics
public sealed class DiscreteGpu : IComponent
{
    /*
 - Высота и ширина видеокарты
 - Количество видеопамяти
 - Версия PCI-E
 - Частота чипа
 - Потребляемая мощность (в ватт)
     */
    public DiscreteGpu(
        string name,
        DimensionInfo dimensions,
        int videoMemory,
        int pcieVersion,
        double chipFrequency,
        int powerConsumption)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");
        if (videoMemory <= 0) throw new NegativeArgumentException($"{nameof(videoMemory)} must be positive");
        if (pcieVersion <= 0) throw new NegativeArgumentException($"{nameof(pcieVersion)} must be positive");
        if (chipFrequency <= 0) throw new NegativeArgumentException($"{nameof(chipFrequency)} must be positive");
        if (powerConsumption <= 0) throw new NegativeArgumentException($"{nameof(powerConsumption)} must be positive");

        Name = name;
        Dimensions = dimensions;
        VideoMemory = videoMemory;
        PcieVersion = pcieVersion;
        ChipFrequency = chipFrequency;
        PowerConsumption = powerConsumption;
    }

    public string Name { get; }
    public DimensionInfo Dimensions { get; }
    public int VideoMemory { get; }
    public int PcieVersion { get; }
    public double ChipFrequency { get; }
    public int PowerConsumption { get; }
}
