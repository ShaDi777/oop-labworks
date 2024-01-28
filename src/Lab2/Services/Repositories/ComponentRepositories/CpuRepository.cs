using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class CpuRepository : RepositoryBase<Cpu>
{
    public CpuRepository(Dictionary<string, Cpu> componentDictionary)
        : base(componentDictionary) { }
}