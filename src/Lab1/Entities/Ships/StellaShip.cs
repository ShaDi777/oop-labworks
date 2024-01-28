using Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.HullStrengths;
using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;

public class StellaShip : BaseShip
{
    public StellaShip()
        : base(
            new ImpulseEngineC(),
            new JumpEngineOmega(),
            new DeflectorL1(),
            false,
            new HullL1(),
            Settings.WeightShipSmall) { }
}
