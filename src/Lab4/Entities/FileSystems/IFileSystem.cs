using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystems;

public interface IFileSystem
{
    bool IsFolder(string path);
    bool IsHidden(string path);
    bool Connect(string address);
    void Disconnect();
    IEnumerable<string> ListDirectory(string path);
    string? ShowFile(string path);
    FileResult MoveFile(string sourcePath, string destinationPath);
    FileResult CopyFile(string sourcePath, string destinationPath);
    FileResult DeleteFile(string path);
    FileResult RenameFile(string path, string newFileName);
}