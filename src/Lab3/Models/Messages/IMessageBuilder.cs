using Itmo.ObjectOrientedProgramming.Lab3.Models.EnumTypes;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

public interface IMessageBuilder
{
    IMessageBuilder WithTitle(string title);
    IMessageBuilder WithText(string text);
    IMessageBuilder SetPriority(Priority priority);
    Message Build();
}