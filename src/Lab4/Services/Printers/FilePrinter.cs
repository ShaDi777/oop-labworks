using System;
using System.IO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Printers;

public class FilePrinter : IPrinter, IDisposable
{
    private readonly StreamWriter _file;
    private bool _isDisposed;

    public FilePrinter(string path)
    {
        _file = new StreamWriter(path);
    }

    public void Print(string text)
    {
        _file.Write(text);
    }

    public void PrintLine(string text)
    {
        _file.WriteLine(text);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            _file.Close();
        }

        _isDisposed = true;
    }
}