using System.Diagnostics.CodeAnalysis;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Builders;

[SuppressMessage("All", "SA1649", Justification = "Interface driven builder")]
public interface ICaseBuilder
{
    IPowerSupplyUnitBuilder WithCase(string caseName);
}

public interface IPowerSupplyUnitBuilder
{
    IMotherBoardBuilder WithPowerSupplyUnit(string powerSupplyUnitName);
}

public interface IMotherBoardBuilder
{
    IComputerSequentialBuilder WithMotherBoard(string motherBoardName);
}

public interface ICoolingSystemBuilder
{
    IComputerSequentialBuilder WithCpuCoolingSystem(string cpuCoolingSystemName);
}

public interface IComputerSequentialBuilder
{
    ICoolingSystemBuilder WithCpu(string cpuName);
    IComputerSequentialBuilder WithGpu(string gpuName);
    IComputerSequentialBuilder AddRam(string ramName);
    IComputerSequentialBuilder WithWifiAdapter(string wifiAdapterName);
    IComputerSequentialBuilder AddStorage(string storageName);

    Computer Build();
}
