using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public record CopyCommand(string SourcePath, string DestinationPath) : ICommand
{
    public ExecutionResult Execute(IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        FileResult result =
            context.FileSystem.CopyFile(
                context.ResolvePath(SourcePath),
                context.ResolvePath(DestinationPath));
        return new ExecutionResult(result.IsSuccess, result.Exception?.Message ?? string.Empty);
    }
}