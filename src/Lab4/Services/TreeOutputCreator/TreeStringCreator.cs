using System;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Components;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.TreeOutputCreator;

public class TreeStringCreator : ITreeStringCreator
{
    private char _folderSymbol;
    private char _fileSymbol;
    private char _horizontalSymbol;
    private int _indent;

    public TreeStringCreator(
        char folderSymbol = '#',
        char fileSymbol = '*',
        char horizontalSymbol = '-',
        int horizontalIndent = 4)
    {
        _folderSymbol = folderSymbol;
        _fileSymbol = fileSymbol;
        _horizontalSymbol = horizontalSymbol;
        _indent = horizontalIndent;
    }

    public string GetStringTree(IComponent tree)
    {
        string result = string.Empty;
        return GetTree(ref result, tree, 0);
    }

    private string GetTree(ref string result, IComponent component, int depth)
    {
        ArgumentNullException.ThrowIfNull(component);

        result += new string(_horizontalSymbol, depth * _indent) +
                  (component is IFolderComponent ? _folderSymbol : _fileSymbol) +
                  component.Name +
                  Environment.NewLine;

        if (component is not IFolderComponent folder) return result;
        foreach (IComponent child in folder.Children)
        {
            GetTree(ref result, child, depth + 1);
        }

        return result;
    }
}