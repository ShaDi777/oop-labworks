using System;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public class GpuValidator : IComputerValidator
{
    public void Validate(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if ((computer.DiscreteGpu is null || computer.MotherBoard.PcieLineCount == 0) &&
            !computer.Cpu.HasIntegratedGraphics)
            throw new NoGpuException();
    }

    public string ValidateWithComment(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if ((computer.DiscreteGpu is null || computer.MotherBoard.PcieLineCount == 0) &&
            !computer.Cpu.HasIntegratedGraphics)
        {
            return "Computer has no GPU!\n";
        }

        return string.Empty;
    }
}
