using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class NotEnoughPcieLinesException : Exception
{
    public NotEnoughPcieLinesException() { }

    public NotEnoughPcieLinesException(string message, Exception innerException)
        : base(message, innerException) { }

    public NotEnoughPcieLinesException(string message)
        : base(message) { }
}