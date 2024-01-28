using System;
using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngines;

public class ImpulseEngineE : BaseImpulseEngine
{
    private bool _initialized;

    public ImpulseEngineE()
        : base(Settings.StartUpFuelEngineE) { }

    public override int InitializeEngine()
    {
        if (_initialized) return 0;
        _initialized = true;
        return StartUpFuel * Settings.FuelPrice;
    }

    public override double CalculateTime(int distance, int shipWeight)
    {
        double time = 0;
        double speed = 1.0 * Settings.SpeedConstant / shipWeight;
        while (distance > 0)
        {
            if (distance > speed)
            {
                time++;
                distance -= (int)speed;
            }
            else
            {
                time += 1.0 * distance / speed;
                distance = 0;
            }

            speed = Math.Exp(speed);
        }

        return time;
    }

    public override double CalculatePrice(int distance, int shipWeight)
    {
        return InitializeEngine() + (CalculateTime(distance, shipWeight) * Settings.FuelPrice);
    }
}
