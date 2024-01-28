using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;

public class DeflectorL3 : BaseDeflector
{
    public DeflectorL3(bool hasPhotonDeflection = false)
        : base(Settings.HealthDeflectorC, hasPhotonDeflection) { }
}
