using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngines;

public class JumpEngineAlpha : BaseJumpEngine
{
    public JumpEngineAlpha()
        : base(Settings.JumpDistanceAlpha) { }

    public override double CalculatePrice(int distance, int shipWeight)
    {
        return distance * Settings.GravityMatterPrice;
    }
}
