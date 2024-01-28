using Itmo.ObjectOrientedProgramming.Lab4.Models.Components;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.TreeOutputCreator;

public interface ITreeStringCreator
{
    string GetStringTree(IComponent tree);
}