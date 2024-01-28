using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystems;

[SuppressMessage("Exception", "CA1031", Justification = "Pass exception")]
public class LocalFileSystem : IFileSystem
{
    public bool IsFolder(string path)
    {
        return Directory.Exists(path);
    }

    public bool IsHidden(string path)
    {
        return (File.GetAttributes(path) & FileAttributes.Hidden) != 0;
    }

    public bool Connect(string address)
    {
        return Path.Exists(address);
    }

    public void Disconnect() { }

    public IEnumerable<string> ListDirectory(string path)
    {
        return Directory.EnumerateFileSystemEntries(path);
    }

    public string? ShowFile(string path)
    {
        return File.Exists(path) ? File.ReadAllText(path) : null;
    }

    public FileResult MoveFile(string sourcePath, string destinationPath)
    {
        try
        {
            File.Move(sourcePath, destinationPath);
            return new FileResult(true);
        }
        catch (Exception e)
        {
            return new FileResult(false, e);
        }
    }

    public FileResult CopyFile(string sourcePath, string destinationPath)
    {
        try
        {
            File.Copy(sourcePath, destinationPath);
            return new FileResult(true);
        }
        catch (Exception e)
        {
            return new FileResult(false, e);
        }
    }

    public FileResult DeleteFile(string path)
    {
        try
        {
            File.Delete(path);
            return new FileResult(true);
        }
        catch (Exception e)
        {
            return new FileResult(false, e);
        }
    }

    public FileResult RenameFile(string path, string newFileName)
    {
        return MoveFile(path, Path.Combine(Directory.GetParent(path)?.FullName ?? string.Empty, newFileName));
    }
}