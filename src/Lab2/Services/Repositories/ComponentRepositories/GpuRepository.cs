using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class GpuRepository : RepositoryBase<DiscreteGpu>
{
    public GpuRepository(Dictionary<string, DiscreteGpu> componentDictionary)
        : base(componentDictionary) { }
}