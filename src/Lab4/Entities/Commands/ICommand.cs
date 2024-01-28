using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public interface ICommand
{
    // public IList<Parameter> Parameters { get; }
    ExecutionResult Execute(IContext context);
}