using System;
using System.Linq;
using System.Text;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public class MotherboardAndCpuValidator : IComputerValidator
{
    public void Validate(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if (!computer.MotherBoard.Socket.Name.Equals(computer.Cpu.Socket.Name, StringComparison.OrdinalIgnoreCase))
            throw new SocketIncompatibilityException();
        if (!computer.MotherBoard.Bios.SupportedCpu.Contains(computer.Cpu.Name))
            throw new CpuNotSupportedBiosException();
    }

    public string ValidateWithComment(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        var stringBuilder = new StringBuilder();
        if (!computer.MotherBoard.Socket.Name.Equals(computer.Cpu.Socket.Name, StringComparison.OrdinalIgnoreCase))
            stringBuilder.Append("Sockets of motherboard and CPU are incompatible!\n");
        if (!computer.MotherBoard.Bios.SupportedCpu.Contains(computer.Cpu.Name))
            stringBuilder.Append("Bios does not support this CPU!\n");

        return stringBuilder.ToString();
    }
}
