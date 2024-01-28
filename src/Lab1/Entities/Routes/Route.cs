using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Routes;

public class Route
{
    public Route(IEnumerable<Segment> segments)
    {
        Segments = segments;
    }

    public Route()
        : this(new List<Segment>()) { }

    public IEnumerable<Segment> Segments { get; }
}
