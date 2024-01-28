using System;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Topics;

public class TopicBuilder : IAppendableAddresseeBuilder<TopicBuilder>
{
    private string? _name;
    private IAddressee? _addressee;

    public UniversalAddresseeBuilder<TopicBuilder> ChooseAddressee()
    {
        return new UniversalAddresseeBuilder<TopicBuilder>(this);
    }

    public TopicBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TopicBuilder WithAddressee(IAddressee addressee)
    {
        _addressee = addressee;
        return this;
    }

    public Topic Build()
    {
        ArgumentNullException.ThrowIfNull(_addressee);
        ArgumentNullException.ThrowIfNull(_name);
        return new Topic(_name, _addressee);
    }
}