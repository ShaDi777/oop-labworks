using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class CpuNotSupportedBiosException : Exception
{
    public CpuNotSupportedBiosException() { }

    public CpuNotSupportedBiosException(string message, Exception innerException)
        : base(message, innerException) { }

    public CpuNotSupportedBiosException(string message)
        : base(message) { }
}
