using System;
using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngines;

public class JumpEngineOmega : BaseJumpEngine
{
    public JumpEngineOmega()
        : base(Settings.JumpDistanceOmega) { }

    public override double CalculatePrice(int distance, int shipWeight)
    {
        return distance * Math.Log10(distance) * Settings.GravityMatterPrice;
    }
}
