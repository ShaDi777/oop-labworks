using Itmo.ObjectOrientedProgramming.Lab4.Models.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Traversal;

public interface ITreeTraversal
{
    IComponent GetTree(IContext context, int maxDepth);
}