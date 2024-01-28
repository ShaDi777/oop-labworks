using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public record RenameCommand(string SourcePath, string NewFileName) : ICommand
{
    public ExecutionResult Execute(IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        FileResult result =
            context.FileSystem.RenameFile(
                context.ResolvePath(SourcePath),
                context.ResolvePath(NewFileName));
        return new ExecutionResult(result.IsSuccess, result.Exception?.Message ?? string.Empty);
    }
}