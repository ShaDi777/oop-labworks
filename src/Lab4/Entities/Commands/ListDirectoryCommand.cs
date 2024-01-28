using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public record ListDirectoryCommand(int MaxDepth = 1) : ICommand
{
    public ExecutionResult Execute(IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        IComponent tree = context.TreeTraversal.GetTree(context, MaxDepth);
        string stringTree = context.TreeStringCreator.GetStringTree(tree);
        return new ExecutionResult(true, stringTree);
    }
}