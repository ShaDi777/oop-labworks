namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

public class UniversalAddresseeBuilder<T>
    where T : IAppendableAddresseeBuilder<T>
{
    private readonly T _prev;

    public UniversalAddresseeBuilder(T prev)
    {
        _prev = prev;
    }

    public UserAddresseeBuilder<T> NewUserAddressee()
    {
        return new UserAddresseeBuilder<T>(_prev);
    }

    public CompoundAddresseeBuilder<T> NewCompoundAddressee()
    {
        return new CompoundAddresseeBuilder<T>(_prev);
    }

    public LoggerProxyAddresseeBuilder<T> NewLoggerProxyAddressee()
    {
        return new LoggerProxyAddresseeBuilder<T>(_prev);
    }

    public PriorityProxyAddresseeBuilder<T> NewPriorityProxyAddressee()
    {
        return new PriorityProxyAddresseeBuilder<T>(_prev);
    }

    public MessengerAddresseeBuilder<T> NewMessengerAddressee()
    {
        return new MessengerAddresseeBuilder<T>(_prev);
    }

    public DisplayAddresseeBuilder<T> NewDisplayAddressee()
    {
        return new DisplayAddresseeBuilder<T>(_prev);
    }
}