using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class NoGpuException : Exception
{
    public NoGpuException() { }

    public NoGpuException(string message, Exception innerException)
        : base(message, innerException) { }

    public NoGpuException(string message)
        : base(message) { }
}