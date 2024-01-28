using Itmo.ObjectOrientedProgramming.Lab3.Models.EnumTypes;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

public class MessageBuilder : IMessageBuilder
{
    private string _title = string.Empty;
    private string _text = string.Empty;
    private Priority _priority;

    public IMessageBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public IMessageBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public IMessageBuilder SetPriority(Priority priority)
    {
        _priority = priority;
        return this;
    }

    public Message Build()
    {
        return new Message(_title, _text, _priority);
    }
}