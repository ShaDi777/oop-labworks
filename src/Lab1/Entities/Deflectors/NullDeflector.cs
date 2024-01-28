namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;

public class NullDeflector : BaseDeflector
{
    public NullDeflector()
        : base(0, false) { }

    public override void SetPhotonDeflection() { }
}