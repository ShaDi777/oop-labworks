using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.Displays;

public class DisplayDriver : IDisplayDriver
{
    public string Text { get; private set; } = string.Empty;
    public Color Color { get; private set; }

    public void ClearOutput()
    {
        Text = string.Empty;
    }

    public void SetTextColor(Color color)
    {
        Color = color;
    }

    public void SetText(string text)
    {
        Text = text;
    }

    public string GetColoredText()
    {
        return Crayon.Output.Rgb(Color.R, Color.G, Color.B).Text(Text);
    }
}