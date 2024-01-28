namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

public class AddresseeAppender<T>
    where T : IAppendableAddresseeBuilder<T>
{
    private T _previousBuilder;

    protected AddresseeAppender(T previousBuilder)
    {
        _previousBuilder = previousBuilder;
    }

    protected T Append(IAddressee addressee)
    {
        return _previousBuilder.WithAddressee(addressee);
    }
}