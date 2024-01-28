using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;

public class UserAddressee : IAddressee
{
    private readonly List<MessageWithStatus> _messages = new();
    public IEnumerable<MessageWithStatus> ReceivedMessages => _messages;

    public void ReceiveMessage(IMessage message)
    {
        _messages.Add(new MessageWithStatus(message));
    }

    public void ReadMessage(Guid id)
    {
        _messages.FirstOrDefault(message => message.Id == id)?.MarkAsRead();
    }
}