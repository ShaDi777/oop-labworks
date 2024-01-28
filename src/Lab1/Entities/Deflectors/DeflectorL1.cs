using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;

public class DeflectorL1 : BaseDeflector
{
    public DeflectorL1(bool hasPhotonDeflection = false)
        : base(Settings.HealthDeflectorA, hasPhotonDeflection) { }
}
