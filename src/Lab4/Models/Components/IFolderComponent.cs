using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models.Components;

public interface IFolderComponent : IComponent
{
    public IEnumerable<IComponent> Children { get; }
    public void AddChild(IComponent component);
}