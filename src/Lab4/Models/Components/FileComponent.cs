namespace Itmo.ObjectOrientedProgramming.Lab4.Models.Components;

public class FileComponent : IFileComponent
{
    public FileComponent(string name)
    {
        name ??= string.Empty;
        Name = name;
        int startIndex = name.LastIndexOf('.');
        if (startIndex == -1) startIndex = 0;
        Extension = name[startIndex..];
    }

    public string Name { get; }
    public string Extension { get; }

    public IComponent Clone()
    {
        return new FileComponent(Name);
    }
}