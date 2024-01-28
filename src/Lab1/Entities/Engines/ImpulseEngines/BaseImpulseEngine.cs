namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngines;

public abstract class BaseImpulseEngine : BaseEngine
{
    protected BaseImpulseEngine(int startUpFuel)
    {
        StartUpFuel = startUpFuel;
    }

    protected int StartUpFuel { get; init; }

    public abstract int InitializeEngine();
}
