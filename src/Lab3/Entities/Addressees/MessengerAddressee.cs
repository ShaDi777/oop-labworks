using System;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messengers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;

public class MessengerAddressee : IAddressee
{
    private readonly IMessenger _messenger;

    public MessengerAddressee(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void ReceiveMessage(IMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        _messenger.PrintText("MESSENGER\n" + message.Title + "\nPriority: " + message.Priority + "\n\n" + message.Text + "\n");
    }
}