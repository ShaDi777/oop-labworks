using System;

namespace Itmo.ObjectOrientedProgramming.Lab3.Exceptions;

public class ReadMessageException : Exception
{
    public ReadMessageException() { }

    public ReadMessageException(string message, Exception innerException)
        : base(message, innerException) { }

    public ReadMessageException(string message)
        : base(message) { }
}
