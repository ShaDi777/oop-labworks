using System;
using Itmo.ObjectOrientedProgramming.Lab3.Models.EnumTypes;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

public class Message : IMessage
{
    public Message(IMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        Id = message.Id;
        Title = message.Title;
        Text = message.Text;
        Priority = message.Priority;
    }

    public Message(string title, string text, Priority priority)
    {
        Id = Guid.NewGuid();
        Title = title;
        Text = text;
        Priority = priority;
    }

    public static IMessageBuilder Builder => new MessageBuilder();

    public Guid Id { get; }
    public string Title { get; }
    public string Text { get; }
    public Priority Priority { get; }
}