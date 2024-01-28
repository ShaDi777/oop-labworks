using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Printers;

public class ConsolePrinter : IPrinter
{
    public void Print(string text)
    {
        Console.Write(text);
    }

    public void PrintLine(string text)
    {
        Console.WriteLine(text);
    }
}