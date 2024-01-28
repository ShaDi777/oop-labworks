using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class Computer
{
    private readonly List<RamStick> _ram;
    private readonly List<IStorage> _storages;

    public Computer(
        ComputerCase computerCase,
        MotherBoard motherBoard,
        Cpu cpu,
        CpuCoolingSystem cpuCoolingSystem,
        IEnumerable<RamStick> ram,
        IEnumerable<IStorage> storages,
        DiscreteGpu? discreteGpu,
        WifiAdapter? wifiAdapter,
        PowerSupplyUnit powerSupplyUnit,
        IEnumerable<IComputerValidator> validators)
    {
        ComputerCase = computerCase;
        MotherBoard = motherBoard;
        Cpu = cpu;
        CpuCoolingSystem = cpuCoolingSystem;
        _ram = ram.ToList();
        _storages = storages.ToList();
        DiscreteGpu = discreteGpu;
        WifiAdapter = wifiAdapter;
        PowerSupplyUnit = powerSupplyUnit;

        RunCompatibilityCheck(validators ?? Array.Empty<IComputerValidator>());
    }

    public ComputerCase ComputerCase { get; }
    public MotherBoard MotherBoard { get; }
    public Cpu Cpu { get; }
    public CpuCoolingSystem CpuCoolingSystem { get; }
    public IReadOnlyList<RamStick> Rams => _ram;
    public IReadOnlyList<IStorage> Storages => _storages;
    public DiscreteGpu? DiscreteGpu { get; }
    public WifiAdapter? WifiAdapter { get; }
    public PowerSupplyUnit PowerSupplyUnit { get; }

    public static ICaseBuilder SequentialBuilder(
        IPartsRepository partsRepository,
        IEnumerable<IComputerValidator> validators)
        => ComputerSequentialBuilder.GetBuilder(partsRepository, validators);

    private void RunCompatibilityCheck(IEnumerable<IComputerValidator> validators)
    {
        ArgumentNullException.ThrowIfNull(ComputerCase);
        ArgumentNullException.ThrowIfNull(MotherBoard);
        ArgumentNullException.ThrowIfNull(Cpu);
        ArgumentNullException.ThrowIfNull(CpuCoolingSystem);
        ArgumentNullException.ThrowIfNull(_ram);
        ArgumentNullException.ThrowIfNull(PowerSupplyUnit);
        ArgumentNullException.ThrowIfNull(_storages);

        foreach (IComputerValidator validator in validators)
        {
            validator.Validate(this);
        }
    }
}
