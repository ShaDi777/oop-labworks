using System;
using System.Linq;
using System.Text;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public class CaseValidator : IComputerValidator
{
    public void Validate(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if (!computer.ComputerCase.SupportedFormFactors.Contains(computer.MotherBoard.MotherboardFormFactor))
            throw new FormFactorIncompatibilityException();

        if (computer.DiscreteGpu is not null &&
            !computer.ComputerCase.GpuMaxDimensions.CanStore(computer.DiscreteGpu.Dimensions))
            throw new GpuLargeSizeException();
    }

    public string ValidateWithComment(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        var stringBuilder = new StringBuilder();
        if (!computer.ComputerCase.SupportedFormFactors.Contains(computer.MotherBoard.MotherboardFormFactor))
            stringBuilder.Append("Case doesn't support this motherboard form-factor.\n");

        if (computer.DiscreteGpu is not null &&
            !computer.ComputerCase.GpuMaxDimensions.CanStore(computer.DiscreteGpu.Dimensions))
            stringBuilder.Append("Case can't store this GPU.\n");

        return stringBuilder.ToString();
    }
}
