using System;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.EnumTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Must have
public sealed class MotherBoard : IComponent
{
    /*
 - Сокет процессора
 - Кол-во распаянных на плате PCI-E линий
 - Кол-во распаянных на плате SATA портов
 - Чипсет (доступные частоты памяти, поддержка XMP)
 - Поддерживаемый стандарт DDR
 - Кол-во слотов под ОЗУ
 - Форм-фактор
 - BIOS (Тип, Версия)
     */
    public MotherBoard(
        string name,
        SocketType socket,
        int pcieLineCount,
        int sataPortsCount,
        ChipsetInfo chipset,
        int supportedDdrVersion,
        int ramSlots,
        MotherboardFormFactorType motherboardFormFactor,
        Bios bios)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");
        if (pcieLineCount <= 0) throw new NegativeArgumentException($"{nameof(pcieLineCount)} must be positive");
        if (sataPortsCount < 0) throw new NegativeArgumentException($"{nameof(sataPortsCount)} must be positive");
        if (supportedDdrVersion <= 0) throw new NegativeArgumentException($"{nameof(supportedDdrVersion)} must be positive");
        if (ramSlots <= 0) throw new NegativeArgumentException($"{nameof(ramSlots)} must be positive");

        Name = name;
        Socket = socket;
        PcieLineCount = pcieLineCount;
        SataPortsCount = sataPortsCount;
        Chipset = chipset;
        SupportedDdrVersion = supportedDdrVersion;
        RamSlots = ramSlots;
        MotherboardFormFactor = motherboardFormFactor;
        Bios = bios;
    }

    public string Name { get; }
    public SocketType Socket { get; }
    public int PcieLineCount { get; }
    public int SataPortsCount { get; }
    public ChipsetInfo Chipset { get; }
    public int SupportedDdrVersion { get; }
    public int RamSlots { get; }
    public MotherboardFormFactorType MotherboardFormFactor { get; }
    public Bios Bios { get; }
}
