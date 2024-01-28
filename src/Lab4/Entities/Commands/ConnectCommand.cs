using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public record ConnectCommand(string Address, string Mode) : ICommand
{
    public ExecutionResult Execute(IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        bool isSuccess = context.ConnectToPath(Address, Mode);

        return new ExecutionResult(isSuccess, isSuccess ? "Successfully connected" : "Couldn't connect to this path");
    }
}