using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Must have
public sealed class Cpu : IComponent
{
    /*
 - Частота ядер
 - Кол-во ядер
 - Сокет
 - Наличие встроенного видеоядра
 - Поддерживаемые частоты памяти
 - Тепловыделение (TDP)
 - Потребляемая мощность (в ватт)
     */
    public Cpu(
        string name,
        double clockRate,
        int cores,
        SocketType socket,
        bool hasIntegratedGraphics,
        IEnumerable<RamVersionFrequency> supportedRamType,
        int tdp,
        int powerConsumption)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");
        if (clockRate <= 0) throw new NegativeArgumentException($"{nameof(clockRate)} must be positive");
        if (cores <= 0) throw new NegativeArgumentException($"{nameof(cores)} must be positive");
        if (tdp <= 0) throw new NegativeArgumentException($"{nameof(tdp)} must be positive");
        if (powerConsumption <= 0) throw new NegativeArgumentException($"{nameof(powerConsumption)} must be positive");

        Name = name;
        ClockRate = clockRate;
        Cores = cores;
        Socket = socket;
        HasIntegratedGraphics = hasIntegratedGraphics;
        SupportedRamType = supportedRamType;
        Tdp = tdp;
        PowerConsumption = powerConsumption;
    }

    public string Name { get; }
    public double ClockRate { get; }
    public int Cores { get; }
    public SocketType Socket { get; }
    public bool HasIntegratedGraphics { get; }
    public IEnumerable<RamVersionFrequency> SupportedRamType { get; }
    public int Tdp { get; }
    public int PowerConsumption { get; }
}
