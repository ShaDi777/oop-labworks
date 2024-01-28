using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class OrderViolationException : Exception
{
    public OrderViolationException() { }

    public OrderViolationException(string message, Exception innerException)
        : base(message, innerException) { }

    public OrderViolationException(string message)
        : base(message) { }
}