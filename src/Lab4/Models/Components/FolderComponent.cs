using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models.Components;

public class FolderComponent : IFolderComponent
{
    private readonly List<IComponent> _components;

    public FolderComponent(string name)
    {
        Name = name;
        _components = new List<IComponent>();
    }

    public string Name { get; }

    public IEnumerable<IComponent> Children => _components;

    public IComponent Clone()
    {
        return new FolderComponent(Name);
    }

    public void AddChild(IComponent component)
    {
        _components.Add(component);
    }
}