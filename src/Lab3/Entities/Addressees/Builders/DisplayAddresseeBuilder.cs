using System;
using System.Diagnostics.CodeAnalysis;
using Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.Displays;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

public class DisplayAddresseeBuilder
{
    private IDisplay? _display;

    public DisplayAddresseeBuilder WithDisplay(IDisplay display)
    {
        _display = display;
        return this;
    }

    public DisplayAddressee Build()
    {
        ArgumentNullException.ThrowIfNull(_display);

        return new DisplayAddressee(_display);
    }
}

[SuppressMessage("category", "SA1402", Justification = "Same class")]
public class DisplayAddresseeBuilder<T> : AddresseeAppender<T>
    where T : IAppendableAddresseeBuilder<T>
{
    private DisplayAddresseeBuilder _builder = new DisplayAddresseeBuilder();

    public DisplayAddresseeBuilder(T previousBuilder)
        : base(previousBuilder) { }

    public DisplayAddresseeBuilder<T> WithDisplay(IDisplay display)
    {
        _builder.WithDisplay(display);
        return this;
    }

    public T AppendAddressee()
    {
        return Append(Build());
    }

    private DisplayAddressee Build()
    {
        return _builder.Build();
    }
}