using System;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Traversal;
using Itmo.ObjectOrientedProgramming.Lab4.Services.TreeOutputCreator;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

public class Context : IContext
{
    public Context(
        string? rootPath,
        IFileSystem? fileSystem,
        ITreeTraversal treeTraversal,
        ITreeStringCreator treeStringCreator)
    {
        RootPath = rootPath ?? string.Empty;
        CurrentPath = string.Empty;
        FileSystem = fileSystem ?? new NullFileSystem();
        TreeTraversal = treeTraversal;
        TreeStringCreator = treeStringCreator;
    }

    public string RootPath { get; private set; }
    public string CurrentPath { get; private set; }
    public IFileSystem FileSystem { get; private set; }
    public ITreeTraversal TreeTraversal { get; set; }
    public ITreeStringCreator TreeStringCreator { get; set; }

    public bool ConnectToPath(string path, string mode)
    {
        ArgumentNullException.ThrowIfNull(mode);

        FileSystem = mode.ToUpperInvariant() switch
        {
            "LOCAL" => new LocalFileSystem(),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), "No such mode in File Systems"),
        };

        if (!FileSystem.Connect(path)) return false;
        RootPath = ResolvePath(path);
        return true;
    }

    public string ResolvePath(string path)
    {
        return Path.GetFullPath(Path.Combine(RootPath, CurrentPath, path) + Path.DirectorySeparatorChar);
    }

    public void ChangeCurrentPath(string path)
    {
        string result = ResolvePath(path);

        if (!result.StartsWith(RootPath, StringComparison.CurrentCulture))
        {
            throw new ArgumentException("Can not resolve path outside root folder");
        }

        CurrentPath = Path.GetRelativePath(RootPath, result);
        if (CurrentPath == ".") CurrentPath = string.Empty;
    }

    public void Clear()
    {
        RootPath = string.Empty;
        CurrentPath = string.Empty;
        FileSystem = new NullFileSystem();
    }

    public IContext Clone()
    {
        return new Context(RootPath, FileSystem, TreeTraversal, TreeStringCreator)
        {
            CurrentPath = CurrentPath,
        };
    }
}