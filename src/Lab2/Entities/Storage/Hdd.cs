using System;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;

public sealed class Hdd : IStorage
{
    /*
 - Ёмкость в Гб
 - Cкорость вращения шпинделя
 - Потребляемая мощность (в ватт)
     */
    public Hdd(
        string name,
        int spindleSpeed,
        int capacity,
        int powerUsage)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");
        if (spindleSpeed <= 0) throw new NegativeArgumentException($"{nameof(spindleSpeed)} must be positive");
        if (capacity <= 0) throw new NegativeArgumentException($"{nameof(capacity)} must be positive");
        if (powerUsage <= 0) throw new NegativeArgumentException($"{nameof(powerUsage)} must be positive");

        Name = name;
        SpindleSpeed = spindleSpeed;
        Capacity = capacity;
        PowerUsage = powerUsage;
    }

    public string Name { get; }
    public IStorage.ConnectionType Connection => IStorage.ConnectionType.Sata;
    public int SpindleSpeed { get; }
    public int Capacity { get; }
    public int PowerUsage { get; }
}
