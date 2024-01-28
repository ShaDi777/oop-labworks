using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories;

public interface IPartsRepository
{
    IRepository<Cpu> CpuRepository { get; }
    IRepository<DiscreteGpu> GpuRepository { get; }
    IRepository<ComputerCase> CaseRepository { get; }
    IRepository<CpuCoolingSystem> CpuCoolingSystemRepository { get; }
    IRepository<RamStick> RamStickRepository { get; }
    IRepository<WifiAdapter> WifiAdapterRepository { get; }
    IRepository<IStorage> StorageRepository { get; }
    IRepository<MotherBoard> MotherBoardRepository { get; }
    IRepository<PowerSupplyUnit> PowerSupplyUnitRepository { get; }
}