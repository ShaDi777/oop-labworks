using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class DimensionInfo
{
    public DimensionInfo(int length, int width, int height)
    {
        if (length < 0) throw new ArgumentException($"{nameof(length)} can't be negative");
        if (width < 0) throw new ArgumentException($"{nameof(length)} can't be negative");
        if (height < 0) throw new ArgumentException($"{nameof(length)} can't be negative");

        Length = length;
        Width = width;
        Height = height;
    }

    public int Length { get; }
    public int Width { get; }
    public int Height { get; }

    public bool CanStore(DimensionInfo other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return Length >= other.Length && Width >= other.Width && Height >= other.Height;
    }
}
