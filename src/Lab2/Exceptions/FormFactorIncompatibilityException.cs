using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class FormFactorIncompatibilityException : Exception
{
    public FormFactorIncompatibilityException() { }

    public FormFactorIncompatibilityException(string message, Exception innerException)
        : base(message, innerException) { }

    public FormFactorIncompatibilityException(string message)
        : base(message) { }
}