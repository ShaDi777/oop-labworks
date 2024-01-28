using System;
using Itmo.ObjectOrientedProgramming.Lab3.Models.EnumTypes;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;

public class PriorityProxyAddressee : IAddressee
{
    private readonly IAddressee _addressee;
    private readonly Priority _priority;

    public PriorityProxyAddressee(IAddressee addressee, Priority priority)
    {
        _addressee = addressee;
        _priority = priority;
    }

    public void ReceiveMessage(IMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (message.Priority >= _priority)
        {
            _addressee.ReceiveMessage(message);
        }
    }
}