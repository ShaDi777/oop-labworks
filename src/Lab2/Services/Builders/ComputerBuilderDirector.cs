using System;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Builders;

public class ComputerBuilderDirector : IComputerBuilderDirector
{
    private readonly ICaseBuilder _computerBuilder;

    public ComputerBuilderDirector(ICaseBuilder builder)
    {
        _computerBuilder = builder;
    }

    public IComputerSequentialBuilder DirectFromSpecification(Specification specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        IComputerSequentialBuilder builder = _computerBuilder.WithCase(specification.CaseName)
            .WithPowerSupplyUnit(specification.PowerSupplyUnitName)
            .WithMotherBoard(specification.MotherBoardName)
            .WithCpu(specification.CpuName)
            .WithCpuCoolingSystem(specification.CpuCoolingName)
            .WithGpu(specification.GpuName)
            .WithWifiAdapter(specification.WifiAdapterName);

        foreach (string ramName in specification.RamSticksName.Where(name => !string.IsNullOrEmpty(name)))
        {
            builder.AddRam(ramName);
        }

        foreach (string storageName in specification.StoragesName.Where(name => !string.IsNullOrEmpty(name)))
        {
            builder.AddStorage(storageName);
        }

        return builder;
    }
}
