using Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.HullStrengths;
using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;

public class AvgurShip : BaseShip
{
    public AvgurShip()
        : base(
            new ImpulseEngineE(),
            new JumpEngineAlpha(),
            new DeflectorL3(),
            false,
            new HullL3(),
            Settings.WeightShipHigh) { }
}
