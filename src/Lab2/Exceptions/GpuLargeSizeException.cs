using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class GpuLargeSizeException : Exception
{
    public GpuLargeSizeException() { }

    public GpuLargeSizeException(string message, Exception innerException)
        : base(message, innerException) { }

    public GpuLargeSizeException(string message)
        : base(message) { }
}