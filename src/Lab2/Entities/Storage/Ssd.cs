using System;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;

public sealed class Ssd : IStorage
{
    /*
 - Вариант подключения (PCI-E / Sata)
 - Ёмкость в Гб
 - Максимальная скорость работы
 - Потребляемая мощность (в ватт)
     */
    public Ssd(
        string name,
        IStorage.ConnectionType connectionType,
        int maxWorkSpeed,
        int capacity,
        int powerUsage)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");
        if (maxWorkSpeed <= 0) throw new NegativeArgumentException($"{nameof(maxWorkSpeed)} must be positive");
        if (capacity <= 0) throw new NegativeArgumentException($"{nameof(capacity)} must be positive");
        if (powerUsage <= 0) throw new NegativeArgumentException($"{nameof(powerUsage)} must be positive");

        Name = name;
        Connection = connectionType;
        MaxWorkSpeed = maxWorkSpeed;
        Capacity = capacity;
        PowerUsage = powerUsage;
    }

    public string Name { get; }
    public IStorage.ConnectionType Connection { get; }
    public int MaxWorkSpeed { get; }
    public int Capacity { get; }
    public int PowerUsage { get; }
}