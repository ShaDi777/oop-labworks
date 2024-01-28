using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;
using Itmo.ObjectOrientedProgramming.Lab1.Tools.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;

public class NormalSpace : BaseEnvironment
{
    public NormalSpace(ICollection<BaseObstacle> obstacles)
        : base(obstacles)
    {
        if (obstacles.Any(obstacle => obstacle is not (Meteor or Asteroid)))
        {
            throw new IllegalObstacleException("Illegal obstacle!");
        }
    }

    public override BaseEngine? ChooseEngine(BaseShip ship)
    {
        if (ship is null) throw new ArgumentNullException(nameof(ship));

        return ship.ImpulseEngine;
    }
}
