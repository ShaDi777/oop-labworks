using System;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

// Optional
public sealed class XmpProfile
{
    /*
 - Тайминги (ex. 18-18-36-54)
 - Вольтаж
 - Частота
     */
    public XmpProfile(
        ICollection<int> timings,
        double voltage,
        double frequency)
    {
        if (timings is null || timings.Count != 4)
        {
            throw new ArgumentException("Get wrong timings in {}", nameof(timings));
        }

        Timings = timings;
        Voltage = voltage;
        Frequency = frequency;
    }

    public IEnumerable<int> Timings { get; }
    public double Voltage { get; }
    public double Frequency { get; }
}
