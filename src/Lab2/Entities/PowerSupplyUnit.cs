using System;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Must have
public sealed class PowerSupplyUnit : IComponent
{
    /*
 - Пиковая нагрузка (в ватт)
     */
    public PowerSupplyUnit(string name, int powerSupply)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");
        if (powerSupply <= 0) throw new NegativeArgumentException($"{nameof(powerSupply)} must be positive");

        Name = name;
        PowerSupply = powerSupply;
    }

    public string Name { get; }
    public int PowerSupply { get; }
}
