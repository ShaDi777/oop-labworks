using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories;

public class PartsRepository : IPartsRepository
{
    public PartsRepository(
        IRepository<Cpu> cpuRepository,
        IRepository<DiscreteGpu> gpuRepository,
        IRepository<ComputerCase> caseRepository,
        IRepository<CpuCoolingSystem> cpuCoolingSystemRepository,
        IRepository<RamStick> ramStickRepository,
        IRepository<WifiAdapter> wifiAdapterRepository,
        IRepository<IStorage> storageRepository,
        IRepository<MotherBoard> motherBoardRepository,
        IRepository<PowerSupplyUnit> powerSupplyUnitRepository)
    {
        CpuRepository = cpuRepository;
        GpuRepository = gpuRepository;
        CaseRepository = caseRepository;
        CpuCoolingSystemRepository = cpuCoolingSystemRepository;
        RamStickRepository = ramStickRepository;
        WifiAdapterRepository = wifiAdapterRepository;
        StorageRepository = storageRepository;
        MotherBoardRepository = motherBoardRepository;
        PowerSupplyUnitRepository = powerSupplyUnitRepository;
    }

    public IRepository<Cpu> CpuRepository { get; }
    public IRepository<DiscreteGpu> GpuRepository { get; }
    public IRepository<ComputerCase> CaseRepository { get; }
    public IRepository<CpuCoolingSystem> CpuCoolingSystemRepository { get; }
    public IRepository<RamStick> RamStickRepository { get; }
    public IRepository<WifiAdapter> WifiAdapterRepository { get; }
    public IRepository<IStorage> StorageRepository { get; }
    public IRepository<MotherBoard> MotherBoardRepository { get; }
    public IRepository<PowerSupplyUnit> PowerSupplyUnitRepository { get; }
}