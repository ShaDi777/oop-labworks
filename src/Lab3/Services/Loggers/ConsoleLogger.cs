using System;
namespace Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;

public class ConsoleLogger : ILogger
{
    public void Log(string text)
    {
        Console.WriteLine(text);
    }
}