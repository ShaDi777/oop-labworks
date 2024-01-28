using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Traversal;
using Itmo.ObjectOrientedProgramming.Lab4.Services.TreeOutputCreator;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;

public interface IContext
{
    public string RootPath { get; }
    public string CurrentPath { get; }
    public string FullPath => Path.Combine(RootPath, CurrentPath);
    public IFileSystem FileSystem { get; }
    public ITreeTraversal TreeTraversal { get; set; }
    public ITreeStringCreator TreeStringCreator { get; set; }

    bool ConnectToPath(string path, string mode);
    string ResolvePath(string path);
    void ChangeCurrentPath(string path);
    void Clear();
    IContext Clone();
}