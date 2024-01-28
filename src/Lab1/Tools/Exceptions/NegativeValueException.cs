using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tools.Exceptions;

public class NegativeValueException : Exception
{
    public NegativeValueException() { }

    public NegativeValueException(string message, Exception innerException)
        : base(message, innerException) { }

    public NegativeValueException(string message)
        : base(message) { }
}
