using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.Displays;

public interface IDisplayDriver
{
    string Text { get; }

    void ClearOutput();
    void SetTextColor(Color color);
    void SetText(string text);
    string GetColoredText();
}