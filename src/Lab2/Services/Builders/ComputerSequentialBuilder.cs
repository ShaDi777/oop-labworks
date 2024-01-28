using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Builders;

public class ComputerSequentialBuilder :
    ICaseBuilder, IPowerSupplyUnitBuilder, IMotherBoardBuilder,
    ICoolingSystemBuilder, IComputerSequentialBuilder
{
    private readonly IPartsRepository _partsRepository;
    private readonly IEnumerable<IComputerValidator> _validators;
    private readonly IList<RamStick> _ram;
    private readonly IList<IStorage> _storages;

    private ComputerCase? _case;
    private MotherBoard? _motherBoard;
    private Cpu? _cpu;
    private CpuCoolingSystem? _cpuCoolingSystem;
    private DiscreteGpu? _discreteGpu;
    private WifiAdapter? _wifiAdapter;
    private PowerSupplyUnit? _powerSupplyUnit;

    private ComputerSequentialBuilder(IPartsRepository partsFactory, IEnumerable<IComputerValidator> validators)
    {
        _partsRepository = partsFactory;
        _validators = validators;
        _storages = new List<IStorage>();
        _ram = new List<RamStick>();
    }

    public static ICaseBuilder GetBuilder(IPartsRepository partsFactory, IEnumerable<IComputerValidator> validators) =>
        new ComputerSequentialBuilder(partsFactory, validators);

    public IPowerSupplyUnitBuilder WithCase(string caseName)
    {
        _case = _partsRepository.CaseRepository.FindByName(caseName);
        return this;
    }

    public IMotherBoardBuilder WithPowerSupplyUnit(string powerSupplyUnitName)
    {
        _powerSupplyUnit = _partsRepository.PowerSupplyUnitRepository.FindByName(powerSupplyUnitName);
        return this;
    }

    public IComputerSequentialBuilder WithMotherBoard(string motherBoardName)
    {
        _motherBoard = _partsRepository.MotherBoardRepository.FindByName(motherBoardName);
        return this;
    }

    public IComputerSequentialBuilder WithCpuCoolingSystem(string cpuCoolingSystemName)
    {
        _cpuCoolingSystem = _partsRepository.CpuCoolingSystemRepository.FindByName(cpuCoolingSystemName);
        return this;
    }

    public ICoolingSystemBuilder WithCpu(string cpuName)
    {
        _cpu = _partsRepository.CpuRepository.FindByName(cpuName);
        return this;
    }

    public IComputerSequentialBuilder WithGpu(string gpuName)
    {
        _discreteGpu = _partsRepository.GpuRepository.FindByName(gpuName);
        return this;
    }

    public IComputerSequentialBuilder AddRam(string ramName)
    {
        RamStick? ram = _partsRepository.RamStickRepository.FindByName(ramName);
        if (ram is not null) _ram.Add(ram);
        return this;
    }

    public IComputerSequentialBuilder WithWifiAdapter(string wifiAdapterName)
    {
        _wifiAdapter = _partsRepository.WifiAdapterRepository.FindByName(wifiAdapterName);
        return this;
    }

    public IComputerSequentialBuilder AddStorage(string storageName)
    {
        IStorage? storage = _partsRepository.StorageRepository.FindByName(storageName);
        if (storage is not null) _storages.Add(storage);
        return this;
    }

    public Computer Build()
    {
        return new Computer(
            _case ?? throw new ArgumentNullException(nameof(_case)),
            _motherBoard ?? throw new ArgumentNullException(nameof(_motherBoard)),
            _cpu ?? throw new ArgumentNullException(nameof(_cpu)),
            _cpuCoolingSystem ?? throw new ArgumentNullException(nameof(_cpuCoolingSystem)),
            _ram ?? throw new ArgumentNullException(nameof(_ram)),
            _storages,
            _discreteGpu,
            _wifiAdapter,
            _powerSupplyUnit ?? throw new ArgumentNullException(nameof(_powerSupplyUnit)),
            _validators);
    }
}
