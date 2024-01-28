using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.EnumTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Must have
public sealed class RamStick : IComponent
{
    /*
 - Количество доступного размера памяти
 - Поддерживаемые пары частот JEDEC и вольтажа
 - Доступные XMP/DOСP(A-XMP) профили
 - Форм-фактор
 - Версия стандарта DDR
 - Потребляемая мощность (в ватт)
     */
    public RamStick(
        string name,
        int memory,
        IEnumerable<JedecProfile> supportedJedecAndVoltage,
        IEnumerable<XmpProfile> availableXmpProfiles,
        RamFormFactorType ramFormFactor,
        int ddrVersion,
        int powerConsumption)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");
        if (memory <= 0) throw new NegativeArgumentException($"{nameof(memory)} must be positive");
        if (ddrVersion <= 0) throw new NegativeArgumentException($"{nameof(ddrVersion)} must be positive");
        if (powerConsumption <= 0) throw new NegativeArgumentException($"{nameof(powerConsumption)} must be positive");

        Name = name;
        Memory = memory;
        SupportedJedecAndVoltage = supportedJedecAndVoltage;
        AvailableXmpProfiles = availableXmpProfiles;
        RamFormFactor = ramFormFactor;
        DdrVersion = ddrVersion;
        PowerConsumption = powerConsumption;
    }

    public string Name { get; }
    public int Memory { get; }
    public IEnumerable<JedecProfile> SupportedJedecAndVoltage { get; }
    public IEnumerable<XmpProfile> AvailableXmpProfiles { get; }
    public RamFormFactorType RamFormFactor { get; }
    public int DdrVersion { get; }
    public int PowerConsumption { get; }
}
