using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystems;

public class NullFileSystem : IFileSystem
{
    public bool IsFolder(string path)
    {
        return false;
    }

    public bool IsHidden(string path)
    {
        return false;
    }

    public bool Connect(string address)
    {
        return false;
    }

    public void Disconnect() { }

    public IEnumerable<string> ListDirectory(string path)
    {
        return Array.Empty<string>();
    }

    public string? ShowFile(string path)
    {
        return null;
    }

    public FileResult MoveFile(string sourcePath, string destinationPath)
    {
        return new FileResult(false);
    }

    public FileResult CopyFile(string sourcePath, string destinationPath)
    {
        return new FileResult(false);
    }

    public FileResult DeleteFile(string path)
    {
        return new FileResult(false);
    }

    public FileResult RenameFile(string path, string newFileName)
    {
        return new FileResult(false);
    }
}