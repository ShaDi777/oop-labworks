using System;
using System.Diagnostics.CodeAnalysis;
using Itmo.ObjectOrientedProgramming.Lab3.Models.EnumTypes;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

public class PriorityProxyAddresseeBuilder
{
    private IAddressee? _addressee;
    private Priority _priority;

    public PriorityProxyAddresseeBuilder WithPriority(Priority priority)
    {
        _priority = priority;
        return this;
    }

    public PriorityProxyAddresseeBuilder WithAddressee(IAddressee addressee)
    {
        _addressee = addressee;
        return this;
    }

    public PriorityProxyAddressee Build()
    {
        ArgumentNullException.ThrowIfNull(_addressee);

        return new PriorityProxyAddressee(_addressee, _priority);
    }
}

[SuppressMessage("category", "SA1402", Justification = "Same class")]
public class PriorityProxyAddresseeBuilder<T>
    : AddresseeAppender<T>, IAppendableAddresseeBuilder<PriorityProxyAddresseeBuilder<T>>
    where T : IAppendableAddresseeBuilder<T>
{
    private readonly PriorityProxyAddresseeBuilder _builder = new PriorityProxyAddresseeBuilder();

    public PriorityProxyAddresseeBuilder(T previousBuilder)
        : base(previousBuilder) { }

    public PriorityProxyAddresseeBuilder<T> WithPriority(Priority priority)
    {
        _builder.WithPriority(priority);
        return this;
    }

    public PriorityProxyAddresseeBuilder<T> WithAddressee(IAddressee addressee)
    {
        _builder.WithAddressee(addressee);
        return this;
    }

    public UniversalAddresseeBuilder<PriorityProxyAddresseeBuilder<T>> ChooseAddressee()
    {
        return new UniversalAddresseeBuilder<PriorityProxyAddresseeBuilder<T>>(this);
    }

    public T AppendAddressee()
    {
        return Append(Build());
    }

    private PriorityProxyAddressee Build()
    {
        return _builder.Build();
    }
}