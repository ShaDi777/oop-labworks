namespace Itmo.ObjectOrientedProgramming.Lab4.Models.Components;

public interface IComponent
{
    public string Name { get; }
    public IComponent Clone();
}