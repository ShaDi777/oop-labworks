using System;
using System.Text;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public class NetworkValidator : IComputerValidator
{
    public void Validate(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if (computer.WifiAdapter is not null &&
            computer.MotherBoard.Chipset.HasWifiModule)
            throw new NetworkHardwareException();
    }

    public string ValidateWithComment(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        var stringBuilder = new StringBuilder();
        if (computer.WifiAdapter is not null &&
            computer.MotherBoard.Chipset.HasWifiModule)
            stringBuilder.Append("Network hardware conflict!\n");
        else if (computer.WifiAdapter is null &&
                 !computer.MotherBoard.Chipset.HasWifiModule)
            stringBuilder.Append("Computer doesn't have wifi module.\n");

        return stringBuilder.ToString();
    }
}
