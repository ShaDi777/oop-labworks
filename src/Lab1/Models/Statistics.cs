using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;
using Itmo.ObjectOrientedProgramming.Lab1.Tools.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Statistics
{
    public double Time { get; private set; }
    public double Money { get; private set; }
    public bool IsCrewAlive { get; private set; } = true;
    public ShipStatus CurrentShipStatus { get; private set; } = ShipStatus.Functioning;

    public void AddTime(double additionalTime)
    {
        if (additionalTime < 0) throw new NegativeValueException("Can't add negative time to stats");
        Time += additionalTime;
    }

    public void AddMoney(double additionalMoney)
    {
        if (additionalMoney < 0) throw new NegativeValueException("Can't add negative money to stats");
        Money += additionalMoney;
    }

    public void UpdateFromShip(BaseShip? ship)
    {
        if (ship == null) throw new ArgumentNullException(nameof(ship));
        IsCrewAlive = ship.IsCrewAlive;
        CurrentShipStatus = ship.CurrentStatus;
    }

    public void AddStatistic(Statistics statistics)
    {
        if (statistics is null) throw new ArgumentNullException(nameof(statistics));

        Time += statistics.Time;
        Money += statistics.Money;
        IsCrewAlive &= statistics.IsCrewAlive;
        if ((int)CurrentShipStatus > (int)statistics.CurrentShipStatus)
        {
            CurrentShipStatus = statistics.CurrentShipStatus;
        }
    }
}
