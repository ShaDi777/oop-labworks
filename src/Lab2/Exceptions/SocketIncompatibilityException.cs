using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class SocketIncompatibilityException : Exception
{
    public SocketIncompatibilityException() { }

    public SocketIncompatibilityException(string message, Exception innerException)
        : base(message, innerException) { }

    public SocketIncompatibilityException(string message)
        : base(message) { }
}
