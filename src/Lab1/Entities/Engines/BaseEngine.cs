namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;

public abstract class BaseEngine
{
    public abstract double CalculateTime(int distance, int shipWeight);
    public abstract double CalculatePrice(int distance, int shipWeight);
}
