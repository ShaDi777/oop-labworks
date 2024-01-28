using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public record DeleteCommand(string Path) : ICommand
{
    public ExecutionResult Execute(IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        FileResult result = context.FileSystem.DeleteFile(context.ResolvePath(Path));
        return new ExecutionResult(result.IsSuccess, result.Exception?.Message ?? string.Empty);
    }
}