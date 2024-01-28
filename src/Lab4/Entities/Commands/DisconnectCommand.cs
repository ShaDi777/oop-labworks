using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public record DisconnectCommand : ICommand
{
    public ExecutionResult Execute(IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.FileSystem.Disconnect();
        context.Clear();
        return new ExecutionResult(true, "Disconnected");
    }
}