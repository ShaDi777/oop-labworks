using System;
using System.Collections.Generic;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Traversal;

public class TreeTraversal : ITreeTraversal
{
    public IComponent GetTree(IContext context, int maxDepth)
    {
        ArgumentNullException.ThrowIfNull(context);

        IComponent component = CreateCorrectComponent(context.FileSystem, context.FullPath);
        if (maxDepth <= 0 || component is not IFolderComponent folder) return component;

        IEnumerable<string> files;
        try
        {
            files = context.FileSystem.ListDirectory(context.FullPath);
        }
        catch (UnauthorizedAccessException)
        {
            files = Array.Empty<string>();
        }

        foreach (string filePath in files)
        {
            IContext newContext = context.Clone();
            newContext.ChangeCurrentPath(filePath);
            folder.AddChild(GetTree(newContext, maxDepth - 1));
        }

        return component;
    }

    private IComponent CreateCorrectComponent(IFileSystem fileSystem, string path)
    {
        string leafName = Path.GetFileName(Path.GetDirectoryName(path)) ?? path;

        return fileSystem.IsFolder(path)
            ? new FolderComponent(leafName)
            : new FileComponent(leafName);
    }
}