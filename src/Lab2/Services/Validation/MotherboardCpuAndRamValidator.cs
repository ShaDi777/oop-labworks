using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public class MotherboardCpuAndRamValidator : IComputerValidator
{
    public void Validate(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if (computer.Rams.Count == 0 ||
            computer.Rams.All(ramStick => ramStick.DdrVersion != computer.MotherBoard.SupportedDdrVersion) ||
            computer.Rams.All(ramStick =>
                computer.Cpu.SupportedRamType.All(ramType => ramStick.DdrVersion != ramType.DdrVersion)) ||
            computer.Rams.All(ramStick => ramStick.SupportedJedecAndVoltage.All(freqVoltage =>
                computer.Cpu.SupportedRamType.All(supported => supported.Frequency > freqVoltage.Frequency))))
        {
            throw new NoSupportedRamException();
        }
    }

    public string ValidateWithComment(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        var stringBuilder = new StringBuilder();
        if (computer.Rams.Count > computer.MotherBoard.RamSlots)
        {
            stringBuilder.Append(
                CultureInfo.CurrentCulture,
                $"You do not need {computer.Rams.Count} ram sticks. Motherboard free slots: {computer.MotherBoard.RamSlots}.\n");
        }

        if (!computer.MotherBoard.Chipset.IsXmpSupported &&
            computer.Rams.All(ram => ram.AvailableXmpProfiles.Any()))
        {
            stringBuilder.Append("Your RAM supports XMP, but motherboard does not.\n");
        }

        return stringBuilder.ToString();
    }
}
