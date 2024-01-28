using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Routes;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;
using Itmo.ObjectOrientedProgramming.Lab1.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Services;

public class SimpleSimulation : ISimulation
{
    public BaseShip? FindOptimalShip(ICollection<BaseShip> ships, Route route)
    {
        if (ships == null) throw new ArgumentNullException(nameof(ships));

        var results = ships.Select(ship => SimulateCase(ship, route)).ToList();
        BaseShip? optimalShip = ships
                        .Where((ship, index) => results[index].CurrentShipStatus == ShipStatus.Functioning)
                        .MinBy(ship => results[ships.ToList().IndexOf(ship)].Money);

        return optimalShip;
    }

    public Statistics SimulateCase(BaseShip ship, Route route)
    {
        if (ship == null) throw new ArgumentNullException(nameof(ship));
        if (route == null) throw new ArgumentNullException(nameof(route));

        var statistics = new Statistics();

        foreach (Segment segment in route.Segments)
        {
            Statistics segmentStatistics = SimulateSegment(ship, segment);
            statistics.AddStatistic(segmentStatistics);
            if (ship.CurrentStatus != ShipStatus.Functioning) break;
        }

        statistics.UpdateFromShip(ship);
        return statistics;
    }

    private static Statistics SimulateSegment(BaseShip ship, Segment segment)
    {
        if (ship == null) throw new ArgumentNullException(nameof(ship));
        if (segment == null) throw new ArgumentNullException(nameof(segment));

        var statistics = new Statistics();

        BaseEngine? usedEngine = ship.ChooseEngineForSegment(segment);
        if (usedEngine is null) return statistics;

        foreach (BaseObstacle obstacle in segment.Environment.Obstacles)
        {
            ship.EncounterObstacle(obstacle);
            if (ship.CurrentStatus != ShipStatus.Functioning) break;
        }

        if (ship.CurrentStatus == ShipStatus.Functioning)
        {
            statistics.AddTime(usedEngine.CalculateTime(segment.Distance, ship.Weight));
            statistics.AddMoney(usedEngine.CalculatePrice(segment.Distance, ship.Weight));
        }

        statistics.UpdateFromShip(ship);

        return statistics;
    }
}
