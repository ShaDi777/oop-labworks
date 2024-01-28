using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class WifiAdapterRepository : RepositoryBase<WifiAdapter>
{
    public WifiAdapterRepository(Dictionary<string, WifiAdapter> componentDictionary)
        : base(componentDictionary) { }
}