using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class PowerSupplyUnitRepository : RepositoryBase<PowerSupplyUnit>
{
    public PowerSupplyUnitRepository(Dictionary<string, PowerSupplyUnit> componentDictionary)
        : base(componentDictionary) { }
}