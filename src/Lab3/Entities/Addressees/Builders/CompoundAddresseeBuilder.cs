using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

public class CompoundAddresseeBuilder
{
    private readonly List<IAddressee> _addressees = new List<IAddressee>();

    public CompoundAddresseeBuilder AddAddressee(IAddressee addressee)
    {
        _addressees.Add(addressee);
        return this;
    }

    public CompoundAddressee Build()
    {
        return new CompoundAddressee(_addressees);
    }
}

[SuppressMessage("category", "SA1402", Justification = "Same class")]
public class CompoundAddresseeBuilder<T>
    : AddresseeAppender<T>, IAppendableAddresseeBuilder<CompoundAddresseeBuilder<T>>
    where T : IAppendableAddresseeBuilder<T>
{
    private readonly CompoundAddresseeBuilder _builder = new CompoundAddresseeBuilder();

    public CompoundAddresseeBuilder(T previousBuilder)
        : base(previousBuilder) { }

    public CompoundAddresseeBuilder<T> WithAddressee(IAddressee addressee)
    {
        _builder.AddAddressee(addressee);
        return this;
    }

    public UniversalAddresseeBuilder<CompoundAddresseeBuilder<T>> ChooseAddressee()
    {
        return new UniversalAddresseeBuilder<CompoundAddresseeBuilder<T>>(this);
    }

    public T AppendAddressee()
    {
        return Append(Build());
    }

    private CompoundAddressee Build()
    {
        return _builder.Build();
    }
}