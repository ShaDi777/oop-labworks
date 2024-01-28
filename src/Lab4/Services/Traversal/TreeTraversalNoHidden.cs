using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Traversal;

public class TreeTraversalNoHidden : ITreeTraversal
{
    private ITreeTraversal _traversal;

    public TreeTraversalNoHidden(ITreeTraversal traversal)
    {
        _traversal = traversal;
    }

    public IComponent GetTree(IContext context, int maxDepth)
    {
        ArgumentNullException.ThrowIfNull(context);

        IComponent tree = _traversal.GetTree(context, maxDepth);
        IComponent newTree = tree.Clone();
        if (tree is IFolderComponent folder && newTree is IFolderComponent newFolder)
        {
            Filter(context, folder, newFolder);
        }

        return newTree;
    }

    private void Filter(IContext context, IFolderComponent folder, IFolderComponent newFolder)
    {
        foreach (IComponent child in folder.Children)
        {
            IComponent newChild = child.Clone();
            IContext newContext = context.Clone();
            newContext.ChangeCurrentPath(newChild.Name);
            if (newContext.FileSystem.IsHidden(newContext.FullPath))
            {
                continue;
            }

            newFolder.AddChild(newChild);
            if (child is IFolderComponent childFolder &&
                newChild is IFolderComponent newChildFolder)
            {
                Filter(newContext, childFolder, newChildFolder);
            }
        }
    }
}