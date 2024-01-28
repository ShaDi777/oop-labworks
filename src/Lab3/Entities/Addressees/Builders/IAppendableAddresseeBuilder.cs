namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

public interface IAppendableAddresseeBuilder<out T>
{
    public T WithAddressee(IAddressee addressee);
}