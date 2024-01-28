using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public record GotoCommand(string Path) : ICommand
{
    public ExecutionResult Execute(IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            context.ChangeCurrentPath(Path);
            return new ExecutionResult(true, string.Empty);
        }
        catch (ArgumentException e)
        {
            return new ExecutionResult(false, e.Message);
        }
    }
}