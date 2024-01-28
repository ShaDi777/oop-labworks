using Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.HullStrengths;
using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;

public class MeridianShip : BaseShip
{
    public MeridianShip()
        : base(
            new ImpulseEngineE(),
            null,
            new DeflectorL2(),
            true,
            new HullL2(),
            Settings.WeightShipMedium) { }
}
