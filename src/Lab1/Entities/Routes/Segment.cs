using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Routes;

public record Segment(BaseEnvironment Environment, int Distance)
{
    public BaseEngine? ChooseEngine(BaseShip ship)
    {
        if (ship is null) throw new ArgumentNullException(nameof(ship));

        BaseEngine? engine = Environment.ChooseEngine(ship);
        if (engine is BaseJumpEngine jumpEngine)
        {
            engine = jumpEngine.JumpDistance >= Distance ? jumpEngine : null;
        }

        return engine;
    }
}
