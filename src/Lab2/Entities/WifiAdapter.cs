using System;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Must have
public sealed class WifiAdapter : IComponent
{
    /*
 - Версия стандарта WiFi
 - Наличие встроенного модуля Bluetooth
 - Версия PCI-e
 - Потребляемая мощность
     */
    public WifiAdapter(
        string name,
        int wifiVersion,
        bool hasBluetooth,
        int pcieVersion,
        int powerConsumption)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");
        if (wifiVersion <= 0) throw new NegativeArgumentException($"{nameof(wifiVersion)} must be positive");
        if (pcieVersion <= 0) throw new NegativeArgumentException($"{nameof(pcieVersion)} must be positive");
        if (powerConsumption <= 0) throw new NegativeArgumentException($"{nameof(powerConsumption)} must be positive");

        Name = name;
        WifiVersion = wifiVersion;
        HasBluetooth = hasBluetooth;
        PcieVersion = pcieVersion;
        PowerConsumption = powerConsumption;
    }

    public string Name { get; }
    public int WifiVersion { get; }
    public bool HasBluetooth { get; }
    public int PcieVersion { get; }
    public int PowerConsumption { get; }
}
