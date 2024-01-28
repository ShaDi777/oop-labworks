using Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.HullStrengths;
using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;

public class ShuttleShip : BaseShip
{
    public ShuttleShip()
        : base(
            new ImpulseEngineC(),
            null,
            new NullDeflector(),
            false,
            new HullL1(),
            Settings.WeightShipSmall) { }
}
