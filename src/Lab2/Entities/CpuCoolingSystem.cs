using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Must have
public sealed class CpuCoolingSystem : IComponent
{
    /*
 - Габариты
 - Поддерживаемые сокеты
 - Максимально рассеиваемая масса тепла (TDP)
     */
    public CpuCoolingSystem(
        string name,
        DimensionInfo dimensions,
        IEnumerable<SocketType> supportedSockets,
        int maxCoolingTdp)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");
        if (maxCoolingTdp <= 0) throw new NegativeArgumentException($"{nameof(maxCoolingTdp)} must be positive");

        Name = name;
        Dimensions = dimensions;
        SupportedSockets = supportedSockets;
        MaxCoolingTdp = maxCoolingTdp;
    }

    public string Name { get; }
    public DimensionInfo Dimensions { get; }
    public IEnumerable<SocketType> SupportedSockets { get; }
    public int MaxCoolingTdp { get; }
}
