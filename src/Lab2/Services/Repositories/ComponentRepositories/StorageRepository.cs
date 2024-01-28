using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class StorageRepository : RepositoryBase<IStorage>
{
    public StorageRepository(Dictionary<string, IStorage> componentDictionary)
        : base(componentDictionary) { }
}