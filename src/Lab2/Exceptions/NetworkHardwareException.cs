using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

public class NetworkHardwareException : Exception
{
    public NetworkHardwareException() { }

    public NetworkHardwareException(string message, Exception innerException)
        : base(message, innerException) { }

    public NetworkHardwareException(string message)
        : base(message) { }
}