using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class NoSupportedRamException : Exception
{
    public NoSupportedRamException() { }

    public NoSupportedRamException(string message, Exception innerException)
        : base(message, innerException) { }

    public NoSupportedRamException(string message)
        : base(message) { }
}