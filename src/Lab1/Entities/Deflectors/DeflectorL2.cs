using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;

public class DeflectorL2 : BaseDeflector
{
    public DeflectorL2(bool hasPhotonDeflection = false)
        : base(Settings.HealthDeflectorB, hasPhotonDeflection) { }
}
