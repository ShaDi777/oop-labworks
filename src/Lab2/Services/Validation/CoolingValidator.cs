using System;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public class CoolingValidator : IComputerValidator
{
    public void Validate(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if (!computer.CpuCoolingSystem.SupportedSockets.Any(socket =>
                string.Equals(socket.Name, computer.Cpu.Socket.Name, StringComparison.OrdinalIgnoreCase)))
            throw new SocketIncompatibilityException();
    }

    public string ValidateWithComment(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        return computer.CpuCoolingSystem.MaxCoolingTdp < computer.Cpu.Tdp
            ? "Cooling TDP is not enough!\n"
            : string.Empty;
    }
}
