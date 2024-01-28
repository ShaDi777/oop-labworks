using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngines;

public abstract class BaseJumpEngine : BaseEngine
{
    protected BaseJumpEngine(int jumpDistance)
    {
        JumpDistance = jumpDistance;
    }

    public int JumpDistance { get; init; }

    public override double CalculateTime(int distance, int shipWeight)
    {
        return Settings.JumpInitializationTime * shipWeight;
    }
}
