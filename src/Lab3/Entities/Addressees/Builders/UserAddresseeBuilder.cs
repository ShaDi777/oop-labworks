using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

public class UserAddresseeBuilder
{
    public UserAddressee Build()
    {
        return new UserAddressee();
    }
}

[SuppressMessage("category", "SA1402", Justification = "Same class")]
public class UserAddresseeBuilder<T> : AddresseeAppender<T>
    where T : IAppendableAddresseeBuilder<T>
{
    public UserAddresseeBuilder(T previousBuilder)
        : base(previousBuilder) { }

    public T AppendAddressee()
    {
        return Append(Build());
    }

    private UserAddressee Build()
    {
        return new UserAddressee();
    }
}