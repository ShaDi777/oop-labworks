using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;

public class CompoundAddressee : IAddressee
{
    private readonly IEnumerable<IAddressee> _addressees;

    public CompoundAddressee(IEnumerable<IAddressee> addressees)
    {
        _addressees = addressees;
    }

    public void ReceiveMessage(IMessage message)
    {
        foreach (IAddressee addressee in _addressees)
        {
            addressee.ReceiveMessage(message);
        }
    }
}
