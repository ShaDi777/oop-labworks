using System;
using Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;

public class DisplayAddressee : IAddressee
{
    private readonly IDisplay _display;

    public DisplayAddressee(IDisplay display)
    {
        _display = display;
    }

    public void ReceiveMessage(IMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        _display.PrintColoredText(message.Title + "\nPriority: " + message.Priority + "\n\n" + message.Text + "\n");
    }
}