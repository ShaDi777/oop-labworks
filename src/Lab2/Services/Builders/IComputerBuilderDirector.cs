using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Builders;

public interface IComputerBuilderDirector
{
    IComputerSequentialBuilder DirectFromSpecification(Specification specification);
}
