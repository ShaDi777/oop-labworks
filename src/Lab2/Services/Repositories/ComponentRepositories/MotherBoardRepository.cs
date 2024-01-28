using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;

public class MotherBoardRepository : RepositoryBase<MotherBoard>
{
    public MotherBoardRepository(Dictionary<string, MotherBoard> componentDictionary)
        : base(componentDictionary) { }
}