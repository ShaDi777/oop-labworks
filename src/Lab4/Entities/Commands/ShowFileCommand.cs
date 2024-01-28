using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public record ShowFileCommand(string Path, string Mode) : ICommand
{
    public ExecutionResult Execute(IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        string result = context.FileSystem.ShowFile(context.ResolvePath(Path)) ?? string.Empty;

        return new ExecutionResult(true, result, Mode.ToUpperInvariant());
    }
}