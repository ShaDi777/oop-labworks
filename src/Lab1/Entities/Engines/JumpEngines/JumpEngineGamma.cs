using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngines;

public class JumpEngineGamma : BaseJumpEngine
{
    public JumpEngineGamma()
        : base(Settings.JumpDistanceGamma) { }

    public override double CalculatePrice(int distance, int shipWeight)
    {
        return distance * distance * Settings.GravityMatterPrice;
    }
}
