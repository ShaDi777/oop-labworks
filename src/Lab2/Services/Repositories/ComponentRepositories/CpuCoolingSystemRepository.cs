using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class CpuCoolingSystemRepository : RepositoryBase<CpuCoolingSystem>
{
    public CpuCoolingSystemRepository(Dictionary<string, CpuCoolingSystem> componentDictionary)
        : base(componentDictionary) { }
}