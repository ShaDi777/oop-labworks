using System;
using Itmo.ObjectOrientedProgramming.Lab3.Models.EnumTypes;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

public interface IMessage
{
    public Guid Id { get; }
    public string Title { get; }
    public string Text { get; }
    public Priority Priority { get; }
}