using System;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public class PowerSupplyValidator : IComputerValidator
{
    public void Validate(Computer computer)
    {
        return;
    }

    public string ValidateWithComment(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        string comment = string.Empty;
        if (computer.PowerSupplyUnit.PowerSupply < TotalPowerUsage(computer))
        {
            comment = $"Not enough power! Usage: {TotalPowerUsage(computer)}. Max: {computer.PowerSupplyUnit.PowerSupply}";
        }

        return comment;
    }

    private int TotalPowerUsage(Computer computer)
    {
        int totalPower = computer.Cpu.PowerConsumption +
            computer.Rams.Sum(ramStick => ramStick.PowerConsumption) +
            computer.DiscreteGpu?.PowerConsumption ?? 0 +
            computer.Storages.Sum(storage => storage.PowerUsage) +
            computer.WifiAdapter?.PowerConsumption ?? 0;
        return totalPower;
    }
}
