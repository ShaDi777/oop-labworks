using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class RamStickRepository : RepositoryBase<RamStick>
{
    public RamStickRepository(Dictionary<string, RamStick> componentDictionary)
        : base(componentDictionary) { }
}