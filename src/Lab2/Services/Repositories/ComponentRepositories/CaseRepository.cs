using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class CaseRepository : RepositoryBase<ComputerCase>
{
    public CaseRepository(Dictionary<string, ComputerCase> componentDictionary)
        : base(componentDictionary) { }
}