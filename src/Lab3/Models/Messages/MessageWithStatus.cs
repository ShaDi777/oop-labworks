using Itmo.ObjectOrientedProgramming.Lab3.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

public class MessageWithStatus : Message
{
    public MessageWithStatus(IMessage message)
        : base(message)
    {
        IsRead = false;
    }

    public bool IsRead { get; private set; }

    public void MarkAsRead()
    {
        if (IsRead)
        {
            throw new ReadMessageException("Message was already read!");
        }

        IsRead = true;
    }
}