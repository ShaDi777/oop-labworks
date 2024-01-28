using Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.HullStrengths;
using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;

public class VaklasShip : BaseShip
{
    public VaklasShip()
        : base(
            new ImpulseEngineE(),
            new JumpEngineGamma(),
            new DeflectorL1(),
            false,
            new HullL2(),
            Settings.WeightShipMedium) { }
}
