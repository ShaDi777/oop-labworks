using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Routes;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;
using Itmo.ObjectOrientedProgramming.Lab1.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Services;

public interface ISimulation
{
    public BaseShip? FindOptimalShip(ICollection<BaseShip> ships, Route route);
    Statistics SimulateCase(BaseShip ship, Route route);
}
