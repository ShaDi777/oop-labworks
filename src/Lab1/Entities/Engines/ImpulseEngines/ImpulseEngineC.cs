using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngines;

public class ImpulseEngineC : BaseImpulseEngine
{
    private bool _initialized;
    public ImpulseEngineC()
        : base(Settings.StartUpFuelEngineC) { }

    public override int InitializeEngine()
    {
        if (_initialized) return 0;
        _initialized = true;
        return StartUpFuel * Settings.FuelPrice;
    }

    public override double CalculateTime(int distance, int shipWeight)
    {
        return distance / (1.0 * Settings.SpeedConstant / shipWeight);
    }

    public override double CalculatePrice(int distance, int shipWeight)
    {
        return InitializeEngine() + (CalculateTime(distance, shipWeight) * Settings.FuelPrice);
    }
}
