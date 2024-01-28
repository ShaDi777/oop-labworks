using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;

public abstract class BaseEnvironment
{
    protected BaseEnvironment(IEnumerable<BaseObstacle> obstacles)
    {
        Obstacles = obstacles;
    }

    public IEnumerable<BaseObstacle> Obstacles { get; }

    public abstract BaseEngine? ChooseEngine(BaseShip ship);
}
