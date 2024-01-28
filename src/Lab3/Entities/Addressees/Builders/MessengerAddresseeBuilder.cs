using System;
using System.Diagnostics.CodeAnalysis;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messengers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

public class MessengerAddresseeBuilder
{
    private IMessenger? _messenger;

    public MessengerAddresseeBuilder WithMessenger(IMessenger messenger)
    {
        _messenger = messenger;
        return this;
    }

    public MessengerAddressee Build()
    {
        ArgumentNullException.ThrowIfNull(_messenger);

        return new MessengerAddressee(_messenger);
    }
}

[SuppressMessage("category", "SA1402", Justification = "Same class")]
public class MessengerAddresseeBuilder<T> : AddresseeAppender<T>
    where T : IAppendableAddresseeBuilder<T>
{
    private MessengerAddresseeBuilder _builder = new MessengerAddresseeBuilder();

    public MessengerAddresseeBuilder(T previousBuilder)
        : base(previousBuilder) { }

    public MessengerAddresseeBuilder<T> WithMessenger(IMessenger messenger)
    {
        _builder.WithMessenger(messenger);
        return this;
    }

    public T AppendAddressee()
    {
        return Append(Build());
    }

    private MessengerAddressee Build()
    {
        return _builder.Build();
    }
}