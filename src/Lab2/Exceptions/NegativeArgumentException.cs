using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class NegativeArgumentException : Exception
{
    public NegativeArgumentException() { }

    public NegativeArgumentException(string message, Exception innerException)
        : base(message, innerException) { }

    public NegativeArgumentException(string message)
        : base(message) { }
}