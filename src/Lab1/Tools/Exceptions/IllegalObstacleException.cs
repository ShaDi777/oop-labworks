using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tools.Exceptions;

public class IllegalObstacleException : Exception
{
    public IllegalObstacleException() { }

    public IllegalObstacleException(string message, Exception innerException)
        : base(message, innerException) { }

    public IllegalObstacleException(string message)
        : base(message) { }
}
